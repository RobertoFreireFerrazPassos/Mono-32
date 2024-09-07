using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MONO_32.SpriteGUIEditor.Buttons;

internal class Button
{
    public string Name;

    public Texture2D Texture;

    public Rectangle Rectangle;

    public int Size = 32;

    public Button(string name, int x, int y, Texture2D texture)
    {
        Name = name;
        Texture = texture;
        Rectangle = new Rectangle(x, y, Size, Size);
    }
}
