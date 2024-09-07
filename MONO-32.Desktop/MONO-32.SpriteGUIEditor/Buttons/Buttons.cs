using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MONO_32.SpriteGUIEditor.Extensions;
using System.Collections.Generic;

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
                if (Values[i].Name == "save" && !string.IsNullOrWhiteSpace(UIVariables.TextFileName))
                {
                    var exportTexture = spriteGrid.ConvertToTexture2D(graphicsDevice);
                    exportTexture.SaveImageAsPng(UIVariables.TextFileName + ".png");
                }
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
