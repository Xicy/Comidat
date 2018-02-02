using System;
using System.Collections.Generic;
using UnityEngine;

namespace TriLib.Extras
{
	// Token: 0x02000033 RID: 51
	public class AvatarLoader : MonoBehaviour
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00008EE3 File Offset: 0x000070E3
		protected void Start()
		{
			this._loaderOptions = AssetLoaderOptions.CreateInstance();
			this._loaderOptions.UseLegacyAnimations = false;
			this._loaderOptions.AnimatorController = this.RuntimeAnimatorController;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008F10 File Offset: 0x00007110
		public bool LoadAvatar(string filename, GameObject templateAvatar)
		{
			if (this.CurrentAvatar != null)
			{
				UnityEngine.Object.Destroy(this.CurrentAvatar);
			}
			GameObject gameObject;
			try
			{
				using (AssetLoader assetLoader = new AssetLoader())
				{
					gameObject = assetLoader.LoadFromFile(filename, this._loaderOptions, templateAvatar);
				}
			}
			catch
			{
				return false;
			}
			if (gameObject != null)
			{
				if (templateAvatar != null)
				{
					gameObject.transform.parent = templateAvatar.transform;
					this.CurrentAvatar = templateAvatar;
				}
				else
				{
					this.CurrentAvatar = gameObject;
				}
				this.CurrentAvatar.transform.localScale = Vector3.one * this.Scale;
				this.CurrentAvatar.tag = "Player";
				this.SetupCapsuleCollider();
				return this.BuildAvatar();
			}
			return false;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009008 File Offset: 0x00007208
		private bool BuildAvatar()
		{
			Animator component = this.CurrentAvatar.GetComponent<Animator>();
			if (component == null)
			{
				return false;
			}
			List<SkeletonBone> list = new List<SkeletonBone>();
			List<HumanBone> list2 = new List<HumanBone>();
			Dictionary<string, Transform> dictionary = this.FindOutBoneTransforms(this.CurrentAvatar);
			if (dictionary.Count == 0)
			{
				return false;
			}
			foreach (KeyValuePair<string, Transform> keyValuePair in dictionary)
			{
				list2.Add(AvatarLoader.CreateHumanBone(keyValuePair.Key, keyValuePair.Value.name));
			}
			Transform[] componentsInChildren = this.CurrentAvatar.GetComponentsInChildren<Transform>();
			Transform transform = componentsInChildren[1];
			list.Add(AvatarLoader.CreateSkeletonBone(transform));
			transform.localEulerAngles = Vector3.zero;
			foreach (Transform transform2 in componentsInChildren)
			{
				MeshRenderer[] componentsInChildren2 = transform2.GetComponentsInChildren<MeshRenderer>();
				if (componentsInChildren2.Length <= 0)
				{
					SkinnedMeshRenderer[] componentsInChildren3 = transform2.GetComponentsInChildren<SkinnedMeshRenderer>();
					if (componentsInChildren3.Length <= 0)
					{
						list.Add(AvatarLoader.CreateSkeletonBone(transform2));
					}
				}
			}
			HumanDescription humanDescription = default(HumanDescription);
			humanDescription.armStretch = this.ArmStretch;
			humanDescription.feetSpacing = this.FeetSpacing;
			humanDescription.hasTranslationDoF = this.HasTranslationDof;
			humanDescription.legStretch = this.LegStretch;
			humanDescription.lowerArmTwist = this.LowerArmTwist;
			humanDescription.lowerLegTwist = this.LowerLegTwist;
			humanDescription.upperArmTwist = this.UpperArmTwist;
			humanDescription.upperLegTwist = this.UpperLegTwist;
			humanDescription.skeleton = list.ToArray();
			humanDescription.human = list2.ToArray();
			component.avatar = AvatarBuilder.BuildHumanAvatar(this.CurrentAvatar, humanDescription);
			return true;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000091E0 File Offset: 0x000073E0
		private Dictionary<string, Transform> FindOutBoneTransforms(GameObject loadedObject)
		{
			Dictionary<string, Transform> dictionary = new Dictionary<string, Transform>();
			List<BoneRelationshipList> list = new List<BoneRelationshipList>();
			list.Add(AvatarLoader.BipedBoneNames);
			list.Add(AvatarLoader.MixamoBoneNames);
			if (this.CustomBoneNames != null)
			{
				list.Add(this.CustomBoneNames);
			}
			bool flag = false;
			foreach (BoneRelationshipList boneRelationshipList in list)
			{
				if (flag)
				{
					break;
				}
				flag = true;
				foreach (BoneRelationship boneRelationship in boneRelationshipList)
				{
					Transform transform = loadedObject.transform.FindDeepChild(boneRelationship.BoneName, true);
					if (transform == null)
					{
						if (!boneRelationship.Optional)
						{
							dictionary.Clear();
							flag = false;
							break;
						}
					}
					else
					{
						dictionary.Add(boneRelationship.HumanBone, transform);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000930C File Offset: 0x0000750C
		private void SetupCapsuleCollider()
		{
			CapsuleCollider component = this.CurrentAvatar.GetComponent<CapsuleCollider>();
			if (component == null)
			{
				return;
			}
			Bounds bounds = this.CurrentAvatar.transform.EncapsulateBounds();
			float num = 1f / this.Scale;
			float num2 = bounds.extents.x * num;
			float num3 = bounds.extents.y * num;
			float num4 = bounds.extents.z * num;
			component.height = (float)Math.Round((double)(num3 * 2f), 1);
			component.radius = (float)Math.Round((double)(Mathf.Sqrt(num2 * num2 + num4 * num4) * 0.5f), 1);
			component.center = new Vector3(0f, (float)Math.Round((double)num3, 1) + this.HeightOffset, 0f);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000093EC File Offset: 0x000075EC
		private static SkeletonBone CreateSkeletonBone(Transform boneTransform)
		{
			return new SkeletonBone
			{
				name = boneTransform.name,
				position = boneTransform.localPosition,
				rotation = boneTransform.localRotation,
				scale = boneTransform.localScale
			};
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009438 File Offset: 0x00007638
		private static HumanBone CreateHumanBone(string humanName, string boneName)
		{
			HumanBone result = default(HumanBone);
			result.boneName = boneName;
			result.humanName = humanName;
			result.limit.useDefaultValues = true;
			return result;
		}

		// Token: 0x04000121 RID: 289
		public GameObject CurrentAvatar;

		// Token: 0x04000122 RID: 290
		public RuntimeAnimatorController RuntimeAnimatorController;

		// Token: 0x04000123 RID: 291
		public float ArmStretch = 0.05f;

		// Token: 0x04000124 RID: 292
		public float FeetSpacing;

		// Token: 0x04000125 RID: 293
		public bool HasTranslationDof;

		// Token: 0x04000126 RID: 294
		public float LegStretch = 0.5f;

		// Token: 0x04000127 RID: 295
		public float LowerArmTwist = 0.5f;

		// Token: 0x04000128 RID: 296
		public float LowerLegTwist = 0.5f;

		// Token: 0x04000129 RID: 297
		public float UpperArmTwist = 0.5f;

		// Token: 0x0400012A RID: 298
		public float UpperLegTwist = 0.5f;

		// Token: 0x0400012B RID: 299
		public float Scale = 0.01f;

		// Token: 0x0400012C RID: 300
		public float HeightOffset = 0.01f;

		// Token: 0x0400012D RID: 301
		public BoneRelationshipList CustomBoneNames;

		// Token: 0x0400012E RID: 302
		private static readonly BoneRelationshipList BipedBoneNames = new BoneRelationshipList
		{
			{
				"Head",
				"Head",
				false
			},
			{
				"Neck",
				"Neck",
				true
			},
			{
				"Chest",
				"Spine3",
				true
			},
			{
				"UpperChest",
				"Spine1",
				true
			},
			{
				"Spine",
				"Spine",
				false
			},
			{
				"Hips",
				"Bip01",
				false
			},
			{
				"LeftShoulder",
				"L Clavicle",
				true
			},
			{
				"LeftUpperArm",
				"L UpperArm",
				false
			},
			{
				"LeftLowerArm",
				"L Forearm",
				false
			},
			{
				"LeftHand",
				"L Hand",
				false
			},
			{
				"RightShoulder",
				"R Clavicle",
				true
			},
			{
				"RightUpperArm",
				"R UpperArm",
				false
			},
			{
				"RightLowerArm",
				"R Forearm",
				false
			},
			{
				"RightHand",
				"R Hand",
				false
			},
			{
				"LeftUpperLeg",
				"L Thigh",
				false
			},
			{
				"LeftLowerLeg",
				"L Calf",
				false
			},
			{
				"LeftFoot",
				"L Foot",
				false
			},
			{
				"LeftToes",
				"L Toe0",
				true
			},
			{
				"RightUpperLeg",
				"R Thigh",
				false
			},
			{
				"RightLowerLeg",
				"R Calf",
				false
			},
			{
				"RightFoot",
				"R Foot",
				false
			},
			{
				"RightToes",
				"R Toe0",
				true
			},
			{
				"Left Thumb Proximal",
				"L Finger0",
				true
			},
			{
				"Left Thumb Intermediate",
				"L Finger01",
				true
			},
			{
				"Left Thumb Distal",
				"L Finger02",
				true
			},
			{
				"Left Index Proximal",
				"L Finger1",
				true
			},
			{
				"Left Index Intermediate",
				"L Finger11",
				true
			},
			{
				"Left Index Distal",
				"L Finger12",
				true
			},
			{
				"Left Middle Proximal",
				"L Finger2",
				true
			},
			{
				"Left Middle Intermediate",
				"L Finger21",
				true
			},
			{
				"Left Middle Distal",
				"L Finger22",
				true
			},
			{
				"Left Ring Proximal",
				"L Finger3",
				true
			},
			{
				"Left Ring Intermediate",
				"L Finger31",
				true
			},
			{
				"Left Ring Distal",
				"L Finger32",
				true
			},
			{
				"Left Little Proximal",
				"L Finger4",
				true
			},
			{
				"Left Little Intermediate",
				"L Finger41",
				true
			},
			{
				"Left Little Distal",
				"L Finger42",
				true
			},
			{
				"Right Thumb Proximal",
				"R Finger0",
				true
			},
			{
				"Right Thumb Intermediate",
				"R Finger01",
				true
			},
			{
				"Right Thumb Distal",
				"R Finger02",
				true
			},
			{
				"Right Index Proximal",
				"R Finger1",
				true
			},
			{
				"Right Index Intermediate",
				"R Finger11",
				true
			},
			{
				"Right Index Distal",
				"R Finger12",
				true
			},
			{
				"Right Middle Proximal",
				"R Finger2",
				true
			},
			{
				"Right Middle Intermediate",
				"R Finger21",
				true
			},
			{
				"Right Middle Distal",
				"R Finger22",
				true
			},
			{
				"Right Ring Proximal",
				"R Finger3",
				true
			},
			{
				"Right Ring Intermediate",
				"R Finger31",
				true
			},
			{
				"Right Ring Distal",
				"R Finger32",
				true
			},
			{
				"Right Little Proximal",
				"R Finger4",
				true
			},
			{
				"Right Little Intermediate",
				"R Finger41",
				true
			},
			{
				"Right Little Distal",
				"R Finger42",
				true
			}
		};

		// Token: 0x0400012F RID: 303
		private static readonly BoneRelationshipList MixamoBoneNames = new BoneRelationshipList
		{
			{
				"Head",
				"Head",
				false
			},
			{
				"Neck",
				"Neck",
				true
			},
			{
				"Chest",
				"Spine1",
				true
			},
			{
				"UpperChest",
				"Spine2",
				true
			},
			{
				"Spine",
				"Spine",
				false
			},
			{
				"Hips",
				"Hips",
				false
			},
			{
				"LeftShoulder",
				"LeftShoulder",
				true
			},
			{
				"LeftUpperArm",
				"LeftArm",
				false
			},
			{
				"LeftLowerArm",
				"LeftForeArm",
				false
			},
			{
				"LeftHand",
				"LeftHand",
				false
			},
			{
				"RightShoulder",
				"RightShoulder",
				true
			},
			{
				"RightUpperArm",
				"RightArm",
				false
			},
			{
				"RightLowerArm",
				"RightForeArm",
				false
			},
			{
				"RightHand",
				"RightHand",
				false
			},
			{
				"LeftUpperLeg",
				"LeftUpLeg",
				false
			},
			{
				"LeftLowerLeg",
				"LeftLeg",
				false
			},
			{
				"LeftFoot",
				"LeftFoot",
				false
			},
			{
				"LeftToes",
				"LeftToeBase",
				true
			},
			{
				"RightUpperLeg",
				"RightUpLeg",
				false
			},
			{
				"RightLowerLeg",
				"RightLeg",
				false
			},
			{
				"RightFoot",
				"RightFoot",
				false
			},
			{
				"RightToes",
				"RightToeBase",
				true
			},
			{
				"Left Thumb Proximal",
				"LeftHandThumb1",
				true
			},
			{
				"Left Thumb Intermediate",
				"LeftHandThumb2",
				true
			},
			{
				"Left Thumb Distal",
				"LeftHandThumb3",
				true
			},
			{
				"Left Index Proximal",
				"LeftHandIndex1",
				true
			},
			{
				"Left Index Intermediate",
				"LeftHandIndex2",
				true
			},
			{
				"Left Index Distal",
				"LeftHandIndex3",
				true
			},
			{
				"Left Middle Proximal",
				"LeftHandMiddle1",
				true
			},
			{
				"Left Middle Intermediate",
				"LeftHandMiddle2",
				true
			},
			{
				"Left Middle Distal",
				"LeftHandMiddle3",
				true
			},
			{
				"Left Ring Proximal",
				"LeftHandRing1",
				true
			},
			{
				"Left Ring Intermediate",
				"LeftHandRing2",
				true
			},
			{
				"Left Ring Distal",
				"LeftHandRing3",
				true
			},
			{
				"Left Little Proximal",
				"LeftHandPinky1",
				true
			},
			{
				"Left Little Intermediate",
				"LeftHandPinky2",
				true
			},
			{
				"Left Little Distal",
				"LeftHandPinky3",
				true
			},
			{
				"Right Thumb Proximal",
				"RightHandThumb1",
				true
			},
			{
				"Right Thumb Intermediate",
				"RightHandThumb2",
				true
			},
			{
				"Right Thumb Distal",
				"RightHandThumb3",
				true
			},
			{
				"Right Index Proximal",
				"RightHandIndex1",
				true
			},
			{
				"Right Index Intermediate",
				"RightHandIndex2",
				true
			},
			{
				"Right Index Distal",
				"RightHandIndex3",
				true
			},
			{
				"Right Middle Proximal",
				"RightHandMiddle1",
				true
			},
			{
				"Right Middle Intermediate",
				"RightHandMiddle2",
				true
			},
			{
				"Right Middle Distal",
				"RightHandMiddle3",
				true
			},
			{
				"Right Ring Proximal",
				"RightHandRing1",
				true
			},
			{
				"Right Ring Intermediate",
				"RightHandRing2",
				true
			},
			{
				"Right Ring Distal",
				"RightHandRing3",
				true
			},
			{
				"Right Little Proximal",
				"RightHandPinky1",
				true
			},
			{
				"Right Little Intermediate",
				"RightHandPinky2",
				true
			},
			{
				"Right Little Distal",
				"RightHandPinky3",
				true
			}
		};

		// Token: 0x04000130 RID: 304
		private AssetLoaderOptions _loaderOptions;
	}
}
