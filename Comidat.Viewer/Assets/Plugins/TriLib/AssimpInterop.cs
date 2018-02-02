using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace TriLib
{
	// Token: 0x02000029 RID: 41
	public static class AssimpInterop
	{
		// Token: 0x060000A7 RID: 167
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCreatePropertyStore")]
		public static extern IntPtr _aiCreatePropertyStore();

		// Token: 0x060000A8 RID: 168 RVA: 0x00006EE8 File Offset: 0x000050E8
		public static IntPtr ai_CreatePropertyStore()
		{
			return AssimpInterop._aiCreatePropertyStore();
		}

		// Token: 0x060000A9 RID: 169
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiReleasePropertyStore")]
		public static extern void _aiReleasePropertyStore(IntPtr ptrPropertyStore);

		// Token: 0x060000AA RID: 170 RVA: 0x00006EFC File Offset: 0x000050FC
		public static void ai_CreateReleasePropertyStore(IntPtr ptrPropertyStore)
		{
			AssimpInterop._aiReleasePropertyStore(ptrPropertyStore);
		}

		// Token: 0x060000AB RID: 171
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiSetImportPropertyInteger")]
		public static extern IntPtr _aiSetImportPropertyInteger(IntPtr ptrStore, IntPtr name, int value);

		// Token: 0x060000AC RID: 172 RVA: 0x00006F04 File Offset: 0x00005104
		public static IntPtr ai_SetImportPropertyInteger(IntPtr ptrStore, string name, int value)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(name);
			IntPtr result = AssimpInterop._aiSetImportPropertyInteger(ptrStore, stringBuffer.AddrOfPinnedObject(), value);
			stringBuffer.Free();
			return result;
		}

		// Token: 0x060000AD RID: 173
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiSetImportPropertyFloat")]
		public static extern IntPtr _aiSetImportPropertyFloat(IntPtr ptrStore, IntPtr name, float value);

		// Token: 0x060000AE RID: 174 RVA: 0x00006F30 File Offset: 0x00005130
		public static IntPtr ai_SetImportPropertyFloat(IntPtr ptrStore, string name, float value)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(name);
			IntPtr result = AssimpInterop._aiSetImportPropertyFloat(ptrStore, stringBuffer.AddrOfPinnedObject(), value);
			stringBuffer.Free();
			return result;
		}

		// Token: 0x060000AF RID: 175
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiSetImportPropertyString")]
		public static extern IntPtr _aiSetImportPropertyString(IntPtr ptrStore, IntPtr name, IntPtr ptrValue);

		// Token: 0x060000B0 RID: 176 RVA: 0x00006F5C File Offset: 0x0000515C
		public static IntPtr ai_SetImportPropertyString(IntPtr ptrStore, string name, string value)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(name);
			IntPtr assimpStringBuffer = AssimpInterop.GetAssimpStringBuffer(value);
			IntPtr result = AssimpInterop._aiSetImportPropertyString(ptrStore, stringBuffer.AddrOfPinnedObject(), assimpStringBuffer);
			stringBuffer.Free();
			Marshal.FreeHGlobal(assimpStringBuffer);
			return result;
		}

		// Token: 0x060000B1 RID: 177
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiSetImportPropertyMatrix")]
		public static extern IntPtr _aiSetImportPropertyMatrix(IntPtr ptrStore, IntPtr name, IntPtr ptrValue);

		// Token: 0x060000B2 RID: 178 RVA: 0x00006F94 File Offset: 0x00005194
		public static IntPtr ai_SetImportPropertyMatrix(IntPtr ptrStore, string name, Vector3 translation, Vector3 rotation, Vector3 scale)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(name);
			GCHandle gchandle = AssimpInterop.Matrix4x4ToAssimp(translation, rotation, scale);
			IntPtr result = AssimpInterop._aiSetImportPropertyMatrix(ptrStore, stringBuffer.AddrOfPinnedObject(), gchandle.AddrOfPinnedObject());
			stringBuffer.Free();
			gchandle.Free();
			return result;
		}

		// Token: 0x060000B3 RID: 179
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiImportFileFromMemory")]
		public static extern IntPtr _aiImportFileFromMemory(IntPtr ptrBuffer, uint uintLength, uint uintFlags, string strHint);

		// Token: 0x060000B4 RID: 180 RVA: 0x00006FD8 File Offset: 0x000051D8
		public static IntPtr ai_ImportFileFromMemory(byte[] fileBytes, uint uintFlags, string strHint)
		{
			GCHandle gchandle = AssimpInterop.LockGc(fileBytes);
			IntPtr result = AssimpInterop._aiImportFileFromMemory(gchandle.AddrOfPinnedObject(), (uint)fileBytes.Length, uintFlags, strHint);
			gchandle.Free();
			return result;
		}

		// Token: 0x060000B5 RID: 181
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiImportFileFromMemoryWithProperties")]
		public static extern IntPtr _aiImportFileFromMemoryWithProperties(IntPtr ptrBuffer, uint uintLength, uint uintFlags, string strHint, IntPtr ptrProps);

		// Token: 0x060000B6 RID: 182 RVA: 0x00007008 File Offset: 0x00005208
		public static IntPtr ai_ImportFileFromMemoryWithProperties(byte[] fileBytes, uint uintFlags, string strHint, IntPtr ptrProps)
		{
			GCHandle gchandle = AssimpInterop.LockGc(fileBytes);
			IntPtr result = AssimpInterop._aiImportFileFromMemoryWithProperties(gchandle.AddrOfPinnedObject(), (uint)fileBytes.Length, uintFlags, strHint, ptrProps);
			gchandle.Free();
			return result;
		}

		// Token: 0x060000B7 RID: 183
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiImportFile")]
		public static extern IntPtr _aiImportFile(string filename, uint flags);

		// Token: 0x060000B8 RID: 184 RVA: 0x00007038 File Offset: 0x00005238
		public static IntPtr ai_ImportFile(string filename, uint flags)
		{
			return AssimpInterop._aiImportFile(filename, flags);
		}

		// Token: 0x060000B9 RID: 185
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiImportFileExWithProperties")]
		public static extern IntPtr _aiImportFileEx(string filename, uint flags, IntPtr ptrFS, IntPtr ptrProps);

		// Token: 0x060000BA RID: 186 RVA: 0x00007050 File Offset: 0x00005250
		public static IntPtr ai_ImportFileEx(string filename, uint flags, IntPtr ptrFS, IntPtr ptrProp)
		{
			return AssimpInterop._aiImportFileEx(filename, flags, ptrFS, ptrProp);
		}

		// Token: 0x060000BB RID: 187
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiReleaseImport")]
		public static extern void _aiReleaseImport(IntPtr scene);

		// Token: 0x060000BC RID: 188 RVA: 0x00007068 File Offset: 0x00005268
		public static void ai_ReleaseImport(IntPtr scene)
		{
			AssimpInterop._aiReleaseImport(scene);
		}

		// Token: 0x060000BD RID: 189
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiGetExtensionList")]
		public static extern void _aiGetExtensionList(IntPtr ptrExtensionList);

		// Token: 0x060000BE RID: 190 RVA: 0x00007070 File Offset: 0x00005270
		public static void ai_GetExtensionList(out string strExtensionList)
		{
			byte[] array;
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out array);
			AssimpInterop._aiGetExtensionList(newStringBuffer.AddrOfPinnedObject());
			newStringBuffer.Free();
			long num = (!AssimpInterop.Is32Bits) ? BitConverter.ToInt64(array, 0) : ((long)BitConverter.ToInt32(array, 0));
			strExtensionList = Encoding.UTF8.GetString(array, AssimpInterop.IntSize, (int)num);
		}

		// Token: 0x060000BF RID: 191
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiGetErrorString")]
		public static extern IntPtr _aiGetErrorString();

		// Token: 0x060000C0 RID: 192 RVA: 0x000070CC File Offset: 0x000052CC
		public static string ai_GetErrorString()
		{
			IntPtr ptr = AssimpInterop._aiGetErrorString();
			return Marshal.PtrToStringAnsi(ptr);
		}

		// Token: 0x060000C1 RID: 193
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiIsExtensionSupported")]
		public static extern bool _aiIsExtensionSupported(IntPtr strExtension);

		// Token: 0x060000C2 RID: 194 RVA: 0x000070E8 File Offset: 0x000052E8
		public static bool ai_IsExtensionSupported(string strExtension)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(strExtension);
			bool result = AssimpInterop._aiIsExtensionSupported(stringBuffer.AddrOfPinnedObject());
			stringBuffer.Free();
			return result;
		}

		// Token: 0x060000C3 RID: 195
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasMaterials")]
		private static extern bool _aiScene_HasMaterials(IntPtr ptrScene);

		// Token: 0x060000C4 RID: 196 RVA: 0x00007114 File Offset: 0x00005314
		public static bool aiScene_HasMaterials(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasMaterials(ptrScene);
		}

		// Token: 0x060000C5 RID: 197
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumMaterials")]
		private static extern uint _aiScene_GetNumMaterials(IntPtr ptrScene);

		// Token: 0x060000C6 RID: 198 RVA: 0x0000712C File Offset: 0x0000532C
		public static uint aiScene_GetNumMaterials(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumMaterials(ptrScene);
		}

		// Token: 0x060000C7 RID: 199
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumMeshes")]
		private static extern uint _aiScene_GetNumMeshes(IntPtr ptrScene);

		// Token: 0x060000C8 RID: 200 RVA: 0x00007144 File Offset: 0x00005344
		public static uint aiScene_GetNumMeshes(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumMeshes(ptrScene);
		}

		// Token: 0x060000C9 RID: 201
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumAnimations")]
		private static extern uint _aiScene_GetNumAnimations(IntPtr ptrScene);

		// Token: 0x060000CA RID: 202 RVA: 0x0000715C File Offset: 0x0000535C
		public static uint aiScene_GetNumAnimations(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumAnimations(ptrScene);
		}

		// Token: 0x060000CB RID: 203
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumCameras")]
		private static extern uint _aiScene_GetNumCameras(IntPtr ptrScene);

		// Token: 0x060000CC RID: 204 RVA: 0x00007174 File Offset: 0x00005374
		public static uint aiScene_GetNumCameras(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumCameras(ptrScene);
		}

		// Token: 0x060000CD RID: 205
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumLights")]
		private static extern uint _aiScene_GetNumLights(IntPtr ptrScene);

		// Token: 0x060000CE RID: 206 RVA: 0x0000718C File Offset: 0x0000538C
		public static uint aiScene_GetNumLights(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumLights(ptrScene);
		}

		// Token: 0x060000CF RID: 207
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasMeshes")]
		private static extern bool _aiScene_HasMeshes(IntPtr ptrScene);

		// Token: 0x060000D0 RID: 208 RVA: 0x000071A4 File Offset: 0x000053A4
		public static bool aiScene_HasMeshes(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasMeshes(ptrScene);
		}

		// Token: 0x060000D1 RID: 209
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasAnimation")]
		private static extern bool _aiScene_HasAnimation(IntPtr ptrScene);

		// Token: 0x060000D2 RID: 210 RVA: 0x000071BC File Offset: 0x000053BC
		public static bool aiScene_HasAnimation(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasAnimation(ptrScene);
		}

		// Token: 0x060000D3 RID: 211
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasCameras")]
		private static extern bool _aiScene_HasCameras(IntPtr ptrScene);

		// Token: 0x060000D4 RID: 212 RVA: 0x000071D4 File Offset: 0x000053D4
		public static bool aiScene_HasCameras(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasCameras(ptrScene);
		}

		// Token: 0x060000D5 RID: 213
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasLights")]
		private static extern bool _aiScene_HasLights(IntPtr ptrScene);

		// Token: 0x060000D6 RID: 214 RVA: 0x000071EC File Offset: 0x000053EC
		public static bool aiScene_HasLights(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasLights(ptrScene);
		}

		// Token: 0x060000D7 RID: 215
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetRootNode")]
		private static extern IntPtr _aiScene_GetRootNode(IntPtr ptrScene);

		// Token: 0x060000D8 RID: 216 RVA: 0x00007204 File Offset: 0x00005404
		public static IntPtr aiScene_GetRootNode(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetRootNode(ptrScene);
		}

		// Token: 0x060000D9 RID: 217
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetMaterial")]
		private static extern IntPtr _aiScene_GetMaterial(IntPtr ptrScene, uint uintIndex);

		// Token: 0x060000DA RID: 218 RVA: 0x0000721C File Offset: 0x0000541C
		public static IntPtr aiScene_GetMaterial(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetMaterial(ptrScene, uintIndex);
		}

		// Token: 0x060000DB RID: 219
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetMesh")]
		private static extern IntPtr _aiScene_GetMesh(IntPtr ptrScene, uint uintIndex);

		// Token: 0x060000DC RID: 220 RVA: 0x00007234 File Offset: 0x00005434
		public static IntPtr aiScene_GetMesh(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetMesh(ptrScene, uintIndex);
		}

		// Token: 0x060000DD RID: 221
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetAnimation")]
		private static extern IntPtr _aiScene_GetAnimation(IntPtr ptrScene, uint uintIndex);

		// Token: 0x060000DE RID: 222 RVA: 0x0000724C File Offset: 0x0000544C
		public static IntPtr aiScene_GetAnimation(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetAnimation(ptrScene, uintIndex);
		}

		// Token: 0x060000DF RID: 223
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetCamera")]
		private static extern IntPtr _aiScene_GetCamera(IntPtr ptrScene, uint uintIndex);

		// Token: 0x060000E0 RID: 224 RVA: 0x00007264 File Offset: 0x00005464
		public static IntPtr aiScene_GetCamera(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetCamera(ptrScene, uintIndex);
		}

		// Token: 0x060000E1 RID: 225
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetLight")]
		private static extern IntPtr _aiScene_GetLight(IntPtr ptrScene, uint uintIndex);

		// Token: 0x060000E2 RID: 226 RVA: 0x0000727C File Offset: 0x0000547C
		public static IntPtr aiScene_GetLight(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetLight(ptrScene, uintIndex);
		}

		// Token: 0x060000E3 RID: 227
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetName")]
		private static extern IntPtr _aiNode_GetName(IntPtr ptrNode);

		// Token: 0x060000E4 RID: 228 RVA: 0x00007294 File Offset: 0x00005494
		public static string aiNode_GetName(IntPtr ptrNode)
		{
			IntPtr pointer = AssimpInterop._aiNode_GetName(ptrNode);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		// Token: 0x060000E5 RID: 229
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetNumChildren")]
		private static extern uint _aiNode_GetNumChildren(IntPtr ptrNode);

		// Token: 0x060000E6 RID: 230 RVA: 0x000072B0 File Offset: 0x000054B0
		public static uint aiNode_GetNumChildren(IntPtr ptrNode)
		{
			return AssimpInterop._aiNode_GetNumChildren(ptrNode);
		}

		// Token: 0x060000E7 RID: 231
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetNumMeshes")]
		private static extern uint _aiNode_GetNumMeshes(IntPtr ptrNode);

		// Token: 0x060000E8 RID: 232 RVA: 0x000072C8 File Offset: 0x000054C8
		public static uint aiNode_GetNumMeshes(IntPtr ptrNode)
		{
			return AssimpInterop._aiNode_GetNumMeshes(ptrNode);
		}

		// Token: 0x060000E9 RID: 233
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetChildren")]
		private static extern IntPtr _aiNode_GetChildren(IntPtr ptrNode, uint uintIndex);

		// Token: 0x060000EA RID: 234 RVA: 0x000072E0 File Offset: 0x000054E0
		public static IntPtr aiNode_GetChildren(IntPtr ptrNode, uint uintIndex)
		{
			return AssimpInterop._aiNode_GetChildren(ptrNode, uintIndex);
		}

		// Token: 0x060000EB RID: 235
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetMeshIndex")]
		private static extern uint _aiNode_GetMeshIndex(IntPtr ptrNode, uint uintIndex);

		// Token: 0x060000EC RID: 236 RVA: 0x000072F8 File Offset: 0x000054F8
		public static uint aiNode_GetMeshIndex(IntPtr ptrNode, uint uintIndex)
		{
			return AssimpInterop._aiNode_GetMeshIndex(ptrNode, uintIndex);
		}

		// Token: 0x060000ED RID: 237
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetParent")]
		private static extern IntPtr _aiNode_GetParent(IntPtr ptrNode);

		// Token: 0x060000EE RID: 238 RVA: 0x00007310 File Offset: 0x00005510
		public static IntPtr aiNode_GetParent(IntPtr ptrNode)
		{
			return AssimpInterop._aiNode_GetParent(ptrNode);
		}

		// Token: 0x060000EF RID: 239
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetTransformation")]
		private static extern IntPtr _aiNode_GetTransformation(IntPtr ptrNode);

		// Token: 0x060000F0 RID: 240 RVA: 0x00007328 File Offset: 0x00005528
		public static Matrix4x4 aiNode_GetTransformation(IntPtr ptrNode)
		{
			IntPtr pointer = AssimpInterop._aiNode_GetTransformation(ptrNode);
			float[] newFloat16Array = AssimpInterop.GetNewFloat16Array(pointer);
			return AssimpInterop.LoadMatrix4x4FromArray(newFloat16Array);
		}

		// Token: 0x060000F1 RID: 241
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_IsEmbeddedTextureCompressed")]
		private static extern bool _aiMaterial_IsEmbeddedTextureCompressed(IntPtr ptrScene, IntPtr ptrTexture);

		// Token: 0x060000F2 RID: 242 RVA: 0x0000734C File Offset: 0x0000554C
		public static bool aiMaterial_IsEmbeddedTextureCompressed(IntPtr ptrScene, IntPtr ptrTexture)
		{
			return AssimpInterop._aiMaterial_IsEmbeddedTextureCompressed(ptrScene, ptrTexture);
		}

		// Token: 0x060000F3 RID: 243
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetEmbeddedTextureDataSize")]
		private static extern uint _aiMaterial_GetEmbeddedTextureDataSize(IntPtr ptrScene, IntPtr ptrTexture, bool boolCompressed);

		// Token: 0x060000F4 RID: 244 RVA: 0x00007364 File Offset: 0x00005564
		public static uint aiMaterial_GetEmbeddedTextureDataSize(IntPtr ptrScene, IntPtr ptrTexture, bool boolCompressed)
		{
			return AssimpInterop._aiMaterial_GetEmbeddedTextureDataSize(ptrScene, ptrTexture, boolCompressed);
		}

		// Token: 0x060000F5 RID: 245
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetEmbeddedTextureData")]
		private static extern void _aiMaterial_GetEmbeddedTextureData(IntPtr ptrScene, IntPtr ptrData, IntPtr ptrTexture, uint uintSize);

		// Token: 0x060000F6 RID: 246 RVA: 0x0000737C File Offset: 0x0000557C
		public static byte[] aiMaterial_GetEmbeddedTextureData(IntPtr ptrScene, IntPtr ptrTexture, uint uintSize)
		{
			byte[] array = new byte[uintSize];
			GCHandle gchandle = AssimpInterop.LockGc(array);
			AssimpInterop._aiMaterial_GetEmbeddedTextureData(ptrScene, gchandle.AddrOfPinnedObject(), ptrTexture, uintSize);
			gchandle.Free();
			return array;
		}

		// Token: 0x060000F7 RID: 247
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetEmbeddedTexture")]
		private static extern IntPtr _aiScene_GetEmbeddedTexture(IntPtr ptrScene, string strFilename);

		// Token: 0x060000F8 RID: 248 RVA: 0x000073AF File Offset: 0x000055AF
		public static IntPtr aiScene_GetEmbeddedTexture(IntPtr ptrScene, string strFilename)
		{
			return AssimpInterop._aiScene_GetEmbeddedTexture(ptrScene, strFilename);
		}

		// Token: 0x060000F9 RID: 249
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureCount")]
		private static extern uint _aiMaterial_GetTextureCount(IntPtr ptrMat, uint uintType);

		// Token: 0x060000FA RID: 250 RVA: 0x000073B8 File Offset: 0x000055B8
		public static uint aiMaterial_GetTextureCount(IntPtr ptrMat, uint uintType)
		{
			return AssimpInterop._aiMaterial_GetTextureCount(ptrMat, uintType);
		}

		// Token: 0x060000FB RID: 251
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureDiffuse")]
		private static extern bool _aiMaterial_HasTextureDiffuse(IntPtr ptrMat, uint uintType);

		// Token: 0x060000FC RID: 252 RVA: 0x000073D0 File Offset: 0x000055D0
		public static bool aiMaterial_HasTextureDiffuse(IntPtr ptrMat, uint uintType)
		{
			return AssimpInterop._aiMaterial_HasTextureDiffuse(ptrMat, uintType);
		}

		// Token: 0x060000FD RID: 253
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureDiffuse")]
		private static extern bool _aiMaterial_GetTextureDiffuse(IntPtr ptrMat, uint uintType, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		// Token: 0x060000FE RID: 254 RVA: 0x000073E8 File Offset: 0x000055E8
		public static bool aiMaterial_GetTextureDiffuse(IntPtr ptrMat, uint uintType, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value;
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureDiffuse(ptrMat, uintType, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		// Token: 0x060000FF RID: 255
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureDiffuse")]
		private static extern uint _aiMaterial_GetNumTextureDiffuse(IntPtr ptrMat);

		// Token: 0x06000100 RID: 256 RVA: 0x00007490 File Offset: 0x00005690
		public static uint aiMaterial_GetNumTextureDiffuse(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureDiffuse(ptrMat);
		}

		// Token: 0x06000101 RID: 257
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureEmissive")]
		private static extern bool _aiMaterial_HasTextureEmissive(IntPtr ptrMat, uint uintIndex);

		// Token: 0x06000102 RID: 258 RVA: 0x000074A8 File Offset: 0x000056A8
		public static bool aiMaterial_HasTextureEmissive(IntPtr ptrMat, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_HasTextureEmissive(ptrMat, uintIndex);
		}

		// Token: 0x06000103 RID: 259
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureEmissive")]
		private static extern bool _aiMaterial_GetTextureEmissive(IntPtr ptrMat, uint uintIndex, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		// Token: 0x06000104 RID: 260 RVA: 0x000074C0 File Offset: 0x000056C0
		public static bool aiMaterial_GetTextureEmissive(IntPtr ptrMat, uint uintIndex, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value;
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureEmissive(ptrMat, uintIndex, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		// Token: 0x06000105 RID: 261
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureEmissive")]
		private static extern uint _aiMaterial_GetNumTextureEmissive(IntPtr ptrMat);

		// Token: 0x06000106 RID: 262 RVA: 0x00007568 File Offset: 0x00005768
		public static uint aiMaterial_GetNumTextureEmissive(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureEmissive(ptrMat);
		}

		// Token: 0x06000107 RID: 263
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureSpecular")]
		private static extern bool _aiMaterial_HasTextureSpecular(IntPtr ptrMat, uint uintIndex);

		// Token: 0x06000108 RID: 264 RVA: 0x00007580 File Offset: 0x00005780
		public static bool aiMaterial_HasTextureSpecular(IntPtr ptrMat, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_HasTextureSpecular(ptrMat, uintIndex);
		}

		// Token: 0x06000109 RID: 265
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureSpecular")]
		private static extern bool _aiMaterial_GetTextureSpecular(IntPtr ptrMat, uint uintIndex, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		// Token: 0x0600010A RID: 266 RVA: 0x00007598 File Offset: 0x00005798
		public static bool aiMaterial_GetTextureSpecular(IntPtr ptrMat, uint uintIndex, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value;
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureSpecular(ptrMat, uintIndex, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		// Token: 0x0600010B RID: 267
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureSpecular")]
		private static extern uint _aiMaterial_GetNumTextureSpecular(IntPtr ptrMat);

		// Token: 0x0600010C RID: 268 RVA: 0x00007640 File Offset: 0x00005840
		public static uint aiMaterial_GetNumTextureSpecular(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureSpecular(ptrMat);
		}

		// Token: 0x0600010D RID: 269
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureNormals")]
		private static extern bool _aiMaterial_HasTextureNormals(IntPtr ptrMat, uint uintIndex);

		// Token: 0x0600010E RID: 270 RVA: 0x00007658 File Offset: 0x00005858
		public static bool aiMaterial_HasTextureNormals(IntPtr ptrMat, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_HasTextureNormals(ptrMat, uintIndex);
		}

		// Token: 0x0600010F RID: 271
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureNormals")]
		private static extern bool _aiMaterial_GetTextureNormals(IntPtr ptrMat, uint uintIndex, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		// Token: 0x06000110 RID: 272 RVA: 0x00007670 File Offset: 0x00005870
		public static bool aiMaterial_GetTextureNormals(IntPtr ptrMat, uint uintIndex, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value;
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureNormals(ptrMat, uintIndex, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		// Token: 0x06000111 RID: 273
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureNormals")]
		private static extern uint _aiMaterial_GetNumTextureNormals(IntPtr ptrMat);

		// Token: 0x06000112 RID: 274 RVA: 0x00007718 File Offset: 0x00005918
		public static uint aiMaterial_GetNumTextureNormals(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureNormals(ptrMat);
		}

		// Token: 0x06000113 RID: 275
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureHeight")]
		private static extern bool _aiMaterial_HasTextureHeight(IntPtr ptrMat, uint uintIndex);

		// Token: 0x06000114 RID: 276 RVA: 0x00007730 File Offset: 0x00005930
		public static bool aiMaterial_HasTextureHeight(IntPtr ptrMat, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_HasTextureHeight(ptrMat, uintIndex);
		}

		// Token: 0x06000115 RID: 277
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureHeight")]
		private static extern bool _aiMaterial_GetTextureHeight(IntPtr ptrMat, uint uintIndex, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		// Token: 0x06000116 RID: 278 RVA: 0x00007748 File Offset: 0x00005948
		public static bool aiMaterial_GetTextureHeight(IntPtr ptrMat, uint uintIndex, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value;
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureHeight(ptrMat, uintIndex, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		// Token: 0x06000117 RID: 279
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureHeight")]
		private static extern uint _aiMaterial_GetNumTextureHeight(IntPtr ptrMat);

		// Token: 0x06000118 RID: 280 RVA: 0x000077F0 File Offset: 0x000059F0
		public static uint aiMaterial_GetNumTextureHeight(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureHeight(ptrMat);
		}

		// Token: 0x06000119 RID: 281
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasAmbient")]
		private static extern bool _aiMaterial_HasAmbient(IntPtr ptrMat);

		// Token: 0x0600011A RID: 282 RVA: 0x00007808 File Offset: 0x00005A08
		public static bool aiMaterial_HasAmbient(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasAmbient(ptrMat);
		}

		// Token: 0x0600011B RID: 283
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetAmbient")]
		private static extern bool _aiMaterial_GetAmbient(IntPtr ptrMat, IntPtr colorOut);

		// Token: 0x0600011C RID: 284 RVA: 0x00007820 File Offset: 0x00005A20
		public static bool aiMaterial_GetAmbient(IntPtr ptrMat, out Color colorOut)
		{
			float[] array;
			GCHandle newFloat4Buffer = AssimpInterop.GetNewFloat4Buffer(out array);
			bool result = AssimpInterop._aiMaterial_GetAmbient(ptrMat, newFloat4Buffer.AddrOfPinnedObject());
			colorOut = AssimpInterop.LoadColorFromArray(array);
			newFloat4Buffer.Free();
			return result;
		}

		// Token: 0x0600011D RID: 285
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasDiffuse")]
		private static extern bool _aiMaterial_HasDiffuse(IntPtr ptrMat);

		// Token: 0x0600011E RID: 286 RVA: 0x00007858 File Offset: 0x00005A58
		public static bool aiMaterial_HasDiffuse(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasDiffuse(ptrMat);
		}

		// Token: 0x0600011F RID: 287
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetDiffuse")]
		private static extern bool _aiMaterial_GetDiffuse(IntPtr ptrMat, IntPtr colorOut);

		// Token: 0x06000120 RID: 288 RVA: 0x00007870 File Offset: 0x00005A70
		public static bool aiMaterial_GetDiffuse(IntPtr ptrMat, out Color colorOut)
		{
			float[] array;
			GCHandle newFloat4Buffer = AssimpInterop.GetNewFloat4Buffer(out array);
			bool result = AssimpInterop._aiMaterial_GetDiffuse(ptrMat, newFloat4Buffer.AddrOfPinnedObject());
			colorOut = AssimpInterop.LoadColorFromArray(array);
			newFloat4Buffer.Free();
			return result;
		}

		// Token: 0x06000121 RID: 289
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasSpecular")]
		private static extern bool _aiMaterial_HasSpecular(IntPtr ptrMat);

		// Token: 0x06000122 RID: 290 RVA: 0x000078A8 File Offset: 0x00005AA8
		public static bool aiMaterial_HasSpecular(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasSpecular(ptrMat);
		}

		// Token: 0x06000123 RID: 291
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetSpecular")]
		private static extern bool _aiMaterial_GetSpecular(IntPtr ptrMat, IntPtr colorOut);

		// Token: 0x06000124 RID: 292 RVA: 0x000078C0 File Offset: 0x00005AC0
		public static bool aiMaterial_GetSpecular(IntPtr ptrMat, out Color colorOut)
		{
			float[] array;
			GCHandle newFloat4Buffer = AssimpInterop.GetNewFloat4Buffer(out array);
			bool result = AssimpInterop._aiMaterial_GetSpecular(ptrMat, newFloat4Buffer.AddrOfPinnedObject());
			colorOut = AssimpInterop.LoadColorFromArray(array);
			newFloat4Buffer.Free();
			return result;
		}

		// Token: 0x06000125 RID: 293
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasEmissive")]
		private static extern bool _aiMaterial_HasEmissive(IntPtr ptrMat);

		// Token: 0x06000126 RID: 294 RVA: 0x000078F8 File Offset: 0x00005AF8
		public static bool aiMaterial_HasEmissive(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasEmissive(ptrMat);
		}

		// Token: 0x06000127 RID: 295
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetEmissive")]
		private static extern bool _aiMaterial_GetEmissive(IntPtr ptrMat, IntPtr colorOut);

		// Token: 0x06000128 RID: 296 RVA: 0x00007910 File Offset: 0x00005B10
		public static bool aiMaterial_GetEmissive(IntPtr ptrMat, out Color colorOut)
		{
			float[] array;
			GCHandle newFloat4Buffer = AssimpInterop.GetNewFloat4Buffer(out array);
			bool result = AssimpInterop._aiMaterial_GetEmissive(ptrMat, newFloat4Buffer.AddrOfPinnedObject());
			colorOut = AssimpInterop.LoadColorFromArray(array);
			newFloat4Buffer.Free();
			return result;
		}

		// Token: 0x06000129 RID: 297
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasName")]
		private static extern bool _aiMaterial_HasName(IntPtr ptrMat);

		// Token: 0x0600012A RID: 298 RVA: 0x00007948 File Offset: 0x00005B48
		public static bool aiMaterial_HasName(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasName(ptrMat);
		}

		// Token: 0x0600012B RID: 299
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetName")]
		private static extern bool _aiMaterial_GetName(IntPtr ptrMat, IntPtr strName);

		// Token: 0x0600012C RID: 300 RVA: 0x00007960 File Offset: 0x00005B60
		public static bool aiMaterial_GetName(IntPtr ptrMat, out string strName)
		{
			byte[] value;
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			bool result = AssimpInterop._aiMaterial_GetName(ptrMat, newStringBuffer.AddrOfPinnedObject());
			strName = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			return result;
		}

		// Token: 0x0600012D RID: 301
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasBumpScaling")]
		private static extern bool _aiMaterial_HasBumpScaling(IntPtr ptrMat);

		// Token: 0x0600012E RID: 302 RVA: 0x00007994 File Offset: 0x00005B94
		public static bool aiMaterial_HasBumpScaling(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasBumpScaling(ptrMat);
		}

		// Token: 0x0600012F RID: 303
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetBumpScaling")]
		private static extern bool _aiMaterial_GetBumpScaling(IntPtr ptrMat, IntPtr floatOut);

		// Token: 0x06000130 RID: 304 RVA: 0x000079AC File Offset: 0x00005BAC
		public static bool aiMaterial_GetBumpScaling(IntPtr ptrMat, out float floatOut)
		{
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatOut);
			bool result = AssimpInterop._aiMaterial_GetBumpScaling(ptrMat, newFloatBuffer.AddrOfPinnedObject());
			newFloatBuffer.Free();
			return result;
		}

		// Token: 0x06000131 RID: 305
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasShininess")]
		private static extern bool _aiMaterial_HasShininess(IntPtr ptrMat);

		// Token: 0x06000132 RID: 306 RVA: 0x000079D8 File Offset: 0x00005BD8
		public static bool aiMaterial_HasShininess(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasShininess(ptrMat);
		}

		// Token: 0x06000133 RID: 307
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetShininess")]
		private static extern bool _aiMaterial_GetShininess(IntPtr ptrMat, IntPtr floatOut);

		// Token: 0x06000134 RID: 308 RVA: 0x000079F0 File Offset: 0x00005BF0
		public static bool aiMaterial_GetShininess(IntPtr ptrMat, out float floatOut)
		{
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatOut);
			bool result = AssimpInterop._aiMaterial_GetShininess(ptrMat, newFloatBuffer.AddrOfPinnedObject());
			newFloatBuffer.Free();
			return result;
		}

		// Token: 0x06000135 RID: 309
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasShininessStrength")]
		private static extern bool _aiMaterial_HasShininessStrength(IntPtr ptrMat);

		// Token: 0x06000136 RID: 310 RVA: 0x00007A1C File Offset: 0x00005C1C
		public static bool aiMaterial_HasShininessStrength(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasShininessStrength(ptrMat);
		}

		// Token: 0x06000137 RID: 311
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetShininessStrength")]
		private static extern bool _aiMaterial_GetShininessStrength(IntPtr ptrMat, IntPtr floatOut);

		// Token: 0x06000138 RID: 312 RVA: 0x00007A34 File Offset: 0x00005C34
		public static bool aiMaterial_GetShininessStrength(IntPtr ptrMat, out float floatOut)
		{
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatOut);
			bool result = AssimpInterop._aiMaterial_GetShininessStrength(ptrMat, newFloatBuffer.AddrOfPinnedObject());
			newFloatBuffer.Free();
			return result;
		}

		// Token: 0x06000139 RID: 313
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasOpacity")]
		private static extern bool _aiMaterial_HasOpacity(IntPtr ptrMat);

		// Token: 0x0600013A RID: 314 RVA: 0x00007A60 File Offset: 0x00005C60
		public static bool aiMaterial_HasOpacity(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasOpacity(ptrMat);
		}

		// Token: 0x0600013B RID: 315
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetOpacity")]
		private static extern bool _aiMaterial_GetOpacity(IntPtr ptrMat, IntPtr floatOut);

		// Token: 0x0600013C RID: 316 RVA: 0x00007A78 File Offset: 0x00005C78
		public static bool aiMaterial_GetOpacity(IntPtr ptrMat, out float floatOut)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(4);
			bool result = AssimpInterop._aiMaterial_GetOpacity(ptrMat, intPtr);
			float[] array = new float[1];
			Marshal.Copy(intPtr, array, 0, 1);
			floatOut = array[0];
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x0600013D RID: 317
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_VertexCount")]
		private static extern uint _aiMesh_VertexCount(IntPtr ptrMesh);

		// Token: 0x0600013E RID: 318 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public static uint aiMesh_VertexCount(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_VertexCount(ptrMesh);
		}

		// Token: 0x0600013F RID: 319
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasNormals")]
		private static extern bool _aiMesh_HasNormals(IntPtr ptrMesh);

		// Token: 0x06000140 RID: 320 RVA: 0x00007AC8 File Offset: 0x00005CC8
		public static bool aiMesh_HasNormals(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_HasNormals(ptrMesh);
		}

		// Token: 0x06000141 RID: 321
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasTangentsAndBitangents")]
		private static extern bool _aiMesh_HasTangentsAndBitangents(IntPtr ptrMesh);

		// Token: 0x06000142 RID: 322 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public static bool aiMesh_HasTangentsAndBitangents(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_HasTangentsAndBitangents(ptrMesh);
		}

		// Token: 0x06000143 RID: 323
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasTextureCoords")]
		private static extern bool _aiMesh_HasTextureCoords(IntPtr ptrMesh, uint uintIndex);

		// Token: 0x06000144 RID: 324 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public static bool aiMesh_HasTextureCoords(IntPtr ptrMesh, uint uintIndex)
		{
			return AssimpInterop._aiMesh_HasTextureCoords(ptrMesh, uintIndex);
		}

		// Token: 0x06000145 RID: 325
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasVertexColors")]
		private static extern bool _aiMesh_HasVertexColors(IntPtr ptrMesh, uint uintIndex);

		// Token: 0x06000146 RID: 326 RVA: 0x00007B10 File Offset: 0x00005D10
		public static bool aiMesh_HasVertexColors(IntPtr ptrMesh, uint uintIndex)
		{
			return AssimpInterop._aiMesh_HasVertexColors(ptrMesh, uintIndex);
		}

		// Token: 0x06000147 RID: 327
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetVertex")]
		private static extern IntPtr _aiMesh_GetVertex(IntPtr ptrMesh, uint uintIndex);

		// Token: 0x06000148 RID: 328 RVA: 0x00007B28 File Offset: 0x00005D28
		public static Vector3 aiMesh_GetVertex(IntPtr ptrMesh, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetVertex(ptrMesh, uintIndex);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x06000149 RID: 329
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetNormal")]
		private static extern IntPtr _aiMesh_GetNormal(IntPtr ptrMesh, uint uintIndex);

		// Token: 0x0600014A RID: 330 RVA: 0x00007B4C File Offset: 0x00005D4C
		public static Vector3 aiMesh_GetNormal(IntPtr ptrMesh, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetNormal(ptrMesh, uintIndex);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x0600014B RID: 331
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetTangent")]
		private static extern IntPtr _aiMesh_GetTangent(IntPtr ptrMesh, uint uintIndex);

		// Token: 0x0600014C RID: 332 RVA: 0x00007B70 File Offset: 0x00005D70
		public static Vector3 aiMesh_GetTangent(IntPtr ptrMesh, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetTangent(ptrMesh, uintIndex);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x0600014D RID: 333
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetBitangent")]
		private static extern IntPtr _aiMesh_GetBitangent(IntPtr ptrMesh, uint uintIndex);

		// Token: 0x0600014E RID: 334 RVA: 0x00007B94 File Offset: 0x00005D94
		public static Vector3 aiMesh_GetBitangent(IntPtr ptrMesh, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetBitangent(ptrMesh, uintIndex);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x0600014F RID: 335
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetTextureCoord")]
		private static extern IntPtr _aiMesh_GetTextureCoord(IntPtr ptrMesh, uint uintChannel, uint uintIndex);

		// Token: 0x06000150 RID: 336 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public static Vector2 aiMesh_GetTextureCoord(IntPtr ptrMesh, uint uintChannel, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetTextureCoord(ptrMesh, uintChannel, uintIndex);
			float[] newFloat2Array = AssimpInterop.GetNewFloat2Array(pointer);
			return AssimpInterop.LoadVector2FromArray(newFloat2Array);
		}

		// Token: 0x06000151 RID: 337
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetVertexColor")]
		private static extern IntPtr _aiMesh_GetVertexColor(IntPtr ptrMesh, uint uintChannel, uint uintIndex);

		// Token: 0x06000152 RID: 338 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public static Color aiMesh_GetVertexColor(IntPtr ptrMesh, uint uintChannel, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetVertexColor(ptrMesh, uintChannel, uintIndex);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadColorFromArray(newFloat4Array);
		}

		// Token: 0x06000153 RID: 339
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetMatrialIndex")]
		private static extern uint _aiMesh_GetMatrialIndex(IntPtr ptrMesh);

		// Token: 0x06000154 RID: 340 RVA: 0x00007C08 File Offset: 0x00005E08
		public static uint aiMesh_GetMatrialIndex(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_GetMatrialIndex(ptrMesh);
		}

		// Token: 0x06000155 RID: 341
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetName")]
		private static extern IntPtr _aiMesh_GetName(IntPtr ptrMesh);

		// Token: 0x06000156 RID: 342 RVA: 0x00007C20 File Offset: 0x00005E20
		public static string aiMesh_GetName(IntPtr ptrMesh)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetName(ptrMesh);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		// Token: 0x06000157 RID: 343
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasFaces")]
		private static extern bool _aiMesh_HasFaces(IntPtr ptrMesh);

		// Token: 0x06000158 RID: 344 RVA: 0x00007C3C File Offset: 0x00005E3C
		public static bool aiMesh_HasFaces(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_HasFaces(ptrMesh);
		}

		// Token: 0x06000159 RID: 345
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetNumFaces")]
		private static extern uint _aiMesh_GetNumFaces(IntPtr ptrMesh);

		// Token: 0x0600015A RID: 346 RVA: 0x00007C54 File Offset: 0x00005E54
		public static uint aiMesh_GetNumFaces(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_GetNumFaces(ptrMesh);
		}

		// Token: 0x0600015B RID: 347
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetFace")]
		private static extern IntPtr _aiMesh_GetFace(IntPtr ptrMesh, uint uintIndex);

		// Token: 0x0600015C RID: 348 RVA: 0x00007C6C File Offset: 0x00005E6C
		public static IntPtr aiMesh_GetFace(IntPtr ptrMesh, uint uintIndex)
		{
			return AssimpInterop._aiMesh_GetFace(ptrMesh, uintIndex);
		}

		// Token: 0x0600015D RID: 349
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasBones")]
		private static extern bool _aiMesh_HasBones(IntPtr ptrMesh);

		// Token: 0x0600015E RID: 350 RVA: 0x00007C84 File Offset: 0x00005E84
		public static bool aiMesh_HasBones(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_HasBones(ptrMesh);
		}

		// Token: 0x0600015F RID: 351
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetNumBones")]
		private static extern uint _aiMesh_GetNumBones(IntPtr ptrMesh);

		// Token: 0x06000160 RID: 352 RVA: 0x00007C9C File Offset: 0x00005E9C
		public static uint aiMesh_GetNumBones(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_GetNumBones(ptrMesh);
		}

		// Token: 0x06000161 RID: 353
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetBone")]
		private static extern IntPtr _aiMesh_GetBone(IntPtr ptrMesh, uint uintIndex);

		// Token: 0x06000162 RID: 354 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public static IntPtr aiMesh_GetBone(IntPtr ptrMesh, uint uintIndex)
		{
			return AssimpInterop._aiMesh_GetBone(ptrMesh, uintIndex);
		}

		// Token: 0x06000163 RID: 355
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiFace_GetNumIndices")]
		private static extern uint _aiFace_GetNumIndices(IntPtr ptrFace);

		// Token: 0x06000164 RID: 356 RVA: 0x00007CCC File Offset: 0x00005ECC
		public static uint aiFace_GetNumIndices(IntPtr ptrFace)
		{
			return AssimpInterop._aiFace_GetNumIndices(ptrFace);
		}

		// Token: 0x06000165 RID: 357
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiFace_GetIndex")]
		private static extern uint _aiFace_GetIndex(IntPtr ptrFace, uint uintIndex);

		// Token: 0x06000166 RID: 358 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public static uint aiFace_GetIndex(IntPtr ptrFace, uint uintIndex)
		{
			return AssimpInterop._aiFace_GetIndex(ptrFace, uintIndex);
		}

		// Token: 0x06000167 RID: 359
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiBone_GetName")]
		private static extern IntPtr _aiBone_GetName(IntPtr ptrBone);

		// Token: 0x06000168 RID: 360 RVA: 0x00007CFC File Offset: 0x00005EFC
		public static string aiBone_GetName(IntPtr ptrBone)
		{
			IntPtr pointer = AssimpInterop._aiBone_GetName(ptrBone);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		// Token: 0x06000169 RID: 361
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiBone_GetNumWeights")]
		private static extern uint _aiBone_GetNumWeights(IntPtr ptrBone);

		// Token: 0x0600016A RID: 362 RVA: 0x00007D18 File Offset: 0x00005F18
		public static uint aiBone_GetNumWeights(IntPtr ptrBone)
		{
			return AssimpInterop._aiBone_GetNumWeights(ptrBone);
		}

		// Token: 0x0600016B RID: 363
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiBone_GetWeights")]
		private static extern IntPtr _aiBone_GetWeights(IntPtr ptrBone, uint uintIndex);

		// Token: 0x0600016C RID: 364 RVA: 0x00007D30 File Offset: 0x00005F30
		public static IntPtr aiBone_GetWeights(IntPtr ptrBone, uint uintIndex)
		{
			return AssimpInterop._aiBone_GetWeights(ptrBone, uintIndex);
		}

		// Token: 0x0600016D RID: 365
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiBone_GetOffsetMatrix")]
		private static extern IntPtr _aiBone_GetOffsetMatrix(IntPtr ptrBone);

		// Token: 0x0600016E RID: 366 RVA: 0x00007D48 File Offset: 0x00005F48
		public static Matrix4x4 aiBone_GetOffsetMatrix(IntPtr ptrBone)
		{
			IntPtr pointer = AssimpInterop._aiBone_GetOffsetMatrix(ptrBone);
			float[] newFloat16Array = AssimpInterop.GetNewFloat16Array(pointer);
			return AssimpInterop.LoadMatrix4x4FromArray(newFloat16Array);
		}

		// Token: 0x0600016F RID: 367
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiVertexWeight_GetWeight")]
		private static extern float _aiVertexWeight_GetWeight(IntPtr ptrVweight);

		// Token: 0x06000170 RID: 368 RVA: 0x00007D6C File Offset: 0x00005F6C
		public static float aiVertexWeight_GetWeight(IntPtr ptrVweight)
		{
			return AssimpInterop._aiVertexWeight_GetWeight(ptrVweight);
		}

		// Token: 0x06000171 RID: 369
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiVertexWeight_GetVertexId")]
		private static extern uint _aiVertexWeight_GetVertexId(IntPtr ptrVweight);

		// Token: 0x06000172 RID: 370 RVA: 0x00007D84 File Offset: 0x00005F84
		public static uint aiVertexWeight_GetVertexId(IntPtr ptrVweight)
		{
			return AssimpInterop._aiVertexWeight_GetVertexId(ptrVweight);
		}

		// Token: 0x06000173 RID: 371
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetName")]
		private static extern IntPtr _aiAnimation_GetName(IntPtr ptrAnimation);

		// Token: 0x06000174 RID: 372 RVA: 0x00007D9C File Offset: 0x00005F9C
		public static string aiAnimation_GetName(IntPtr ptrAnimation)
		{
			IntPtr pointer = AssimpInterop._aiAnimation_GetName(ptrAnimation);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		// Token: 0x06000175 RID: 373
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetDuraction")]
		private static extern float _aiAnimation_GetDuraction(IntPtr ptrAnimation);

		// Token: 0x06000176 RID: 374 RVA: 0x00007DB8 File Offset: 0x00005FB8
		public static float aiAnimation_GetDuraction(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetDuraction(ptrAnimation);
		}

		// Token: 0x06000177 RID: 375
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetTicksPerSecond")]
		private static extern float _aiAnimation_GetTicksPerSecond(IntPtr ptrAnimation);

		// Token: 0x06000178 RID: 376 RVA: 0x00007DD0 File Offset: 0x00005FD0
		public static float aiAnimation_GetTicksPerSecond(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetTicksPerSecond(ptrAnimation);
		}

		// Token: 0x06000179 RID: 377
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetNumChannels")]
		private static extern uint _aiAnimation_GetNumChannels(IntPtr ptrAnimation);

		// Token: 0x0600017A RID: 378 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public static uint aiAnimation_GetNumChannels(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetNumChannels(ptrAnimation);
		}

		// Token: 0x0600017B RID: 379
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetNumMorphChannels")]
		private static extern uint _aiAnimation_GetNumMorphChannels(IntPtr ptrAnimation);

		// Token: 0x0600017C RID: 380 RVA: 0x00007E00 File Offset: 0x00006000
		public static uint aiAnimation_GetNumMorphChannels(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetNumMorphChannels(ptrAnimation);
		}

		// Token: 0x0600017D RID: 381
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetNumMeshChannels")]
		private static extern uint _aiAnimation_GetNumMeshChannels(IntPtr ptrAnimation);

		// Token: 0x0600017E RID: 382 RVA: 0x00007E18 File Offset: 0x00006018
		public static uint aiAnimation_GetNumMeshChannels(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetNumMeshChannels(ptrAnimation);
		}

		// Token: 0x0600017F RID: 383
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetAnimationChannel")]
		private static extern IntPtr _aiAnimation_GetAnimationChannel(IntPtr ptrAnimation, uint uintIndex);

		// Token: 0x06000180 RID: 384 RVA: 0x00007E30 File Offset: 0x00006030
		public static IntPtr aiAnimation_GetAnimationChannel(IntPtr ptrAnimation, uint uintIndex)
		{
			return AssimpInterop._aiAnimation_GetAnimationChannel(ptrAnimation, uintIndex);
		}

		// Token: 0x06000181 RID: 385
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetNodeName")]
		private static extern IntPtr _aiNodeAnim_GetNodeName(IntPtr ptrNodeAnim);

		// Token: 0x06000182 RID: 386 RVA: 0x00007E48 File Offset: 0x00006048
		public static string aiNodeAnim_GetNodeName(IntPtr ptrNodeAnim)
		{
			IntPtr pointer = AssimpInterop._aiNodeAnim_GetNodeName(ptrNodeAnim);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		// Token: 0x06000183 RID: 387
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetNumPositionKeys")]
		private static extern uint _aiNodeAnim_GetNumPositionKeys(IntPtr ptrNodeAnim);

		// Token: 0x06000184 RID: 388 RVA: 0x00007E64 File Offset: 0x00006064
		public static uint aiNodeAnim_GetNumPositionKeys(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetNumPositionKeys(ptrNodeAnim);
		}

		// Token: 0x06000185 RID: 389
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetNumRotationKeys")]
		private static extern uint _aiNodeAnim_GetNumRotationKeys(IntPtr ptrNodeAnim);

		// Token: 0x06000186 RID: 390 RVA: 0x00007E7C File Offset: 0x0000607C
		public static uint aiNodeAnim_GetNumRotationKeys(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetNumRotationKeys(ptrNodeAnim);
		}

		// Token: 0x06000187 RID: 391
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetNumScalingKeys")]
		private static extern uint _aiNodeAnim_GetNumScalingKeys(IntPtr ptrNodeAnim);

		// Token: 0x06000188 RID: 392 RVA: 0x00007E94 File Offset: 0x00006094
		public static uint aiNodeAnim_GetNumScalingKeys(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetNumScalingKeys(ptrNodeAnim);
		}

		// Token: 0x06000189 RID: 393
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetPostState")]
		private static extern uint _aiNodeAnim_GetPostState(IntPtr ptrNodeAnim);

		// Token: 0x0600018A RID: 394 RVA: 0x00007EAC File Offset: 0x000060AC
		public static uint aiNodeAnim_GetPostState(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetPostState(ptrNodeAnim);
		}

		// Token: 0x0600018B RID: 395
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetPreState")]
		private static extern uint _aiNodeAnim_GetPreState(IntPtr ptrNodeAnim);

		// Token: 0x0600018C RID: 396 RVA: 0x00007EC4 File Offset: 0x000060C4
		public static uint aiNodeAnim_GetPreState(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetPreState(ptrNodeAnim);
		}

		// Token: 0x0600018D RID: 397
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetPositionKey")]
		private static extern IntPtr _aiNodeAnim_GetPositionKey(IntPtr ptrNodeAnim, uint uintIndex);

		// Token: 0x0600018E RID: 398 RVA: 0x00007EDC File Offset: 0x000060DC
		public static IntPtr aiNodeAnim_GetPositionKey(IntPtr ptrNodeAnim, uint uintIndex)
		{
			return AssimpInterop._aiNodeAnim_GetPositionKey(ptrNodeAnim, uintIndex);
		}

		// Token: 0x0600018F RID: 399
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetRotationKey")]
		private static extern IntPtr _aiNodeAnim_GetRotationKey(IntPtr ptrNodeAnim, uint uintIndex);

		// Token: 0x06000190 RID: 400 RVA: 0x00007EF4 File Offset: 0x000060F4
		public static IntPtr aiNodeAnim_GetRotationKey(IntPtr ptrNodeAnim, uint uintIndex)
		{
			return AssimpInterop._aiNodeAnim_GetRotationKey(ptrNodeAnim, uintIndex);
		}

		// Token: 0x06000191 RID: 401
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetScalingKey")]
		private static extern IntPtr _aiNodeAnim_GetScalingKey(IntPtr ptrNodeAnim, uint uintIndex);

		// Token: 0x06000192 RID: 402 RVA: 0x00007F0C File Offset: 0x0000610C
		public static IntPtr aiNodeAnim_GetScalingKey(IntPtr ptrNodeAnim, uint uintIndex)
		{
			return AssimpInterop._aiNodeAnim_GetScalingKey(ptrNodeAnim, uintIndex);
		}

		// Token: 0x06000193 RID: 403
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiVectorKey_GetTime")]
		private static extern float _aiVectorKey_GetTime(IntPtr ptrVectorKey);

		// Token: 0x06000194 RID: 404 RVA: 0x00007F24 File Offset: 0x00006124
		public static float aiVectorKey_GetTime(IntPtr ptrVectorKey)
		{
			return AssimpInterop._aiVectorKey_GetTime(ptrVectorKey);
		}

		// Token: 0x06000195 RID: 405
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiVectorKey_GetValue")]
		private static extern IntPtr _aiVectorKey_GetValue(IntPtr ptrVectorKey);

		// Token: 0x06000196 RID: 406 RVA: 0x00007F3C File Offset: 0x0000613C
		public static Vector3 aiVectorKey_GetValue(IntPtr ptrVectorKey)
		{
			IntPtr pointer = AssimpInterop._aiVectorKey_GetValue(ptrVectorKey);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x06000197 RID: 407
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiQuatKey_GetTime")]
		private static extern float _aiQuatKey_GetTime(IntPtr ptrQuatKey);

		// Token: 0x06000198 RID: 408 RVA: 0x00007F60 File Offset: 0x00006160
		public static float aiQuatKey_GetTime(IntPtr ptrQuatKey)
		{
			return AssimpInterop._aiQuatKey_GetTime(ptrQuatKey);
		}

		// Token: 0x06000199 RID: 409
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiQuatKey_GetValue")]
		private static extern IntPtr _aiQuatKey_GetValue(IntPtr ptrQuatKey);

		// Token: 0x0600019A RID: 410 RVA: 0x00007F78 File Offset: 0x00006178
		public static Quaternion aiQuatKey_GetValue(IntPtr ptrQuatKey)
		{
			IntPtr pointer = AssimpInterop._aiQuatKey_GetValue(ptrQuatKey);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadQuaternionFromArray(newFloat4Array);
		}

		// Token: 0x0600019B RID: 411
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetAspect")]
		private static extern float _aiCamera_GetAspect(IntPtr ptrCamera);

		// Token: 0x0600019C RID: 412 RVA: 0x00007F9C File Offset: 0x0000619C
		public static float aiCamera_GetAspect(IntPtr ptrCamera)
		{
			return AssimpInterop._aiCamera_GetAspect(ptrCamera);
		}

		// Token: 0x0600019D RID: 413
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetClipPlaneFar")]
		private static extern float _aiCamera_GetClipPlaneFar(IntPtr ptrCamera);

		// Token: 0x0600019E RID: 414 RVA: 0x00007FB4 File Offset: 0x000061B4
		public static float aiCamera_GetClipPlaneFar(IntPtr ptrCamera)
		{
			return AssimpInterop._aiCamera_GetClipPlaneFar(ptrCamera);
		}

		// Token: 0x0600019F RID: 415
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetClipPlaneNear")]
		private static extern float _aiCamera_GetClipPlaneNear(IntPtr ptrCamera);

		// Token: 0x060001A0 RID: 416 RVA: 0x00007FCC File Offset: 0x000061CC
		public static float aiCamera_GetClipPlaneNear(IntPtr ptrCamera)
		{
			return AssimpInterop._aiCamera_GetClipPlaneNear(ptrCamera);
		}

		// Token: 0x060001A1 RID: 417
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetHorizontalFOV")]
		private static extern float _aiCamera_GetHorizontalFOV(IntPtr ptrCamera);

		// Token: 0x060001A2 RID: 418 RVA: 0x00007FE4 File Offset: 0x000061E4
		public static float aiCamera_GetHorizontalFOV(IntPtr ptrCamera)
		{
			return AssimpInterop._aiCamera_GetHorizontalFOV(ptrCamera);
		}

		// Token: 0x060001A3 RID: 419
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetLookAt")]
		private static extern IntPtr _aiCamera_GetLookAt(IntPtr ptrCamera);

		// Token: 0x060001A4 RID: 420 RVA: 0x00007FFC File Offset: 0x000061FC
		public static Vector3 aiCamera_GetLookAt(IntPtr ptrCamera)
		{
			IntPtr pointer = AssimpInterop._aiCamera_GetLookAt(ptrCamera);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x060001A5 RID: 421
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetName")]
		private static extern IntPtr _aiCamera_GetName(IntPtr ptrCamera);

		// Token: 0x060001A6 RID: 422 RVA: 0x00008020 File Offset: 0x00006220
		public static string aiCamera_GetName(IntPtr ptrCamera)
		{
			IntPtr pointer = AssimpInterop._aiCamera_GetName(ptrCamera);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		// Token: 0x060001A7 RID: 423
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetPosition")]
		private static extern IntPtr _aiCamera_GetPosition(IntPtr ptrCamera);

		// Token: 0x060001A8 RID: 424 RVA: 0x0000803C File Offset: 0x0000623C
		public static Vector3 aiCamera_GetPosition(IntPtr ptrCamera)
		{
			IntPtr pointer = AssimpInterop._aiCamera_GetPosition(ptrCamera);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x060001A9 RID: 425
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetUp")]
		private static extern IntPtr _aiCamera_GetUp(IntPtr ptrCamera);

		// Token: 0x060001AA RID: 426 RVA: 0x00008060 File Offset: 0x00006260
		public static Vector3 aiCamera_GetUp(IntPtr ptrCamera)
		{
			IntPtr pointer = AssimpInterop._aiCamera_GetUp(ptrCamera);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x060001AB RID: 427
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAngleInnerCone")]
		private static extern float _aiLight_GetAngleInnerCone(IntPtr ptrLight);

		// Token: 0x060001AC RID: 428 RVA: 0x00008084 File Offset: 0x00006284
		public static float aiLight_GetAngleInnerCone(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAngleInnerCone(ptrLight);
		}

		// Token: 0x060001AD RID: 429
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAngleOuterCone")]
		private static extern float _aiLight_GetAngleOuterCone(IntPtr ptrLight);

		// Token: 0x060001AE RID: 430 RVA: 0x0000809C File Offset: 0x0000629C
		public static float aiLight_GetAngleOuterCone(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAngleOuterCone(ptrLight);
		}

		// Token: 0x060001AF RID: 431
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAttenuationConstant")]
		private static extern float _aiLight_GetAttenuationConstant(IntPtr ptrLight);

		// Token: 0x060001B0 RID: 432 RVA: 0x000080B4 File Offset: 0x000062B4
		public static float aiLight_GetAttenuationConstant(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAttenuationConstant(ptrLight);
		}

		// Token: 0x060001B1 RID: 433
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAttenuationLinear")]
		private static extern float _aiLight_GetAttenuationLinear(IntPtr ptrLight);

		// Token: 0x060001B2 RID: 434 RVA: 0x000080CC File Offset: 0x000062CC
		public static float aiLight_GetAttenuationLinear(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAttenuationLinear(ptrLight);
		}

		// Token: 0x060001B3 RID: 435
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAttenuationQuadratic")]
		private static extern float _aiLight_GetAttenuationQuadratic(IntPtr ptrLight);

		// Token: 0x060001B4 RID: 436 RVA: 0x000080E4 File Offset: 0x000062E4
		public static float aiLight_GetAttenuationQuadratic(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAttenuationQuadratic(ptrLight);
		}

		// Token: 0x060001B5 RID: 437
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetColorAmbient")]
		private static extern IntPtr _aiLight_GetColorAmbient(IntPtr ptrLight);

		// Token: 0x060001B6 RID: 438 RVA: 0x000080FC File Offset: 0x000062FC
		public static Color aiLight_GetColorAmbient(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetColorAmbient(ptrLight);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadColorFromArray(newFloat4Array);
		}

		// Token: 0x060001B7 RID: 439
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetColorDiffuse")]
		private static extern IntPtr _aiLight_GetColorDiffuse(IntPtr ptrLight);

		// Token: 0x060001B8 RID: 440 RVA: 0x00008120 File Offset: 0x00006320
		public static Color aiLight_GetColorDiffuse(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetColorDiffuse(ptrLight);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadColorFromArray(newFloat4Array);
		}

		// Token: 0x060001B9 RID: 441
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetColorSpecular")]
		private static extern IntPtr _aiLight_GetColorSpecular(IntPtr ptrLight);

		// Token: 0x060001BA RID: 442 RVA: 0x00008144 File Offset: 0x00006344
		public static Color aiLight_GetColorSpecular(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetColorSpecular(ptrLight);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadColorFromArray(newFloat4Array);
		}

		// Token: 0x060001BB RID: 443
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetDirection")]
		private static extern IntPtr _aiLight_GetDirection(IntPtr ptrLight);

		// Token: 0x060001BC RID: 444 RVA: 0x00008168 File Offset: 0x00006368
		public static Vector3 aiLight_GetDirection(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetDirection(ptrLight);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		// Token: 0x060001BD RID: 445
		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetName")]
		private static extern IntPtr _aiLight_GetName(IntPtr ptrLight);

		// Token: 0x060001BE RID: 446 RVA: 0x0000818C File Offset: 0x0000638C
		public static string aiLight_GetName(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetName(ptrLight);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000081A8 File Offset: 0x000063A8
		public static byte[] StringToByteArray(string str, int length)
		{
			return Encoding.ASCII.GetBytes(str.PadRight(length, '\0'));
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000081BC File Offset: 0x000063BC
		public static GCHandle LockGc(object value)
		{
			return GCHandle.Alloc(value, GCHandleType.Pinned);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000081C8 File Offset: 0x000063C8
		public static GCHandle GetStringBuffer(string value)
		{
			byte[] value2 = AssimpInterop.StringToByteArray(value, 1024);
			return AssimpInterop.LockGc(value2);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000081E8 File Offset: 0x000063E8
		public static string ByteArrayToString(byte[] value)
		{
			int num = Array.IndexOf<byte>(value, 0, 0);
			if (num < 0)
			{
				num = value.Length;
			}
			return Encoding.ASCII.GetString(value, 0, num);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008218 File Offset: 0x00006418
		public static IntPtr GetAssimpStringBuffer(string value)
		{
			int num = (!AssimpInterop.Is32Bits) ? 8 : 4;
			IntPtr intPtr = Marshal.AllocHGlobal(num + value.Length);
			if (AssimpInterop.Is32Bits)
			{
				Marshal.WriteInt32(intPtr, value.Length);
			}
			Marshal.WriteInt64(intPtr, (long)value.Length);
			byte[] bytes = Encoding.ASCII.GetBytes(value);
			Marshal.Copy(bytes, 0, new IntPtr((!AssimpInterop.Is32Bits) ? (intPtr.ToInt64() + (long)num) : ((long)intPtr.ToInt32())), value.Length);
			return intPtr;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000082A8 File Offset: 0x000064A8
		private static GCHandle GetNewStringBuffer(out byte[] byteArray)
		{
			byteArray = new byte[2048];
			return AssimpInterop.LockGc(byteArray);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000082BD File Offset: 0x000064BD
		private static GCHandle GetNewFloatBuffer(out float value)
		{
			value = 0f;
			return AssimpInterop.LockGc(value);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000082D2 File Offset: 0x000064D2
		private static GCHandle GetNewFloat2Buffer(out float[] array)
		{
			array = new float[2];
			return AssimpInterop.LockGc(array);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000082E3 File Offset: 0x000064E3
		private static GCHandle GetNewFloat3Buffer(out float[] array)
		{
			array = new float[3];
			return AssimpInterop.LockGc(array);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000082F4 File Offset: 0x000064F4
		private static GCHandle GetNewFloat4Buffer(out float[] array)
		{
			array = new float[4];
			return AssimpInterop.LockGc(array);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008305 File Offset: 0x00006505
		private static GCHandle GetNewFloat16Buffer(out float[] array)
		{
			array = new float[16];
			return AssimpInterop.LockGc(array);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008317 File Offset: 0x00006517
		private static GCHandle GetNewUIntBuffer(out uint value)
		{
			value = 0u;
			return AssimpInterop.LockGc(value);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008328 File Offset: 0x00006528
		private static float[] GetNewFloat2Array(IntPtr pointer)
		{
			float[] array = new float[2];
			Marshal.Copy(pointer, array, 0, 2);
			return array;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008348 File Offset: 0x00006548
		private static float[] GetNewFloat3Array(IntPtr pointer)
		{
			float[] array = new float[3];
			Marshal.Copy(pointer, array, 0, 3);
			return array;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008368 File Offset: 0x00006568
		private static float[] GetNewFloat4Array(IntPtr pointer)
		{
			float[] array = new float[4];
			Marshal.Copy(pointer, array, 0, 4);
			return array;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008388 File Offset: 0x00006588
		private static float[] GetNewFloat16Array(IntPtr pointer)
		{
			float[] array = new float[16];
			Marshal.Copy(pointer, array, 0, 16);
			return array;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000083A8 File Offset: 0x000065A8
		private static string ReadStringFromPointer(IntPtr pointer)
		{
			return Marshal.PtrToStringAnsi(pointer);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000083B0 File Offset: 0x000065B0
		private static Vector2 LoadVector2FromArray(float[] array)
		{
			return new Vector2(array[0], array[1]);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000083BD File Offset: 0x000065BD
		private static Vector3 LoadVector3FromArray(float[] array)
		{
			return new Vector3(array[0], array[1], array[2]);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000083CD File Offset: 0x000065CD
		private static Color LoadColorFromArray(float[] array)
		{
			return new Color(array[0], array[1], array[2], array[3]);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000083E0 File Offset: 0x000065E0
		private static Quaternion LoadQuaternionFromArray(float[] array)
		{
			return new Quaternion(array[1], array[2], array[3], array[0]);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000083F4 File Offset: 0x000065F4
		private static Matrix4x4 LoadMatrix4x4FromArray(float[] array)
		{
			Matrix4x4 result = default(Matrix4x4);
			result[0] = array[0];
			result[1] = array[4];
			result[2] = array[8];
			result[3] = array[12];
			result[4] = array[1];
			result[5] = array[5];
			result[6] = array[9];
			result[7] = array[13];
			result[8] = array[2];
			result[9] = array[6];
			result[10] = array[10];
			result[11] = array[14];
			result[12] = array[3];
			result[13] = array[7];
			result[14] = array[11];
			result[15] = array[15];
			return result;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000084C8 File Offset: 0x000066C8
		private static GCHandle Matrix4x4ToAssimp(Vector3 translation, Vector3 rotation, Vector3 scale)
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(translation, Quaternion.Euler(rotation), scale);
			float[] array = new float[16];
			array[0] = matrix4x[0];
			array[4] = matrix4x[1];
			array[8] = matrix4x[2];
			array[12] = matrix4x[3];
			array[1] = matrix4x[4];
			array[5] = matrix4x[5];
			array[9] = matrix4x[6];
			array[13] = matrix4x[7];
			array[2] = matrix4x[8];
			array[6] = matrix4x[9];
			array[10] = matrix4x[10];
			array[14] = matrix4x[11];
			array[3] = matrix4x[12];
			array[7] = matrix4x[13];
			array[11] = matrix4x[14];
			array[15] = matrix4x[15];
			return AssimpInterop.LockGc(array);
		}

		// Token: 0x04000110 RID: 272
		public const string DllPath = "assimp";

		// Token: 0x04000111 RID: 273
		private const int MaxStringLength = 1024;

		// Token: 0x04000112 RID: 274
		private const int MaxInputStringLength = 2048;

		// Token: 0x04000113 RID: 275
		private static readonly bool Is32Bits = IntPtr.Size == 4;

		// Token: 0x04000114 RID: 276
		private static readonly int IntSize = (!AssimpInterop.Is32Bits) ? 8 : 4;
	}
}
