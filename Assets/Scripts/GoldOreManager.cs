using UnityEngine;

public class GoldOreManager : MonoBehaviour
{
    [SerializeField] private GoldOreData goldOreData;
    private const string MoneyKey = "Money";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        int saved = PlayerPrefs.GetInt(MoneyKey, 0);
        goldOreData.SetValue(saved);
    }

    public void Add(int amount)
    {
        goldOreData.Add(amount);
        Save();
    }
    public bool Spend(int amount)
    {
        bool result = goldOreData.Spend(amount);
        if (result) Save();
        return result;
    }

    public int Get()
    {
        return goldOreData.Value;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(MoneyKey, goldOreData.Value);
        PlayerPrefs.Save();
    }
}
