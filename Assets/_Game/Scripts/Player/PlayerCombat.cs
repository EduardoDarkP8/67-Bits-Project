using UnityEngine;
using UnityEngine.Events;

namespace BitsProject
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField]BaseHitCollider hitCollider;

		UnityAction<UnityAction, UnityAction, UnityAction> punchAnimation;
        [SerializeField] HitInfo hitInfo;
		private void OnEnable()
		{
			hitCollider.onHit.AddListener(PunchAttack);
		}
		private void OnDisable()
		{
			hitCollider.onHit.RemoveListener(PunchAttack);
		}
        public void PunchAttack(IDamageable damageable)
        {

            punchAnimation.Invoke(() => { },
                () => {if(hitCollider.VerifyTarget()) damageable.Hit(hitInfo);}
                ,() => { if (hitCollider.VerifyTarget()) hitCollider.onHit.Invoke(hitCollider.currentTarget); });

        }
        public void SetAttackAnimation(UnityAction<UnityAction, UnityAction, UnityAction> punchAnimation) 
        {
            this.punchAnimation = punchAnimation;
        }

    }
}
