using System;

namespace TriLib
{
	// Token: 0x02000028 RID: 40
	public class AssimpProcessPreset
	{
		// Token: 0x0400010C RID: 268
		public const AssimpPostProcessSteps ConvertToLeftHanded = AssimpPostProcessSteps.MakeLeftHanded | AssimpPostProcessSteps.FlipWindingOrder;

		// Token: 0x0400010D RID: 269
		public const AssimpPostProcessSteps TargetRealtimeFast = AssimpPostProcessSteps.CalcTangentSpace | AssimpPostProcessSteps.JoinIdenticalVertices | AssimpPostProcessSteps.Triangulate | AssimpPostProcessSteps.GenNormals | AssimpPostProcessSteps.SortByPType | AssimpPostProcessSteps.GenUvCoords;

		// Token: 0x0400010E RID: 270
		public const AssimpPostProcessSteps TargetRealtimeQuality = AssimpPostProcessSteps.CalcTangentSpace | AssimpPostProcessSteps.JoinIdenticalVertices | AssimpPostProcessSteps.Triangulate | AssimpPostProcessSteps.GenSmoothNormals | AssimpPostProcessSteps.SplitLargeMeshes | AssimpPostProcessSteps.LimitBoneWeights | AssimpPostProcessSteps.ImproveCacheLocality | AssimpPostProcessSteps.RemoveRedundantMaterials | AssimpPostProcessSteps.SortByPType | AssimpPostProcessSteps.FindInvalidData | AssimpPostProcessSteps.GenUvCoords;

		// Token: 0x0400010F RID: 271
		public const AssimpPostProcessSteps TargetRealtimeMaxQuality = AssimpPostProcessSteps.CalcTangentSpace | AssimpPostProcessSteps.JoinIdenticalVertices | AssimpPostProcessSteps.Triangulate | AssimpPostProcessSteps.GenSmoothNormals | AssimpPostProcessSteps.SplitLargeMeshes | AssimpPostProcessSteps.LimitBoneWeights | AssimpPostProcessSteps.ValidateDataStructure | AssimpPostProcessSteps.ImproveCacheLocality | AssimpPostProcessSteps.RemoveRedundantMaterials | AssimpPostProcessSteps.SortByPType | AssimpPostProcessSteps.FindInvalidData | AssimpPostProcessSteps.GenUvCoords | AssimpPostProcessSteps.FindInstances | AssimpPostProcessSteps.OptimizeMeshes;
	}
}
