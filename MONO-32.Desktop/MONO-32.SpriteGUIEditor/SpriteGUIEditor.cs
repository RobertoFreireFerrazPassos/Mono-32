using Buttons;
using Grid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MONO_32.Core;
using MONO_32.Engine.Input;
using MONO_32.SpriteGUIEditor.Buttons;
using MONO_32.SpriteGUIEditor.Enums;
using MONO_32.SpriteGUIEditor.Grid;
using System.Collections.Generic;
using System.IO;

namespace MONO_32.SpriteGUIEditor;

public class SpriteGUIEditor : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    SpriteGrids spriteGrids;
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

        spriteGrids = new SpriteGrids();
        spriteGrids.AddSprite();
        palette = new Palette(ColorPaletteEnum.Vinik24, 8, 32);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        pixelTexture.SetData(new Color[] { Color.White });
        spriteBatch = new SpriteBatch(GraphicsDevice);

        var paintMode = PaintModeEnum.Pencil;
        var buttonSelected = ButtonTypeEnum.Pencil;
        UIVariables.LoadVariables(
            pixelTexture,
            paintMode,
            palette.ColorPalette[0], 
            32, 
            32);
        palette.CreatePalleteRectangles();
        var textures = FileUtils.GetAllImages(GraphicsDevice, Directory.GetFiles("assets\\imgs\\", "*.png", SearchOption.AllDirectories));
        var saveButton = new Button(
            ButtonTypeEnum.Save,
            textures["save_button"]);
        var pencilButton = new Button(
            ButtonTypeEnum.Pencil,
            textures["pencil_button"]);
        var eraserButton = new Button(
            ButtonTypeEnum.Eraser,
            textures["eraser_button"]);
        var bucketButton = new Button(
            ButtonTypeEnum.Bucket,
            textures["bucket_button"]);
        buttons = new Buttons.Buttons(new List<Button>()
            {
                saveButton,
                bucketButton,
                eraserButton,
                pencilButton
            }, buttonSelected);

        UIVariables.DefaultFont = Content.Load<SpriteFont>(@"fonts\default");
        textInputField = new TextInputField(
            UIVariables.DefaultFont,
            new Rectangle(
                saveButton.Rectangle.Right + UIVariables.Margin,
                saveButton.Rectangle.Y,
                120,
                saveButton.Rectangle.Height));
    }

    protected override void Update(GameTime gameTime)
    {
        InputUtils.Update();
        if (InputUtils.IsMouseLeftButtonPressed())
        {
            ProcessMouseClicked(InputUtils.MousePosition());
        }
        spriteGrids.Update();
        textInputField.Update(gameTime);
        base.Update(gameTime);
    }

    private void ProcessMouseClicked(Point mousePosition)
    {
        textInputField.UpdateMouseLeftClicked(mousePosition);
        spriteGrids.UpdateMouseLeftClicked(mousePosition);
        UIVariables.SelectedColor = palette.UpdateSelectedColor(mousePosition) ?? UIVariables.SelectedColor;
        buttons.Update(mousePosition, spriteGrids.currentSpriteGrid, GraphicsDevice);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        buttons.Draw(spriteBatch);
        spriteGrids.Draw(spriteBatch);
        palette.Draw(spriteBatch);
        textInputField.Draw(spriteBatch);
        base.Draw(gameTime);
    }
}