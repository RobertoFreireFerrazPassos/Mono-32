using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MONO_32.SpriteGUIEditor;

internal class SpriteGrid
{
    public Color[,] GridColors;
    public int CellSize = 16; // Size of each cell in the grid
    public int GridSize = 32;  // 8x8 grid
    public int offsetX = 32; 
    public int offsetY = 32;

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
        int x = mousePosition.X - offsetX >= 0 ? (mousePosition.X - offsetX) / CellSize : -1;
        int y = mousePosition.Y - offsetY >= 0 ? (mousePosition.Y - offsetY) / CellSize : -1;

        if (x >= 0 && x < GridSize && y >= 0 && y < GridSize)
        {
            // Change the color of the cell
            GridColors[x, y] = selectedColor;
        }
    }
    
    public void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture)
    {
        Color gridColor = Color.Black;
        // Draw the outer rectangle with offsets
        spriteBatch.Draw(pixelTexture, new Rectangle(offsetX, offsetY, GridSize * CellSize, 1), gridColor); // Top
        spriteBatch.Draw(pixelTexture, new Rectangle(offsetX, offsetY, 1, GridSize * CellSize), gridColor); // Left
        spriteBatch.Draw(pixelTexture, new Rectangle(offsetX, offsetY + (GridSize * CellSize) - 1, GridSize * CellSize, 1), gridColor); // Bottom
        spriteBatch.Draw(pixelTexture, new Rectangle(offsetX + (GridSize * CellSize) - 1, offsetY, 1, GridSize * CellSize), gridColor); // Right

        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                // Draw the cell with offsets
                spriteBatch.Draw(pixelTexture, new Rectangle(offsetX + x * CellSize, offsetY + y * CellSize, CellSize, CellSize), GridColors[x, y]);
                // Draw vertical lines with offsets
                spriteBatch.Draw(pixelTexture, new Rectangle(offsetX + x * CellSize, offsetY, 1, GridSize * CellSize), gridColor);
                // Draw horizontal lines with offsets
                spriteBatch.Draw(pixelTexture, new Rectangle(offsetX, offsetY + y * CellSize, GridSize * CellSize, 1), gridColor);
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
