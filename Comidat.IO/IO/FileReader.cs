using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Comidat.Runtime;

namespace Comidat.IO
{
    /// <summary>
    ///     file reader for localization file
    /// </summary>
    public sealed class FileReader : IEnumerable<FileReaderLine>, IDisposable
    {
        /// <summary>
        ///     file path
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        ///     relative path
        /// </summary>
        private readonly string _relativePath;

        /// <summary>
        ///     stream reader
        /// </summary>
        private StreamReader _streamReader;

        /// <summary>
        ///     Read from stream
        /// </summary>
        /// <param name="file">Stream of file</param>
        public FileReader(Stream file)
        {
            _streamReader = new StreamReader(file, Encoding.UTF8);
        }

        /// <summary>
        ///     Read from file path
        /// </summary>
        /// <param name="filePath">Path of file</param>
        public FileReader(string filePath)
        {
            //Chech file is exist
            if (!File.Exists(filePath))
                throw new FileNotFoundException(string.Format(
                    Localization.Get("Comidat.Util.FileReader.Constructor.FileNotFoundException"), filePath));

            _filePath = filePath;
            _relativePath = Path.GetDirectoryName(Path.GetFullPath(filePath));

            _streamReader = new StreamReader(filePath, Encoding.UTF8);
        }

#pragma warning disable CS0628 // New protected member declared in sealed class
                              /// <summary>
                              ///     Current Line of reading file
                              /// </summary>
        public int CurrentLine { get; protected set; }
#pragma warning restore CS0628 // New protected member declared in sealed class

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        ~FileReader()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            // free managed resources  
            if (_streamReader == null) return;
            _streamReader.Close();
            _streamReader.Dispose();
            _streamReader = null;
        }

        /// <summary>
        ///     Enumerator for foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator<FileReaderLine> GetEnumerator()
        {
            string line;

            // Until EOF
            while ((line = _streamReader.ReadLine()) != null)
            {
                //increse line counter
                CurrentLine++;

                //trim line from head and tail
                line = line.Trim();

                //check line is null
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Ignore very short or commented lines
                if (line.Length < 2 || line[0] == '!' || line[0] == ';' || line[0] == '#' || line.StartsWith("//") ||
                    line.StartsWith("--"))
                    continue;

                // Include files
                bool require = false, divert = false;
                //if start with include require or divert add in source
                if (line.StartsWith("include ") || (require = line.StartsWith("require ")) ||
                    (divert = line.StartsWith("divert ")))
                {
                    //get file name
                    var fileName = line.Substring(line.IndexOf(' ')).Trim(' ', '"');
                    var includeFilePath = Path.Combine(!fileName.StartsWith("/") ? _relativePath : "",
                        fileName.TrimStart('/'));

                    // Prevent recursive including
                    if (includeFilePath != _filePath)
                        if (File.Exists(includeFilePath))
                        {
                            using (var fr = new FileReader(includeFilePath))
                            {
                                foreach (var incLine in fr)
                                    yield return incLine;
                            }

                            // Stop reading current file if divert was successful
                            if (divert)
                                yield break;
                        }
                        else if (require)
                        {
                            throw new FileNotFoundException(string.Format(
                                Localization.Get("Comidat.Util.FileReader.GetEnumerator.FileNotFoundException"),
                                includeFilePath));
                        }

                    continue;
                }

                yield return new FileReaderLine(line, _filePath);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FileReaderLine
    {
        /// <summary>
        ///     New FileReaderLine.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="file"></param>
        public FileReaderLine(string line, string file)
        {
            Value = line;
            File = file != null ? Path.GetFullPath(file) : null;
        }

        /// <summary>
        ///     Current line.
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     Full path to the file the value was read from.
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string File { get; }
    }
}