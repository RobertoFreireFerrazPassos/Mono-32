using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MONO_32.SpriteGUIEditor.Extensions;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace MONO_32.SpriteGUIEditor.Buttons;

internal class Buttons
{
    private List<Button> Values = new List<Button>();

    public Buttons(List<Button> buttons)
    {
        Values = buttons;
    }

    public void Update(Point mousePosition, SpriteGrid spriteGrid, GraphicsDevice graphicsDevice)
    {
        for (int i = 0; i < Values.Count; i++)
        {
            if (Values[i].Rectangle.Contains(mousePosition))
            {
                var exportTexture = spriteGrid.ConvertToTexture2D(graphicsDevice);
                exportTexture.SaveImageAsPng("wall.png");
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Values.Count; i++)
        {
            spriteBatch.Draw(Values[i].Texture, Values[i].Rectangle, Color.White);
        }
    }
}
