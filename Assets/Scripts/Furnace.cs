using UnityEngine;
using UnityEngine.UI; // UI Image için

public class Furnace : MonoBehaviour
{
    [SerializeField] private GoldOreManager goldOreManager;
    [SerializeField] private Image progressFillImage; // Fill bar için Image referansý

    public int craftTime = 5;
    public int stackSize = 5;
    private bool isCrafting = false;

    private void Awake()
    {
        // Gerekirse init iþlemleri
    }

    public void StartCrafting()
    {
        if (goldOreManager == null)
            goldOreManager = Object.FindFirstObjectByType<GoldOreManager>();
        if (goldOreManager == null) return;
        if (!isCrafting)
            StartCoroutine(CraftCoroutine());
    }

    private System.Collections.IEnumerator CraftCoroutine()
    {
        isCrafting = true;
        int crafted = 0;

        while (crafted < stackSize && goldOreManager.GetGoldOre() > 0)
        {
            float timer = 0f;
            while (timer < craftTime)
            {
                timer += Time.deltaTime;
                float fill = Mathf.Clamp01(timer / craftTime); // 0 -> 1
                progressFillImage.fillAmount = fill; // 0% -> 100%
                yield return null;
            }

            progressFillImage.fillAmount = 1f;

            if (goldOreManager.SpendGoldOre(1))
            {
                goldOreManager.AddGoldIngot(1);
                crafted++;
                goldOreManager.UpdateGoldIngotUI();
            }
            else
            {
                break;
            }
        }

        progressFillImage.fillAmount = 0f;
        isCrafting = false;
    }
}
