using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;

namespace MONO_32.SpriteGUIEditor;

public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D pixelTexture;
    Color[,] gridColors;
    int cellSize = 8; // Size of each cell in the grid
    int gridSize = 32;  // 8x8 grid
    Color selectedColor = Color.Black; // Default color

    // Define a palette of colors
    Color[] colorPalette = new Color[]
    {
        new Color(0,0,0,0), Color.Black, Color.Red, Color.Green, Color.Blue,
        Color.Yellow, Color.Cyan, Color.Magenta, Color.White
    };

    int paletteSize = 32; // Size of each color cell in the palette
    Rectangle[] paletteRectangles;
    int paletteStartX = 300; // X position of the palette
    int paletteStartY = 100; // Y position of the palette

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        ResetGridColors();
        InitializePalette();
        base.Initialize();
    }

    private void ResetGridColors()
    {
        gridColors = new Color[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                gridColors[x, y] = new Color(0,0,0,0); // Default color
            }
        }
    }

    private void InitializePalette()
    {
        paletteRectangles = new Rectangle[colorPalette.Length];
        for (int i = 0; i < colorPalette.Length; i++)
        {
            int x = paletteStartX + (i % 4) * paletteSize; // Arrange palette in 4 columns
            int y = paletteStartY + (i / 4) * paletteSize; // Arrange palette in rows
            paletteRectangles[i] = new Rectangle(x, y, paletteSize, paletteSize);
        }
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // Create a 1x1 pixel texture
        pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        pixelTexture.SetData(new Color[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        // Check for mouse input
        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            // Convert mouse position to grid cell
            int x = mouseState.X / cellSize;
            int y = mouseState.Y / cellSize;

            if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
            {
                // Change the color of the cell
                gridColors[x, y] = selectedColor;
            }

            // Check if the mouse click is within the palette area
            for (int i = 0; i < colorPalette.Length; i++)
            {
                if (paletteRectangles[i].Contains(mouseState.Position))
                {
                    selectedColor = colorPalette[i];
                }
            }
        }

        if (mouseState.RightButton == ButtonState.Pressed)
        {
            // Create a Texture2D from gridColors
            Texture2D exportTexture = new Texture2D(GraphicsDevice, gridSize, gridSize);
            Color[] colorData = new Color[gridSize * gridSize];
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    colorData[x + y * gridSize] = gridColors[x, y];
                }
            }
            exportTexture.SetData(colorData);

            // Save the Texture2D as PNG
            string projectRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\MONO-32.DektopGame\assets\imgs"));
            string filePath = Path.Combine(projectRootPath, "wall.png");
            exportTexture.SaveAsPng(filePath);

            //ResetGridColors();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();

        // Draw the grid
        Color gridColor = Color.Black;
        // Draw the outer rectangle
        spriteBatch.Draw(pixelTexture, new Rectangle(0, 0, gridSize * cellSize, 1), gridColor); // Top
        spriteBatch.Draw(pixelTexture, new Rectangle(0, 0, 1, gridSize * cellSize), gridColor); // Left
        spriteBatch.Draw(pixelTexture, new Rectangle(0, (gridSize * cellSize) - 1, gridSize * cellSize, 1), gridColor); // Bottom
        spriteBatch.Draw(pixelTexture, new Rectangle((gridSize * cellSize) - 1, 0, 1, gridSize * cellSize), gridColor); // Right

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Draw the cell
                spriteBatch.Draw(pixelTexture, new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize), gridColors[x, y]);

                // Draw vertical lines
                spriteBatch.Draw(pixelTexture, new Rectangle(x * cellSize, 0, 1, gridSize * cellSize), gridColor);
                // Draw horizontal lines
                spriteBatch.Draw(pixelTexture, new Rectangle(0, y * cellSize, gridSize * cellSize, 1), gridColor);
            }
        }

        // Draw the color palette
        for (int i = 0; i < colorPalette.Length; i++)
        {
            spriteBatch.Draw(pixelTexture, paletteRectangles[i], colorPalette[i]);
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}

public static class Texture2DExtensions
{
    public static void SaveAsPng(this Texture2D texture, string filePath)
    {
        // Get the texture data
        Color[] data = new Color[texture.Width * texture.Height];
        texture.GetData(data);

        // Convert to System.Drawing.Bitmap
        using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(texture.Width, texture.Height))
        {
            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    // Convert Color to System.Drawing.Color
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(data[x + y * texture.Width].A,
                                                                              data[x + y * texture.Width].R,
                                                                              data[x + y * texture.Width].G,
                                                                              data[x + y * texture.Width].B);
                    bitmap.SetPixel(x, y, color);
                }
            }

            // Save the bitmap to a file
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}

