using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MONO_32.SpriteGUIEditor;

internal static class UIVariables
{
    public static Texture2D PixelTexture;

    public static int OffsetX;

    public static int OffsetY;

    public static Color SelectedColor;

    public static SpriteFont DefaultFont;

    public static void LoadVariables(
        Texture2D pixelTexture,
        Color color,
        int offsetX,
        int offsetY)
    {
        PixelTexture = pixelTexture;
        SelectedColor = color;
        OffsetX = offsetX;
        OffsetY = offsetY;
    }
}
