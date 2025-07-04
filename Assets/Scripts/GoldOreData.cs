using UnityEngine;

[CreateAssetMenu(menuName = "Game/MoneyData")]
public class GoldOreData : ScriptableObject
{
    [SerializeField] private int value;
    public int Value => value;

    public void SetValue(int val)
    {
        value = val;
    }

    public void Add(int amount)
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
