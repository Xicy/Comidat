using System;
using System.IO;
using DevIL;
using UnityEngine;

namespace TriLib
{
	// Token: 0x02000030 RID: 48
	public static class Texture2DUtils
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000885C File Offset: 0x00006A5C
		public static Texture2D LoadTextureFromFile(IntPtr scene, string path, string name, Material material, string propertyName, ref bool checkAlphaChannel, TextureWrapMode textureWrapMode = TextureWrapMode.Repeat, string basePath = null, TextureLoadHandle onTextureLoaded = null, TextureCompression textureCompression = TextureCompression.None, string textureFileNameWithoutExtension = null, bool isNormalMap = false)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			IntPtr intPtr = AssimpInterop.aiScene_GetEmbeddedTexture(scene, path);
			bool flag;
			byte[] array;
			string text;
			if (intPtr != IntPtr.Zero)
			{
				flag = !AssimpInterop.aiMaterial_IsEmbeddedTextureCompressed(scene, intPtr);
				uint uintSize = AssimpInterop.aiMaterial_GetEmbeddedTextureDataSize(scene, intPtr, !flag);
				array = AssimpInterop.aiMaterial_GetEmbeddedTextureData(scene, intPtr, uintSize);
				text = Path.GetFileNameWithoutExtension(path);
			}
			else
			{
				string text2 = null;
				text = path;
				array = FileUtils.LoadFileData(text);
				if (array.Length == 0 && basePath != null)
				{
					text = Path.Combine(basePath, path);
				}
				array = FileUtils.LoadFileData(text);
				if (array.Length == 0)
				{
					text2 = Path.GetFileName(path);
					text = text2;
				}
				array = FileUtils.LoadFileData(text);
				if (array.Length == 0 && basePath != null && text2 != null)
				{
					text = Path.Combine(basePath, text2);
				}
				array = FileUtils.LoadFileData(text);
				if (array.Length == 0)
				{
					return null;
				}
				flag = false;
			}
			Texture2D texture2D;
			bool flag2;
			if (flag)
			{
				int num = Mathf.FloorToInt(Mathf.Sqrt((float)(array.Length / 4)));
				texture2D = new Texture2D(num, num, TextureFormat.ARGB32, true);
				texture2D.LoadRawTextureData(array);
				texture2D.Apply();
				flag2 = true;
			}
			else
			{
				flag2 = IlLoader.LoadTexture2DFromByteArray(array, array.Length, out texture2D);
			}
			texture2D.name = name;
			texture2D.wrapMode = textureWrapMode;
			if (flag2)
			{
				Color32[] pixels = texture2D.GetPixels32();
				Texture2D texture2D2 = new Texture2D(texture2D.width, texture2D.height, TextureFormat.ARGB32, true);
				if (isNormalMap)
				{
					for (int i = 0; i < pixels.Length; i++)
					{
						Color32 color = pixels[i];
						color.a = color.r;
						color.r = 0;
						color.b = 0;
						pixels[i] = color;
					}
					texture2D2.SetPixels32(pixels);
					texture2D2.Apply();
				}
				else
				{
					texture2D2.SetPixels32(pixels);
					texture2D2.Apply();
					if (textureCompression != TextureCompression.None)
					{
						texture2D.Compress(textureCompression == TextureCompression.HighQuality);
					}
				}
				if (checkAlphaChannel)
				{
					checkAlphaChannel = false;
					foreach (Color32 color2 in pixels)
					{
						if (color2.a != 255)
						{
							checkAlphaChannel = true;
							break;
						}
					}
				}
				if (material != null)
				{
					material.SetTexture(propertyName, texture2D2);
				}
				if (onTextureLoaded != null)
				{
					onTextureLoaded(text, material, propertyName, texture2D2);
				}
				return texture2D2;
			}
			return null;
		}
	}
}
