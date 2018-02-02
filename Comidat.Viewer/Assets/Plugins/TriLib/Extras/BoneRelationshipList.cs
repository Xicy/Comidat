using System;
using System.Collections;
using System.Collections.Generic;

namespace TriLib.Extras
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class BoneRelationshipList : IEnumerable<BoneRelationship>, IEnumerable
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00009B96 File Offset: 0x00007D96
		public BoneRelationshipList()
		{
			this._relationships = new List<BoneRelationship>();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009BA9 File Offset: 0x00007DA9
		public void Add(string humanBone, string boneName, bool optional)
		{
			this._relationships.Add(new BoneRelationship(humanBone, boneName, optional));
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009BBE File Offset: 0x00007DBE
		public IEnumerator<BoneRelationship> GetEnumerator()
		{
			return this._relationships.GetEnumerator();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009BD0 File Offset: 0x00007DD0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000134 RID: 308
		private readonly List<BoneRelationship> _relationships;
	}
}
