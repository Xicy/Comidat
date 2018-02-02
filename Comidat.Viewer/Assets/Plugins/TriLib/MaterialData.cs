using System;
using UnityEngine;

namespace TriLib
{
	// Token: 0x02000025 RID: 37
	public class MaterialData : IDisposable
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00006DF3 File Offset: 0x00004FF3
		public void Dispose()
		{
			this.UnityMaterial = null;
		}

		// Token: 0x040000DB RID: 219
		public Material UnityMaterial;
	}
}
