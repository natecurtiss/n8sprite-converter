using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace N8SpriteConverter.Colors
{
    public static class ColorGenerator
    {
        private static readonly ColorContainer[] _baseColors =
        {
            new ColorContainer(new Color32(0, 0, 0, 255), ConsoleColor.Black), 
            new ColorContainer(new Color32(0, 0, 139, 255), ConsoleColor.DarkBlue), 
            new ColorContainer(new Color32(0, 100, 0, 255), ConsoleColor.DarkGreen), 
            new ColorContainer(new Color32(0, 139, 139, 255), ConsoleColor.DarkCyan), 
            new ColorContainer(new Color32(139, 0, 0, 255), ConsoleColor.DarkRed), 
            new ColorContainer(new Color32(139, 0, 139, 255), ConsoleColor.DarkMagenta), 
            new ColorContainer(new Color32(215, 195, 42, 255), ConsoleColor.DarkYellow), 
            new ColorContainer(new Color32(128, 128, 128, 255), ConsoleColor.Gray), 
            new ColorContainer(new Color32(169, 169, 169, 255), ConsoleColor.DarkGray),
            new ColorContainer(new Color32(0, 0, 255, 255), ConsoleColor.Blue), 
            new ColorContainer(new Color32(0, 128, 0, 255), ConsoleColor.Green), 
            new ColorContainer(new Color32(0, 255, 255, 255), ConsoleColor.Cyan), 
            new ColorContainer(new Color32(255, 0, 0, 255), ConsoleColor.Red), 
            new ColorContainer(new Color32(255, 0, 255, 255), ConsoleColor.Magenta), 
            new ColorContainer(new Color32(255, 255, 0, 255), ConsoleColor.Yellow), 
            new ColorContainer(new Color32(255, 255, 255, 255), ConsoleColor.White)
        };
        
        private static IEnumerable<ColorContainer> _allColors;
        private static bool _hasInitializedAllColors;
        
        private static IEnumerable<ColorContainer> AllColors
        {
            get
            {
                if (_hasInitializedAllColors)
                    return _allColors;
                _hasInitializedAllColors = true;
                
                _allColors = _baseColors.Concat(MixedColors).ToArray();
                return _allColors;
            }
        }
        private static IEnumerable<ColorContainer> MixedColors
        {
            get
            {
                var mixedColors = new List<ColorContainer>();
                foreach (var baseColor in _baseColors)
                {
                    foreach (var otherBaseColor in _baseColors)
                    {
                        var mixedColor = Color32.Lerp(baseColor.Color, otherBaseColor.Color, 0.5f);
                        var newColor = new ColorContainer(mixedColor, baseColor.ForegroundColor, otherBaseColor.ForegroundColor);

                        var colorAlreadyExists = false;
                        foreach (var unused in mixedColors.Where
                            (existingColor => existingColor.Color.IsEqualTo(newColor.Color))) colorAlreadyExists = true;
                        foreach (var unused in _baseColors.Where
                            (existingColor => existingColor.Color.IsEqualTo(newColor.Color))) colorAlreadyExists = true;

                        if (!colorAlreadyExists) mixedColors.Add(newColor);
                    }
                }
                return mixedColors;
            }
        }
        
        public static Texture2D ColoredWithConsoleColors(this Texture2D oldTexture)
        {
            var newTexture = new Texture2D(oldTexture.width, oldTexture.height);
            for (var x = 0; x < newTexture.width; x++)
            {
                for (var y = 0; y < newTexture.height; y++)
                {
                    var newColor = oldTexture.GetPixel(x, y);
                    if (newColor.a < 1)
                        newTexture.SetPixel(x, y, Color.clear);
                    else
                        newTexture.SetPixel(x, y, ((Color32) newColor).FindClosestColor(AllColors.Select(colorContainer =>  colorContainer.Color)));
                }
            }
            newTexture.Apply();
            return newTexture;
        }
        
        public static ColorContainer MatchToColorContainer(this Color color)
        {
            foreach (var colorContainer in AllColors)
                if (colorContainer.Color == color) return colorContainer;
            return new ColorContainer(Color.clear, ConsoleColor.Black);
        }
    }
}