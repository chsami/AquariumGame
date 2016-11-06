using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquariumGame._0.User;
using System.Threading;
using System.Threading.Tasks;

namespace AquariumGame._0
{
    public class Fonts
    {
        string name;
        string text;
        SpriteFont font;
        Vector2 position;
        Color color;
        bool visible;
        private int timer;
        private bool startThread = false;

        public string Name { get { return name; } set { name = value; } }

        public bool Visible { get { return visible; } set { visible = value; } }

        public Vector2 Position { get { return position; } set { position = value; } }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        


        public Fonts(string name,  string text, SpriteFont font, Vector2 position, Color color, bool visible)
        {
            this.name = name;
            this.text = text;
            this.font = font;
            this.position = position;
            this.color = color;
            Visible = visible;
        }

        public Fonts(string name,  string text, SpriteFont font, Vector2 position, Color color, bool visible, int timer, int letterTimer)
            : this(name, text, font, position, color, visible)
        {
            if (letterTimer > 0)
            {
                this.text = "";
                Screens.ScreenManager.gameThread.Add(Task.Factory.StartNew(() =>
                {
                    int counter = 0;
                    while (counter < text.Length)
                    {
                        Thread.Sleep(letterTimer);
                        if (Visible)
                            this.text += text[counter++];
                    }
                }));
            }
            this.timer = timer;
        }

        public void Update(GameTime gameTime)
        {
            if (Name == "playerGold")
            {
                if (Player.Gold > 1000000)
                {
                    text = (Player.Gold / 1000000).ToString() + "M";
                }
                else
                {
                    text = (Player.Gold).ToString();
                }
            }
        }

        public void Draw()
        {
            if (visible && timer > 0 && startThread == false)
            {
                startThread = true;
                Screens.ScreenManager.gameThread.Add(Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(timer);
                    Visible = false;
                    startThread = false;
                }));
            }
            if (visible)
            {
                Screens.ScreenManager.Sprites.DrawString(font, text, new Vector2(position.X, position.Y), color);
            }
        }
    }
}
