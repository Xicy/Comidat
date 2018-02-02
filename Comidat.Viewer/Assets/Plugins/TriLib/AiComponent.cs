using System;

namespace TriLib
{
	// Token: 0x02000019 RID: 25
	[Flags]
	public enum AiComponent
	{
		// Token: 0x0400006A RID: 106
		Normals = 2,
		// Token: 0x0400006B RID: 107
		TangentsAndBitangents = 4,
		// Token: 0x0400006C RID: 108
		Colors = 8,
		// Token: 0x0400006D RID: 109
		TexCoords = 16,
		// Token: 0x0400006E RID: 110
		BoneWeights = 32,
		// Token: 0x0400006F RID: 111
		Animations = 64,
		// Token: 0x04000070 RID: 112
		Textures = 128,
		// Token: 0x04000071 RID: 113
		Lights = 256,
		// Token: 0x04000072 RID: 114
		Cameras = 512,
		// Token: 0x04000073 RID: 115
		Meshes = 1024,
		// Token: 0x04000074 RID: 116
		Materials = 2048
	}
}
