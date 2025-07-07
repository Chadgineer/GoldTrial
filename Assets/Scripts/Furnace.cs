using UnityEngine;
using UnityEngine.UIElements;

public class Furnace : MonoBehaviour
{

    bool isPurchased;
    public int level = 0;
    public float craftTime = 5;
    float craftTimeHold;
    public GameObject timerPanel;

    private void Awake()
    {
        craftTimeHold = craftTime;
    }
    public bool IsPurchased
    {
        get { return isPurchased; }
        set
        {
            isPurchased = value;
            notPurchasedPanel.SetActive(!isPurchased);
        }
    }
    public GameObject notPurchasedPanel;
    public void CraftGold()
    {

    }
}
