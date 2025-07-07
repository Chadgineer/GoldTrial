using UnityEngine;
using UnityEngine.UIElements;

public class Furnace : MonoBehaviour
{
    [SerializeField] private GoldOreManager goldOreManager;

    bool isPurchased; 
    public int level = 0;
    public int craftTime = 5;
    public int stackSize = 5;
    float craftTimeHold;

    private void Awake()
    {
        craftTimeHold = craftTime;
    }
    public void StartCrafting()
    {
        goldOreManager.UpdateGoldIngotUI();
        if (goldOreManager == null)
            goldOreManager = Object.FindFirstObjectByType<GoldOreManager>();

        if (goldOreManager == null)
        {
            return;
        }

        if (!isCrafting)
        {
            StartCoroutine(CraftCoroutine());
            Debug.Log("Crafting started. Level: " + level + ", Craft Time: " + craftTime + ", Stack Size: " + stackSize); 
        }
    }

    private bool isCrafting = false;

    private System.Collections.IEnumerator CraftCoroutine()
    {
        isCrafting = true;
        int crafted = 0;

        while (crafted < stackSize && goldOreManager.GetGoldOre() > 0)
        {
            yield return new WaitForSeconds(craftTime);

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

        isCrafting = false;
    }
    public GameObject notPurchasedPanel;
}
