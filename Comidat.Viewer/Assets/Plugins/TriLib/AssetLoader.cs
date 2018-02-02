using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TriLib
{
	// Token: 0x02000022 RID: 34
	public class AssetLoader : IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600007C RID: 124 RVA: 0x00004BCC File Offset: 0x00002DCC
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x00004C04 File Offset: 0x00002E04
		public event MeshCreatedHandle OnMeshCreated;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600007E RID: 126 RVA: 0x00004C3C File Offset: 0x00002E3C
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x00004C74 File Offset: 0x00002E74
		public event MaterialCreatedHandle OnMaterialCreated;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000080 RID: 128 RVA: 0x00004CAC File Offset: 0x00002EAC
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x00004CE4 File Offset: 0x00002EE4
		public event TextureLoadHandle OnTextureLoaded;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000082 RID: 130 RVA: 0x00004D1C File Offset: 0x00002F1C
		// (remove) Token: 0x06000083 RID: 131 RVA: 0x00004D54 File Offset: 0x00002F54
		public event AnimationClipCreatedHandle OnAnimationClipCreated;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000084 RID: 132 RVA: 0x00004D8C File Offset: 0x00002F8C
		// (remove) Token: 0x06000085 RID: 133 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public event ObjectLoadedHandle OnObjectLoaded;

		// Token: 0x06000086 RID: 134 RVA: 0x00004DFA File Offset: 0x00002FFA
		public static bool IsExtensionSupported(string extension)
		{
			return AssimpInterop.ai_IsExtensionSupported(extension);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004E04 File Offset: 0x00003004
		public static string GetSupportedFileExtensions()
		{
			string result;
			AssimpInterop.ai_GetExtensionList(out result);
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004E1C File Offset: 0x0000301C
		public void Dispose()
		{
			this.OnMeshCreated = null;
			this.OnMaterialCreated = null;
			this.OnAnimationClipCreated = null;
			this.OnTextureLoaded = null;
			this.OnObjectLoaded = null;
			if (this._nodeDataDictionary != null)
			{
				foreach (NodeData nodeData in this._nodeDataDictionary.Values)
				{
					nodeData.Dispose();
				}
				this._nodeDataDictionary = null;
			}
			if (this._meshData != null)
			{
				foreach (MeshData meshData2 in this._meshData)
				{
					meshData2.Dispose();
				}
				this._meshData = null;
			}
			if (this._materialData != null)
			{
				foreach (MaterialData materialData2 in this._materialData)
				{
					materialData2.Dispose();
				}
				this._materialData = null;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004F30 File Offset: 0x00003130
		private bool LoadStandardMaterials()
		{
			if (this._standardBaseMaterial == null)
			{
				this._standardBaseMaterial = (Resources.Load("StandardMaterial") as Material);
			}
			if (this._standardSpecularMaterial == null)
			{
				this._standardSpecularMaterial = (Resources.Load("StandardSpecularMaterial") as Material);
			}
			if (this._standardBaseAlphaMaterial == null)
			{
				this._standardBaseAlphaMaterial = (Resources.Load("StandardBaseAlphaMaterial") as Material);
			}
			if (this._standardSpecularAlphaMaterial == null)
			{
				this._standardSpecularAlphaMaterial = (Resources.Load("StandardSpecularAlphaMaterial") as Material);
			}
			if (this._standardBaseCutoutMaterial == null)
			{
				this._standardBaseCutoutMaterial = (Resources.Load("StandardBaseCutoutMaterial") as Material);
			}
			if (this._standardSpecularCutoutMaterial == null)
			{
				this._standardSpecularCutoutMaterial = (Resources.Load("StandardSpecularCutoutMaterial") as Material);
			}
			return this._standardBaseMaterial != null && this._standardSpecularMaterial != null && this._standardBaseAlphaMaterial != null && this._standardSpecularAlphaMaterial != null && this._standardBaseCutoutMaterial != null && this._standardSpecularCutoutMaterial != null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005085 File Offset: 0x00003285
		private bool LoadNotFoundTexture()
		{
			if (this._notFoundTexture == null)
			{
				this._notFoundTexture = (Resources.Load("NotFound") as Texture2D);
			}
			return this._notFoundTexture != null;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000050B9 File Offset: 0x000032B9
		private void LoadContextOptions(GameObject rootGameObject, AssetLoaderOptions options)
		{
			rootGameObject.transform.rotation = Quaternion.Euler(options.RotationAngles);
			rootGameObject.transform.localScale = Vector3.one * options.Scale;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000050EC File Offset: 0x000032EC
		public GameObject LoadFromMemory(byte[] fileBytes, string filename, AssetLoaderOptions options = null, GameObject wrapperGameObject = null)
		{
			if (options == null)
			{
				options = AssetLoaderOptions.CreateInstance();
			}
			IntPtr intPtr;
			try
			{
				string fileHint = (!File.Exists(filename)) ? filename : Path.GetExtension(filename);
				intPtr = AssetLoader.ImportFileFromMemory(fileBytes, fileHint, options);
			}
			catch (Exception innerException)
			{
				throw new Exception("Error parsing file.", innerException);
			}
			if (intPtr == IntPtr.Zero)
			{
				string arg = AssimpInterop.ai_GetErrorString();
				throw new Exception(string.Format("Error loading asset. Assimp returns: [{0}]", arg));
			}
			GameObject gameObject = null;
			try
			{
				gameObject = this.LoadInternal(filename, intPtr, options, wrapperGameObject);
			}
			catch
			{
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				throw;
			}
			AssimpInterop.ai_ReleaseImport(intPtr);
			if (this.OnObjectLoaded != null)
			{
				this.OnObjectLoaded(gameObject);
			}
			return gameObject;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000051D0 File Offset: 0x000033D0
		public GameObject LoadFromFile(string filename, AssetLoaderOptions options = null, GameObject wrapperGameObject = null)
		{
			if (options == null)
			{
				options = AssetLoaderOptions.CreateInstance();
			}
			IntPtr intPtr;
			try
			{
				intPtr = AssetLoader.ImportFile(filename, options);
			}
			catch (Exception innerException)
			{
				throw new Exception(string.Format("Error parsing file: {0}", filename), innerException);
			}
			if (intPtr == IntPtr.Zero)
			{
				string arg = AssimpInterop.ai_GetErrorString();
				throw new Exception(string.Format("Error loading asset. Assimp returns: [{0}]", arg));
			}
			GameObject gameObject = null;
			try
			{
				gameObject = this.LoadInternal(filename, intPtr, options, wrapperGameObject);
			}
			catch
			{
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				throw;
			}
			AssimpInterop.ai_ReleaseImport(intPtr);
			if (this.OnObjectLoaded != null)
			{
				this.OnObjectLoaded(gameObject);
			}
			return gameObject;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005298 File Offset: 0x00003498
		private static IntPtr BuildPropertyStore(AssetLoaderOptions options)
		{
			IntPtr intPtr = AssimpInterop.ai_CreatePropertyStore();
			foreach (AssetAdvancedConfig assetAdvancedConfig in options.AdvancedConfigs)
			{
				AssetAdvancedConfigType assetAdvancedConfigType;
				string text;
				string text2;
				string text3;
				object obj;
				object obj2;
				object obj3;
				bool flag;
				bool flag2;
				bool flag3;
				AssetAdvancedPropertyMetadata.GetOptionMetadata(assetAdvancedConfig.Key, out assetAdvancedConfigType, out text, out text2, out text3, out obj, out obj2, out obj3, out flag, out flag2, out flag3);
				switch (assetAdvancedConfigType)
				{
				case AssetAdvancedConfigType.Bool:
					AssimpInterop.ai_SetImportPropertyInteger(intPtr, assetAdvancedConfig.Key, (!assetAdvancedConfig.BoolValue) ? 0 : 1);
					break;
				case AssetAdvancedConfigType.Integer:
					AssimpInterop.ai_SetImportPropertyInteger(intPtr, assetAdvancedConfig.Key, assetAdvancedConfig.IntValue);
					break;
				case AssetAdvancedConfigType.Float:
					AssimpInterop.ai_SetImportPropertyFloat(intPtr, assetAdvancedConfig.Key, assetAdvancedConfig.FloatValue);
					break;
				case AssetAdvancedConfigType.String:
					AssimpInterop.ai_SetImportPropertyString(intPtr, assetAdvancedConfig.Key, assetAdvancedConfig.StringValue);
					break;
				case AssetAdvancedConfigType.AiComponent:
					AssimpInterop.ai_SetImportPropertyInteger(intPtr, assetAdvancedConfig.Key, assetAdvancedConfig.IntValue << 1);
					break;
				case AssetAdvancedConfigType.AiPrimitiveType:
					AssimpInterop.ai_SetImportPropertyInteger(intPtr, assetAdvancedConfig.Key, assetAdvancedConfig.IntValue << 1);
					break;
				case AssetAdvancedConfigType.AiUVTransform:
					AssimpInterop.ai_SetImportPropertyInteger(intPtr, assetAdvancedConfig.Key, assetAdvancedConfig.IntValue << 1);
					break;
				case AssetAdvancedConfigType.AiMatrix:
					AssimpInterop.ai_SetImportPropertyMatrix(intPtr, assetAdvancedConfig.Key, assetAdvancedConfig.TranslationValue, assetAdvancedConfig.RotationValue, assetAdvancedConfig.ScaleValue);
					break;
				}
			}
			return intPtr;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000542C File Offset: 0x0000362C
		private static IntPtr ImportFileFromMemory(byte[] fileBytes, string fileHint, AssetLoaderOptions options)
		{
			IntPtr result;
			if (options != null && options.AdvancedConfigs != null)
			{
				IntPtr intPtr = AssetLoader.BuildPropertyStore(options);
				result = AssimpInterop.ai_ImportFileFromMemoryWithProperties(fileBytes, (uint)options.PostProcessSteps, fileHint, intPtr);
				AssimpInterop.ai_CreateReleasePropertyStore(intPtr);
			}
			else
			{
				result = AssimpInterop.ai_ImportFileFromMemory(fileBytes, (uint)((!(options == null)) ? options.PostProcessSteps : ((AssimpPostProcessSteps)0)), fileHint);
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005494 File Offset: 0x00003694
		private static IntPtr ImportFile(string filename, AssetLoaderOptions options)
		{
			IntPtr result;
			if (options != null && options.AdvancedConfigs != null)
			{
				IntPtr intPtr = AssetLoader.BuildPropertyStore(options);
				result = AssimpInterop.ai_ImportFileEx(filename, (uint)options.PostProcessSteps, IntPtr.Zero, intPtr);
				AssimpInterop.ai_CreateReleasePropertyStore(intPtr);
			}
			else
			{
				result = AssimpInterop.ai_ImportFile(filename, (uint)((!(options == null)) ? options.PostProcessSteps : ((AssimpPostProcessSteps)0)));
			}
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005500 File Offset: 0x00003700
		private GameObject LoadInternal(string filename, IntPtr scene, AssetLoaderOptions options, GameObject wrapperGameObject = null)
		{
			this._nodeDataDictionary = new Dictionary<string, NodeData>();
			this._nodeId = 0;
			if (!this.LoadNotFoundTexture())
			{
				throw new Exception("Please add the \"NotFound\" asset from the source package at the project 'Resources' folder.");
			}
			if (!this.LoadStandardMaterials())
			{
				throw new Exception("Please add the \"StandardMaterial\" and \"StandardSpecularMaterial\" assets from the source package at the project 'Resources' folder.");
			}
			if (AssimpInterop.aiScene_HasMaterials(scene) && !options.DontLoadMaterials)
			{
				this._materialData = new MaterialData[AssimpInterop.aiScene_GetNumMaterials(scene)];
				this.BuildMaterials(filename, scene, options);
			}
			if (AssimpInterop.aiScene_HasMeshes(scene))
			{
				this._meshData = new MeshData[AssimpInterop.aiScene_GetNumMeshes(scene)];
				this.BuildMeshes(scene, options);
			}
			wrapperGameObject = this.BuildWrapperObject(scene, options, wrapperGameObject);
			if (AssimpInterop.aiScene_HasMeshes(scene))
			{
				this.BuildBones(scene);
			}
			if (AssimpInterop.aiScene_HasAnimation(scene) && !options.DontLoadAnimations)
			{
				this.BuildAnimations(wrapperGameObject, scene, options);
			}
			if (AssimpInterop.aiScene_HasCameras(scene) && !options.DontLoadCameras)
			{
				AssetLoader.BuildCameras(wrapperGameObject, scene, options);
			}
			if (AssimpInterop.aiScene_HasLights(scene) && !options.DontLoadLights)
			{
				AssetLoader.BuildLights(wrapperGameObject, scene, options);
			}
			this._nodeDataDictionary = null;
			this._meshData = null;
			this._materialData = null;
			return wrapperGameObject;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005634 File Offset: 0x00003834
		private void BuildMeshes(IntPtr scene, AssetLoaderOptions options)
		{
			uint num = AssimpInterop.aiScene_GetNumMeshes(scene);
			for (uint num2 = 0u; num2 < num; num2 += 1u)
			{
				IntPtr ptrMesh = AssimpInterop.aiScene_GetMesh(scene, num2);
				uint num3 = AssimpInterop.aiMesh_VertexCount(ptrMesh);
				Vector3[] array = new Vector3[num3];
				Vector3[] array2 = null;
				bool flag = AssimpInterop.aiMesh_HasNormals(ptrMesh);
				if (flag)
				{
					array2 = new Vector3[num3];
				}
				Vector4[] array3 = null;
				Vector4[] array4 = null;
				bool flag2 = AssimpInterop.aiMesh_HasTangentsAndBitangents(ptrMesh);
				if (flag2)
				{
					array3 = new Vector4[num3];
					array4 = new Vector4[num3];
				}
				Vector2[] array5 = null;
				bool flag3 = AssimpInterop.aiMesh_HasTextureCoords(ptrMesh, 0u);
				if (flag3)
				{
					array5 = new Vector2[num3];
				}
				Vector2[] array6 = null;
				bool flag4 = AssimpInterop.aiMesh_HasTextureCoords(ptrMesh, 1u);
				if (flag4)
				{
					array6 = new Vector2[num3];
				}
				Vector2[] array7 = null;
				bool flag5 = AssimpInterop.aiMesh_HasTextureCoords(ptrMesh, 2u);
				if (flag5)
				{
					array7 = new Vector2[num3];
				}
				Vector2[] array8 = null;
				bool flag6 = AssimpInterop.aiMesh_HasTextureCoords(ptrMesh, 3u);
				if (flag6)
				{
					array8 = new Vector2[num3];
				}
				Color[] array9 = null;
				bool flag7 = AssimpInterop.aiMesh_HasVertexColors(ptrMesh, 0u);
				if (flag7)
				{
					array9 = new Color[num3];
				}
				for (uint num4 = 0u; num4 < num3; num4 += 1u)
				{
					array[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetVertex(ptrMesh, num4);
					if (flag)
					{
						array2[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetNormal(ptrMesh, num4);
					}
					if (flag2)
					{
						array3[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetTangent(ptrMesh, num4);
						array4[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetBitangent(ptrMesh, num4);
					}
					if (flag3)
					{
						array5[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetTextureCoord(ptrMesh, 0u, num4);
					}
					if (flag4)
					{
						array6[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetTextureCoord(ptrMesh, 1u, num4);
					}
					if (flag5)
					{
						array7[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetTextureCoord(ptrMesh, 2u, num4);
					}
					if (flag6)
					{
						array8[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetTextureCoord(ptrMesh, 3u, num4);
					}
					if (flag7)
					{
						array9[(int)((UIntPtr)num4)] = AssimpInterop.aiMesh_GetVertexColor(ptrMesh, 0u, num4);
					}
				}
				string text = AssimpInterop.aiMesh_GetName(ptrMesh);
				Mesh mesh = new Mesh
				{
					name = ((!string.IsNullOrEmpty(text)) ? text : ("Mesh_" + StringUtils.GenerateUniqueName(num2))),
					vertices = array
				};
				if (flag)
				{
					mesh.normals = array2;
				}
				if (flag2)
				{
					mesh.tangents = array3;
				}
				if (flag3)
				{
					mesh.uv = array5;
				}
				if (flag4)
				{
					mesh.uv2 = array6;
				}
				if (flag5)
				{
					mesh.uv3 = array7;
				}
				if (flag6)
				{
					mesh.uv4 = array8;
				}
				if (flag7)
				{
					mesh.colors = array9;
				}
				if (AssimpInterop.aiMesh_HasFaces(ptrMesh))
				{
					uint num5 = AssimpInterop.aiMesh_GetNumFaces(ptrMesh);
					int[] array10 = new int[num5 * 3u];
					for (uint num6 = 0u; num6 < num5; num6 += 1u)
					{
						IntPtr ptrFace = AssimpInterop.aiMesh_GetFace(ptrMesh, num6);
						uint num7 = AssimpInterop.aiFace_GetNumIndices(ptrFace);
						if (num7 > 3u)
						{
							throw new UnityException("More than three face indices is not supported. Please enable \"Triangulate\" in your \"AssetLoaderOptions\" \"PostProcessSteps\" field");
						}
						for (uint num8 = 0u; num8 < num7; num8 += 1u)
						{
							array10[(int)((UIntPtr)(num6 * 3u + num8))] = (int)AssimpInterop.aiFace_GetIndex(ptrFace, num8);
						}
					}
					mesh.SetIndices(array10, MeshTopology.Triangles, 0);
				}
				MeshData meshData = new MeshData
				{
					UnityMesh = mesh
				};
				this._meshData[(int)((UIntPtr)num2)] = meshData;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000059D1 File Offset: 0x00003BD1
		private static void BuildLights(GameObject wrapperGameObject, IntPtr scene, AssetLoaderOptions options)
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000059D4 File Offset: 0x00003BD4
		private static void BuildCameras(GameObject wrapperGameObject, IntPtr scene, AssetLoaderOptions options)
		{
			for (uint num = 0u; num < AssimpInterop.aiScene_GetNumCameras(scene); num += 1u)
			{
				IntPtr ptrCamera = AssimpInterop.aiScene_GetCamera(scene, num);
				string name = AssimpInterop.aiCamera_GetName(ptrCamera);
				Transform transform = wrapperGameObject.transform.FindDeepChild(name, false);
				if (!(transform == null))
				{
					Camera camera = transform.gameObject.AddComponent<Camera>();
					camera.aspect = AssimpInterop.aiCamera_GetAspect(ptrCamera);
					camera.nearClipPlane = AssimpInterop.aiCamera_GetClipPlaneNear(ptrCamera);
					camera.farClipPlane = AssimpInterop.aiCamera_GetClipPlaneFar(ptrCamera);
					camera.fieldOfView = AssimpInterop.aiCamera_GetHorizontalFOV(ptrCamera);
					camera.transform.localPosition = AssimpInterop.aiCamera_GetPosition(ptrCamera);
					camera.transform.LookAt(AssimpInterop.aiCamera_GetLookAt(ptrCamera), AssimpInterop.aiCamera_GetUp(ptrCamera));
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005A94 File Offset: 0x00003C94
		private void BuildMaterials(string filename, IntPtr scene, AssetLoaderOptions options)
		{
			string text = null;
			string textureFileNameWithoutExtension = null;
			if (filename != null)
			{
				FileInfo fileInfo = new FileInfo(filename);
				text = fileInfo.Directory.FullName;
				textureFileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
			}
			string basePath = string.IsNullOrEmpty(options.TexturesPathOverride) ? text : options.TexturesPathOverride;
			List<Material> list = options.MaterialsOverride ?? new List<Material>();
			for (uint num = 0u; num < AssimpInterop.aiScene_GetNumMaterials(scene); num += 1u)
			{
				IntPtr ptrMat = AssimpInterop.aiScene_GetMaterial(scene, num);
				bool isOverriden;
				Material material;
				if ((long)list.Count > (long)((ulong)num))
				{
					isOverriden = true;
					material = list[(int)num];
				}
				else
				{
					isOverriden = false;
					string name;
					if (AssimpInterop.aiMaterial_HasName(ptrMat))
					{
						if (!AssimpInterop.aiMaterial_GetName(ptrMat, out name))
						{
							name = "Material_" + StringUtils.GenerateUniqueName(num);
						}
					}
					else
					{
						name = "Material_" + StringUtils.GenerateUniqueName(num);
					}
					bool flag = false;
					float num2 = 1f;
					float num3;
					if (AssimpInterop.aiMaterial_HasOpacity(ptrMat) && AssimpInterop.aiMaterial_GetOpacity(ptrMat, out num3))
					{
						num2 = num3;
					}
					Texture2D value = null;
					bool flag2 = false;
					uint num4 = AssimpInterop.aiMaterial_GetNumTextureDiffuse(ptrMat);
					string text2;
					uint num5;
					uint num6;
					float num7;
					uint num8;
					uint num9;
					if (num4 > 0u && AssimpInterop.aiMaterial_GetTextureDiffuse(ptrMat, 0u, out text2, out num5, out num6, out num7, out num8, out num9))
					{
						TextureWrapMode textureWrapMode = (num9 != 1u) ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
						string name2 = StringUtils.GenerateUniqueName(text2);
						bool applyAlphaMaterials = options.ApplyAlphaMaterials;
						value = Texture2DUtils.LoadTextureFromFile(scene, text2, name2, null, null, ref applyAlphaMaterials, textureWrapMode, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, false);
						flag = (options.ApplyAlphaMaterials && applyAlphaMaterials);
						flag2 = true;
					}
					bool flag3 = AssimpInterop.aiMaterial_HasSpecular(ptrMat);
					uint num10 = AssimpInterop.aiMaterial_GetNumTextureSpecular(ptrMat);
					if (flag || num2 < 1f)
					{
						if (options.UseCutoutMaterials)
						{
							material = new Material((!options.UseStandardSpecularMaterial || (!flag3 && num10 <= 0u)) ? this._standardBaseCutoutMaterial : this._standardSpecularCutoutMaterial);
						}
						else
						{
							material = new Material((!options.UseStandardSpecularMaterial || (!flag3 && num10 <= 0u)) ? this._standardBaseAlphaMaterial : this._standardSpecularAlphaMaterial);
						}
					}
					else
					{
						material = new Material((!options.UseStandardSpecularMaterial || (!flag3 && num10 <= 0u)) ? this._standardBaseMaterial : this._standardSpecularMaterial);
					}
					material.name = name;
					if (!flag2)
					{
						material.SetTexture("_MainTex", null);
					}
					else
					{
						material.SetTexture("_MainTex", value);
					}
					bool flag4 = false;
					Color value2;
					if (AssimpInterop.aiMaterial_HasDiffuse(ptrMat) && AssimpInterop.aiMaterial_GetDiffuse(ptrMat, out value2))
					{
						value2.a = num2;
						material.SetColor("_Color", value2);
						flag4 = true;
					}
					if (!flag4)
					{
						material.SetColor("_Color", Color.white);
					}
					bool flag5 = false;
					bool flag6 = AssimpInterop.aiMaterial_HasEmissive(ptrMat);
					Color value3;
					if (flag6 && AssimpInterop.aiMaterial_GetEmissive(ptrMat, out value3))
					{
						material.SetColor("_EmissionColor", value3);
						flag5 = true;
					}
					if (!flag5)
					{
						material.SetColor("_EmissionColor", Color.black);
					}
					bool flag7 = false;
					uint num11 = AssimpInterop.aiMaterial_GetNumTextureEmissive(ptrMat);
					string text3;
					uint num12;
					uint num13;
					float num14;
					uint num15;
					uint num16;
					if (num11 > 0u && AssimpInterop.aiMaterial_GetTextureEmissive(ptrMat, 0u, out text3, out num12, out num13, out num14, out num15, out num16))
					{
						TextureWrapMode textureWrapMode2 = (num16 != 1u) ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
						string name3 = StringUtils.GenerateUniqueName(text3);
						bool flag8 = false;
						Texture2DUtils.LoadTextureFromFile(scene, text3, name3, material, "_EmissionMap", ref flag8, textureWrapMode2, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, false);
						flag7 = true;
					}
					if (!flag7)
					{
						material.SetTexture("_EmissionMap", null);
						if (!flag5)
						{
							material.DisableKeyword("_EMISSION");
						}
					}
					bool flag9 = false;
					Color value4;
					if (flag3 && AssimpInterop.aiMaterial_GetSpecular(ptrMat, out value4))
					{
						value4.a = num2;
						material.SetColor("_SpecColor", value4);
						flag9 = true;
					}
					if (!flag9)
					{
						material.SetColor("_SpecColor", Color.black);
					}
					bool flag10 = false;
					string text4;
					uint num17;
					uint num18;
					float num19;
					uint num20;
					uint num21;
					if (num10 > 0u && AssimpInterop.aiMaterial_GetTextureSpecular(ptrMat, 0u, out text4, out num17, out num18, out num19, out num20, out num21))
					{
						TextureWrapMode textureWrapMode3 = (num21 != 1u) ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
						string name4 = StringUtils.GenerateUniqueName(text4);
						bool flag11 = false;
						Texture2DUtils.LoadTextureFromFile(scene, text4, name4, material, "_SpecGlossMap", ref flag11, textureWrapMode3, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, false);
						flag10 = true;
					}
					if (!flag10)
					{
						material.SetTexture("_SpecGlossMap", null);
						material.DisableKeyword("_SPECGLOSSMAP");
					}
					bool flag12 = false;
					uint num22 = AssimpInterop.aiMaterial_GetNumTextureNormals(ptrMat);
					string text5;
					uint num23;
					uint num24;
					float num25;
					uint num26;
					uint num27;
					if (num22 > 0u && AssimpInterop.aiMaterial_GetTextureNormals(ptrMat, 0u, out text5, out num23, out num24, out num25, out num26, out num27))
					{
						TextureWrapMode textureWrapMode4 = (num27 != 1u) ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
						string name5 = StringUtils.GenerateUniqueName(text5);
						bool flag13 = false;
						Texture2DUtils.LoadTextureFromFile(scene, text5, name5, material, "_BumpMap", ref flag13, textureWrapMode4, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, true);
						flag12 = true;
					}
					bool flag14 = false;
					uint num28 = AssimpInterop.aiMaterial_GetNumTextureHeight(ptrMat);
					string text6;
					uint num29;
					uint num30;
					float num31;
					uint num32;
					uint num33;
					if (num28 > 0u && AssimpInterop.aiMaterial_GetTextureHeight(ptrMat, 0u, out text6, out num29, out num30, out num31, out num32, out num33))
					{
						TextureWrapMode textureWrapMode5 = (num33 != 1u) ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
						string name6 = StringUtils.GenerateUniqueName(text6);
						bool flag15 = false;
						Texture2DUtils.LoadTextureFromFile(scene, text6, name6, material, "_BumpMap", ref flag15, textureWrapMode5, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, false);
						flag14 = true;
					}
					if (!flag14 && !flag12)
					{
						material.SetTexture("_BumpMap", null);
						material.DisableKeyword("_NORMALMAP");
					}
					bool flag16 = false;
					float num34;
					if (AssimpInterop.aiMaterial_HasBumpScaling(ptrMat) && AssimpInterop.aiMaterial_GetBumpScaling(ptrMat, out num34))
					{
						if (Mathf.Approximately(num34, 0f))
						{
							num34 = 1f;
						}
						material.SetFloat("_BumpScale", num34);
						flag16 = true;
					}
					if (!flag16)
					{
						material.SetFloat("_BumpScale", 1f);
					}
					bool flag17 = false;
					float value5;
					if (AssimpInterop.aiMaterial_HasShininess(ptrMat) && AssimpInterop.aiMaterial_GetShininess(ptrMat, out value5))
					{
						material.SetFloat("_Glossiness", value5);
						flag17 = true;
					}
					if (!flag17)
					{
						material.SetFloat("_Glossiness", 0.5f);
					}
					bool flag18 = false;
					if (AssimpInterop.aiMaterial_HasShininessStrength(ptrMat))
					{
						float value6;
						if (AssimpInterop.aiMaterial_GetShininessStrength(ptrMat, out value6))
						{
							material.SetFloat("_GlossMapScale", value6);
							flag18 = true;
						}
						else
						{
							material.SetFloat("_GlossMapScale", 1f);
						}
					}
					if (!flag18)
					{
						material.SetFloat("_GlossMapScale", 1f);
					}
				}
				if (!(material == null))
				{
					MaterialData materialData = new MaterialData
					{
						UnityMaterial = material
					};
					this._materialData[(int)((UIntPtr)num)] = materialData;
					if (this.OnMaterialCreated != null)
					{
						this.OnMaterialCreated(num, isOverriden, material);
					}
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000061A0 File Offset: 0x000043A0
		private void BuildBones(IntPtr scene)
		{
			uint num = AssimpInterop.aiScene_GetNumMeshes(scene);
			for (uint num2 = 0u; num2 < num; num2 += 1u)
			{
				MeshData meshData = this._meshData[(int)((UIntPtr)num2)];
				IntPtr ptrMesh = AssimpInterop.aiScene_GetMesh(scene, num2);
				Mesh unityMesh = meshData.UnityMesh;
				if (AssimpInterop.aiMesh_HasBones(ptrMesh))
				{
					uint num3 = AssimpInterop.aiMesh_VertexCount(ptrMesh);
					uint num4 = AssimpInterop.aiMesh_GetNumBones(ptrMesh);
					Matrix4x4[] array = new Matrix4x4[num4];
					Transform[] array2 = new Transform[num4];
					BoneWeight[] array3 = new BoneWeight[num3];
					int[] array4 = new int[num3];
					for (uint num5 = 0u; num5 < num4; num5 += 1u)
					{
						IntPtr ptrBone = AssimpInterop.aiMesh_GetBone(ptrMesh, num5);
						string key = AssimpInterop.aiBone_GetName(ptrBone);
						if (this._nodeDataDictionary.ContainsKey(key))
						{
							NodeData nodeData = this._nodeDataDictionary[key];
							GameObject gameObject = nodeData.GameObject;
							Transform transform = gameObject.transform;
							array2[(int)((UIntPtr)num5)] = transform;
							Matrix4x4 matrix4x = AssimpInterop.aiBone_GetOffsetMatrix(ptrBone);
							array[(int)((UIntPtr)num5)] = matrix4x;
							uint num6 = AssimpInterop.aiBone_GetNumWeights(ptrBone);
							for (uint num7 = 0u; num7 < num6; num7 += 1u)
							{
								IntPtr ptrVweight = AssimpInterop.aiBone_GetWeights(ptrBone, num7);
								float num8 = AssimpInterop.aiVertexWeight_GetWeight(ptrVweight);
								uint num9 = AssimpInterop.aiVertexWeight_GetVertexId(ptrVweight);
								int num10 = array4[(int)((UIntPtr)num9)];
								int num11 = (int)num5;
								if (num10 == 0)
								{
									BoneWeight boneWeight = new BoneWeight
									{
										boneIndex0 = num11,
										weight0 = num8
									};
									array3[(int)((UIntPtr)num9)] = boneWeight;
								}
								else if (num10 == 1)
								{
									BoneWeight boneWeight = array3[(int)((UIntPtr)num9)];
									boneWeight.boneIndex1 = num11;
									boneWeight.weight1 = num8;
									array3[(int)((UIntPtr)num9)] = boneWeight;
								}
								else if (num10 == 2)
								{
									BoneWeight boneWeight = array3[(int)((UIntPtr)num9)];
									boneWeight.boneIndex2 = num11;
									boneWeight.weight2 = num8;
									array3[(int)((UIntPtr)num9)] = boneWeight;
								}
								else if (num10 == 3)
								{
									BoneWeight boneWeight = array3[(int)((UIntPtr)num9)];
									boneWeight.boneIndex3 = num11;
									boneWeight.weight3 = num8;
									array3[(int)((UIntPtr)num9)] = boneWeight;
								}
								else
								{
									BoneWeight boneWeight = array3[(int)((UIntPtr)num9)];
									boneWeight.boneIndex3 = num11;
									boneWeight.weight3 = num8;
									array3[(int)((UIntPtr)num9)] = boneWeight;
								}
								array4[(int)((UIntPtr)num9)]++;
							}
						}
					}
					SkinnedMeshRenderer skinnedMeshRenderer = meshData.SkinnedMeshRenderer;
					skinnedMeshRenderer.bones = array2;
					unityMesh.bindposes = array;
					unityMesh.boneWeights = array3;
				}
				if (this.OnMeshCreated != null)
				{
					this.OnMeshCreated(num2, unityMesh);
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006470 File Offset: 0x00004670
		private void BuildAnimations(GameObject wrapperGameObject, IntPtr scene, AssetLoaderOptions options)
		{
			uint num = AssimpInterop.aiScene_GetNumAnimations(scene);
			AnimationClip[] array = new AnimationClip[num];
			for (uint num2 = 0u; num2 < num; num2 += 1u)
			{
				IntPtr ptrAnimation = AssimpInterop.aiScene_GetAnimation(scene, num2);
				float num3 = AssimpInterop.aiAnimation_GetTicksPerSecond(ptrAnimation);
				if (num3 <= 0f)
				{
					num3 = 60f;
				}
				string text = AssimpInterop.aiAnimation_GetName(ptrAnimation);
				AnimationClip animationClip = new AnimationClip
				{
					name = ((!string.IsNullOrEmpty(text)) ? text : ("Animation_" + StringUtils.GenerateUniqueName(num2))),
					legacy = true,
					frameRate = num3
				};
				float num4 = AssimpInterop.aiAnimation_GetDuraction(ptrAnimation);
				float animationLength = num4 / num3;
				uint num5 = AssimpInterop.aiAnimation_GetNumChannels(ptrAnimation);
				for (uint num6 = 0u; num6 < num5; num6 += 1u)
				{
					IntPtr ptrNodeAnim = AssimpInterop.aiAnimation_GetAnimationChannel(ptrAnimation, num6);
					string text2 = AssimpInterop.aiNodeAnim_GetNodeName(ptrNodeAnim);
					if (!string.IsNullOrEmpty(text2))
					{
						if (this._nodeDataDictionary.ContainsKey(text2))
						{
							NodeData nodeData = this._nodeDataDictionary[text2];
							uint num7 = AssimpInterop.aiNodeAnim_GetNumRotationKeys(ptrNodeAnim);
							if (num7 > 0u)
							{
								AnimationCurve animationCurve = new AnimationCurve();
								AnimationCurve animationCurve2 = new AnimationCurve();
								AnimationCurve animationCurve3 = new AnimationCurve();
								AnimationCurve animationCurve4 = new AnimationCurve();
								for (uint num8 = 0u; num8 < num7; num8 += 1u)
								{
									IntPtr ptrQuatKey = AssimpInterop.aiNodeAnim_GetRotationKey(ptrNodeAnim, num8);
									float time = AssimpInterop.aiQuatKey_GetTime(ptrQuatKey) / num3;
									Quaternion quaternion = AssimpInterop.aiQuatKey_GetValue(ptrQuatKey);
									animationCurve.AddKey(time, quaternion.x);
									animationCurve2.AddKey(time, quaternion.y);
									animationCurve3.AddKey(time, quaternion.z);
									animationCurve4.AddKey(time, quaternion.w);
								}
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localRotation.x", AssetLoader.FixCurve(animationLength, animationCurve));
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localRotation.y", AssetLoader.FixCurve(animationLength, animationCurve2));
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localRotation.z", AssetLoader.FixCurve(animationLength, animationCurve3));
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localRotation.w", AssetLoader.FixCurve(animationLength, animationCurve4));
							}
							uint num9 = AssimpInterop.aiNodeAnim_GetNumPositionKeys(ptrNodeAnim);
							if (num9 > 0u)
							{
								AnimationCurve animationCurve5 = new AnimationCurve();
								AnimationCurve animationCurve6 = new AnimationCurve();
								AnimationCurve animationCurve7 = new AnimationCurve();
								for (uint num10 = 0u; num10 < num9; num10 += 1u)
								{
									IntPtr ptrVectorKey = AssimpInterop.aiNodeAnim_GetPositionKey(ptrNodeAnim, num10);
									float time2 = AssimpInterop.aiVectorKey_GetTime(ptrVectorKey) / num3;
									Vector3 vector = AssimpInterop.aiVectorKey_GetValue(ptrVectorKey);
									animationCurve5.AddKey(time2, vector.x);
									animationCurve6.AddKey(time2, vector.y);
									animationCurve7.AddKey(time2, vector.z);
								}
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localPosition.x", AssetLoader.FixCurve(animationLength, animationCurve5));
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localPosition.y", AssetLoader.FixCurve(animationLength, animationCurve6));
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localPosition.z", AssetLoader.FixCurve(animationLength, animationCurve7));
							}
							uint num11 = AssimpInterop.aiNodeAnim_GetNumScalingKeys(ptrNodeAnim);
							if (num11 > 0u)
							{
								AnimationCurve animationCurve8 = new AnimationCurve();
								AnimationCurve animationCurve9 = new AnimationCurve();
								AnimationCurve animationCurve10 = new AnimationCurve();
								for (uint num12 = 0u; num12 < num11; num12 += 1u)
								{
									IntPtr ptrVectorKey2 = AssimpInterop.aiNodeAnim_GetScalingKey(ptrNodeAnim, num12);
									float time3 = AssimpInterop.aiVectorKey_GetTime(ptrVectorKey2) / num3;
									Vector3 vector2 = AssimpInterop.aiVectorKey_GetValue(ptrVectorKey2);
									animationCurve8.AddKey(time3, vector2.x);
									animationCurve9.AddKey(time3, vector2.y);
									animationCurve10.AddKey(time3, vector2.z);
								}
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localScale.x", AssetLoader.FixCurve(animationLength, animationCurve8));
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localScale.y", AssetLoader.FixCurve(animationLength, animationCurve9));
								animationClip.SetCurve(nodeData.Path, typeof(Transform), "localScale.z", AssetLoader.FixCurve(animationLength, animationCurve10));
							}
						}
					}
				}
				animationClip.EnsureQuaternionContinuity();
				animationClip.wrapMode = options.AnimationWrapMode;
				array[(int)((UIntPtr)num2)] = animationClip;
				if (this.OnAnimationClipCreated != null)
				{
					this.OnAnimationClipCreated(num2, animationClip);
				}
			}
			if (options.UseLegacyAnimations)
			{
				Animation animation = wrapperGameObject.GetComponent<Animation>();
				if (animation == null)
				{
					animation = wrapperGameObject.AddComponent<Animation>();
				}
				AnimationClip clip = null;
				for (int i = 0; i < array.Length; i++)
				{
					AnimationClip animationClip2 = array[i];
					animation.AddClip(animationClip2, animationClip2.name);
					if (i == 0)
					{
						clip = animationClip2;
					}
				}
				animation.clip = clip;
				if (options.AutoPlayAnimations)
				{
					animation.Play();
				}
			}
			else
			{
				Animator animator = wrapperGameObject.GetComponent<Animator>();
				if (animator == null)
				{
					animator = wrapperGameObject.AddComponent<Animator>();
				}
				if (options.AnimatorController != null)
				{
					animator.runtimeAnimatorController = options.AnimatorController;
				}
				if (options.Avatar != null)
				{
					animator.avatar = options.Avatar;
				}
				else
				{
					animator.avatar = AvatarBuilder.BuildGenericAvatar(wrapperGameObject, string.Empty);
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000069F8 File Offset: 0x00004BF8
		private GameObject BuildWrapperObject(IntPtr scene, AssetLoaderOptions options, GameObject templateObject = null)
		{
			IntPtr intPtr = AssimpInterop.aiScene_GetRootNode(scene);
			NodeData nodeData = new NodeData();
			int id = this._nodeId++;
			nodeData.Node = intPtr;
			nodeData.Id = id;
			string text = this.FixName(AssimpInterop.aiNode_GetName(intPtr), id);
			nodeData.Name = text;
			nodeData.Path = text;
			GameObject gameObject = templateObject ?? new GameObject
			{
				name = string.Format("Wrapper_{0}", text)
			};
			GameObject gameObject2 = this.BuildObject(scene, nodeData, options);
			this.LoadContextOptions(gameObject2, options);
			gameObject2.transform.parent = gameObject.transform;
			return gameObject;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006AA0 File Offset: 0x00004CA0
		private GameObject BuildObject(IntPtr scene, NodeData nodeData, AssetLoaderOptions options)
		{
			GameObject gameObject = new GameObject
			{
				name = nodeData.Name
			};
			IntPtr node = nodeData.Node;
			uint num = AssimpInterop.aiNode_GetNumMeshes(node);
			bool flag = AssimpInterop.aiScene_HasMeshes(scene);
			if (num > 0u && flag)
			{
				for (uint num2 = 0u; num2 < num; num2 += 1u)
				{
					uint num3 = AssimpInterop.aiNode_GetMeshIndex(node, num2);
					IntPtr ptrMesh = AssimpInterop.aiScene_GetMesh(scene, num3);
					uint num4 = AssimpInterop.aiMesh_GetMatrialIndex(ptrMesh);
					Material material = null;
					if (this._materialData != null)
					{
						MaterialData materialData = this._materialData[(int)((UIntPtr)num4)];
						if (materialData != null)
						{
							material = materialData.UnityMaterial;
						}
					}
					if (material == null)
					{
						material = this._standardBaseMaterial;
					}
					MeshData meshData = this._meshData[(int)((UIntPtr)num3)];
					Mesh unityMesh = meshData.UnityMesh;
					GameObject gameObject2 = new GameObject
					{
						name = string.Format("<{0}:Mesh:{1}>", gameObject.name, num2)
					};
					gameObject2.transform.parent = gameObject.transform;
					MeshFilter meshFilter = gameObject2.AddComponent<MeshFilter>();
					meshFilter.mesh = unityMesh;
					if (AssimpInterop.aiMesh_HasBones(ptrMesh))
					{
						SkinnedMeshRenderer skinnedMeshRenderer = gameObject2.AddComponent<SkinnedMeshRenderer>();
						skinnedMeshRenderer.sharedMesh = unityMesh;
						skinnedMeshRenderer.quality = SkinQuality.Bone4;
						skinnedMeshRenderer.sharedMaterial = material;
						meshData.SkinnedMeshRenderer = skinnedMeshRenderer;
					}
					else
					{
						MeshRenderer meshRenderer = gameObject2.AddComponent<MeshRenderer>();
						meshRenderer.sharedMaterial = material;
						if (options.GenerateMeshColliders)
						{
							MeshCollider meshCollider = gameObject2.AddComponent<MeshCollider>();
							meshCollider.sharedMesh = unityMesh;
							meshCollider.convex = options.ConvexMeshColliders;
						}
					}
				}
			}
			if (nodeData.ParentNodeData != null)
			{
				gameObject.transform.parent = nodeData.ParentNodeData.GameObject.transform;
			}
			gameObject.transform.LoadMatrix(AssimpInterop.aiNode_GetTransformation(node), true);
			nodeData.GameObject = gameObject;
			this._nodeDataDictionary.Add(nodeData.Name, nodeData);
			uint num5 = AssimpInterop.aiNode_GetNumChildren(node);
			if (num5 > 0u)
			{
				for (uint num6 = 0u; num6 < num5; num6 += 1u)
				{
					IntPtr intPtr = AssimpInterop.aiNode_GetChildren(node, num6);
					int id = this._nodeId++;
					NodeData nodeData2 = new NodeData
					{
						ParentNodeData = nodeData,
						Node = intPtr,
						Id = id,
						Name = this.FixName(AssimpInterop.aiNode_GetName(intPtr), id)
					};
					nodeData2.Path = string.Format("{0}/{1}", nodeData.Path, nodeData2.Name);
					this.BuildObject(scene, nodeData2, options);
				}
			}
			return gameObject;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006D2B File Offset: 0x00004F2B
		private string FixName(string name, int id)
		{
			return (!string.IsNullOrEmpty(name) && !this._nodeDataDictionary.ContainsKey(name)) ? name : StringUtils.GenerateUniqueName(id);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006D5C File Offset: 0x00004F5C
		private static AnimationCurve FixCurve(float animationLength, AnimationCurve curve)
		{
			if (Mathf.Approximately(animationLength, 0f))
			{
				animationLength = 1f;
			}
			if (curve.keys.Length == 1)
			{
				curve.AddKey(new Keyframe(animationLength, curve.keys[0].value));
			}
			return curve;
		}

		// Token: 0x040000C3 RID: 195
		private MaterialData[] _materialData;

		// Token: 0x040000C4 RID: 196
		private MeshData[] _meshData;

		// Token: 0x040000C5 RID: 197
		private Dictionary<string, NodeData> _nodeDataDictionary;

		// Token: 0x040000C6 RID: 198
		private int _nodeId;

		// Token: 0x040000C7 RID: 199
		private Material _standardBaseMaterial;

		// Token: 0x040000C8 RID: 200
		private Material _standardSpecularMaterial;

		// Token: 0x040000C9 RID: 201
		private Material _standardBaseAlphaMaterial;

		// Token: 0x040000CA RID: 202
		private Material _standardSpecularAlphaMaterial;

		// Token: 0x040000CB RID: 203
		private Material _standardBaseCutoutMaterial;

		// Token: 0x040000CC RID: 204
		private Material _standardSpecularCutoutMaterial;

		// Token: 0x040000CD RID: 205
		private Texture2D _notFoundTexture;
	}
}
