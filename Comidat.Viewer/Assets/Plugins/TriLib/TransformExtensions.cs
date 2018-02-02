using System;
using System.Collections;
using UnityEngine;

namespace TriLib
{
	// Token: 0x02000031 RID: 49
	public static class TransformExtensions
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00008ACC File Offset: 0x00006CCC
		public static void LoadMatrix(this Transform transform, Matrix4x4 matrix, bool local = true)
		{
			if (local)
			{
				transform.localScale = matrix.ExtractScale();
				transform.localRotation = matrix.ExtractRotation();
				transform.localPosition = matrix.ExtractPosition();
			}
			else
			{
				transform.rotation = matrix.ExtractRotation();
				transform.position = matrix.ExtractPosition();
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008B20 File Offset: 0x00006D20
		public static Bounds EncapsulateBounds(this Transform transform)
		{
			Bounds result = default(Bounds);
			Renderer[] componentsInChildren = transform.GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null)
			{
				foreach (Renderer renderer in componentsInChildren)
				{
					result.Encapsulate(renderer.bounds);
				}
			}
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008B70 File Offset: 0x00006D70
		public static Transform FindDeepChild(this Transform transform, string name, bool endsWith = false)
		{
			if ((!endsWith) ? transform.name.EndsWith(name) : (transform.name == name))
			{
				return transform;
			}
			IEnumerator enumerator = transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform2 = (Transform)obj;
					Transform transform3 = transform2.FindDeepChild(name, false);
					if (transform3 != null)
					{
						return transform3;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return null;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008C14 File Offset: 0x00006E14
		public static void DestroyChildren(this Transform transform, bool destroyImmediate = false)
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				Transform child = transform.GetChild(i);
				if (destroyImmediate)
				{
					UnityEngine.Object.DestroyImmediate(child.gameObject);
				}
				else
				{
					UnityEngine.Object.Destroy(child.gameObject);
				}
			}
		}
	}
}
