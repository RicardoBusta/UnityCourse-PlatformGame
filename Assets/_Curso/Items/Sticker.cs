namespace _Curso.Items
{
    using System.Collections.Generic;
    using Game;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(SpriteRenderer))]
    public class Sticker : MonoBehaviour
    {
        public Stack<StickerData> collectedStickers = new Stack<StickerData>();

        public StickerData data;

        public SpriteRenderer icon;

        private void OnEnable()
        {
            Setup();
        }

        private void Setup()
        {
            icon.sprite = data.sprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var controller = GameController.Instance;
            var stickerUi = Instantiate(controller.StickerUiPrefab, controller.StickerLayout.transform);
            stickerUi.Setup(data);
            collectedStickers.Push(data);
            gameObject.SetActive(false);
        }
    }
}