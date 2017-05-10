using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.View.States
{
    class DrawEnd
    {
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteFont menuOptions;

        public DrawEnd()
        {
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            this.menuOptions = Program.game.Content.Load<SpriteFont>("Fonts/MenuOptions");

        }
        public void Draw()
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(menuOptions, "You won!", new Vector2(765, 400), Color.White);
            spriteBatch.DrawString(menuOptions, "Press ENTER to return to menu", new Vector2(295, 600), Color.White);

        }
    }
}
