using AquariumGame._0.GameInterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquariumGame._0.Particles;
using Microsoft.Xna.Framework.Graphics;

namespace AquariumGame._0.Screens
{
    class GameMenu : GameScreen
    {
        ParticleManager particleManager;
        public static bool popup;
        public override void LoadAssets()
        {
            popup = false;
            ScreenManager.images = new List<Image>();
            ScreenManager.buttons = new List<Button>();
            ScreenManager.textboxes = new List<TextBox>();
            ScreenManager.fonts = new List<Fonts>();
            particleManager = new Particles.ParticleManager();
            particleManager.LoadContent();
            ScreenManager.addImage("menu_background", ScreenManager.SPRITE_GAME_MENU + "menu_background", new Vector2(Misc.scaleInterfacePosition(0, "w"), Misc.scaleInterfacePosition(0, "h")), true, "", true, new Point(ScreenManager.DEFAULT_SCREEN_WIDTH, ScreenManager.DEFAULT_SCREEN_HEIGHT));
            //ScreenManager.addImage("menu_panel", "menu_panel", new Vector2(Misc.scaleInterfacePosition(100, "w"), Misc.scaleInterfacePosition(100, "w")), true, "", true, false);
            ScreenManager.addButton("menu_button_1", ScreenManager.SPRITE_GAME_MENU + "menu_panel", new Vector2(Misc.scaleInterfacePosition(100, "w"), Misc.scaleInterfacePosition(100, "h")), true, "", true, true, true, "menu_button_1", "SpriteFont1", Color.NavajoWhite, "Start Now!");
            ScreenManager.addButton("menu_button_2", ScreenManager.SPRITE_GAME_MENU + "menu_panel", new Vector2(Misc.scaleInterfacePosition(100, "w"), Misc.scaleInterfacePosition(250, "h")), true, "", true, true, true, "menu_button_2", "SpriteFont1", Color.NavajoWhite, "Settings");
            ScreenManager.addButton("menu_button_3", ScreenManager.SPRITE_GAME_MENU + "menu_panel", new Vector2(Misc.scaleInterfacePosition(100, "w"), Misc.scaleInterfacePosition(400, "h")), true, "", true, true, true, "menu_button_3", "SpriteFont1", Color.NavajoWhite, "Quit");
            ScreenManager.addTextbox("username", new Vector2(Misc.scaleInterfacePosition(600 - 250 /2, "w"), Misc.scaleInterfacePosition(100, "h")), new Point(250, 50), false, "username");
            ScreenManager.addButton("start_game", ScreenManager.SPRITE_GAME_MENU + "menu_panel", new Vector2(Misc.scaleInterfacePosition(600 - 250 / 2, "w"), Misc.scaleInterfacePosition(200, "h")), true, "", false, true, true, "start_game", "SpriteFont1", Color.NavajoWhite, "Start/Load game");
            ScreenManager.addButton("back_to_menu", ScreenManager.SPRITE_GAME_MENU + "menu_panel", new Vector2(Misc.scaleInterfacePosition(600 - 250 / 2, "w"), Misc.scaleInterfacePosition(350, "h")), true, "", false, true, true, "back_to_menu", "SpriteFont1", Color.NavajoWhite, "Back");
            ScreenManager.effect = ScreenManager.ContentMgr.Load<Effect>("Ambient");
        }

        public override void UnloadAssets()
        {
            particleManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            particleManager.Update(gameTime);
            
            foreach (Button item in ScreenManager.buttons.ToList())
            {
                if (popup)
                {
                    if (item.Name.Equals("back_to_menu") || item.Name.Equals("start_game"))
                    {
                        item.UpdateComponent();
                        item.HandleButtonActions();
                    }
                }
                else
                {
                    item.UpdateComponent();
                    item.HandleButtonActions();
                }
            }
            foreach (TextBox item in ScreenManager.textboxes.ToList())
            {
                item.UpdateComponent();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.Sprites.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            if (popup)
            {
                ScreenManager.effect.CurrentTechnique.Passes[0].Apply();
            }
            foreach (Image item in ScreenManager.images.ToList())
            {
                item.DrawComponent();
            } 
            foreach (Button item in ScreenManager.buttons.ToList())
            {
                if (item.Name.Equals("back_to_menu") || item.Name.Equals("start_game") && popup)
                {
                    ScreenManager.effect.CurrentTechnique.Passes[1].Apply();
                    item.DrawComponent();
                }
                else if (popup)
                {
                    ScreenManager.effect.CurrentTechnique.Passes[0].Apply();
                    item.DrawComponent();
                }
                else
                {
                    item.DrawComponent();
                }
            }
            ScreenManager.effect.CurrentTechnique.Passes[1].Apply();
            foreach (TextBox item in ScreenManager.textboxes.ToList())
            {
                item.DrawComponent();
            }
            foreach (Fonts item in ScreenManager.fonts.ToList())
            {
                item.Draw();
            }
            particleManager.Draw();
            ScreenManager.Sprites.End();
        }
    }
}
