using AquariumGame._0.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AquariumGame._0.GameInterfaces
{
    public class TextBox : Components
    {
        
        private bool focus;

        public bool Focus
        {
            get { return focus; }
            set { focus = value; }
        }
        
        public TextBox(string name, Texture2D texture, Vector2 position, Point size, bool focus, string text)
        {
            this.Name = name;
            this.Texture = this.OriginalTexture = texture;
            this.Position = position;
            this.TextureWidth = size.X;
            this.TextureHeight = size.Y;
            this.focus = focus;
            this.Visible = false;
            ComponentColor = Color.Aquamarine;
            ScreenManager.addFont(name, text, "SpriteFont3", new Vector2(Misc.scaleInterfacePosition(position.X + 10, "w"), Misc.scaleInterfacePosition(position.Y + size.Y / 4, "h")), Color.Black, false, 0, 0);
        }
        private void resetAllFocus()
        {
            foreach (TextBox item in ScreenManager.textboxes)
            {
                if (item.focus)
                {
                    item.focus = false;
                    item.ComponentColor = Color.Aquamarine;
                }
            }
            foreach (Fonts item in ScreenManager.fonts)
            {
                if (item.Name == Name)
                    item.Text = "";
            }
        }

        public bool handleText(Keys key)
        {
            if (focus)
            {
                foreach (Fonts item in ScreenManager.fonts)
                {
                    if (item.Name == Name)
                    {
                        if (Keys.Back == key)
                        {
                            item.Text = item.Text.Length > 0 ? item.Text.Remove(item.Text.Length - 1) : item.Text;
                        }
                        else if (Keys.Space == key)
                        {
                            item.Text += " ";
                        }
                        else
                        {
                            item.Text += key;
                        }
                        if (item.Text.Length > 1)
                        {
                            item.Text = char.ToUpper(item.Text[0]) + item.Text.Substring(1).ToLower();
                        }
                        else if (item.Text.Length == 1)
                        {
                            item.Text = char.ToUpper(item.Text[0]).ToString();
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public override void UpdateComponent()
        {
            if (clickComponent())
            {
                resetAllFocus();
                focus = true;
                ComponentColor = Color.White;
            }

        }
    }
}
