using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MONO_32.SpriteGUIEditor;

internal class SpriteGrid
{
    public Color[,] GridColors;
    public int CellSize = 16; // Size of each cell in the grid
    public int GridSize = 32;  // 8x8 grid

    public SpriteGrid()
    {
        GridColors = new Color[GridSize, GridSize];
        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                GridColors[x, y] = new Color(0, 0, 0, 0); // Default color
            }
        }
    }

    public void Update(Point mousePosition, Color selectedColor)
    {
        // Convert mouse position to grid cell
        int x = mousePosition.X - UIVariables.OffsetX >= 0 ? (mousePosition.X - UIVariables.OffsetX) / CellSize : -1;
        int y = mousePosition.Y - UIVariables.OffsetY >= 0 ? (mousePosition.Y - UIVariables.OffsetY) / CellSize : -1;

        if (x >= 0 && x < GridSize && y >= 0 && y < GridSize)
        {
            // Change the color of the cell
            GridColors[x, y] = selectedColor;
        }
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        Color gridColor = Color.Black;
        // Draw the outer rectangle with UISettings.Offsets
        spriteBatch.Draw(UIVariables.PixelTexture, new Rectangle(UIVariables.OffsetX, UIVariables.OffsetY, GridSize * CellSize, 1), gridColor); // Top
        spriteBatch.Draw(UIVariables.PixelTexture, new Rectangle(UIVariables.OffsetX, UIVariables.OffsetY, 1, GridSize * CellSize), gridColor); // Left
        spriteBatch.Draw(UIVariables.PixelTexture, new Rectangle(UIVariables.OffsetX, UIVariables.OffsetY + (GridSize * CellSize) - 1, GridSize * CellSize, 1), gridColor); // Bottom
        spriteBatch.Draw(UIVariables.PixelTexture, new Rectangle(UIVariables.OffsetX + (GridSize * CellSize) - 1, UIVariables.OffsetY, 1, GridSize * CellSize), gridColor); // Right

        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                // Draw the cell with UISettings.Offsets
                spriteBatch.Draw(UIVariables.PixelTexture, new Rectangle(UIVariables.OffsetX + x * CellSize, UIVariables.OffsetY + y * CellSize, CellSize, CellSize), GridColors[x, y]);
                // Draw vertical lines with UISettings.Offsets
                spriteBatch.Draw(UIVariables.PixelTexture, new Rectangle(UIVariables.OffsetX + x * CellSize, UIVariables.OffsetY, 1, GridSize * CellSize), gridColor);
                // Draw horizontal lines with UISettings.Offsets
                spriteBatch.Draw(UIVariables.PixelTexture, new Rectangle(UIVariables.OffsetX, UIVariables.OffsetY + y * CellSize, GridSize * CellSize, 1), gridColor);
            }
        }
    }

    public Texture2D ConvertToTexture2D(GraphicsDevice graphicsDevice)
    {
        var exportTexture = new Texture2D(graphicsDevice, GridSize, GridSize);
        Color[] colorData = new Color[GridSize * GridSize];
        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                colorData[x + y * GridSize] = GridColors[x, y];
            }
        }
        exportTexture.SetData(colorData);

        return exportTexture;
    }
}
