using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
namespace BitsProject
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]Rigidbody m_Rigidbody;
		[SerializeField]Rigidbody bodyRigidbody;
        [SerializeField]NavMeshAgent m_NavMeshAgent;
        [SerializeField]float maxMoveTypeTimer;
        EnemyMoveType _moveType = EnemyMoveType.none;
        public EnemyMoveType moveType => _moveType;
        float currentMoveTimer;
        float moveTime;
        int pathIndex;
		public void KnockBack(HitInfo onHit) 
        {
            Vector3 direction = transform.position - onHit.hitPosition.position ;
            direction.y = 0;
            direction = direction.normalized;
			bodyRigidbody.AddForce(onHit.knockBackForce * direction, ForceMode.Impulse);
        }
        public void EnableRigidbody(bool enable)
        {
            m_Rigidbody.isKinematic = !enable;
            bodyRigidbody.isKinematic = enable;
        }
		public void DisableGravity() 
        {
			m_Rigidbody.isKinematic = true;
			bodyRigidbody.isKinematic = true;

            m_Rigidbody.useGravity = false;
            bodyRigidbody.useGravity = false;
		}
        
        public void UpdatePathing()
		{
            ChangeMoveType();
            if (_moveType == EnemyMoveType.move) m_NavMeshAgent.SetDestination(NavMeshPaths.Instance.GetPosition(pathIndex).position);
            if (_moveType == EnemyMoveType.wait) m_NavMeshAgent.SetDestination(transform.position);
            currentMoveTimer += Time.deltaTime;
		}
        public void ChangeMoveType()
        {
			switch (_moveType)
			{
				case EnemyMoveType.none:

					_moveType = (EnemyMoveType)Random.Range(0, 2);
                    if (_moveType == EnemyMoveType.wait) moveTime = 0.7f;
                    break;
                default:
                    if (currentMoveTimer > moveTime) 
                    {
						_moveType = EnemyMoveType.none;
                        currentMoveTimer = 0;
                        moveTime = Random.Range(maxMoveTypeTimer/2, maxMoveTypeTimer);
                        var newIndex = NavMeshPaths.Instance.GetRandomIndex();
                        if (newIndex != pathIndex) newIndex++;
                        pathIndex = newIndex;
                    }
                    break;
				

			}
		}
        public void DisableNavMesh() 
        {
            m_NavMeshAgent.enabled = false;
        }
        public void SetIndex(int newIndex) 
        {
            pathIndex = newIndex;
        }
	}
}