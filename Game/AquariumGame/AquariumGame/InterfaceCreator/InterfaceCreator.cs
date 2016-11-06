using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AquariumGame._0.InterfaceBuilder
{
    public class InterfaceCreator
    {
        static Texture2D[] textures;
        static string[] names;
        static Vector2 position;
        static Texture2D currentSelectedTexture;
        static int currentSelectedIndex = -1;
        static Dictionary<Point, Texture2D> tempTextures = new Dictionary<Point, Texture2D>();

        public static Dictionary<string, Texture2D> loadTextures()
        {
            DirectoryInfo dir = new DirectoryInfo(Screens.ScreenManager.ContentMgr.RootDirectory + "/" + Screens.ScreenManager.SPRITES);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            Dictionary<String, Texture2D> result = new Dictionary<String, Texture2D>();

            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);


                result[key] = Screens.ScreenManager.ContentMgr.Load<Texture2D>(Screens.ScreenManager.SPRITES + key);
            }
            textures = new Texture2D[result.Count];
            names = new string[result.Count];
            return result;
        }

        public static void initTextures()
        {
            Dictionary<string, Texture2D> images = loadTextures();
            int counter = 0;
            foreach (var item in images)
            {
                textures[counter] = item.Value;
                names[counter] = item.Key;
                counter++;
            }
        }

        private static void selectTexture()
        {
            if (MouseInput.LastMouseState.MiddleButton == ButtonState.Pressed && MouseInput.MouseState.MiddleButton == ButtonState.Released)
            {
                currentSelectedTexture = textures[currentSelectedIndex = currentSelectedIndex > textures.Length - 2 ? 0 : ++currentSelectedIndex];
                currentSelectedTexture.Name = names[currentSelectedIndex];
            }
            if (MouseInput.LastMouseState.LeftButton == ButtonState.Pressed && MouseInput.MouseState.LeftButton == ButtonState.Released)
            {
                if (currentSelectedTexture != null)
                {
                    foreach (var item in tempTextures.ToList())
                    {
                        if (item.Key.Equals(new Point(MouseInput.getMouseX(), MouseInput.getMouseY())))
                            return;
                    }
                    tempTextures.Add(new Point(MouseInput.getMouseX(), MouseInput.getMouseY()), currentSelectedTexture);
                    saveCodeIntoTxt(new Point(MouseInput.getMouseX(), MouseInput.getMouseY()), currentSelectedTexture.Name);
                }
            }
        }

        private static void handleSelectedTexture()
        {
            if (currentSelectedTexture != null)
            {
                position = new Vector2((float) MouseInput.getMouseX(), (float) MouseInput.getMouseY());
            }
        }

        public static void Update()
        {
            selectTexture();
            handleSelectedTexture();
        }

        private static void saveCodeIntoTxt(Point p, string name)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(Screens.ScreenManager.ContentMgr.RootDirectory + "/ICBUILDER.txt", true);
            file.WriteLine("Coordinations : " + p + " || Name : " + name);
            file.Close();
        }

        public static void Draw()
        {
            if (currentSelectedTexture != null)
                Screens.ScreenManager.Sprites.Draw(currentSelectedTexture, new Rectangle((int)position.X, (int)position.Y, currentSelectedTexture.Width, currentSelectedTexture.Height), Color.White);
            foreach (var item in tempTextures.ToList())
            {
                Screens.ScreenManager.Sprites.Draw(item.Value, new Rectangle(item.Key.X, item.Key.Y, item.Value.Width, item.Value.Height), Color.White);
            }
        }
    }
}
