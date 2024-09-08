using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MONO_32.Engine.Input;
using System.Collections.Generic;

namespace MONO_32.SpriteGUIEditor;

internal class SpriteGrid
{
    public Color[,] GridColors;
    public Color[,] LastGridColors;
    public int CellSize; // Size of each cell in the grid
    public int GridSize;  // 8x8 grid

    public SpriteGrid(int cellSize, int gridSize)
    {
        CellSize = cellSize;
        GridSize = gridSize;
        GridColors = new Color[GridSize, GridSize];
        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                GridColors[x, y] = new Color(0, 0, 0, 0); // invisible
            }
        }
    }

    public void UpdateMouseLeftClicked(Point mousePosition)
    {
        var gridPoint = ConvertMousePositionToGridCell(mousePosition);
        int x = gridPoint.X;
        int y = gridPoint.Y;

        if (x >= 0 && x < GridSize && y >= 0 && y < GridSize)
        {
            if (InputUtils.IsMouseLeftButtonJustPressed())
            {
                LastGridColors = CopyColorArray(GridColors);
            }
            
            switch (UIVariables.PaintMode)
            {
                case Enums.PaintModeEnum.Bucket:
                    Fill(mousePosition);
                    break;
                case Enums.PaintModeEnum.Pencil:
                    GridColors[x, y] = UIVariables.SelectedColor;
                    break;
                case Enums.PaintModeEnum.Eraser:
                    GridColors[x, y] = new Color(0, 0, 0, 0);
                    break;
            }
        }
    }

    public void Update()
    {
        if (InputUtils.IsControlZReleased())
        {
            GridColors = CopyColorArray(LastGridColors);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Color gridColor = Color.Black;

        spriteBatch.Begin();
        // Draw selected color
        spriteBatch.Draw(UIVariables.PixelTexture, new Rectangle(UIVariables.OffsetX + ((GridSize + 8)* CellSize), UIVariables.OffsetY, CellSize, CellSize), UIVariables.SelectedColor);

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
        spriteBatch.End();
    }

    public void Fill(Point mousePosition)
    {
        var gridPoint = ConvertMousePositionToGridCell(mousePosition);
        Color targetColor = GridColors[gridPoint.X, gridPoint.Y];
        if (targetColor == UIVariables.SelectedColor) return;

        Queue<Point> pixels = new Queue<Point>();
        pixels.Enqueue(gridPoint);

        while (pixels.Count > 0)
        {
            Point p = pixels.Dequeue();
            int x = p.X;
            int y = p.Y;

            if (x < 0 || x >= GridSize || y < 0 || y >= GridSize || GridColors[x, y] != targetColor)
                continue;

            GridColors[x, y] = UIVariables.SelectedColor;

            pixels.Enqueue(new Point(x - 1, y));
            pixels.Enqueue(new Point(x + 1, y));
            pixels.Enqueue(new Point(x, y - 1));
            pixels.Enqueue(new Point(x, y + 1));
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

    private Point ConvertMousePositionToGridCell(Point mousePosition)
    {
        // Convert mouse position to grid cell
        int x = mousePosition.X - UIVariables.OffsetX >= 0 ? (mousePosition.X - UIVariables.OffsetX) / CellSize : -1;
        int y = mousePosition.Y - UIVariables.OffsetY >= 0 ? (mousePosition.Y - UIVariables.OffsetY) / CellSize : -1;

        return new Point(x, y);
    }

    private static Color[,] CopyColorArray(Color[,] original)
    {
        int rows = original.GetLength(0);
        int cols = original.GetLength(1);
        Color[,] copy = new Color[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copy[i, j] = original[i, j];
            }
        }

        return copy;
    }
}
