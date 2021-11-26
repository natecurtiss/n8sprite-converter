using UnityEngine;
using UnityEngine.UI;

namespace N8SpriteConverter.UI
{
    [ExecuteAlways, DisallowMultipleComponent, RequireComponent(typeof(RawImage))]
    sealed class HideEmptyRawImage : MonoBehaviour
    {
        RawImage _rawImage;
        Color _color;

        void Awake()
        {
            _rawImage = GetComponent<RawImage>();
            _color = _rawImage.color;
        }

        void LateUpdate()
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