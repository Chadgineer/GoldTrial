using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] private int countdownTime = 60;
    [SerializeField] private TextMeshProUGUI timeLeft;
    [SerializeField] private Button buttonToClick;

    private int currentTime;
    private Coroutine countdownRoutine;
    private float lastInputTime;
    private bool countdownActive;
    private bool buttonPressed;

    public void StartCountdown()
    {
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        buttonPressed = false;
        countdownActive = true;
        lastInputTime = Time.time;
        countdownRoutine = StartCoroutine(CountdownCoroutine());
    }

    void Update()
    {
        NoInputController();
    }

    private void NoInputController()
    {
        if (!countdownActive || buttonPressed) return;

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            lastInputTime = Time.time;
        }

        if (Time.time - lastInputTime >= 3f)
        {
            buttonPressed = true;
            if (buttonToClick != null)
                buttonToClick.onClick.Invoke();

            countdownActive = false; // input kontrol�n� bitir
        }
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

        // S�re bitti�inde butona otomatik bas (e�er input ile zaten bas�lmad�ysa)
        if (buttonToClick != null && !buttonPressed)
            buttonToClick.onClick.Invoke();

        countdownActive = false;
    }
}
