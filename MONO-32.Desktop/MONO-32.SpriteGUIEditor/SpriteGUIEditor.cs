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
        palette = new Palette(new Color[]
        {
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

            // Reds and Pink (Moderately Less Intense)
            new Color(0xBF, 0x40, 0x40), // Soft Red
            new Color(0x60, 0x00, 0x00), // Soft Dark Red
            new Color(0xBF, 0x6C, 0x6C), // Soft Light Red
            new Color(0xBF, 0x40, 0x60), // Soft Pink
            new Color(0xBF, 0x6C, 0x6C), // Soft Salmon
            new Color(0xBF, 0xBF, 0x6C), // Soft Coral
            new Color(0xBF, 0x40, 0x40), // Soft Red-Orange

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

            // Greens (Moderately Less Intense)
            new Color(0x40, 0xBF, 0x40), // Soft Green
            new Color(0x20, 0x40, 0x20), // Soft Dark Green
            new Color(0x6C, 0xBF, 0x6C), // Soft Light Green
            new Color(0x40, 0xBF, 0x60), // Soft Light Green 2
            new Color(0x6C, 0xBF, 0x20), // Soft Lime
            new Color(0x40, 0xBF, 0x30), // Soft Green-Yellow
            new Color(0x20, 0xBF, 0x20), // Soft Yellow-Green
            new Color(0x30, 0xBF, 0x30), // Soft Lime Green

            // Blues
            new Color(0x00, 0x00, 0xFF), // Blue
            new Color(0x00, 0x00, 0x80), // Dark Blue
            new Color(0x80, 0x80, 0xFF), // Light Blue
            new Color(0x00, 0x80, 0xFF), // Light Blue 2
            new Color(0x40, 0x40, 0xFF), // Royal Blue
            new Color(0x80, 0xC0, 0xFF), // Sky Blue
            new Color(0x40, 0xFF, 0xFF), // Aqua
            new Color(0x00, 0xFF, 0xFF), // Aqua 2

            // Blues (Moderately Less Intense)
            new Color(0x40, 0x40, 0xBF), // Soft Blue
            new Color(0x20, 0x20, 0x60), // Soft Dark Blue
            new Color(0x6C, 0x6C, 0xBF), // Soft Light Blue
            new Color(0x40, 0x60, 0xBF), // Soft Light Blue 2
            new Color(0x30, 0x30, 0xBF), // Soft Royal Blue
            new Color(0x60, 0xBF, 0xBF), // Soft Sky Blue
            new Color(0x30, 0xBF, 0xBF), // Soft Aqua
            new Color(0x40, 0xBF, 0xBF), // Soft Aqua 2

            // Yellows
            new Color(0xFF, 0xFF, 0x00), // Yellow
            new Color(0xFF, 0xFF, 0x80), // Light Yellow
            new Color(0xFF, 0xFF, 0x40), // Light Yellow 2
            new Color(0xFF, 0xFF, 0x80), // Lemon

            // Yellows (Moderately Less Intense)
            new Color(0xBF, 0xBF, 0x40), // Soft Yellow
            new Color(0xBF, 0xBF, 0x60), // Soft Light Yellow
            new Color(0xBF, 0xBF, 0x40), // Soft Light Yellow 2

            // Purples and Magentas
            new Color(0xFF, 0x00, 0xFF), // Magenta
            new Color(0x80, 0x00, 0x80), // Purple
            new Color(0x80, 0x00, 0xFF), // Violet
            new Color(0xFF, 0x80, 0xFF), // Light Magenta
            new Color(0xFF, 0x40, 0xFF), // Light Magenta 2
            new Color(0xFF, 0xC0, 0xFF), // Lavender
            new Color(0xC0, 0x80, 0xFF), // Lilac

             // Purples and Magentas (Moderately Less Intense)
            new Color(0xBF, 0x40, 0xBF), // Soft Magenta
            new Color(0x60, 0x20, 0x60), // Soft Purple
            new Color(0x60, 0x20, 0xBF), // Soft Violet
            new Color(0xBF, 0x60, 0xBF), // Soft Light Magenta
            new Color(0xBF, 0x40, 0xBF), // Soft Light Magenta 2
            new Color(0xBF, 0xBF, 0xBF), // Soft Lavender
            new Color(0xBF, 0x80, 0xBF), // Soft Lilac

            // Oranges and Browns
            new Color(0xFF, 0x80, 0x00), // Orange
            new Color(0xFF, 0xC0, 0x80), // Coral

            // Oranges and Browns (Moderately Less Intense)
            new Color(0xBF, 0x60, 0x00), // Soft Orange

            // Cyan and Teal
            new Color(0x00, 0x80, 0x80), // Teal
            new Color(0x80, 0xFF, 0xFF), // Light Cyan

            // Cyan and Teal (Moderately Less Intense)
            new Color(0x00, 0x60, 0x60), // Soft Teal
            new Color(0x60, 0xBF, 0xBF), // Soft Light Cyan
        }, 8, 32);
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