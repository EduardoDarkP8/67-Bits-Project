using UnityEngine;
namespace BitsProject
{
    public class SellBodies : PurchasePlaceBase
	{
		[SerializeField] int gain;
		public override void UpdateText()
		{
			infoText.text = $"Sell Bodies\r\n(+{gain})";
		}
		public override void Purchase()
		{
			if (currentPlayer != null) 
			{
				var player = currentPlayer;
				player.playerBodyStacks.PopBody(purchaseTime, transform, () => player.playerCurrency.AddMoney(gain)); 
			}
		}
		public override bool VerifyRequisit()
		{
			if (currentPlayer == null) return false;
			else 
			{
				return currentPlayer.playerBodyStacks.stackedBodies.Count > 0; 
			}
		}
	}
}