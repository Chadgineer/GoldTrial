using UnityEngine;

[CreateAssetMenu(menuName = "Game/MoneyData")]
public class GoldOreData : ScriptableObject
{
    [SerializeField] private int goldOreValue;
    [SerializeField] private int goldIngotValue;
    [SerializeField] private int moneyValue;

    public int goldOres => goldOreValue;
    public int goldIngot => goldIngotValue;
    public int Money => moneyValue;

    public void SetGoldOre(int val)
    {
        goldOreValue = val;
    }

    public void AddGoldOre(int amount)
    {
        goldOreValue += amount;
    }

    public bool SpendGoldOre(int amount)
    {
        if (goldOreValue < amount) return false;
        goldOreValue -= amount;
        return true;
    }
    public void SetGoldIngot(int val)
    {
        goldIngotValue = val;
    }

    public void AddGoldIngot(int amount)
    {
        goldIngotValue += amount;
    }

    public bool SpendGoldIngot(int amount)
    {
        if (goldIngotValue < amount) return false;
        goldIngotValue -= amount;
        return true;
    }

    public void SetMoney(int val)
    {
        moneyValue = val;
    }

    public void AddMoney(int amount)
    {
        moneyValue += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (moneyValue < amount) return false;
        moneyValue -= amount;
        return true;
    }
}
