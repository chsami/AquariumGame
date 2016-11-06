using AquariumGame._0.Entities;
using AquariumGame._0.Screens;
using AquariumGame._0.User;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AquariumGame._0.GameInterfaces
{
    public class Button : Components
    {
        public Button(string name, Texture2D texture, Vector2 position, bool clickable, string hoverableText, Texture2D hoverableTexture, bool visible, bool animated, bool font, string fontName, string fontType, Color fontColor, string fontText)
        {
            Name = name;
            Texture = texture;
            OriginalTexture = texture;
            Position = position;
            Clickable = clickable;
            HoverableText = hoverableText;
            HoverableTexture = hoverableTexture;
            Visible = visible;
            ComponentColor = Color.White;
            this.Animated = animated;
            if (animated)
            {
                TextureWidth = 0;
                TextureHeight = 0;
            }
            else
            {
                TextureWidth = Texture.Width;
                TextureHeight = Texture.Height;
            }
            if (font)
            {
                ScreenManager.addFont(fontName, fontText, fontType, new Vector2(Position.X + 20, Position.Y + Texture.Height / 2 - 14), fontColor, Visible, 0, 0);
                TextureWidth = fontText.Length * 18;
            }
        }

        /**
         * Actions performed by every button
         * */
        public void HandleButtonActions()
        {
            string text;
            IAsyncResult result;
            StorageDevice device;
            if (clickComponent())
            {
                switch (Name)
                {
                    case "buy_normal_fish":
                        if (Player.Gold >= 10)
                        {
                            ScreenManager.addFriendly("normal_fish", new Vector2(100, 100), 100, 3, 1000, 100);
                            Player.Gold -= 10;
                        }
                        break;
                    case "continue_pause":
                        PlayScreen.pauseGame = false;
                        break;
                    case "replay_pause":
                        ScreenManager.ChangeScreens(ScreenManager.getCurrentScreen(), ScreenManager.playScreen);
                        EnemyManager.enemySpawnTimer = Enemy.ENEMY_SPAWN;
                        //reset total game time here
                        PlayScreen.pauseGame = false;
                        break;
                    case "menu_pause":
                        break;
                    case "menu_button_1": //start playing
                        foreach (TextBox item in ScreenManager.textboxes)
                        {
                            if (item.Name.Equals("username"))
                            {
                                item.Visible = true;
                            }
                        }
                        foreach (Fonts item in ScreenManager.fonts)
                        {
                            if (item.Name.Equals("username") || item.Name.Equals("back_to_menu") || item.Name.Equals("start_game"))
                            {
                                item.Visible = true;
                            }
                        }
                        foreach (Button item in ScreenManager.buttons)
                        {
                            if (item.Name.Equals("back_to_menu") || item.Name.Equals("start_game"))
                            {
                                item.Visible = true;
                            }
                        }
                        ScreenManager.gameThread.Add(Task.Factory.StartNew(() =>
                        {
                            foreach (Fonts item in ScreenManager.fonts)
                            {
                                if (item.Name.Equals("username"))
                                {
                                    while (item.Visible)
                                    {
                                        foreach (TextBox textbox in ScreenManager.textboxes)
                                        {
                                            if (textbox.Name.Equals("username") && textbox.Focus == false)
                                            {
                                                text = "";
                                                text = item.Text;
                                                item.Text = "|";
                                                Thread.Sleep(500);
                                                item.Text = text;
                                                Thread.Sleep(500);
                                            }
                                        }
                                        Thread.Sleep(250);
                                    }
                                }
                            }
                            
                        }));
                        GameMenu.popup = true;
                        break;
                    case "menu_button_2":
                        //settings
                        break;
                    case "menu_button_3":
                        result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                        device = StorageDevice.EndShowSelector(result);
                        result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                        device = StorageDevice.EndShowSelector(result);
                        FileHandler.FileManager.saveGame(device);
                        break;
                    case "back_to_menu":
                        foreach (TextBox item in ScreenManager.textboxes)
                        {
                            if (item.Name.Equals("username"))
                            {
                                item.Visible = false;
                                item.Focus = false;
                            }
                        }
                        foreach (Fonts item in ScreenManager.fonts)
                        {
                            if (item.Name.Equals("username") || item.Name.Equals("back_to_menu") || item.Name.Equals("start_game"))
                            {
                                item.Visible = false;
                            }
                        }
                        foreach (Button item in ScreenManager.buttons)
                        {
                            if (item.Name.Equals("back_to_menu") || item.Name.Equals("start_game"))
                            {
                                item.Visible = false;
                            }
                        }
                        GameMenu.popup = false;
                        break;
                    case "start_game":
                         result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                        device = StorageDevice.EndShowSelector(result);
                        text = "";
                        foreach (Fonts item in ScreenManager.fonts)
	                    {
		                    if (item.Name.Equals("username")) 
                            {
                                text = item.Text;
                                Console.WriteLine(text);
                            }
	                    }
                        if (FileHandler.FileManager.loadGame(device, text)) 
                        {
                            GameMenu.popup = false;
                            foreach (Button item in ScreenManager.buttons)
                            {
                                if (!item.Name.Equals(Name))
                                    item.Visible = false;
                            }
                            foreach (Fonts item in ScreenManager.fonts)
                            {
                                if (!item.Name.Equals(Name))
                                    item.Visible = false;
                            }
                            ScreenManager.gameThread.Add(Task.Factory.StartNew(() => 
                            {
                                while (Position.X > 0 - Texture.Width)
                                {
                                    Position = new Vector2(Position.X - 5, Position.Y);
                                    foreach (var item in ScreenManager.fonts)
                                    {
                                        if (item.Name == Name)
                                        {
                                            item.Position = new Vector2(item.Position.X - 5, item.Position.Y);
                                        }
                                    }
                                    Thread.Sleep(16);
                                }
                                ScreenManager.screenState = 2;
                            }));
                        }
                        else
                        {
                            foreach (Fonts item in ScreenManager.fonts)
                            {
                                if (item.Name.Equals("username"))
                                {
                                    item.Text = "SAVE FILE NOT FOUND!";
                                }
                            }
                        }
                       
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
