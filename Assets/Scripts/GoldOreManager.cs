using TMPro;
using UnityEngine;

public class GoldOreManager : MonoBehaviour
{
    [SerializeField] private GoldOreData goldOreData;
    private const string GoldOreKey = "GoldOre";
    private const string GoldIngotKey = "GoldIngot";
    private const string MoneyKey = "Money";
    [SerializeField] private TextMeshProUGUI goldOreUI;
    [SerializeField] private TextMeshProUGUI goldIngotUI;

    private void Awake()
    {
        int saved = PlayerPrefs.GetInt(GoldOreKey, 0);
        goldOreData.SetGoldOre(saved);
        goldOreData.SetGoldIngot(PlayerPrefs.GetInt(GoldIngotKey, 0));
        goldOreData.SetMoney(PlayerPrefs.GetInt(MoneyKey, 0));
        UpdateGoldOreUI();
        UpdateGoldIngotUI();
    }

    public void UpdateGoldOreUI()
    {
        if (goldOreUI == null) return; 
        goldOreUI.text = goldOreData.goldOres.ToString();
    }

    public void UpdateGoldIngotUI()
    {
        if (goldIngotUI != null)
        {
            goldIngotUI.text = goldOreData.goldIngot.ToString();
        }
    }

    public void AddGoldOre(int amount)
    {
        goldOreData.AddGoldOre(amount);
        SaveGoldOre();
        UpdateGoldOreUI();
    }
    public bool SpendGoldOre(int amount)
    {
        bool result = goldOreData.SpendGoldOre(amount); 
        if (result) SaveGoldOre();
        UpdateGoldOreUI();
        return result;
    }


    public int GetGoldOre()
    {
        UpdateGoldOreUI();
        return goldOreData.goldOres;
    }

    private void SaveGoldOre()
    {
        PlayerPrefs.SetInt(GoldOreKey, goldOreData.goldOres);
        PlayerPrefs.Save();
        UpdateGoldOreUI();
    }

    public void AddGoldIngot(int amount)
    {
        goldOreData.AddGoldIngot(amount);
        SaveGoldIngot();
        UpdateGoldIngotUI();
    }

    public bool SpendGoldIngot(int amount)
    {
        bool result = goldOreData.SpendGoldIngot(amount);
        if (result) SaveGoldIngot();
        UpdateGoldIngotUI();
        return result;
    }


    private void SaveGoldIngot()
    {
        PlayerPrefs.SetInt(GoldIngotKey, goldOreData.goldIngot);
        PlayerPrefs.Save();
        UpdateGoldIngotUI();
    }

    public void AddMoney(int amount)
    {
        goldOreData.AddMoney(amount);
        SaveMoney();
    }

    public bool SpendMoney(int amount)
    {
        bool result = goldOreData.SpendMoney(amount);
        if (result) SaveMoney();
        return result;
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MoneyKey, goldOreData.Money);
        PlayerPrefs.Save();
        UpdateGoldOreUI();
    }
}
