using UnityEngine;
namespace BitsProject
{
    public class EnemyManager : MonoBehaviour
    {

		[SerializeField] EnemyAnimation _enemyAnimation;
		[SerializeField] EnemyCombat _enemyCombat;
        [SerializeField] EnemyMovement _enemyMovement;
        [SerializeField] EnemyBody _enemyBody;

		public EnemyAnimation enemyAnimation => _enemyAnimation;
		public EnemyCombat enemyCombat => _enemyCombat;
		public EnemyMovement enemyMovement => _enemyMovement;
		public EnemyBody enemyBody => _enemyBody;


		EnemyState enemyState;

		void Start()
        {
            _enemyCombat.SetOnHit(Die);
        }
		private void Update()
		{
			if(enemyState != EnemyState.dead) 
			{
				_enemyMovement.UpdatePathing();
				_enemyAnimation.SetMove((int)_enemyMovement.moveType);
			}
		}
		private void OnEnable()
		{
			_enemyBody.OnStack.AddListener(Stack);
		}
		private void OnDisable()
		{
			_enemyBody.OnStack.RemoveListener(Stack);
		}
		void Stack() 
		{
			_enemyCombat.EnableTrigger();
			_enemyMovement.DisableGravity();
		}
		void Die(HitInfo hit)
		{
			_enemyMovement.DisableNavMesh();
			_enemyMovement.EnableRigidbody(false);
			_enemyMovement.KnockBack(hit);
			_enemyAnimation.EnableAnimator(false);
			_enemyBody.ActiveCollider();
			EnemySpawner.Instance?.PopEnemie(this);
			enemyState = EnemyState.dead;
		}
	}
}