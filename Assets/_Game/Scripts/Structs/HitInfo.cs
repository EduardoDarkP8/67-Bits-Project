using System;
using UnityEngine;
namespace BitsProject
{
	[Serializable]
	public struct HitInfo
	{
		public Transform hitPosition;
		public float knockBackForce;
	}
}