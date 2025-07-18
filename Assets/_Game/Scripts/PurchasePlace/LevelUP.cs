using UnityEngine;
namespace BitsProject
{
    public class LevelUP : PurchasePlaceBase
    {
		[SerializeField] int cost;
		public override void UpdateText()
		{
			infoText.text = $"Level UP\r\n(-{cost})";
		}
		public override void Purchase()
		{
			if (currentPlayer != null) { currentPlayer.playerCurrency.AddMoney(-cost);  currentPlayer.playerCurrency.AddLevel(1); }
		}
		public override bool VerifyRequisit()
		{
			if (currentPlayer == null) return false;
			else
			{
				return currentPlayer.playerCurrency.money >= cost;
			}
		}
		
	}
}