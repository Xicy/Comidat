using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;

namespace TriLib
{
	// Token: 0x0200002D RID: 45
	public class StringUtils
	{
		// Token: 0x060001DF RID: 479 RVA: 0x000087A0 File Offset: 0x000069A0
		public static string GenerateUniqueName(object id)
		{
			return id.GetHashCode().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000087C0 File Offset: 0x000069C0
		public static byte[] UnZip(string value)
		{
			byte[] array = Convert.FromBase64String(value);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = BitConverter.ToInt32(array, 0);
				memoryStream.Write(array, 4, array.Length - 4);
				byte[] array2 = new byte[num];
				memoryStream.Position = 0L;
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					gzipStream.Read(array2, 0, array2.Length);
				}
				result = array2;
			}
			return result;
		}
	}
}
