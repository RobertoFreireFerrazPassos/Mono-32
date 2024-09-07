using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MONO_32.Core;
using MONO_32.Engine.Input;
using MONO_32.SpriteGUIEditor.Buttons;
using MONO_32.SpriteGUIEditor.Enums;
using System.Collections.Generic;
using System.IO;

namespace MONO_32.SpriteGUIEditor;

public class SpriteGUIEditor : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    SpriteGrid spriteGrid;
    Palette palette;
    Buttons.Buttons buttons;
    TextInputField textInputField;

    public SpriteGUIEditor()
    {
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Content.RootDirectory = @"assets";

        graphics.PreferredBackBufferWidth = 1200;
        graphics.PreferredBackBufferHeight = 800;
        graphics.IsFullScreen = false;
        graphics.ApplyChanges();

        spriteGrid = new SpriteGrid(16, 32);
        palette = new Palette(ColorPaletteEnum.PAX24, 8, 32);
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
            6 * spriteGrid.CellSize + UIVariables.OffsetY);
        var textures = FileUtils.GetAllImages(GraphicsDevice, Directory.GetFiles("assets\\imgs\\", "*.png", SearchOption.AllDirectories));
        var saveButton = new Button(
            ButtonTypeEnum.Save,
            (spriteGrid.GridSize + 1) * spriteGrid.CellSize + UIVariables.OffsetX,
            UIVariables.OffsetY,
            textures["save_button"]);
        var pencilButton = new Button(
            ButtonTypeEnum.Pencil,
            (spriteGrid.GridSize + 1) * spriteGrid.CellSize + UIVariables.OffsetX,
            3 * spriteGrid.CellSize + UIVariables.OffsetY,
            textures["pencil_button"]);
        var eraserButton = new Button(
            ButtonTypeEnum.Eraser,
            (spriteGrid.GridSize + 4) * spriteGrid.CellSize + UIVariables.OffsetX,
            3 * spriteGrid.CellSize + UIVariables.OffsetY,
            textures["eraser_button"]);
        var bucketButton = new Button(
            ButtonTypeEnum.Bucket,
            (spriteGrid.GridSize + 7) * spriteGrid.CellSize + UIVariables.OffsetX,
            3 * spriteGrid.CellSize + UIVariables.OffsetY,
            textures["bucket_button"]);
        buttons = new Buttons.Buttons(new List<Button>()
        {
            saveButton,
            bucketButton,
            eraserButton,
            pencilButton
        });

        UIVariables.DefaultFont = Content.Load<SpriteFont>(@"fonts\default");
        textInputField = new TextInputField(
            UIVariables.DefaultFont, 
            new Rectangle(
                saveButton.Rectangle.X + 3 * spriteGrid.CellSize,
                saveButton.Rectangle.Y, 
                120, 
                saveButton.Rectangle.Height));
    }

    protected override void Update(GameTime gameTime)
    {
        // Check for mouse input
        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            var mousePosition = mouseState.Position;
            spriteGrid.Update(mousePosition);
            UIVariables.SelectedColor = palette.UpdateSelectedColor(mousePosition) ?? UIVariables.SelectedColor;
            buttons.Update(mousePosition, spriteGrid, GraphicsDevice);
        }

        textInputField.Update(gameTime);
        InputUtils.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();
        buttons.Draw(spriteBatch);
        spriteGrid.Draw(spriteBatch);
        palette.Draw(spriteBatch);
        spriteBatch.End();
        textInputField.Draw(spriteBatch);

        base.Draw(gameTime);
    }
}