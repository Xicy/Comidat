using System;
using UnityEngine;

namespace TriLib
{
	// Token: 0x0200002C RID: 44
	public static class MatrixExtensions
	{
		// Token: 0x060001DB RID: 475 RVA: 0x00008724 File Offset: 0x00006924
		public static Quaternion ExtractRotation(this Matrix4x4 matrix)
		{
			return Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008745 File Offset: 0x00006945
		public static Vector3 ExtractPosition(this Matrix4x4 matrix)
		{
			return matrix.GetColumn(3);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008754 File Offset: 0x00006954
		public static Vector3 ExtractScale(this Matrix4x4 matrix)
		{
			return new Vector3(matrix.GetColumn(0).magnitude, matrix.GetColumn(1).magnitude, matrix.GetColumn(2).magnitude);
		}
	}
}
