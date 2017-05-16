using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.View.States
{
    class DrawGameOver
    {
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteFont menuOptions;

        public DrawGameOver()
        {
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            this.menuOptions = Program.game.Content.Load<SpriteFont>("Fonts/MenuOptions");
            
        }
        public void Draw(Player player)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(menuOptions, "GAME OVER", new Vector2(765, 400), Color.White);
            spriteBatch.DrawString(menuOptions, "FINAL SCORE: " + player.score, new Vector2(750, 400), Color.White);
            spriteBatch.DrawString(menuOptions, "Press ENTER to return to menu", new Vector2(295, 600), Color.White);

        }
    }
}
