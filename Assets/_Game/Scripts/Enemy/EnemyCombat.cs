using UnityEngine;
using UnityEngine.Events;
namespace BitsProject
{
    public class EnemyCombat : MonoBehaviour, IDamageable
    {
		[SerializeField] Collider mainCollider;
		[SerializeField] Collider[] rgdollCollider;
        [SerializeField] LayerMask playerIgnoreMask;
        UnityAction<HitInfo> onHit;
		public void Hit(HitInfo hitInfo)
        {
            ActiveRagDoll();
            onHit.Invoke(hitInfo);
        }
        void ActiveRagDoll() 
        {
            mainCollider.enabled = false;
            foreach (var collider in rgdollCollider) 
            {
                collider.enabled = true;
            }
        }
        public void SetOnHit(UnityAction<HitInfo> onHit) 
        {
            this.onHit = onHit;
        }
        public void EnableTrigger() 
        {
			foreach (var collider in rgdollCollider)
			{
                collider.isTrigger = true;
			}
		}
    }
}
