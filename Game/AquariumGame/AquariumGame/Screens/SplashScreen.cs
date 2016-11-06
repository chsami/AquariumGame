using AquariumGame._0.GameInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AquariumGame._0.Screens
{
    class SplashScreen : GameScreen
    {

        static float alpha = 1f;

        public override void LoadAssets()
        {
            ScreenManager.images = new List<Image>();
            ScreenManager.addImage("splashScreen_1", ScreenManager.SPRITE_SPLASHSCREEN + "splashScreen_1", new Vector2(Misc.scaleInterfacePosition(300, "w"), ScreenManager.DEFAULT_SCREEN_HEIGHT / 2), false, "", true, true);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var item in ScreenManager.images.ToList())
            {
                item.UpdateComponent();
            }
        }



        public override void Draw(GameTime gameTime)
        {
            if (alpha > 0)
                alpha -= 0.0025f;
            if (alpha < 0)
            {
                alpha = 0f;
                ScreenManager.screenState = 1;
            }
            Console.WriteLine(alpha);
            ScreenManager.Sprites.Begin();
            foreach (var item in ScreenManager.images.ToList())
            {
                if (item.Name == "splashScreen_1" && item.TextureWidth < item.OriginalTexture.Width)
                {
                    item.Position = new Vector2((float) (item.Position.X + Misc.generaterValue(1, -10, 10)), item.Position.Y);     
                }
                if (item.Name == "splashScreen_1")
                    item.ComponentColor = Color.White * alpha;
                item.DrawComponent();
            }
            ScreenManager.Sprites.End();
        }
    }
}
