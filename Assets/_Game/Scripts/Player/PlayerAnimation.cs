using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace BitsProject
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        const string MOVE = "move";
        const string PUNCH = "punch";
        bool punching = false;
		bool punch = true;
		[SerializeField] float fadeDuration;
		UnityAction onPunchStart; 
		UnityAction onPunch; 
		UnityAction onPunchEnd;
		public void PunchStart()
		{
			
			onPunchStart?.Invoke();
			punching = true;
		}
		public void Punch() 
		{
			onPunch?.Invoke();
			StartCoroutine(IPunchFade());
		}
		public void PunchEnd()
		{
			onPunchEnd?.Invoke();
			punching = false;
			_animator.SetBool(PUNCH, false);
		}
		public void Move(Vector2 direction)
        {
            float moveValue = direction.sqrMagnitude;
            _animator.SetFloat(MOVE, moveValue);

        }
        public void SetPunch(UnityAction onPunchStart,	UnityAction onPunch, UnityAction onPunchEnd)
        {
            if (!punching) 
			{
				_animator.SetLayerWeight(1, 1);
				this.onPunchStart = onPunchStart;
				this.onPunch = onPunch;
				this.onPunchEnd = onPunchEnd;
				_animator.SetBool(PUNCH,true);
			}
        }


		IEnumerator IPunchFade()
		{
			float timer = 0;
			while (timer < fadeDuration)
			{
				timer += Time.deltaTime;
				float normalized = Mathf.Clamp01(timer / fadeDuration);
				_animator.SetLayerWeight(1, normalized);
				yield return null;
			}
			_animator.SetLayerWeight(1, 0);
			punching = false;
			_animator.SetBool(PUNCH, false);
		}
	}
}