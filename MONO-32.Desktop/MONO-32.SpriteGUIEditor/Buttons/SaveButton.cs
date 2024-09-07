using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MONO_32.SpriteGUIEditor.Buttons;

internal class SaveButton : Button
{

    public Texture2D Texture;

    public Rectangle Hitbox;

    public SaveButton(int x, int y, Texture2D texture) : base(x, y, texture)
    {
    }
}
