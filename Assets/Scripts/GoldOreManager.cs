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
    [SerializeField] private TextMeshProUGUI moneyUI;

    private void Awake()
    {
        int saved = PlayerPrefs.GetInt(GoldOreKey, 0);
        goldOreData.SetGoldOre(saved);
        goldOreData.SetGoldIngot(PlayerPrefs.GetInt(GoldIngotKey, 0));
        goldOreData.SetMoney(PlayerPrefs.GetInt(MoneyKey, 0));
        UpdateGoldOreUI();
        UpdateGoldIngotUI();
        UpdateMoneyUI();
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
    public void UpdateMoneyUI()
    {
        if (moneyUI == null) return;
        moneyUI.text = goldOreData.Money.ToString();
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

    public void SellGolds250()
    {
        if (goldOreData.goldIngot > 0)
        {
            SpendGoldIngot(1);
            AddMoney(100);
            UpdateGoldIngotUI();
            UpdateMoneyUI();
        }
    }

    public void SellGolds500()
    {
        if (goldOreData.goldIngot > 1)
        {
            SpendGoldIngot(2);
            AddMoney(225);
            UpdateGoldIngotUI();
            UpdateMoneyUI();
        }
    }

    public void SellGolds750()
    {
        if (goldOreData.goldIngot > 2)
        {
            SpendGoldIngot(3);
            AddMoney(350);
            UpdateGoldIngotUI();
            UpdateMoneyUI();
        }
    }

    public void SellGolds1000()
    {
        if (goldOreData.goldIngot > 0)
        {
            SpendGoldIngot(4);
            AddMoney(500);
            UpdateGoldIngotUI();
            UpdateMoneyUI();
        }
    }


    public void Gold1button()
    {
        goldOreData.AddGoldOre(1);
        UpdateGoldOreUI();
    }

    public void Gold2button()
    {
        goldOreData.AddGoldOre(2);
        UpdateGoldOreUI();
    }


    public void BombButton()
    {
        goldOreData.AddGoldOre(-5);
        UpdateGoldOreUI();
    }
}
