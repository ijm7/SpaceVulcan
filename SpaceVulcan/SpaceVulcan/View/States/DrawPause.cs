using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceVulcan.View.States
{
    public sealed class DrawPause
    {
        private static readonly DrawPause instance = new DrawPause();
        private GraphicsDevice graphicsDevice;
        private SpriteFont menuOptions;
        private SpriteBatch spriteBatch;
        static DrawPause()
        {

        }
        public DrawPause()
        {
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            this.menuOptions = Program.game.Content.Load<SpriteFont>("Fonts/MenuOptions");
        }
        public static DrawPause Instance
        {
            get
            {
                return instance;
            }
        }

        public void Draw()
        {
            spriteBatch.DrawString(menuOptions, "PAUSE", new Vector2(850, 500), Color.White);
        }

    }
}
