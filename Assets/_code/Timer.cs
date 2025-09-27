using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vopere.Common;

namespace Vopere
{
    public class Timer : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] TextMeshProUGUI dayText;
        [SerializeField] TextMeshProUGUI timerText;

        [Header("Project")]
        [SerializeField] TMP_Dropdown dropdown;
        [SerializeField] TMP_InputField projectCreationInputField;
        [SerializeField] Button projectCreationButton;

        [Header("List")]
        [SerializeField] GameObject dateAndTimeTextsContainer;
        [SerializeField] GameObject dateAndTimeTextPrefab;

        float totalSeconds;
        bool isWorking = false;

        DataSaveLoad dataSaver;

        [Header("Info")]
        public string currentProject;
        public List<string> savedProjects = new List<string>();

        void Start()
        {
            dataSaver = DataSaveLoad.Instance;

            CheckForDropdownChanged();

            foreach (Transform t in dateAndTimeTextsContainer.transform)
                Destroy(t.gameObject);

            CreateDropdownProjectsTexts();
            CreateDateAndTimeTexts();
        }

        void Update()
        {
            currentProject = dropdown.options[dropdown.value].text;

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
                dataSaver.Save(FormatDay() + "_" + currentProject, GetTotalSeconds());
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
                    for (int d = 0; d < 31; d++)
                    {
                        for (int p = 0; p < savedProjects.Count; p++)
                        {
                            if (dataSaver.GetSavedFloat($"{d:D2}.{m:D2}.{y:D2}" + "_" + savedProjects[p]) != -1)
                                CreateSingleDateAndTimeText($"{d:D2}.{m:D2}.{y:D2}" + "_" + savedProjects[p]);
                        }
                    }
                }
            }
        }

        void CreateSingleDateAndTimeText(string data)
        {
            GameObject go = Instantiate(dateAndTimeTextPrefab, dateAndTimeTextsContainer.transform);
            var datText = go.GetComponent<UIDateAndTimeText>();
            datText.Init(data, FormatTime(dataSaver.GetSavedFloat(data)));
        }

        void CreateDropdownProjectsTexts()
        {
            dropdown.options.Clear();
            savedProjects.Clear();

            if (projectCreationInputField.text == "")
                projectCreationButton.interactable = false;
            else
                projectCreationButton.interactable = true;

            for (int i = 0; i < 1000; i++)
            {
                if (dataSaver.GetSavedString("Project" + i.ToString()) != "")
                {
                    TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                    optionData.text = dataSaver.GetSavedString("Project" + i.ToString());
                    dropdown.options.Add(optionData);
                    savedProjects.Add(dataSaver.GetSavedString("Project" + i.ToString()));
                }
            }
        }

        public void CreateProjectInfo()
        {
            for (int i = 0; i < 1000; i++)
            {
                if (dataSaver.GetSavedString("Project" + i.ToString()) != "")
                    savedProjects.Add(dataSaver.GetSavedString("Project" + i.ToString()));
                else
                {
                    dataSaver.Save("Project" + i.ToString(), projectCreationInputField.text);
                    break;
                }
            }

            CreateDropdownProjectsTexts();
            CheckForSavedProject();
        }

        public void CheckForSavedProject()
        {
            for (int i = 0; i < 1000; i++)
            {
                if (dataSaver.GetSavedString("Project" + i.ToString()) == projectCreationInputField.text)
                {
                    projectCreationButton.interactable = false;
                    break;
                }
                else
                    projectCreationButton.interactable = true;
            }
        }

        public void CheckForDropdownChanged()
        {
            currentProject = dropdown.options[dropdown.value].text;

            if (PlayerPrefs.HasKey(FormatDay() + "_" + currentProject))
                totalSeconds = dataSaver.GetSavedFloat(FormatDay() + "_" + currentProject);
            else
                totalSeconds = 0;

            dayText.text = FormatDay();
            timerText.text = FormatTime(totalSeconds);
        }
    }
}