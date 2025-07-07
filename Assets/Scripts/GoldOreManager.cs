using TMPro;
using UnityEngine;

public class GoldOreManager : MonoBehaviour
{
    [SerializeField] private GoldOreData goldOreData;
    private const string GoldOreKey = "Money";
    [SerializeField] private TextMeshProUGUI goldOreUI;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        int saved = PlayerPrefs.GetInt(GoldOreKey, 0);
        goldOreData.SetGoldOre(saved);
        UpdateGoldOreUI();
    }

    public void UpdateGoldOreUI()
    { 
        goldOreUI.text = goldOreData.goldOres.ToString();
    }

    public void AddGoldOre(int amount)
    {
        goldOreData.AddGoldOre(amount);
        SaveGoldOre();
        UpdateGoldOreUI();
    }
    public bool SpendGoldOre(int amount)
    {
        bool result = goldOreData.SpendGoldOre(amount); // Updated method call
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
}
