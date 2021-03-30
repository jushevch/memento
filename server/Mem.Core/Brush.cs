using System.Collections.Generic;

namespace Mem.Core
{
    public enum Color
    {
        Regular,
        Red,
        DimRed,
        Green,
        DimGreen,
        Blue,
        DimBlue,
        Cyan,
        DimCyan,
        Magenta,
        DimMagenta,
        Yellow,
        DimYellow,
        White,
        Black,
    }

    public static class Brush
    {
        private static readonly Dictionary<Color, string> colorNames = new ()
        {
            [Color.Regular] = "#b0b0b0",

            [Color.Red] = "#e00000",
            [Color.DimRed] = "#800000",
            [Color.Green] = "#00e000",
            [Color.DimGreen] = "#008000",
            [Color.Blue] = "#0060ff",
            [Color.DimBlue] = "#004090",

            [Color.Cyan] = "#00e0ff",
            [Color.DimCyan] = "#008080",
            [Color.Magenta] = "#f000f0",
            [Color.DimMagenta] = "#800080",
            [Color.Yellow] = "#ffe000",
            [Color.DimYellow] = "#907000",

            [Color.White] = "#f0f0f0",
            [Color.Black] = "#606060",
        };

        public static string PaintWith(this string text, Color color)
        {
            return $"<span style=\"color:{GetCode(color)}\">{text}</span>";
        }

        public static string GetCode(this Color color)
        {
            return colorNames.TryGetValue(color, out var name) ? name : colorNames[Color.Regular];
        }
    }
}
