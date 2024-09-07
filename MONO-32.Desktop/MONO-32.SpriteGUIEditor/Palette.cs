using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MONO_32.SpriteGUIEditor;

internal class Palette
{
    public Color[] ColorPalette = new Color[]
    {
        new Color(0,0,0,0),

        // Grays
        new Color(0x00, 0x00, 0x00), // Black
        new Color(0xFF, 0xFF, 0xFF), // White
        new Color(0xAA, 0xAA, 0xAA), // Light Gray
        new Color(0x55, 0x55, 0x55), // Dark Gray
        new Color(0x7F, 0x7F, 0x7F), // Gray
        new Color(0xC0, 0xC0, 0xC0), // Silver
        new Color(0xC0, 0xC0, 0xC0), // Medium Gray (same as Silver)
    
        // Reds and Pink
        new Color(0xFF, 0x00, 0x00), // Red
        new Color(0x80, 0x00, 0x00), // Dark Red
        new Color(0xFF, 0x80, 0x80), // Light Red
        new Color(0xFF, 0x00, 0x80), // Pink
        new Color(0xFF, 0x40, 0x40), // Salmon
        new Color(0xFF, 0xC0, 0x80), // Coral
        new Color(0xFF, 0x00, 0x40), // Red-Orange

        // Greens
        new Color(0x00, 0xFF, 0x00), // Green
        new Color(0x00, 0x80, 0x00), // Dark Green
        new Color(0x80, 0xFF, 0x80), // Light Green
        new Color(0x00, 0xFF, 0x80), // Light Green 2
        new Color(0x80, 0xFF, 0x00), // Lime
        new Color(0x00, 0xFF, 0x40), // Green-Yellow
        new Color(0x40, 0xFF, 0x00), // Yellow-Green
        new Color(0x40, 0xFF, 0x40), // Lime Green
        new Color(0xC0, 0xFF, 0x80), // Mint

        // Blues
        new Color(0x00, 0x00, 0xFF), // Blue
        new Color(0x00, 0x00, 0x80), // Dark Blue
        new Color(0x80, 0x80, 0xFF), // Light Blue
        new Color(0x00, 0x80, 0xFF), // Light Blue 2
        new Color(0x40, 0x40, 0xFF), // Royal Blue
        new Color(0x80, 0xC0, 0xFF), // Sky Blue
        new Color(0x40, 0xFF, 0xFF), // Aqua
        new Color(0x00, 0xFF, 0xFF), // Aqua 2

        // Yellows
        new Color(0xFF, 0xFF, 0x00), // Yellow
        new Color(0xFF, 0xFF, 0x80), // Light Yellow
        new Color(0xFF, 0xFF, 0x40), // Light Yellow 2
        new Color(0xFF, 0xFF, 0x80), // Lemon

        // Purples and Magentas
        new Color(0xFF, 0x00, 0xFF), // Magenta
        new Color(0x80, 0x00, 0x80), // Purple
        new Color(0x80, 0x00, 0xFF), // Violet
        new Color(0xFF, 0x80, 0xFF), // Light Magenta
        new Color(0xFF, 0x40, 0xFF), // Light Magenta 2
        new Color(0xFF, 0xC0, 0xFF), // Lavender
        new Color(0xC0, 0x80, 0xFF), // Lilac

        // Oranges and Browns
        new Color(0xFF, 0x80, 0x00), // Orange
        new Color(0xFF, 0xC0, 0x80), // Coral

        // Cyan and Teal
        new Color(0x00, 0x80, 0x80), // Teal
        new Color(0x80, 0xFF, 0xFF), // Light Cyan
    };

    public int PaletteSize = 32; // Size of each color cell in the palette

    public Rectangle[] PaletteRectangles;

    public int PaletteStartX = 700; // X position of the palette

    public int PaletteStartY = 10; // Y position of the palette

    public int Columns = 12;

    public Palette()
    {
        PaletteRectangles = new Rectangle[ColorPalette.Length];
        for (int i = 0; i < ColorPalette.Length; i++)
        {
            int x = PaletteStartX + (i % Columns) * PaletteSize; // Arrange palette in Columns
            int y = PaletteStartY + (i / Columns) * PaletteSize; // Arrange palette in rows
            PaletteRectangles[i] = new Rectangle(x, y, PaletteSize, PaletteSize);
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

    public void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture)
    {
        for (int i = 0; i < ColorPalette.Length; i++)
        {
            spriteBatch.Draw(pixelTexture, PaletteRectangles[i], ColorPalette[i]);
        }
    }
}
