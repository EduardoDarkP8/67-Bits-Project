using UnityEngine;
namespace BitsProject
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] Rigidbody _rigidbody;
		[SerializeField] float velocity;
		Vector3 camForward;
		Vector3 camRight;
		private void Start()
		{
			camForward = Camera.main.transform.forward;
			camRight = Camera.main.transform.right;
			camForward.y = 0f;
			camRight.y = 0f;
			camForward.Normalize();
			camRight.Normalize();
		}
		public void Move(Vector2 direction)
		{
			// Corrige input com base na câmera


			Vector3 moveDir = camForward * direction.y + camRight * direction.x;


			_rigidbody.linearVelocity = new Vector3(moveDir.x * velocity, _rigidbody.linearVelocity.y, moveDir.z * velocity);
			_rigidbody.angularVelocity = Vector3.zero;

			if (moveDir.sqrMagnitude > 0.001f)
			{
				float angle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0f, angle, 0f);
			}
		}

	}
}