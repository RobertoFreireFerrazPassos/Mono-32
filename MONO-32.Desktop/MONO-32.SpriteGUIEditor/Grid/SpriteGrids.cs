using Grid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MONO_32.SpriteGUIEditor.Buttons;
using MONO_32.SpriteGUIEditor.Enums;
using System.Collections.Generic;

namespace MONO_32.SpriteGUIEditor.Grid;

internal class SpriteGrids
{
    public SpriteGrid currentSpriteGrid;

    private List<SpriteGrid> spriteGrids = new List<SpriteGrid>();
    private int scaleFactor = 1;
    private int miniatureScale = 4;

    public SpriteGrids()
    {
        AddSprite();
    }

    public void AddSprite()
    {
        currentSpriteGrid = new SpriteGrid(UIVariables.CellSize, UIVariables.GridSize);

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

        currentSpriteGrid.Buttons = buttons;
        spriteGrids.Add(currentSpriteGrid);
    }

    public void UpdateMouseLeftClicked(Point mousePosition)
    {
        currentSpriteGrid.UpdateMouseLeftClicked(mousePosition, scaleFactor);

    }

    public void UpdateMouseLeftReleased(Point mousePosition)
    {
        UpdateButtonsMiniature(mousePosition);
    }

    public void Update()
    {
        currentSpriteGrid.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var totalSize = UIVariables.CellSize * UIVariables.GridSize;

        currentSpriteGrid.Draw(spriteBatch, scaleFactor, Point.Zero);
        currentSpriteGrid.DrawGrid(spriteBatch, scaleFactor, Point.Zero, Color.Black);

        for (int i = 0; i < spriteGrids.Count; i++)
        {
            var point = GetButtonPoint(i);
            spriteGrids[i].Draw(spriteBatch, miniatureScale, new Point(-UIVariables.Edition.Width + i * totalSize / miniatureScale, 2 * UIVariables.Margin + totalSize));
            spriteGrids[i].DrawGrid(spriteBatch, miniatureScale, new Point(-UIVariables.Edition.Width + i * totalSize / miniatureScale, 2 * UIVariables.Margin + totalSize), Color.Black);
            spriteGrids[i].DrawButtons(spriteBatch, point);
        }
    }

    private Point GetButtonPoint(int i)
    {
        var totalSize = UIVariables.CellSize * UIVariables.GridSize;
        return new Point(-UIVariables.Edition.Width + i * totalSize / miniatureScale, 3 * UIVariables.Margin + totalSize + totalSize / miniatureScale);
    }

    private void UpdateButtonsMiniature(Point mousePosition)
    {
        for (int i = 0; i < spriteGrids.Count; i++)
        {
            var buttons = spriteGrids[i].Buttons;
            var point = GetButtonPoint(i);
            for (int j = 0; j < buttons.Count; j++)
            {
                var rectangle = spriteGrids[i].GetButtonRectangle(j, point);
                if (rectangle.Contains(mousePosition))
                {
                    switch (buttons[j].Type)
                    {
                        case Enums.ButtonTypeEnum.Add:
                            AddSprite();
                            break;
                    }
                }
            }
        }
    }
}
