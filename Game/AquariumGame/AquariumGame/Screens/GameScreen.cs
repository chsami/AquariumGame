using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace AquariumGame._0.Screens
{
    public class GameScreen
    {
        public bool IsActive = true;
        public bool IsPopup = false;
        public Color BackgroundColor = Color.Black;

        public virtual void LoadAssets() {}
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
        public virtual void UnloadAssets() { }
    }
}
