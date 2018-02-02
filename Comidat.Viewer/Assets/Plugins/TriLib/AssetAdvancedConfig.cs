using System;
using UnityEngine;

namespace TriLib
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public class AssetAdvancedConfig
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00004AEE File Offset: 0x00002CEE
		public AssetAdvancedConfig()
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004AF6 File Offset: 0x00002CF6
		public AssetAdvancedConfig(string key)
		{
			this.Key = key;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004B05 File Offset: 0x00002D05
		public AssetAdvancedConfig(string key, int defaultValue)
		{
			this.Key = key;
			this.IntValue = defaultValue;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004B1B File Offset: 0x00002D1B
		public AssetAdvancedConfig(string key, float defaultValue)
		{
			this.Key = key;
			this.FloatValue = defaultValue;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004B31 File Offset: 0x00002D31
		public AssetAdvancedConfig(string key, bool defaultValue)
		{
			this.Key = key;
			this.BoolValue = defaultValue;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004B47 File Offset: 0x00002D47
		public AssetAdvancedConfig(string key, string defaultValue)
		{
			this.Key = key;
			this.StringValue = defaultValue;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004B5D File Offset: 0x00002D5D
		public AssetAdvancedConfig(string key, AiComponent defaultValue)
		{
			this.Key = key;
			this.IntValue = (int)defaultValue;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004B73 File Offset: 0x00002D73
		public AssetAdvancedConfig(string key, AiPrimitiveType defaultValue)
		{
			this.Key = key;
			this.IntValue = (int)defaultValue;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004B89 File Offset: 0x00002D89
		public AssetAdvancedConfig(string key, AiUVTransform defaultValue)
		{
			this.Key = key;
			this.IntValue = (int)defaultValue;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004B9F File Offset: 0x00002D9F
		public AssetAdvancedConfig(string key, Vector3 translation, Vector3 rotation, Vector3 scale)
		{
			this.Key = key;
			this.TranslationValue = translation;
			this.RotationValue = rotation;
			this.ScaleValue = scale;
		}

		// Token: 0x040000BB RID: 187
		public string Key;

		// Token: 0x040000BC RID: 188
		public int IntValue;

		// Token: 0x040000BD RID: 189
		public float FloatValue;

		// Token: 0x040000BE RID: 190
		public bool BoolValue;

		// Token: 0x040000BF RID: 191
		public string StringValue;

		// Token: 0x040000C0 RID: 192
		public Vector3 TranslationValue;

		// Token: 0x040000C1 RID: 193
		public Vector3 RotationValue;

		// Token: 0x040000C2 RID: 194
		public Vector3 ScaleValue;
	}
}
