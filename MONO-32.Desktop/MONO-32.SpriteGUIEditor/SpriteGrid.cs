using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;

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
        int x = mousePosition.X / CellSize;
        int y = mousePosition.Y / CellSize;

        if (x >= 0 && x < GridSize && y >= 0 && y < GridSize)
        {
            // Change the color of the cell
            GridColors[x, y] = selectedColor;
        }
    }
    
    public void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture)
    {
        Color gridColor = Color.Black;
        // Draw the outer rectangle
        spriteBatch.Draw(pixelTexture, new Rectangle(0, 0, GridSize * CellSize, 1), gridColor); // Top
        spriteBatch.Draw(pixelTexture, new Rectangle(0, 0, 1, GridSize * CellSize), gridColor); // Left
        spriteBatch.Draw(pixelTexture, new Rectangle(0, (GridSize * CellSize) - 1, GridSize * CellSize, 1), gridColor); // Bottom
        spriteBatch.Draw(pixelTexture, new Rectangle((GridSize * CellSize) - 1, 0, 1, GridSize * CellSize), gridColor); // Right

        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                // Draw the cell
                spriteBatch.Draw(pixelTexture, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), GridColors[x, y]);
                // Draw vertical lines
                spriteBatch.Draw(pixelTexture, new Rectangle(x * CellSize, 0, 1, GridSize * CellSize), gridColor);
                // Draw horizontal lines
                spriteBatch.Draw(pixelTexture, new Rectangle(0, y * CellSize, GridSize * CellSize, 1), gridColor);
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
