using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vopere.Common;

namespace Vopere
{
    public class UIDateAndTimeText : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] TextMeshProUGUI dateText;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] TextMeshProUGUI projectText;

        [Header("Background")]
        [SerializeField] Image background;
        [SerializeField] float backgroundAlpha;

        DataSaveLoad dataSaver;

        int currentColorSchemeId;

        public void Init(string INdateText, string INtimeText)
        {
            dataSaver = DataSaveLoad.Instance;

            string[] words = INdateText.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            dateText.text = words[0];
            projectText.text = words[1];

            timeText.text = INtimeText;

            if (PlayerPrefs.HasKey(words[1] + "_ColorScheme"))
            {
                currentColorSchemeId = dataSaver.GetSavedInt(words[1] + "_ColorScheme");
                background.color = Timer.Instance.colors[currentColorSchemeId];
                background.color = new Color(background.color.r, background.color.g, background.color.b, backgroundAlpha);
            }
            else
                background.color = new Color(backgroundAlpha, backgroundAlpha, backgroundAlpha, backgroundAlpha);
        }
    }
}
