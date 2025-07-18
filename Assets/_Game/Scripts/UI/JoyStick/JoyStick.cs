using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace BitsProject 
{ 
	public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		public UnityAction<Vector2> OnDirectionChange;
		private Vector2 _direction;

		public Vector2 Direction   
		{
			get => _direction;
			set
			{
				_direction = value;              
				OnDirectionChange?.Invoke(value); 
			}
		}
		[SerializeField]RectTransform joyStickContainer;
		[SerializeField]RectTransform joyStick;
		[SerializeField]Canvas canvas;
		Vector2 anchoredStartPosition;
		Vector2 anchoredCurrentPosition;
		private void Start()
		{
			joyStickContainer.gameObject.SetActive(false);
			InputManager.Instance?.AddJoyStick(this);
		}

		public void OnDrag(PointerEventData eventData)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				joyStickContainer,
				eventData.position,
				canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
				out anchoredCurrentPosition
			);

			Vector2 normalized = anchoredCurrentPosition / (joyStickContainer.sizeDelta / 2f);

			normalized = Vector2.ClampMagnitude(normalized, 1f);

			joyStick.anchoredPosition = normalized * (joyStickContainer.sizeDelta / 2f);


			Direction = normalized;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			joyStickContainer.gameObject.SetActive(true);
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				joyStickContainer.parent as RectTransform,                         
				eventData.pressPosition,
				canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
				out anchoredStartPosition
			);
			joyStickContainer.anchoredPosition = anchoredStartPosition;
			joyStick.anchoredPosition = Vector2.zero;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			Direction = Vector2.zero;
			joyStickContainer.gameObject.SetActive(false);
		}
	}
}
