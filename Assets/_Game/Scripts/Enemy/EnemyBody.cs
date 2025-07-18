using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace BitsProject
{
	public class EnemyBody : MonoBehaviour
	{
		public UnityEvent OnStack;
		[SerializeField] Collider bodyCollider;
		[SerializeField] Collider hipsCollider;
		[SerializeField] float dieCooldown;
		[SerializeField] Material[] outLineMaterials;
		[SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
		Material[] originalMaterials;
		private void Start()
		{
			originalMaterials = skinnedMeshRenderer.materials;
		}
		public void StackEnemyBody(Transform newPos)
		{
			OnStack.Invoke();
			bodyCollider.enabled = false;
			hipsCollider.enabled = false;
			GameObject parent = transform.parent.gameObject;
			parent.transform.parent = newPos;
			transform.localPosition = Vector3.zero;
			skinnedMeshRenderer.materials = originalMaterials;
		}
		public void ActiveCollider()
		{
			StartCoroutine(IActiveCollider());
		}
		IEnumerator IActiveCollider()
		{
			yield return new WaitForSeconds(dieCooldown);
			bodyCollider.enabled = true;
			skinnedMeshRenderer.materials = outLineMaterials;
		}
	}
}