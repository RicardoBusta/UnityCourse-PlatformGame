namespace _Curso.Items
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class StickerUI: MonoBehaviour
    {
        public Image image;
        public TextMeshProUGUI text;

        public void Setup(StickerData data)
        {
            image.sprite = data.sprite;
            text.text = data.name;
        }
    }
}