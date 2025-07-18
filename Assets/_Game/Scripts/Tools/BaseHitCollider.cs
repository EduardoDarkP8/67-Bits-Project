using UnityEngine;
using UnityEngine.Events;
namespace BitsProject
{
    public abstract class BaseHitCollider : MonoBehaviour
    {
        public UnityEvent<IDamageable> onHit;
        protected IDamageable _currentTarget;
        public IDamageable currentTarget => _currentTarget;
        public virtual void OnHit(IDamageable damageable)
        {
            onHit.Invoke(damageable);
        }
        public virtual bool VerifyTarget() 
        {
            return _currentTarget != null;
        }
    }
}