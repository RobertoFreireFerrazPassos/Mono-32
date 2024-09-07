using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MONO_32.SpriteGUIEditor.Enums;

namespace MONO_32.SpriteGUIEditor;

internal static class UIVariables
{
    public static Texture2D PixelTexture;

    public static int OffsetX;

    public static int OffsetY;

    public static Color SelectedColor;

    public static SpriteFont DefaultFont;

    public static string TextFileName = "";

    public static PaintModeEnum PaintMode;

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
        PaintMode = PaintModeEnum.Pencil;
    }
}
