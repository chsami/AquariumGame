using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using AquariumGame._0.Particles;
using AquariumGame._0.User;
using AquariumGame._0.Entities;
using AquariumGame._0.GameInterfaces;
using AquariumGame._0.Timers;
using AquariumGame._0.Config;
using AquariumGame._0.InterfaceBuilder;
using AquariumGame._0.Input;
using AquariumGame._0.GameLevels;
using AquariumGame._0.GameEnvironment;
/**
 * 
 *
 * 
 **/

namespace AquariumGame._0.Screens
{
    class PlayScreen : GameScreen
    {
        public static bool pauseGame = false;
        public override void LoadAssets()
        {
            Level.currentLevel = 1;
            Level.percentageTillNextLevel = 0f;
            Player.Gold = 1000;
            Player.Score = 0;
            ScreenManager.friendly = new List<Friendly>();
            ScreenManager.food = new List<Food>();
            ScreenManager.buttons = new List<Button>();
            ScreenManager.images = new List<Image>();
            ScreenManager.coins = new List<Coins>();
            ScreenManager.enemies = new List<Enemy>();
            ScreenManager.fonts = new List<Fonts>();
            ScreenManager.particles = new List<Particle>();
            ScreenManager.AddTexture2D(ScreenManager.SPRITES_CURSORS + "cursor_damage");
            ScreenManager.AddTexture2D(ScreenManager.SPRITE_PLAYSCREEN + "test_background");
            #region loadFriendly
           // ScreenManager.addFriendly("normal_fish", new Vector2(100, 100), 100, 3, 1000, 255);
           // ScreenManager.addFriendly("carrot_fish", new Vector2(100, 100), 100, 3, 500, 255);
            //ScreenManager.addFriendly("rainbow_fish", new Vector2(100, 100), 100, 3, 300, 255);
            #endregion
            #region loadFonts
            ScreenManager.addFont("enemyFont", "NomNomNOm!", "SpriteFont1", new Vector2(0, 0), new Color(0, 0, 0, 255), false, 1000, 0);
            ScreenManager.addFont("friendlyFont", "NomNomNOm!", "SpriteFont1", new Vector2(0, 0), new Color(0, 0, 0, 255), false, 1000, 0);
            ScreenManager.addFont("playerStats", "RESERVED", "SpriteFont1", new Vector2(ScreenManager.GAMESCREEN_WIDTH / 2 - (11 * 5), 30), new Color(255, 0, 255, 255), false, 0, 0);
            #endregion
            ScreenManager.addButton("buy_normal_fish", ScreenManager.SPRITE_PLAYSCREEN + "buy_fish", new Vector2(50, 10), true, "", true, false, false, "", "", Color.Wheat, "");
            ScreenManager.addButton("non_clickable", ScreenManager.SPRITE_PLAYSCREEN + "gold_interface", new Vector2(Misc.scaleInterfacePosition(10, "w"), Misc.scaleInterfacePosition(500, "h")), false, "", true, false, false, "", "", Color.Wheat, "");
            #region drawPauseInterface
            ScreenManager.addImage("non_clickable_pause", "Interfaces/pause_interfaces/pause_interface", new Vector2(Misc.scaleInterfacePosition(375, "w"), Misc.scaleInterfacePosition(184, "h")), false, "", false, false);
            ScreenManager.addButton("continue_pause", "Interfaces/pause_interfaces/continue_button", new Vector2(Misc.scaleInterfacePosition(435, "w"), Misc.scaleInterfacePosition(225, "h")), false, "", false, false, false, "", "", Color.Wheat, "");
            ScreenManager.addButton("replay_pause", "Interfaces/pause_interfaces/replay_button", new Vector2(Misc.scaleInterfacePosition(535, "w"), Misc.scaleInterfacePosition(225, "h")), false, "", false, false, false, "", "", Color.Wheat, "");
            ScreenManager.addButton("menu_pause", "Interfaces/pause_interfaces/menu_button", new Vector2(Misc.scaleInterfacePosition(641, "w"), Misc.scaleInterfacePosition(227, "h")), false, "", false, false, false, "", "", Color.Wheat, "");
            ScreenManager.addImage("non_clickable_pause", "Interfaces/pause_interfaces/pause_banner", new Vector2(Misc.scaleInterfacePosition(465, "w"), Misc.scaleInterfacePosition(160, "h")), false, "", false, false);
            #endregion
            #region progressbar of a level
            ScreenManager.addImage("loadbar_empty_0", ScreenManager.SPRITE_PLAYSCREEN + "loadbar_0", new Vector2(Misc.scaleInterfacePosition(300, "w"), Misc.scaleInterfacePosition(550, "h")), false, "", true, false);
            ScreenManager.addImage("loadbar_empty_1", ScreenManager.SPRITE_PLAYSCREEN + "loadbar_1", new Vector2(Misc.scaleInterfacePosition(300, "w"), Misc.scaleInterfacePosition(550, "h")), false, "", true, true);
            #endregion
            #region nextLevel Notification
            ScreenManager.addImage("level_cleared", ScreenManager.SPRITE_PLAYSCREEN + "panel_level_cleared", new Vector2(Misc.scaleInterfacePosition(348, "w"), Misc.scaleInterfacePosition(22, "h")), false, "", false, false);
            #endregion
            ScreenManager.addFont("playerGold", Player.Gold.ToString(), "SpriteFont2", new Vector2(Misc.scaleInterfacePosition(100, "w"), Misc.scaleInterfacePosition(530, "h")), Color.WhiteSmoke, true, 0, 0);
            //InterfaceBuilder.InterfaceCreator.initTextures();
            ScreenManager.effect = ScreenManager.ContentMgr.Load<Effect>("Ambient");
        }
        
        public override void Update(GameTime gameTime)
        {
           // Console.WriteLine(EnemyManager.enemySpawnTimer);

            #region worldConfigurator
            if (WorldConfigurator.developerMode)
            {
                InterfaceCreator.Update();
                return;
            }
            #endregion
            #region buttonChecking
            foreach (var button in ScreenManager.buttons)
            {
                button.UpdateComponent();
                button.HandleButtonActions();
            }
            #endregion

            #region PauseGame
            if (MouseInput.LastMouseState.MiddleButton == ButtonState.Pressed && MouseInput.MouseState.MiddleButton == ButtonState.Released)
            {
                pauseGame = pauseGame == true ? false : true;
                foreach (var button in ScreenManager.buttons.ToList())
                {
                    if (button.Name.Contains("pause"))
                    {
                        button.Visible = pauseGame;
                    }
                }
                foreach (var image in ScreenManager.images.ToList())
                {
                    if (image.Name.Contains("pause"))
                    {
                        image.Visible = pauseGame;
                    }
                }
            }
            #region level handler
            Level.Update();
            #endregion
            if (pauseGame || Level.nextLevel)
                return;
            //level accelerator
            Level.percentageTillNextLevel += WorldConfigurator.NEXT_LEVEL_INCREMENT * (float)gameTime.ElapsedGameTime.Milliseconds;
            #endregion

            #region water Handling
            ScreenManager.water.Update();
            #endregion

            #region sun handling
            Sun.Update(gameTime);
            #endregion  

            #region FoodClicking
            Actions.clickForFood();
            foreach (var food in ScreenManager.food.ToList())
            {
                food.UpdateGameObject(gameTime);
                food.Update(gameTime);
            }
            #endregion

            #region Fish Wandering & Fish eating & Fish dropping money
            foreach (var friendly in ScreenManager.friendly.ToList())
            {
                friendly.UpdateEntity(gameTime);      
            }
            #endregion

            #region coins clicking
            foreach (var coin in ScreenManager.coins.ToList())
            {
                coin.UpdateGameObject(gameTime);
                coin.Update(gameTime);
            }
            #endregion

            #region Enemies
            Actions.hitEnemy();
            EnemyManager.Update(gameTime);
            foreach (var enemy in ScreenManager.enemies.ToList())
            {
                enemy.UpdateEntity(gameTime);
            }
            
            #endregion

            #region fonts
            foreach (var font in ScreenManager.fonts.ToList())
            {
                font.Update(gameTime);
            }
            #endregion
        }

        public override void Draw(GameTime gameTime)
        {
            
            ScreenManager.water.DrawToRenderTargets();
            #region waterDrawing
            ScreenManager.water.Draw();
            #endregion
            ScreenManager.Sprites.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            //Console.WriteLine(ScreenManager.getCurrentScreen());
            #region WorldConfigurator developer mode
            if (WorldConfigurator.developerMode)
            {
                InterfaceCreator.Draw();
                ScreenManager.Sprites.End();
                return;
            }
            #endregion
            
            

            
            #region draw Friendly
            foreach (var friendly in ScreenManager.friendly)
            {
                friendly.DrawEntity();
            }
            #endregion
            #region draw Food
            foreach (var food in ScreenManager.food)
            {
                food.Draw();
            }
            #endregion
            
            #region draw Coins
            if (Player.MoneyBuffer > 0)
            {
                Player.Gold += (Player.MoneyBuffer / 2);
                Player.MoneyBuffer -= (Player.MoneyBuffer / 2);
            }
            foreach (var coins in ScreenManager.coins)
            {
                coins.Draw();
            }
            #endregion
            #region draw Enemies
            foreach (var enemy in ScreenManager.enemies)
            {
                enemy.DrawEntity();
            }
            #endregion
            
            #region drawBackground
            foreach (var item in ScreenManager.Textures2D)
            {
                {
                    if (item.Key.Equals(ScreenManager.SPRITE_PLAYSCREEN + "test_background"))
                    {
                       // ScreenManager.effect.CurrentTechnique.Passes[0].Apply();
                        ScreenManager.Sprites.Draw(item.Value, new Rectangle(0, (int)ScreenManager.water.GetHeight(100), item.Value.Width, item.Value.Height * 2), Color.White);
                      //  ScreenManager.effect.CurrentTechnique.Passes[1].Apply();
                    }
                }
            }
            #endregion
            #region draw Buttons
            foreach (var button in ScreenManager.buttons)
            {
                button.DrawComponent();
            }
            foreach (var image in ScreenManager.images)
            {
                image.DrawComponent();
            }
            #endregion
            #region draw fonts
            foreach (var font in ScreenManager.fonts)
            {
                font.Draw();
            }
            #endregion
            #region Draw cursor_damage
            foreach (var item in ScreenManager.Textures2D)
            {
                {
                    if (item.Key.Equals(ScreenManager.SPRITES_CURSORS + "cursor_damage"))
                    {
                        if (ScreenManager.enemies.Count > 0)
                            ScreenManager.Sprites.Draw(item.Value, new Rectangle(MouseInput.getMouseX() - item.Value.Width / 2, MouseInput.getMouseY() - item.Value.Height / 2, item.Value.Width, item.Value.Height), Color.White);
                    }
                }
            }
            #endregion
            ScreenManager.Sprites.End();
        }
    }
}
