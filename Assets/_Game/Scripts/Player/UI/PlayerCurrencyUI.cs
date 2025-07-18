using TMPro;
using UnityEngine;

public class PlayerCurrencyUI : MonoBehaviour
{

    [SerializeField] TMP_Text bodiesText;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text levelText;
    float bodiesMax;
    float currentBodies;

    public void UpdateCurrentBodies(int currentBodies) 
    {
        this.currentBodies = currentBodies;
		bodiesText.text = $"{bodiesMax}/{this.currentBodies}";
    }
    public void UpdateMaxBodies(int bodiesMax) 
    {
		this.bodiesMax = bodiesMax;
		bodiesText.text = $"{this.bodiesMax}/{currentBodies}";
	}
    public void MoneyText(int money) 
    {
        moneyText.text = $"{money}$";
    }
	public void LevelText(int level)
	{
		levelText.text = $"Level: {level}";
	}
}
