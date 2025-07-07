using UnityEngine;

[CreateAssetMenu(menuName = "Game/MoneyData")]
public class GoldOreData : ScriptableObject
{
    [SerializeField] private int value;
    public int goldOres => value;
    public int gold250;
    public int gold500;
    public int gold750;
    public int gold1000;

    public void SetGoldOre(int val)
    {
        value = val;
    }

    public void AddGoldOre(int amount)
    {
        value += amount;
    }

    public bool Spend(int amount)
    {
        if (value < amount) return false;
        value -= amount;
        return true;
    }
}
