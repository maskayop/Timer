using UnityEngine;
using TMPro;

public class UIDateAndTimeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void Init(string INtext)
    {
        text.text = INtext;
    }
}
