using UnityEngine;
using UnityEngine.UI;

namespace Vopere
{
    public class UIColorSchemeButton : MonoBehaviour
    {
        Image image;

        public void Init(Color INcolor)
        {
            image = GetComponent<Image>();
            image.color = INcolor;
        }

        public void SendCurrentColor()
        {
            if (!image)
                return;

            Timer.Instance.SetCurrentProjectColor(image.color);
        }
    }
}
