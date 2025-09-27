using System;
using TMPro;
using UnityEngine;

namespace Vopere
{
    public class UIDateAndTimeText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI dateText;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] TextMeshProUGUI projectText;

        public void Init(string INdateText, string INtimeText)
        {
            string[] words = INdateText.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            dateText.text = words[0];
            projectText.text = words[1];

            timeText.text = INtimeText;
        }
    }
}
