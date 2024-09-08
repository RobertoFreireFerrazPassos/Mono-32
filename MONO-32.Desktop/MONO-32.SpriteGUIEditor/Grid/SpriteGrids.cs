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
        var miniatureScale = 4;
        var totalSize = currentSpriteGrid.GridSize * currentSpriteGrid.CellSize;
        currentSpriteGrid.Draw(spriteBatch, scaleFactor, Point.Zero);
        currentSpriteGrid.DrawGrid(spriteBatch, scaleFactor, Color.Black);

        for (int i = 0; i < spriteGrids.Count; i++)
        {
            spriteGrids[i].Draw(spriteBatch, miniatureScale, new Point(-UIVariables.Edition.Width + i * totalSize/miniatureScale, 2 * UIVariables.Margin + totalSize));
        }
    }
}
