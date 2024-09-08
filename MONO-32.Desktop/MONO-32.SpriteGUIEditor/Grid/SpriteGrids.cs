using Grid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MONO_32.SpriteGUIEditor.Grid;

internal class SpriteGrids
{
    private List<SpriteGrid> spriteGrids = new List<SpriteGrid>();
    private int scaleFactor = 1;
    public SpriteGrid currentSpriteGrid;

    public void AddSprite()
    {
        currentSpriteGrid = new SpriteGrid(16, 32);
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
        currentSpriteGrid.Draw(spriteBatch, scaleFactor, Point.Zero);
        currentSpriteGrid.DrawGrid(spriteBatch, scaleFactor, Color.Black);
        currentSpriteGrid.Draw(spriteBatch, 4, new Point(-UIVariables.Edition.Width, UIVariables.Margin + currentSpriteGrid.GridSize * currentSpriteGrid.CellSize));
    }
}
