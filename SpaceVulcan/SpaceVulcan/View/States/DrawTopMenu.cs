using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.View.States
{
    
    public class DrawTopMenu
    {
        GameTime gameTime;
        private ContentManager content = Program.game.Content;
        private SpriteBatch spriteBatch;
        private SpriteFont menuTitle;
        private SpriteFont menuOptions;
        private Texture2D background;
        private GraphicsDevice graphicsDevice;

        public DrawTopMenu(GameTime gameTime)
        {
            this.gameTime = gameTime;
            this.spriteBatch = Program.game.spriteBatch;
            this.menuTitle = content.Load<SpriteFont>("Fonts/MenuTitle");
            this.menuOptions = content.Load<SpriteFont>("Fonts/MenuOptions");
            this.background = content.Load<Texture2D>("Backgrounds/Stars2");
            this.graphicsDevice = Program.game.GraphicsDevice;

        }
        
        
        public void Draw()
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height), Color.White);
            spriteBatch.DrawString(menuTitle, "SPACE", new Vector2(600, 50), Color.Red);
            spriteBatch.DrawString(menuTitle, "VULCAN", new Vector2(545, 215), Color.Red);
            spriteBatch.DrawString(menuOptions, "Play", new Vector2(850, 575), Color.White);
            spriteBatch.DrawString(menuOptions, "Options", new Vector2(800, 650), Color.White);
            spriteBatch.DrawString(menuOptions, "Exit", new Vector2(850, 725), Color.White);
        }
    }
}
