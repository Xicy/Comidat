using System;
using System.Collections.Generic;
using UnityEngine;

namespace TriLib
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class AssetLoaderOptions : ScriptableObject
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00006EC5 File Offset: 0x000050C5
		public static AssetLoaderOptions CreateInstance()
		{
			return ScriptableObject.CreateInstance<AssetLoaderOptions>();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006ECC File Offset: 0x000050CC
		public void Deserialize(string json)
		{
			JsonUtility.FromJsonOverwrite(json, this);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006ED5 File Offset: 0x000050D5
		public string Serialize()
		{
			return JsonUtility.ToJson(this);
		}

		// Token: 0x040000DC RID: 220
		public bool DontLoadAnimations;

		// Token: 0x040000DD RID: 221
		public bool DontLoadLights = true;

		// Token: 0x040000DE RID: 222
		public bool DontLoadCameras = true;

		// Token: 0x040000DF RID: 223
		public bool AutoPlayAnimations;

		// Token: 0x040000E0 RID: 224
		public WrapMode AnimationWrapMode = WrapMode.Loop;

		// Token: 0x040000E1 RID: 225
		public bool UseLegacyAnimations = true;

		// Token: 0x040000E2 RID: 226
		public RuntimeAnimatorController AnimatorController;

		// Token: 0x040000E3 RID: 227
		public Avatar Avatar;

		// Token: 0x040000E4 RID: 228
		public bool DontLoadMaterials;

		// Token: 0x040000E5 RID: 229
		public bool ApplyAlphaMaterials = true;

		// Token: 0x040000E6 RID: 230
		public bool UseCutoutMaterials = true;

		// Token: 0x040000E7 RID: 231
		public bool UseStandardSpecularMaterial;

		// Token: 0x040000E8 RID: 232
		public bool GenerateMeshColliders;

		// Token: 0x040000E9 RID: 233
		public bool ConvexMeshColliders;

		// Token: 0x040000EA RID: 234
		public List<Material> MaterialsOverride = new List<Material>();

		// Token: 0x040000EB RID: 235
		public Vector3 RotationAngles = new Vector3(0f, 180f, 0f);

		// Token: 0x040000EC RID: 236
		public float Scale = 1f;

		// Token: 0x040000ED RID: 237
		public AssimpPostProcessSteps PostProcessSteps = AssimpPostProcessSteps.CalcTangentSpace | AssimpPostProcessSteps.JoinIdenticalVertices | AssimpPostProcessSteps.MakeLeftHanded | AssimpPostProcessSteps.Triangulate | AssimpPostProcessSteps.GenSmoothNormals | AssimpPostProcessSteps.SplitLargeMeshes | AssimpPostProcessSteps.LimitBoneWeights | AssimpPostProcessSteps.ValidateDataStructure | AssimpPostProcessSteps.ImproveCacheLocality | AssimpPostProcessSteps.RemoveRedundantMaterials | AssimpPostProcessSteps.SortByPType | AssimpPostProcessSteps.FindInvalidData | AssimpPostProcessSteps.GenUvCoords | AssimpPostProcessSteps.FindInstances | AssimpPostProcessSteps.OptimizeMeshes | AssimpPostProcessSteps.FlipWindingOrder;

		// Token: 0x040000EE RID: 238
		public string TexturesPathOverride;

		// Token: 0x040000EF RID: 239
		public TextureCompression TextureCompression = TextureCompression.NormalQuality;

		// Token: 0x040000F0 RID: 240
		public List<AssetAdvancedConfig> AdvancedConfigs = new List<AssetAdvancedConfig>
		{
			new AssetAdvancedConfig(AssetAdvancedPropertyMetadata.GetConfigKey(AssetAdvancedPropertyClassNames.SplitLargeMeshesVertexLimit), 65000),
			new AssetAdvancedConfig(AssetAdvancedPropertyMetadata.GetConfigKey(AssetAdvancedPropertyClassNames.FBXImportReadLights), false),
			new AssetAdvancedConfig(AssetAdvancedPropertyMetadata.GetConfigKey(AssetAdvancedPropertyClassNames.FBXImportReadCameras), false)
		};
	}
}
