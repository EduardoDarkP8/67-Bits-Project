using UnityEngine;
using UnityEngine.Events;

public class PlayerCurrency : MonoBehaviour
{
    int _money = 0;
    int _level = 1;
    public int money => _money;
    public int level => _level;
    public UnityEvent<int> OnUpdateMoney;
	public UnityEvent<int> OnUpdateLevel;

	private void Start()
	{
        if (OnUpdateLevel == null) OnUpdateLevel = new();
		if (OnUpdateMoney == null) OnUpdateMoney = new();

		OnUpdateMoney?.Invoke(_money);
		OnUpdateLevel?.Invoke(_level);
	}
	public void AddMoney(int delta) 
    {
        _money += delta;
        OnUpdateMoney?.Invoke(_money);
	}
    public void AddLevel(int delta) 
    {
        _level += delta;
		OnUpdateLevel?.Invoke(_level);
    }


}
