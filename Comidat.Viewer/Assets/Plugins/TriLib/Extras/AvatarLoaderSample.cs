using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TriLib.Extras
{
	// Token: 0x02000032 RID: 50
	[ExecuteInEditMode]
	public class AvatarLoaderSample : MonoBehaviour
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00008C78 File Offset: 0x00006E78
		protected void Start()
		{
			this._avatarLoader = UnityEngine.Object.FindObjectOfType<AvatarLoader>();
			if (this._avatarLoader == null)
			{
				return;
			}
			string path = Path.Combine(Path.GetFullPath("."), this.ModelsDirectory);
			string supportedExtensions = AssetLoader.GetSupportedFileExtensions();
			this._files = (from x in Directory.GetFiles(path, "*.*")
			where supportedExtensions.Contains(Path.GetExtension(x).ToLower())
			select x).ToArray<string>();
			this._windowRect = new Rect(20f, 20f, 240f, (float)(Screen.height - 40));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008D14 File Offset: 0x00006F14
		protected void OnGUI()
		{
			if (this._files == null || this._avatarLoader == null || this.FreeLookCamPrefab == null || this.ThirdPersonControllerPrefab == null)
			{
				return;
			}
			this._windowRect = GUI.Window(0, this._windowRect, new GUI.WindowFunction(this.HandleWindowFunction), "Available Models");
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008D84 File Offset: 0x00006F84
		private void HandleWindowFunction(int id)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			this._scrollPosition = GUILayout.BeginScrollView(this._scrollPosition, new GUILayoutOption[0]);
			foreach (string text in this._files)
			{
				if (GUILayout.Button(Path.GetFileName(text), new GUILayoutOption[0]))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ThirdPersonControllerPrefab);
					gameObject.transform.DestroyChildren(true);
					if (this._avatarLoader.LoadAvatar(text, gameObject))
					{
						if (this.ActiveCameraGameObject != null)
						{
							UnityEngine.Object.Destroy(this.ActiveCameraGameObject.gameObject);
						}
						this.ActiveCameraGameObject = UnityEngine.Object.Instantiate<GameObject>(this.FreeLookCamPrefab);
					}
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
		}

		// Token: 0x04000119 RID: 281
		public GameObject FreeLookCamPrefab;

		// Token: 0x0400011A RID: 282
		public GameObject ThirdPersonControllerPrefab;

		// Token: 0x0400011B RID: 283
		public GameObject ActiveCameraGameObject;

		// Token: 0x0400011C RID: 284
		public string ModelsDirectory = "Models";

		// Token: 0x0400011D RID: 285
		private string[] _files;

		// Token: 0x0400011E RID: 286
		private Rect _windowRect;

		// Token: 0x0400011F RID: 287
		private Vector3 _scrollPosition;

		// Token: 0x04000120 RID: 288
		private AvatarLoader _avatarLoader;
	}
}
