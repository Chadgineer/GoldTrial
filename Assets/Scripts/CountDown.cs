using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI; // Button için gerekli

public class CountDown : MonoBehaviour
{
    [SerializeField] private int countdownTime = 60;
    [SerializeField] private TextMeshProUGUI timeLeft;
    [SerializeField] private Button buttonToClick; // Inspector’dan atanacak

    private int currentTime;
    private Coroutine countdownRoutine;

    public void StartCountdown()
    {
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);
        countdownRoutine = StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        currentTime = countdownTime;
        while (currentTime > 0)
        {
            timeLeft.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        timeLeft.text = "0";

        // Otomatik butona bas
        if (buttonToClick != null)
            buttonToClick.onClick.Invoke();
    }
}
