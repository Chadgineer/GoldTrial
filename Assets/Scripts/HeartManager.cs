using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class HeartManager : MonoBehaviour
{
    [Header("Can Ayarlarý")]
    public int maxHearts = 5;
    public float heartRegenMinutes = 15f;

    [Header("UI")]
    public TMP_Text heartsText;
    public TMP_Text timerText;

    [HideInInspector] public int currentHearts;
    private DateTime nextHeartTime;
    private bool timeLoaded = false;

    const string LastHeartKey = "LastHeartTime";
    const string HeartsKey = "CurrentHearts";
    const string TimeApiUrl = "https://worldtimeapi.org/api/timezone/Etc/UTC";

    void Start()
    {
        
        StartCoroutine(LoadTimeFromInternet());
        currentHearts--;
    }

    IEnumerator LoadTimeFromInternet()
    {
        UnityWebRequest www = UnityWebRequest.Get(TimeApiUrl);
        yield return www.SendWebRequest();

        DateTime serverTime;
        if (www.result != UnityWebRequest.Result.Success)
        {
            // Fallback: Local time (doðru deðil ama offline için geçici çözüm)
            serverTime = DateTime.UtcNow;
        }
        else
        {
            string json = www.downloadHandler.text;
            string utcDateTime = JsonUtility.FromJson<TimeApiResponse>(json).utc_datetime;
            serverTime = DateTime.Parse(utcDateTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }

        // Kayýtlý can sayýsý ve zamanýný al
        currentHearts = PlayerPrefs.GetInt(HeartsKey, maxHearts);
        string lastTimeStr = PlayerPrefs.GetString(LastHeartKey, "");
        DateTime lastHeartTime = lastTimeStr == "" ? serverTime : DateTime.Parse(lastTimeStr);

        // Ne kadar zaman geçmiþ, kaç can verilmeli?
        int extraHearts = Mathf.FloorToInt((float)(serverTime - lastHeartTime).TotalMinutes / heartRegenMinutes);
        if (extraHearts > 0)
        {
            currentHearts = Mathf.Min(maxHearts, currentHearts + extraHearts);
            lastHeartTime = lastHeartTime.AddMinutes(extraHearts * heartRegenMinutes);
        }

        nextHeartTime = lastHeartTime.AddMinutes(heartRegenMinutes);
        SaveState(serverTime);
        timeLoaded = true;
    }

    void Update()
    {
        if (!timeLoaded) return;

        heartsText.text = $"{currentHearts}/{maxHearts}";

        if (currentHearts < maxHearts)
        {
            TimeSpan timeLeft = nextHeartTime - DateTime.UtcNow;
            if (timeLeft <= TimeSpan.Zero)
            {
                currentHearts++;
                nextHeartTime = nextHeartTime.AddMinutes(heartRegenMinutes);
                SaveState(DateTime.UtcNow);
                timeLeft = nextHeartTime - DateTime.UtcNow;
            }
            timerText.text = $"{timeLeft.Minutes:D2}:{timeLeft.Seconds:D2}";
        }
        else
        {
            timerText.text = "FULL";
        }
    }

    public bool TryUseHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--;
            SaveState(DateTime.UtcNow);
            return true;
        }
        return false;
    }

    void SaveState(DateTime now)
    {
        PlayerPrefs.SetInt(HeartsKey, currentHearts);
        PlayerPrefs.SetString(LastHeartKey, now.ToString("o"));
        PlayerPrefs.Save();
    }

    [Serializable]
    private class TimeApiResponse
    {
        public string utc_datetime;
    }

    public void MinusHearth()
    {
        currentHearts--;
    }

}
