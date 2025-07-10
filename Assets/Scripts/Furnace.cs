using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{
    [SerializeField] private GoldOreManager goldOreManager;
    [SerializeField] private Image progressFillImage;

    public int craftTime = 5;
    public int stackSize = 5;
    private bool isCrafting = false;

    const string KEY_START_TIME = "Furnace_CraftStartTime";
    const string KEY_CRAFTED = "Furnace_Crafted";

    private void Start()
    {
        if (PlayerPrefs.HasKey(KEY_START_TIME))
            StartCoroutine(CraftCoroutine());
    }

    public void StartCrafting()
    {
        if (goldOreManager == null)
            goldOreManager = Object.FindFirstObjectByType<GoldOreManager>();
        if (goldOreManager == null || isCrafting) return;

        PlayerPrefs.SetFloat(KEY_START_TIME, GetTime());
        PlayerPrefs.SetInt(KEY_CRAFTED, 0);
        PlayerPrefs.Save();
        StartCoroutine(CraftCoroutine());
    }

    private System.Collections.IEnumerator CraftCoroutine()
    {
        isCrafting = true;
        int crafted = PlayerPrefs.GetInt(KEY_CRAFTED, 0);
        float startTime = PlayerPrefs.GetFloat(KEY_START_TIME, GetTime());
        float elapsedTotal = GetTime() - startTime;

        while (crafted < stackSize && goldOreManager.GetGoldOre() > 0)
        {
            float timer = 0f;
            if (crafted == 0 && elapsedTotal > 0f)
                timer = Mathf.Min(elapsedTotal, craftTime);

            while (timer < craftTime)
            {
                timer += Time.deltaTime;
                float fill = Mathf.Clamp01(timer / craftTime);
                progressFillImage.fillAmount = fill;
                yield return null;
            }
            progressFillImage.fillAmount = 1f;

            if (goldOreManager.SpendGoldOre(1))
            {
                goldOreManager.AddGoldIngot(1);
                crafted++;
                goldOreManager.UpdateGoldIngotUI();

                PlayerPrefs.SetInt(KEY_CRAFTED, crafted);
                PlayerPrefs.SetFloat(KEY_START_TIME, GetTime());
                PlayerPrefs.Save();
            }
            else
                break;
        }

        progressFillImage.fillAmount = 0f;
        isCrafting = false;
        PlayerPrefs.DeleteKey(KEY_START_TIME);
        PlayerPrefs.DeleteKey(KEY_CRAFTED);
        PlayerPrefs.Save();
    }

    // Oyun zamaný deðil, gerçek zaman kullansýn diye
    private float GetTime()
    {
        // return (float)System.DateTime.Now.Subtract(System.DateTime.UnixEpoch).TotalSeconds; // Internet saatiyle daha güvenli, ama local cihaz yeterli
        return Time.realtimeSinceStartup;
    }
}
