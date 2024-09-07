﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MONO_32.SpriteGUIEditor.Extensions;
using System.Collections.Generic;

namespace MONO_32.SpriteGUIEditor.Buttons;

internal class Buttons
{
    private List<Button> Values = new List<Button>();
    private Button selectedButton;

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
                switch (Values[i].Type)
                {
                    case Enums.ButtonTypeEnum.Save:
                        if (!string.IsNullOrWhiteSpace(UIVariables.TextFileName))
                        {
                            var exportTexture = spriteGrid.ConvertToTexture2D(graphicsDevice);
                            exportTexture.SaveImageAsPng(UIVariables.TextFileName + ".png");
                        }
                        break;
                    case Enums.ButtonTypeEnum.Bucket:
                        selectedButton = Values[i]; 
                        UIVariables.PaintMode = Enums.PaintModeEnum.Bucket;
                        break;
                    case Enums.ButtonTypeEnum.Pencil:
                        selectedButton = Values[i]; 
                        UIVariables.PaintMode = Enums.PaintModeEnum.Pencil;
                        break;
                    case Enums.ButtonTypeEnum.Eraser:
                        selectedButton = Values[i]; 
                        UIVariables.PaintMode = Enums.PaintModeEnum.Eraser;
                        break;
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Values.Count; i++)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            var color = Values[i] == selectedButton ? Color.Green : Color.Black;
            spriteBatch.Draw(Values[i].Texture, Values[i].Rectangle, color);
            spriteBatch.End();
        }
    }
}
