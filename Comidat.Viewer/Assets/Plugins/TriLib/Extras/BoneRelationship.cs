using System;

namespace TriLib.Extras
{
	// Token: 0x02000034 RID: 52
	public class BoneRelationship
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00009B79 File Offset: 0x00007D79
		public BoneRelationship(string humanBone, string boneName, bool optional)
		{
			this.HumanBone = humanBone;
			this.BoneName = boneName;
			this.Optional = optional;
		}

		// Token: 0x04000131 RID: 305
		public string HumanBone;

		// Token: 0x04000132 RID: 306
		public string BoneName;

		// Token: 0x04000133 RID: 307
		public bool Optional;
	}
}
