using System.IO;
using AnotherFileBrowser.Windows;
using N8SpriteConverter.Colors;
using UnityEngine;

namespace N8SpriteConverter
{
    [DisallowMultipleComponent]
    internal sealed class ImportActions : MonoBehaviour
    {
        [SerializeField]
        private ConvertedImage _convertedImage;

        public void Import()
        {
            var browserProperties = new BrowserProperties
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };
            FileBrowser.OpenFileBrowser
            (
                browserProperties, path =>
                {
                    var bytes = File.ReadAllBytes(path);
                    var texture2D = new Texture2D(2, 2);
                    texture2D.LoadImage(bytes);
                    
                    _convertedImage.RectTransform.sizeDelta = new Vector2(texture2D.width, texture2D.height);
                    var fittedWidth = _convertedImage.MaximumSize.x / texture2D.width;
                    _convertedImage.RectTransform.localScale = Vector3.one * fittedWidth;
                    if (_convertedImage.RectTransform.localScale.y * _convertedImage.RectTransform.sizeDelta.y > _convertedImage.MaximumSize.y)
                    {
                        var fittedHeight = _convertedImage.MaximumSize.y / texture2D.height;
                        _convertedImage.RectTransform.localScale = Vector3.one * fittedHeight;
                    }
                    texture2D = texture2D.ColoredWithConsoleColors();
                    _convertedImage.RawImage.texture = texture2D;
                    _convertedImage.RawImage.mainTexture.filterMode = FilterMode.Point;
                    _convertedImage.RawImage.mainTexture.wrapMode = TextureWrapMode.Clamp;
                    _convertedImage.Texture = texture2D;
                }
            );
        }
    }
}