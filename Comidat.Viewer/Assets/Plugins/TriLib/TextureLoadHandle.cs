using System;
using UnityEngine;

namespace TriLib
{
	// Token: 0x0200002F RID: 47
	// (Invoke) Token: 0x060001E2 RID: 482
	[Obsolete("The Material parameter is inconsistent after the alpha textures support update, so, it will be removed on future versions.")]
	public delegate void TextureLoadHandle(string sourcePath, Material material, string propertyName, Texture2D texture);
}
