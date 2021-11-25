using System;
using UnityEngine;

namespace N8SpriteConverter.Colors
{
    public readonly struct ColorContainer
    {
        public readonly Color32 Color;
        public readonly ConsoleColor ForegroundColor;
        public readonly ConsoleColor BackgroundColor;

        public ColorContainer(Color32 color, ConsoleColor consoleColor)
        {
            Color = color;
            ForegroundColor = consoleColor;
            BackgroundColor = consoleColor;
        }
        
        public ColorContainer(Color32 color, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Color = color;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}