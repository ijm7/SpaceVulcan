using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Controller;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Levels;
using SpaceVulcan.Model.Players;
using SpaceVulcan.Model.Projectiles;
using SpaceVulcan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.View.States
{
    class DrawLevel
    {
        private ContentManager content = Program.game.Content;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private SpriteFont menuOptions;
        private SpriteFont smallStandardFont;
        private SpriteFont mediumStandardFont;
        private SpriteFont largeStandardFont;
        private Texture2D background;
        private Texture2D[] sideGUI;
        private Texture2D playerShip;
        private Texture2D projectile;
        private Song song;
        List<SoundEffect> soundEffects;
        ScrollingBackground gameBackground;

        public DrawLevel(/*Level level*/)
        {
            soundEffects = new List<SoundEffect>();
            SoundEffect.MasterVolume = 0.3f;
            this.song = content.Load<Song>("Music/Level1");
            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            playerShip = content.Load<Texture2D>("PlayerSprites/Lasership");
            this.menuOptions = content.Load<SpriteFont>("Fonts/MenuOptions");
            this.smallStandardFont = content.Load<SpriteFont>("Fonts/SmallStandard");
            this.mediumStandardFont = content.Load<SpriteFont>("Fonts/MediumStandard");
            this.largeStandardFont = content.Load<SpriteFont>("Fonts/LargeStandard");
            background = content.Load<Texture2D>("Backgrounds/Stars2");
            projectile = content.Load<Texture2D>("Projectiles/Laser");
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_wpn_laser8"));
            sideGUI = new Texture2D[20];
            for (int i = 0; i < 20; i++)
            {
                sideGUI[i] = new Texture2D(graphicsDevice, 1, 1);
                sideGUI[i].SetData(new[] { Color.White });
            }
            gameBackground = new ScrollingBackground();
            gameBackground.Load(graphicsDevice, background);
        }

        public void Draw(Player player, float elapsed, List<Projectile> projectileList, List<Enemy> existingEnemies)
        {
            DrawBackground(elapsed);
            DrawBackGUI();
            DrawPlayer(player);
            if (existingEnemies.Count!=0)
            {
                DrawEnemies(existingEnemies);
            }
            DrawProjectiles(projectileList);
            DrawHealth(player);
            DrawSoundEffects(player);

        }

        private void DrawBackGUI()
        {
            Color skirtColor = new Color(38, 38, 38);
            Color inlayColorLight = new Color(45, 45, 45);
            Color inlayColorDark = new Color(20, 20, 20);
            for (int i = 0, j = 0; i < 2; i++, j += 1440)
            {
                spriteBatch.Draw(sideGUI[i], new Rectangle(j, 0, 480, 1080), skirtColor);
            }
            for (int i = 2, j = 480; i < 4; i++, j += 960)
            {
                spriteBatch.Draw(sideGUI[i], new Rectangle(j, 0, 1, 1080), Color.DarkOrange);
            }
            spriteBatch.Draw(sideGUI[4], new Rectangle(30, 85, 400, 50), inlayColorDark);
            spriteBatch.Draw(sideGUI[5], new Rectangle(30, 440, 400, 150), inlayColorDark);
            spriteBatch.Draw(sideGUI[6], new Rectangle(40, 480, 380, 100), inlayColorLight);
            spriteBatch.Draw(sideGUI[7], new Rectangle(30, 640, 400, 400), inlayColorDark);
            spriteBatch.Draw(sideGUI[8], new Rectangle(1490, 20, 400, 400), inlayColorDark);
            spriteBatch.Draw(sideGUI[9], new Rectangle(1490, 840, 400, 200), inlayColorDark);
            spriteBatch.Draw(sideGUI[10], new Rectangle(1520, 980, 340, 60), skirtColor);
            spriteBatch.DrawString(mediumStandardFont, "SPACE VULCAN", new Vector2(40, 25), Color.White);
            spriteBatch.DrawString(mediumStandardFont, "SCORE: ", new Vector2(40, 95), Color.White);
            spriteBatch.DrawString(mediumStandardFont, "0", new Vector2(230, 95), Color.White);
            spriteBatch.DrawString(smallStandardFont, "DIALOG", new Vector2(40, 455), Color.White);
            spriteBatch.DrawString(smallStandardFont, "SPECIAL ABILITIES", new Vector2(1555, 40), Color.White);
            spriteBatch.DrawString(smallStandardFont, "ARMOUR", new Vector2(1540, 1000), Color.White);
            spriteBatch.DrawString(smallStandardFont, "SHIELD", new Vector2(1745, 1000), Color.White);
        }

        private void DrawBackground(float elapsed)
        {
            spriteBatch.Draw(sideGUI[5], new Rectangle(30, 800, 900, 150), Color.Wheat);
            gameBackground.Update(elapsed * 25);
            gameBackground.Draw(spriteBatch);

        }

        private void DrawPlayer(Player player)
        {
            spriteBatch.Draw(sideGUI[13], player.boundingBox, Color.Yellow);
            spriteBatch.Draw(player.sprite, player.position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(playerShip, new Rectangle((int)player.position.X,(int)player.position.Y,1,1)), Color.White);

        }

        private void DrawProjectiles(List<Projectile> projectileList)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                spriteBatch.Draw(projectileList[i].sprite, projectileList[i].position);

            }
        }

        private void DrawSoundEffects(Player player)
        {
            if (player.firing)
            {
                soundEffects[0].Play();
            }
        }

        private void DrawHealth(Player player)
        {
            spriteBatch.Draw(sideGUI[11], new Rectangle(1520, 870, (int)player.armour, 100), Color.Orange);
            spriteBatch.Draw(sideGUI[12], new Rectangle(1690, 870, (int)player.shield, 100), Color.LightBlue);
        }

        private void DrawEnemies(List<Enemy> existingEnemies)
        {
            for (int i = 0; i < existingEnemies.Count; i++)
            {
                if (existingEnemies[i].boundingBox.Right < GameArea.RIGHT && existingEnemies[i].boundingBox.Left > GameArea.LEFT)
                {
                    spriteBatch.Draw(existingEnemies[i].sprite, existingEnemies[i].position);
                }
            }
        }
    }
}
