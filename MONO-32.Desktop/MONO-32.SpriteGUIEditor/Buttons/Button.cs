using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MONO_32.SpriteGUIEditor.Buttons;

internal class Button
{
    public Texture2D Texture;

    public Rectangle Rectangle;

    public int Size = 32;

    public Button(int x, int y, Texture2D texture)
    {
        Texture = texture;
        Rectangle = new Rectangle(x, y, Size, Size);
    }
}
