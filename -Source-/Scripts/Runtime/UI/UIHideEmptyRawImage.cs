using UnityEngine;
using UnityEngine.UI;

namespace N8SpriteConverter.UI
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RawImage))]
    internal sealed class UIHideEmptyRawImage : MonoBehaviour
    {
        private RawImage _rawImage;
        private Color _color;
        
        private void Awake()
        {
            _rawImage = GetComponent<RawImage>();
            _color = _rawImage.color;
        }

        private void LateUpdate()
        {
            if (!_rawImage.texture)
            {
                _color = _rawImage.color;
                _rawImage.color = Color.clear;
            }
            else
            {
                _rawImage.color = _color == Color.clear ? Color.white : _color;
            }
        }
    }
}