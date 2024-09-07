using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MONO_32.SpriteGUIEditor;

internal class Palette
{
    public Color[] ColorPalette;

    public int PaletteSize; // Size of each color cell in the palette

    private Rectangle[] PaletteRectangles;

    public int Columns;

    public Palette(
        Color[] colorPalette,
        int columns,
        int paletteSize)
    {
        Columns = columns;
        PaletteSize = paletteSize;
        ColorPalette = colorPalette;
    }

    public void CreatePalleteRectangles(
        int offsetX,
        int offsetY)
    {
        PaletteRectangles = new Rectangle[ColorPalette.Length];
        for (int i = 0; i < ColorPalette.Length; i++)
        {
            int x = (i % Columns) * PaletteSize; // Arrange palette in Columns
            int y = (i / Columns) * PaletteSize; // Arrange palette in rows
            PaletteRectangles[i] = new Rectangle(offsetX + x, offsetY + y, PaletteSize, PaletteSize);
        }
    }

    public Color? UpdateSelectedColor(Point mousePosition)
    {
        for (int i = 0; i < ColorPalette.Length; i++)
        {
            if (PaletteRectangles[i].Contains(mousePosition))
            {
                return ColorPalette[i];
            }
        }

        return null;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < ColorPalette.Length; i++)
        {
            spriteBatch.Draw(UIVariables.PixelTexture, PaletteRectangles[i], ColorPalette[i]);
        }
    }
}
