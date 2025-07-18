
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace BitsProject
{
	public class PlayerBodyStacks : MonoBehaviour
	{
		List<EnemyBody> _stackedBodies = new List<EnemyBody>();
		public List<EnemyBody> stackedBodies => _stackedBodies;
		
		[SerializeField] Transform stackPos;
		[SerializeField] float bodiesDistance;
		[SerializeField] int stacksPerLevel;
		[SerializeField] int minStacks;
		int maxStacks;
		[SerializeField] int positionHistorySize = 100;
		[SerializeField] float followSpeed = 10f; 
		[SerializeField] int followSpacing = 5;
		[SerializeField] float smoothTime = 0.1f;
		List<Vector3> positionHistory = new();

		public UnityEvent<int> onMaxStacksUpdate;
		public UnityEvent<int> onCurretStacksUpdate;
		private void Start()
		{
			if(onMaxStacksUpdate == null)onMaxStacksUpdate = new();
			if (onCurretStacksUpdate == null)onCurretStacksUpdate = new();

		}
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out EnemyBody enemyBody))
			{
				if (stackedBodies.Count >= maxStacks) return;
				enemyBody.StackEnemyBody(stackPos);
				_stackedBodies.Add(enemyBody);
				enemyBody.transform.parent.localPosition = new Vector3(0, (_stackedBodies.Count - 1) * bodiesDistance, 0);
				onCurretStacksUpdate.Invoke(_stackedBodies.Count);
			}
		}
		public void UpdateMaxStacks(int level) 
		{
			maxStacks = minStacks + (level * stacksPerLevel);
			onMaxStacksUpdate.Invoke(maxStacks);
		}
		public void PopBody(float popAnimationTime, Transform targetPos, UnityAction OnPop) 
		{
			StartCoroutine(IPopBody(popAnimationTime, targetPos, OnPop));
		}
		IEnumerator IPopBody(float popAnimationTime, Transform targetPos,UnityAction OnPop) 
		{
			if(_stackedBodies.Count> 0) 
			{
				var body = _stackedBodies[_stackedBodies.Count - 1].transform.parent;
				
				_stackedBodies.RemoveAt(_stackedBodies.Count - 1);

				float timer = 0f;
				Vector3 startPos = body.transform.position;
				Quaternion startRot = body.transform.rotation;
				Vector3 targetPosition = targetPos.position;
				Quaternion targetRotation = targetPos.rotation;

				while (timer < popAnimationTime)
				{
					float delta = timer / popAnimationTime;
					body.transform.position = Vector3.Lerp(startPos, targetPosition, delta);
					body.transform.rotation = Quaternion.Slerp(startRot, targetRotation, delta);

					timer += Time.deltaTime;
					yield return null;
				}

				body.transform.position = targetPosition;
				body.transform.rotation = targetRotation;

				Destroy(body.gameObject);
				
				OnPop.Invoke();
				onCurretStacksUpdate.Invoke(_stackedBodies.Count);
			}
		}
		void LateUpdate()
		{
			UpdateHistory();
			UpdateStackedPositions();
		}

		void UpdateHistory()
		{
			positionHistory.Insert(0, stackPos.position);

			if (positionHistory.Count > positionHistorySize)
				positionHistory.RemoveAt(positionHistory.Count - 1);
		}

		void UpdateStackedPositions()
		{
			for (int i = 0; i < stackedBodies.Count; i++)
			{
				int historyIndex = Mathf.Clamp((i + 1) * followSpacing, 0, positionHistory.Count - 1);
				Vector3 basePos = positionHistory[historyIndex];

				Vector3 targetPos = basePos + Vector3.up * i * bodiesDistance;

				Transform body = stackedBodies[i].transform.parent;
				Vector3 velocity = Vector3.zero;
				body.position = Vector3.SmoothDamp(body.position, targetPos, ref velocity, smoothTime);
			}
		}

	}
}