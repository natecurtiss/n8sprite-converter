using System.IO;
using AnotherFileBrowser.Windows;
using N8SpriteConverter.Colors;
using N8SpriteConverter.UI;
using UnityEngine;

namespace N8SpriteConverter
{
    [DisallowMultipleComponent]
    internal sealed class ExportActions : MonoBehaviour
    {
        [SerializeField]
        private UIConvertedImage _convertedImage;

        public void Export()
        {
            if (!_convertedImage.Texture) return;
            var spriteTexture = _convertedImage.Texture;
            var fileData = string.Empty;
            for (var y = 0; y < spriteTexture.height; y++)
            {
                for (var x = 0; x < spriteTexture.width; x++)
                {
                    var colorContainer = spriteTexture.GetPixel(x, y).MatchToColorContainer();
                    var foregroundColor = colorContainer.Color == Color.clear ? "Clear" : colorContainer.ForegroundColor.ToString();
                    var backgroundColor = colorContainer.Color == Color.clear ? "Clear" : colorContainer.BackgroundColor.ToString();
                    fileData += $"{{{foregroundColor},{backgroundColor}}}";
                }
                fileData += '\n';
            }
            var browserProperties = new BrowserProperties
            {
                Filter = "n8sprite files (*.n8sprite)|*.n8sprite"
            };
            FileBrowser.SaveFileBrowser(browserProperties, "new_sprite", ".n8sprite", path => File.WriteAllText(path, fileData));
        }
    }
}