using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace N8SpriteConverter.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RawImage))]
    public sealed class UIConvertedImage : MonoBehaviour
    {
        [field: SerializeField]
        public Vector2 MaximumSize { get; [UsedImplicitly] private set; }
        
        public RectTransform RectTransform { get; private set; }
        public RawImage RawImage { get; private set; }
        public Texture2D Texture { get; set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            RawImage = GetComponent<RawImage>();
        }
    }
}