using Grid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MONO_32.SpriteGUIEditor.Buttons;
using MONO_32.SpriteGUIEditor.Enums;
using System.Collections.Generic;

namespace MONO_32.SpriteGUIEditor.Grid;

internal class SpriteGrids
{
    private List<SpriteGrid> spriteGrids = new List<SpriteGrid>();
    private int scaleFactor = 1;
    public SpriteGrid currentSpriteGrid;

    public SpriteGrids()
    {
        AddSprite();
    }

    public void AddSprite()
    {
        currentSpriteGrid = new SpriteGrid(16, 32);

        var buttons = new List<Button>();

        buttons.Add(new Button(
                ButtonTypeEnum.DeleteMiniature,
                UIVariables.Textures["delete_button"]
            ));
        buttons.Add(new Button(
               ButtonTypeEnum.LeftArrow,
               UIVariables.Textures["left_arrow_button"]
           ));
        buttons.Add(new Button(
               ButtonTypeEnum.RightArrow,
               UIVariables.Textures["right_arrow_button"]
           ));
        buttons.Add(new Button(
               ButtonTypeEnum.Copy,
               UIVariables.Textures["copy_button"]
           ));
        buttons.Add(new Button(
               ButtonTypeEnum.Add,
               UIVariables.Textures["add_button"]
           ));

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Rectangle = new Rectangle(
                0,
                0,
                UIVariables.ButtonSize * 4 / 5,
                UIVariables.ButtonSize * 4 / 5);
        }

        currentSpriteGrid.AddButtons(buttons);
        spriteGrids.Add(currentSpriteGrid);
    }

    public void UpdateMouseLeftClicked(Point mousePosition)
    {
        currentSpriteGrid.UpdateMouseLeftClicked(mousePosition, scaleFactor);
    }

    public void Update()
    {
        currentSpriteGrid.Update();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        var miniatureScale = 4;
        var totalSize = currentSpriteGrid.GridSize * currentSpriteGrid.CellSize;
        currentSpriteGrid.Draw(spriteBatch, scaleFactor, Point.Zero);
        currentSpriteGrid.DrawGrid(spriteBatch, scaleFactor, Point.Zero, Color.Black);

        for (int i = 0; i < spriteGrids.Count; i++)
        {
            spriteGrids[i].Draw(spriteBatch, miniatureScale, new Point(-UIVariables.Edition.Width + i * totalSize / miniatureScale, 2 * UIVariables.Margin + totalSize));
            spriteGrids[i].DrawGrid(spriteBatch, miniatureScale, new Point(-UIVariables.Edition.Width + i * totalSize / miniatureScale, 2 * UIVariables.Margin + totalSize), Color.Black);
            spriteGrids[i].DrawButtons(spriteBatch, new Point(-UIVariables.Edition.Width + i * totalSize / miniatureScale, 3 * UIVariables.Margin + totalSize + totalSize / miniatureScale));
        }
    }
}
