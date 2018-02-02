using System;
using UnityEngine;

namespace TriLib
{
	// Token: 0x0200002A RID: 42
	public static class CameraExtensions
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x000085D4 File Offset: 0x000067D4
		public static void FitToBounds(this Camera camera, Transform transform, float distance)
		{
			Bounds bounds = transform.EncapsulateBounds();
			float magnitude = bounds.extents.magnitude;
			float num = magnitude / (2f * Mathf.Tan(0.5f * camera.fieldOfView * 0.0174532924f)) * distance;
			if (float.IsNaN(num))
			{
				return;
			}
			camera.farClipPlane = num * 2f;
			camera.transform.position = new Vector3(bounds.center.x, bounds.center.y, bounds.center.z + num);
			camera.transform.LookAt(bounds.center);
		}
	}
}
