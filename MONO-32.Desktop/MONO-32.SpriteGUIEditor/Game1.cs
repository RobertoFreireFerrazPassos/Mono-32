using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MONO_32.SpriteGUIEditor.Extensions;

namespace MONO_32.SpriteGUIEditor;

public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    SpriteGrid spriteGrid;
    Palette palette;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        graphics.PreferredBackBufferWidth = 1200;
        graphics.PreferredBackBufferHeight = 800;
        graphics.IsFullScreen = false; // Set to true if you want full-screen mode
        graphics.ApplyChanges();

        spriteGrid = new SpriteGrid(16, 32);
        palette = new Palette(new Color[]
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
        }, 12, 32);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        pixelTexture.SetData(new Color[] { Color.White });
        spriteBatch = new SpriteBatch(GraphicsDevice);
        UIVariables.LoadVariables(pixelTexture, palette.ColorPalette[1], 32, 32);
        palette.CreatePalleteRectangles(
            (spriteGrid.GridSize + 1) * spriteGrid.CellSize + UIVariables.OffsetX,
            UIVariables.OffsetY);
    }

    protected override void Update(GameTime gameTime)
    {
        // Check for mouse input
        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            var mousePosition = mouseState.Position;
            spriteGrid.Update(mousePosition, UIVariables.SelectedColor);
            UIVariables.SelectedColor = palette.UpdateSelectedColor(mousePosition) ?? UIVariables.SelectedColor;
        }

        if (mouseState.RightButton == ButtonState.Pressed)
        {
            var exportTexture = spriteGrid.ConvertToTexture2D(GraphicsDevice);
            exportTexture.SaveAsPng();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();
        spriteGrid.Draw(spriteBatch);
        palette.Draw(spriteBatch);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}