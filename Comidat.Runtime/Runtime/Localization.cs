using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Comidat.IO;

namespace Comidat.Runtime
{
    /// <summary>
    ///     Localization
    /// </summary>
    public static class Localization
    {
        //Fallback language
        private const string FallbackLangName = "EN";

        //file extention of langs file
        private const string DefaultFileExtention = "lang";

        //Current OS language
        private static string _currentLang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        //create list of langs
        private static readonly Dictionary<string, Dictionary<string, string>> Storage =
            new Dictionary<string, Dictionary<string, string>>();

        //load is in previus
        private static bool _isLoadDefault;

        /// <summary>
        ///     Load default in class
        /// </summary>
        private static void LoadDefault()
        {
            //if loaded return
            if (_isLoadDefault) return;
            //load from executing assembly
            LoadByEmbededAssembly(Assembly.GetExecutingAssembly());
            //load from dll
            LoadByEmbededAssembly(Assembly.GetEntryAssembly());
            //load from directory on OS
            //TODO:Linux Control Close for linux
            //LoadByDirectory(Environment.CurrentDirectory, "*." + DefaultFileExtention);
            _isLoadDefault = true;
        }

        /// <summary>
        ///     Load in directory
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="searchPattern">searching pattern</param>
        /// <param name="searchOption">Searching options for directories</param>
        public static void LoadByDirectory(string path, string searchPattern,
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(searchPattern)) throw new ArgumentNullException(nameof(searchPattern));

            foreach (var file in Directory.GetFiles(path, searchPattern, searchOption))
                using (var fileReader = new FileReader(file))
                {
                    Load(fileReader, Path.GetFileName(file));
                }
        }

        /// <summary>
        ///     Load from assembly
        /// </summary>
        /// <param name="assembly"></param>
        public static void LoadByEmbededAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            foreach (var file in assembly.GetManifestResourceNames().Where(s => s.EndsWith(DefaultFileExtention)))
                using (var fileReader = new FileReader(assembly.GetManifestResourceStream(file)))
                {
                    var filename = file.Split('.');
                    Load(fileReader, filename[filename.Length - 2] + "." + DefaultFileExtention);
                }
        }

        /// <summary>
        ///     loader
        /// </summary>
        /// <param name="fileReader">file reader</param>
        /// <param name="path">file path</param>
        private static void Load(FileReader fileReader, string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (fileReader == null) throw new ArgumentNullException(nameof(fileReader));

            var lang = Path.GetFileNameWithoutExtension(path).ToUpperInvariant();
            if (!Storage.ContainsKey(lang)) Storage[lang] = new Dictionary<string, string>();
            foreach (var eachLine in fileReader)
            {
                var pos = eachLine.Value.IndexOf('\t');
                if (pos < 0) continue;

                var key = eachLine.Value.Substring(0, pos).Trim().ToUpperInvariant();
                var val = eachLine.Value.Substring(pos + 1);

                if (!Storage[lang].ContainsKey(key))
                    Storage[lang][key] = val.Replace("\\t", "\t").Replace("\\r\\n", "\n").Replace("\\n", "\n");
            }
        }

        /// <summary>
        ///     Reset setted language
        /// </summary>
        public static void ResetLanguage()
        {
            _currentLang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        }

        /// <summary>
        ///     Set language
        /// </summary>
        /// <param name="lang">Two letter ISO language name</param>
        public static void SetLanguage(string lang)
        {
            if (lang.Length != 2) throw new ArgumentException("lang attirbute must be two letter iso language name");
            _currentLang = lang;
        }

        /// <summary>
        ///     Get localized value
        /// </summary>
        /// <param name="key">key of localized value</param>
        /// <returns>string of localized value</returns>
        public static string Get(string key)
        {
            return Get(key, _currentLang);
        }

        /// <summary>
        ///     get localized value
        /// </summary>
        /// <param name="key">key of localized value</param>
        /// <param name="lang">get spesifc lang</param>
        /// <returns>string of localized value</returns>
        public static string Get(string key, string lang)
        {
            if (!_isLoadDefault)
                LoadDefault();

            lang = lang.ToUpperInvariant();
            key = key.ToUpperInvariant();

            if (Storage.TryGetValue(lang, out var dic) && dic.TryGetValue(key, out var val)) return val;
            if (Storage.TryGetValue(FallbackLangName, out dic) && dic.TryGetValue(key, out val)) return val;
            return key;
        }
    }
}