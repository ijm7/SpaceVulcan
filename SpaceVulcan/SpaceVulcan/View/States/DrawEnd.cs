using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Model.Players;

namespace SpaceVulcan.View.States
{
    class DrawEnd
    {
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteFont menuOptions;
        Song endSong;
        bool songPlaying = false;
        public DrawEnd()
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
            spriteBatch.DrawString(menuOptions, "You won!", new Vector2(765, 400), Color.White);
            spriteBatch.DrawString(menuOptions, "FINAL SCORE: " + player.score, new Vector2(700, 500), Color.White);
            spriteBatch.DrawString(menuOptions, "Press ENTER to return to menu", new Vector2(280, 600), Color.White);

        }
    }
}
