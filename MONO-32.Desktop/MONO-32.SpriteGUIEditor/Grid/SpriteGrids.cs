using Grid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MONO_32.SpriteGUIEditor.Grid;

internal class SpriteGrids
{
    private List<SpriteGrid> spriteGrids = new List<SpriteGrid>();
    public SpriteGrid currentSpriteGrid;

    public void AddSprite()
    {
        currentSpriteGrid = new SpriteGrid(16, 32);
        spriteGrids.Add(currentSpriteGrid);
    }

    public void UpdateMouseLeftClicked(Point mousePosition)
    {
        currentSpriteGrid.UpdateMouseLeftClicked(mousePosition);
    }

    public void Update()
    {
        currentSpriteGrid.Update();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        currentSpriteGrid.Draw(spriteBatch);
    }
}
