using System;

namespace TriLib
{
	// Token: 0x02000027 RID: 39
	[Flags]
	public enum AssimpPostProcessSteps
	{
		// Token: 0x040000F2 RID: 242
		CalcTangentSpace = 1,
		// Token: 0x040000F3 RID: 243
		JoinIdenticalVertices = 2,
		// Token: 0x040000F4 RID: 244
		MakeLeftHanded = 4,
		// Token: 0x040000F5 RID: 245
		Triangulate = 8,
		// Token: 0x040000F6 RID: 246
		RemoveComponent = 16,
		// Token: 0x040000F7 RID: 247
		GenNormals = 32,
		// Token: 0x040000F8 RID: 248
		GenSmoothNormals = 64,
		// Token: 0x040000F9 RID: 249
		SplitLargeMeshes = 128,
		// Token: 0x040000FA RID: 250
		PreTransformVertices = 256,
		// Token: 0x040000FB RID: 251
		LimitBoneWeights = 512,
		// Token: 0x040000FC RID: 252
		ValidateDataStructure = 1024,
		// Token: 0x040000FD RID: 253
		ImproveCacheLocality = 2048,
		// Token: 0x040000FE RID: 254
		RemoveRedundantMaterials = 4096,
		// Token: 0x040000FF RID: 255
		FixInfacingNormals = 8192,
		// Token: 0x04000100 RID: 256
		SortByPType = 32768,
		// Token: 0x04000101 RID: 257
		FindDegenerates = 65536,
		// Token: 0x04000102 RID: 258
		FindInvalidData = 131072,
		// Token: 0x04000103 RID: 259
		GenUvCoords = 262144,
		// Token: 0x04000104 RID: 260
		TransformUvCoords = 524288,
		// Token: 0x04000105 RID: 261
		FindInstances = 1048576,
		// Token: 0x04000106 RID: 262
		OptimizeMeshes = 2097152,
		// Token: 0x04000107 RID: 263
		OptimizeGraph = 4194304,
		// Token: 0x04000108 RID: 264
		FlipUVs = 8388608,
		// Token: 0x04000109 RID: 265
		FlipWindingOrder = 16777216,
		// Token: 0x0400010A RID: 266
		SplitByBoneCount = 33554432,
		// Token: 0x0400010B RID: 267
		Debone = 67108864
	}
}
