using System;
using System.IO;

namespace TriLib
{
	// Token: 0x0200002B RID: 43
	public static class FileUtils
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00008688 File Offset: 0x00006888
		public static string GetFileDirectory(string filename)
		{
			int length = filename.LastIndexOf('/');
			return filename.Substring(0, length);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000086A8 File Offset: 0x000068A8
		public static string GetFilenameWithoutExtension(string filename)
		{
			int startIndex = filename.LastIndexOf('/');
			int length = filename.LastIndexOf('.');
			return filename.Substring(startIndex, length);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000086D0 File Offset: 0x000068D0
		public static byte[] LoadFileData(string filename)
		{
			byte[] result;
			try
			{
				if (filename == null)
				{
					result = new byte[0];
				}
				else
				{
					result = File.ReadAllBytes(filename.Replace('\\', '/'));
				}
			}
			catch (Exception)
			{
				result = new byte[0];
			}
			return result;
		}
	}
}
