using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] GameObject dateAndTimeTextsContainer;
    [SerializeField] GameObject dateAndTimeTextPrefab;

    float totalSeconds;
    bool isWorking = false;

    DataSaveLoad dataSaver;

    void Start()
    {
        dataSaver = DataSaveLoad.Instance;
        dayText.text = FormatDay();

        if (dataSaver.GetSavedFloat(FormatDay()) != -1)
            totalSeconds = dataSaver.GetSavedFloat(FormatDay());
        else
            totalSeconds = 0;
        
        timerText.text = FormatTime(totalSeconds);

        foreach (Transform t in dateAndTimeTextsContainer.transform)
            Destroy(t.gameObject);

        CreateDateAndTimeTexts();
    }

    void Update()
    {
        if (!isWorking)
            return;

        totalSeconds += Time.deltaTime;

        if (timerText)
            timerText.text = FormatTime(totalSeconds);
    }

    string FormatTime(float seconds)
    {
        int hours = Mathf.FloorToInt(seconds / 3600);
        int minutes = Mathf.FloorToInt((seconds % 3600) / 60);
        int secs = Mathf.FloorToInt(seconds % 60);

        return $"{hours:D2}:{minutes:D2}:{secs:D2}";
    }

    string FormatDay()
    {
        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;
        int year = DateTime.Now.Year;

        return $"{day:D2}.{month:D2}.{year:D4}";
    }

    public void ActivateTimer(bool value)
    {
        isWorking = value;

        if (value == false)
        {
            dataSaver.Save(FormatDay(), GetTotalSeconds());
        }
    }

    float GetTotalSeconds()
    {
        return totalSeconds;
    }

    public void ClearTimer()
    {
        totalSeconds = 0;

        if (timerText)
            timerText.text = FormatTime(totalSeconds);
    }

    public void CreateDateAndTimeTexts()
    {
        for (int y = 2025; y < 2125; y++)
        {
            for (int m = 0; m < 12; m++)
            {
                for (int d = 0; d < 30; d++)
                {
                    if (dataSaver.GetSavedFloat($"{d:D2}.{m:D2}.{y:D2}") != -1)
                    {
                        CreateSingleDateAndTimeText($"{d:D2}.{m:D2}.{y:D2}");
                    }
                }
            }
        }
    }

    void CreateSingleDateAndTimeText(string dateFormat)
    {
        GameObject go = Instantiate(dateAndTimeTextPrefab, dateAndTimeTextsContainer.transform);
        var datText = go.GetComponent<UIDateAndTimeText>();
        datText.Init(dateFormat + " - " + FormatTime(dataSaver.GetSavedFloat(dateFormat)));
    }
}