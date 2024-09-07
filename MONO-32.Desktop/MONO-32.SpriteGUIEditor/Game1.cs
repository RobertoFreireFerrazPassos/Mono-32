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
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        graphics.PreferredBackBufferWidth = 1200;
        graphics.PreferredBackBufferHeight = 800;
        graphics.IsFullScreen = false; // Set to true if you want full-screen mode
        graphics.ApplyChanges();

        spriteGrid = new SpriteGrid();
        palette = new Palette();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        pixelTexture.SetData(new Color[] { Color.White });
        spriteBatch = new SpriteBatch(GraphicsDevice);
        UIVariables.LoadVariables(pixelTexture, palette.ColorPalette[1], 32, 32);
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