namespace _Curso.Items
{
    using System.Collections.Generic;
    using Game;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class Sticker : MonoBehaviour
    {
        public Stack<StickerData> collectedStickers = new Stack<StickerData>();

        public StickerData data;

        public SpriteRenderer icon;

        private void OnEnable()
        {
            icon.sprite = data.sprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            collectedStickers.Push(data);
            gameObject.SetActive(false);
        }
    }
}