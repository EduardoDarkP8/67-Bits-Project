using UnityEngine;
namespace BitsProject
{
	public class PlayerAttackHit : BaseHitCollider
	{
		
		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out IDamageable damageable)) 
			{
				OnHit(damageable);
				_currentTarget = damageable;	
			}
		}
		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out IDamageable damageable))
			{
				if(damageable == _currentTarget) 
				{ 
					_currentTarget = null;
				}
			}
		}
	}
}