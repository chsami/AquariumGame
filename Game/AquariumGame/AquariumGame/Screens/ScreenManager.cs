using System;
using System.Collections.Generic;
using AquariumGame._0.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AquariumGame._0.Particles;
using AquariumGame._0.Entities;
using AquariumGame._0.GameInterfaces;
using AquariumGame._0.GameEnvironment;
using System.Threading;
using AquariumGame._0.Input;
using AquariumGame._0.Timers;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

namespace AquariumGame._0.Screens
{
    public class ScreenManager : Game
    {
        public static GraphicsDeviceManager GraphicsDeviceMgr;
        public static GraphicsDevice graphicsDevice;
        public static SpriteBatch Sprites;
        public static Dictionary<string, Texture2D> Textures2D;
        public static Dictionary<string, Texture3D> Textures3D;
        public static Dictionary<string, SpriteFont> Fonts;
        public static Dictionary<string, Model> Models;
        public static List<GameScreen> ScreenList;
        public static ContentManager ContentMgr;
        public static GameScreen currentScreen;
        public static GameScreen playScreen;
        public static GameScreen gameMenu;
        public static GameScreen splashScreen;

        public static List<Friendly> friendly;
        public static List<Food> food;
        public static List<Coins> coins;
        public static List<Enemy> enemies;
        public static List<Fonts> fonts;
        public static List<Particle> particles;

        public static Water water;
        public static Texture2D particleImage;

        public static Effect effect;

        //GameThreads
        public static List<Task> gameThread;

        //GameInterfaces
        public static List<Button> buttons;
        public static List<Image> images;
        public static List<TextBox> textboxes;

        public const int GAMESCREEN_WIDTH = 1200;
        public const int GAMESCREEN_HEIGHT = 600;

        public const int DEFAULT_SCREEN_WIDTH = 1200;
        public const int DEFAULT_SCREEN_HEIGHT = 600;

        //frame rate counter
        private static int frameRate = 0;
        private static int frameCounter = 0;
        private static TimeSpan elapsedTime = TimeSpan.Zero;

        //screenState
        public static int screenState = 0;

        //sprite const
        public const string SPRITES = "sprites/";
        public const string SPRITES_ENTITIES = "sprites/entities/";
        public const string SPRITES_GAMEOBJECTS = "sprites/gameobjects/";
        public const string SPRITES_CURSORS = "cursors/";
        public const string SPRITE_PARTICLES = "sprites/particles/";
        public const string SPRITE_GAME_MENU = "interfaces/game_menu/";
        public const string SPRITE_PLAYSCREEN = "interfaces/playscreen/";
        public const string SPRITE_SPLASHSCREEN = "interfaces/splashscreen/";


        public static void Main()
        {
            using (ScreenManager manager = new ScreenManager())
            {
                manager.IsMouseVisible = true;
                manager.Run();
               
            }
        }

        public ScreenManager()
        {
            GraphicsDeviceMgr = new GraphicsDeviceManager(this);

            GraphicsDeviceMgr.PreferredBackBufferWidth = GAMESCREEN_WIDTH;
            GraphicsDeviceMgr.PreferredBackBufferHeight = GAMESCREEN_HEIGHT;
            //GraphicsDeviceMgr.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Textures2D = new Dictionary<string, Texture2D>();
            Textures3D = new Dictionary<string, Texture3D>();
            Models = new Dictionary<string, Model>();
            Fonts = new Dictionary<string, SpriteFont>();
            playScreen = new PlayScreen();
            gameMenu = new GameMenu();
            splashScreen = new SplashScreen();
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            ContentMgr = Content;
            Sprites = new SpriteBatch(GraphicsDevice);
            // Load any full game assets here

            particleImage = ScreenManager.ContentMgr.Load<Texture2D>(SPRITE_PARTICLES + "metaparticle");
            water = new Water(GraphicsDevice, particleImage);
            switch (screenState)
            {
                case 0:
                    AddScreen(splashScreen);
                    break;
                case 1:
                    AddScreen(gameMenu);
                    break;
                case 2:
                    AddScreen(playScreen);
                    break;
                default:
                    break;
            }
        }

        protected override void UnloadContent()
        {
            foreach (var screen in ScreenList)
            {
                screen.UnloadAssets();
            }
            Textures2D.Clear();
            Textures3D.Clear();
            Fonts.Clear();
            Models.Clear();
            ScreenList.Clear();
            if (images != null)
                images.Clear();
            if (buttons != null)
                buttons.Clear();
            if (friendly != null)
                friendly.Clear();
            if (enemies != null)
                enemies.Clear();
            if (coins != null)
                coins.Clear();
            if (food != null)
                food.Clear();
            if (fonts != null)
                fonts.Clear();
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            try
            {
                // TODO Remove temp code
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
                if (currentScreen != null)
                {
                    if (screenState == 0 && currentScreen.GetType() != typeof(SplashScreen))
                    {
                        ChangeScreens(getCurrentScreen(), splashScreen);
                    }
                    else if (screenState == 1 && currentScreen.GetType() != typeof(GameMenu))
                    {
                        ChangeScreens(getCurrentScreen(), gameMenu);
                    }
                    else if (screenState == 2 && currentScreen.GetType() != typeof(PlayScreen))
                    {
                        ChangeScreens(getCurrentScreen(), playScreen);
                    }
                }
                foreach (var item in gameThread.ToArray())
                {
                    if (item.IsCompleted)
                    {
                        ScreenManager.gameThread.Remove(item);
                    }
                }
                #region mouseinput
                MouseInput.LastMouseState = MouseInput.MouseState;

                // Get the mouse state relevant for this frame
                MouseInput.MouseState = Mouse.GetState();
                #endregion
                #region keyboardInput
                //keyboard inputs
                KeyBoardInput.handleOverallInput();
                #endregion
                PlayTime.UpdatePlayTime();
                #region framerate
                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromSeconds(1))
                {
                    elapsedTime -= TimeSpan.FromSeconds(1);
                    frameRate = frameCounter;
                    frameCounter = 0;
                }
                #endregion
                var startIndex = ScreenList.Count - 1;
                while (ScreenList[startIndex].IsPopup && ScreenList[startIndex].IsActive)
                {
                    startIndex--;
                }
                for (var i = startIndex; i < ScreenList.Count; i++)
                {
                    ScreenList[i].Update(gameTime);
                }
            }
            catch (Exception ex)
            {
                // ErrorLog.AddError(ex);
                throw ex;
            }
            finally
            {
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            var startIndex = ScreenList.Count - 1;
            while (ScreenList[startIndex].IsPopup)
            {
                startIndex--;
            }
            GraphicsDevice.Clear(ScreenList[startIndex].BackgroundColor);
            GraphicsDeviceMgr.GraphicsDevice.Clear(ScreenList[startIndex].BackgroundColor);

            for (var i = startIndex; i < ScreenList.Count; i++)
            {
                frameCounter++;
                ScreenList[i].Draw(gameTime);
                currentScreen = ScreenList[i];
            }
            base.Draw(gameTime);
        }


        public static void AddFont(string fontName)
        {
            if (Fonts == null)
            {
                Fonts = new Dictionary<string, SpriteFont>();
            }
            if (!Fonts.ContainsKey(fontName))
            {
                Fonts.Add(fontName, ContentMgr.Load<SpriteFont>(fontName));
            }
        }

        public static void RemoveFont(string fontName)
        {
            if (Fonts.ContainsKey(fontName))
            {
                Fonts.Remove(fontName);
            }
        }

        public static void AddTexture2D(string textureName)
        {
            if (Textures2D == null)
            {
                Textures2D = new Dictionary<string, Texture2D>();
            }
            if (!Textures2D.ContainsKey(textureName))
            {
                Textures2D.Add(textureName, ContentMgr.Load<Texture2D>(SPRITES + textureName));
            }
        }

        public static void RemoveTexture2D(string textureName)
        {
            if (Textures2D.ContainsKey(textureName))
            {
                Textures2D.Remove(textureName);
            }
        }

        public static void AddTexture3D(string textureName)
        {
            if (Textures3D == null)
            {
                Textures3D = new Dictionary<string, Texture3D>();
            }
            if (!Textures3D.ContainsKey(textureName))
            {
                Textures3D.Add(textureName, ContentMgr.Load<Texture3D>(textureName));
            }
        }

        public static void RemoveTexture3D(string textureName)
        {
            if (Textures3D.ContainsKey(textureName))
            {
                Textures3D.Remove(textureName);
            }
        }

        public static void AddModel(string modelName)
        {
            if (Models == null)
            {
                Models = new Dictionary<string, Model>();
            }
            if (!Models.ContainsKey(modelName))
            {
                Models.Add(modelName, ContentMgr.Load<Model>(modelName));
            }
        }

        public static void RemoveModel(string modelName)
        {
            if (Models.ContainsKey(modelName))
            {
                Models.Remove(modelName);
            }
        }

        public static void AddScreen(GameScreen gameScreen)
        {
            if (ScreenList == null)
            {
                ScreenList = new List<GameScreen>();
            }
            ScreenList.Add(gameScreen);
            gameScreen.LoadAssets();
        }

        public static void RemoveScreen(GameScreen gameScreen)
        {
            gameScreen.UnloadAssets();
            ScreenList.Remove(gameScreen);
            if(ScreenList.Count < 1)
                AddScreen(new GameMenu());
        }

        public static void ChangeScreens(GameScreen currentScreen, GameScreen targetScreen)
        {
            currentScreen.UnloadAssets();
            targetScreen.LoadAssets();
            AddScreen(targetScreen);
        }

        public static GameScreen getCurrentScreen() 
        {
            foreach (var screen in ScreenList)
	        {
                if (currentScreen == screen)
                    return screen;
	        }
            return null;
        }
        public static void addFriendly(string name, Vector2 position, int health, int size, int dropRate, int hungerLevel) 
        {
            friendly.Add(new Friendly(name, ContentMgr.Load<Texture2D>(SPRITES_ENTITIES + name), position, health, size, dropRate, hungerLevel));
        }

        public static void addFood()
        {
            food.Add(new Food(ContentMgr.Load<Texture2D>(SPRITES_GAMEOBJECTS + "pill"), new Vector2(MouseInput.getMouseX(), 0)));
        }

        public static void addButton(string btnName, string textureName, Vector2 position, bool clickable, string hoverableText, bool visible, bool animated, bool font, string fontName, string fontType, Color fontColor, string fontText)
        {
            try {
                Texture2D hoverableTexture = ContentMgr.Load<Texture2D>(SPRITES + textureName + "_h");
                buttons.Add(new Button(btnName, ContentMgr.Load<Texture2D>(SPRITES + textureName), position, clickable, hoverableText, hoverableTexture, visible, animated, font, fontName, fontType, fontColor, fontText));
            } catch(Exception) {
                buttons.Add(new Button(btnName, ContentMgr.Load<Texture2D>(SPRITES + textureName), position, clickable, hoverableText, null, visible, animated, font, fontName, fontType, fontColor, fontText));
            }
        }

        public static void addImage(string imageName, string textureName, Vector2 position, bool clickable, string hoverableText, bool visible, bool changeSize)
        {
            try
            {
                Texture2D hoverableTexture = ContentMgr.Load<Texture2D>(SPRITES + textureName + "_h");
                images.Add(new Image(imageName, ContentMgr.Load<Texture2D>(SPRITES + textureName), position, clickable, hoverableText, hoverableTexture, visible, changeSize));
            }
            catch (Exception)
            {
                images.Add(new Image(imageName, ContentMgr.Load<Texture2D>(SPRITES + textureName), position, clickable, hoverableText, null, visible, changeSize));
            }
        }

        public static void addImage(string imageName, string textureName, Vector2 position, bool clickable, string hoverableText, bool visible, Point point)
        {
            try
            {
                Texture2D hoverableTexture = ContentMgr.Load<Texture2D>(SPRITES + textureName + "_h");
                images.Add(new Image(imageName, ContentMgr.Load<Texture2D>(SPRITES + textureName), position, clickable, hoverableText, hoverableTexture, visible, point));
            }
            catch (Exception)
            {
                images.Add(new Image(imageName, ContentMgr.Load<Texture2D>(SPRITES + textureName), position, clickable, hoverableText, null, visible, point));
            }
        }

        public static void addCoin(string name, string textureName, Vector2 position, int totalFrames, float timeBetweenFrames)
        {
            coins.Add(new Coins(name, ContentMgr.Load<Texture2D>(SPRITES_GAMEOBJECTS + textureName), position, totalFrames, timeBetweenFrames));
        }

        public static void addEnemy(string name, string textureName, Vector2 position, int health, int damage)
        {
            enemies.Add(new Enemy(name, ContentMgr.Load<Texture2D>(SPRITES_ENTITIES + textureName), position, health, damage));
        }

        public static void addFont(string name, string text, string fontName, Vector2 position, Color color, bool visible, int timer, int letterTimer)
        {
            if (timer == 0 && letterTimer == 0)
            {
                fonts.Add(new Fonts(name, text, ContentMgr.Load<SpriteFont>("fonts/" + fontName), position, color, visible));
            }
            else
            {
                fonts.Add(new Fonts(name, text, ContentMgr.Load<SpriteFont>("fonts/" + fontName), position, color, visible, timer, letterTimer));
            }
        }

        public static void addTextbox(string name, Vector2 position, Point size, bool focus, string text)
        {
            textboxes.Add(new TextBox(name, ContentMgr.Load<Texture2D>(SPRITES + "interfaces/game_menu/menu_panel"), position, size, focus, text));
        }
    }
}
