using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Model.Players;

namespace SpaceVulcan.View.States
{
    class DrawGameOver
    {
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteFont menuOptions;
        Song endSong;
        bool songPlaying = false;

        public DrawGameOver()
        {
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            this.menuOptions = Program.game.Content.Load<SpriteFont>("Fonts/MenuOptions");
            endSong = Program.game.Content.Load<Song>("Music/Ending");

        }
        public void Draw(Player player)
        {
            if (songPlaying == false)
            {
                MediaPlayer.Play(endSong);
                songPlaying = true;
            }
            graphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(menuOptions, "GAME OVER", new Vector2(765, 400), Color.White);
            spriteBatch.DrawString(menuOptions, "FINAL SCORE: " + player.score, new Vector2(675, 500), Color.White);
            spriteBatch.DrawString(menuOptions, "Press ENTER to return to menu", new Vector2(300, 600), Color.White);

        }
    }
}
