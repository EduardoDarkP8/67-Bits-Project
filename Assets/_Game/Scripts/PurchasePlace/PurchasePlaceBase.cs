using BitsProject;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
namespace BitsProject
{
	public abstract class PurchasePlaceBase : MonoBehaviour
	{
		protected PlayerManager currentPlayer;
		public UnityEvent<PlayerManager> OnPurchase;
		[SerializeField] protected float purchaseTime;
		[SerializeField] protected Transform loadingMesh;
		protected Vector3 loadingStartPos;
		protected float currentTime;
		[SerializeField] protected TMP_Text infoText;
		protected virtual void Start() 
		{
			loadingStartPos = loadingMesh.localPosition;
			UpdateText();
		}
		public virtual void Purchase()
		{

		}
		public virtual void UpdateText()
		{

		}
		public virtual bool VerifyRequisit()
		{
			return true;
		}
		public virtual void UpdatePurchase()
		{
			if (currentPlayer != null)
			{
				UpdateTimer(currentTime + Time.deltaTime);
				if (currentTime > purchaseTime)
				{
					if (VerifyRequisit())
					{
						OnPurchase.Invoke(currentPlayer);
						Purchase();
					}
					UpdateTimer(0);
				}
			}
			else UpdateTimer(0);
		}

		protected virtual void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out PlayerManager playerManager))
			{
				currentPlayer = playerManager;
			}
		}
		protected virtual void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out PlayerManager playerManager))
			{
				currentPlayer = null;
			}
		}
		protected virtual void Update()
		{
			UpdatePurchase();
		}

		protected virtual void UpdateTimer(float time)
		{
			currentTime = time;
			float delta = Mathf.Clamp01(currentTime / purchaseTime);
			var positionDelta = loadingStartPos * delta;
			loadingMesh.localScale = new Vector3(loadingMesh.localScale.x, loadingMesh.localScale.y, 1 * delta);
			loadingMesh.localPosition = positionDelta - loadingStartPos;

		}

	}
}