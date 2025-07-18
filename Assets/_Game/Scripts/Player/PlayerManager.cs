using BitsProject;
using UnityEngine;
namespace BitsProject
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] PlayerMovement _playerMovement;
        [SerializeField] PlayerAnimation _playerAnimation;
        [SerializeField] PlayerCombat _playerCombat;
        [SerializeField] PlayerColor _playerColor;
        [SerializeField] PlayerBodyStacks _playerBodyStacks;
        [SerializeField] PlayerCurrency _playerCurrency;
		public PlayerMovement playerMovement => _playerMovement;
		public PlayerAnimation playerAnimation => _playerAnimation;
		public PlayerCombat playerCombat => _playerCombat;
        public PlayerColor playerColor => _playerColor;
		public PlayerBodyStacks playerBodyStacks => _playerBodyStacks;
		public PlayerCurrency playerCurrency => _playerCurrency;
		Vector2 direction;
        void Start()
        {
            _playerCombat.SetAttackAnimation(_playerAnimation.SetPunch);
        }
		private void OnEnable()
		{
            _playerCurrency.OnUpdateLevel.AddListener(_playerBodyStacks.UpdateMaxStacks);
			_playerCurrency.OnUpdateLevel.AddListener(_playerColor.UpdateColor);
		}
		private void OnDisable()
		{
            _playerCurrency.OnUpdateLevel.AddListener(_playerBodyStacks.UpdateMaxStacks);
			_playerCurrency.OnUpdateLevel.RemoveListener(_playerColor.UpdateColor);
		}
		private void Update()
        {
            direction = InputManager.Instance != null ? InputManager.Instance.direction : Vector2.zero;
            _playerAnimation.Move(direction);
        }
        void FixedUpdate()
        {
            _playerMovement.Move(direction);
        }
    }
    
}