using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Controller;
using SpaceVulcan.Model;
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
        List<SoundEffectInstance> soundEffectInstanceList;
        ScrollingBackground gameBackground;

        public DrawLevel(Level level)
        {
            soundEffects = new List<SoundEffect>();
            soundEffectInstanceList = new List<SoundEffectInstance>();
            SoundEffect.MasterVolume = 0.5f;
            this.song = level.song;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            graphicsDevice.Clear(Color.Black);
            playerShip = content.Load<Texture2D>("PlayerSprites/Lasership");
            this.menuOptions = content.Load<SpriteFont>("Fonts/MenuOptions");
            this.smallStandardFont = content.Load<SpriteFont>("Fonts/SmallStandard");
            this.mediumStandardFont = content.Load<SpriteFont>("Fonts/MediumStandard");
            this.largeStandardFont = content.Load<SpriteFont>("Fonts/LargeStandard");
            background = level.background;
            projectile = content.Load<Texture2D>("Projectiles/Laser");
            //SoundEffects
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_wpn_laser8")); //0
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_weapon_shotgun2")); //1
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_wpn_missilelaunch")); //2
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_alarm_loop7")); //3
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_damage_hit5")); //4
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_exp_short_hard14")); //5
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_lowhealth_alarmloop1"));//6
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_sounds_error2"));//7
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_menu_select1"));
            for (int i = 0; i < soundEffects.Count; i++)
            {
                soundEffectInstanceList.Add(soundEffects.ElementAt(i).CreateInstance());
            }
            sideGUI = new Texture2D[20];
            for (int i = 0; i < 20; i++)
            {
                sideGUI[i] = new Texture2D(graphicsDevice, 1, 1);
                sideGUI[i].SetData(new[] { Color.White });
            }
            gameBackground = new ScrollingBackground();
            gameBackground.Load(graphicsDevice, background);
            soundEffects[8].Play();
        }

        public void Draw(Player player, float elapsed, List<Projectile> projectileList, List<Enemy> existingEnemies, EventTracker eventTracker)
        {
            DrawBackground(elapsed);
            DrawBackGUI(player);
            DrawPlayer(player);
            if (existingEnemies.Count!=0)
            {
                DrawEnemies(existingEnemies);
            }
            DrawProjectiles(projectileList);
            DrawHealth(player);
            DrawSoundEffects(player, eventTracker);
            DrawAbilities(player);

        }

        private void DrawBackGUI(Player player)
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
            spriteBatch.Draw(player.sprite, new Vector2(40,650) ,null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(mediumStandardFont, "SPACE VULCAN", new Vector2(40, 25), Color.White);
            spriteBatch.DrawString(mediumStandardFont, "SCORE: "+ player.score, new Vector2(40, 95), Color.White);
            spriteBatch.DrawString(mediumStandardFont, "", new Vector2(230, 95), Color.White);
            spriteBatch.DrawString(smallStandardFont, "DIALOG", new Vector2(40, 455), Color.White);
            spriteBatch.DrawString(smallStandardFont, "SPECIAL ABILITIES", new Vector2(1555, 40), Color.White);
            spriteBatch.DrawString(smallStandardFont, "ARMOUR", new Vector2(1540, 1000), Color.White);
            spriteBatch.DrawString(smallStandardFont, "SHIELD", new Vector2(1745, 1000), Color.White);
            if (player._projectileType == ProjectileType.Laser)
            {
                spriteBatch.DrawString(smallStandardFont, "VINDICATOR", new Vector2(40, 880), Color.White);
            }
            else if (player._projectileType == ProjectileType.MassDriver)
            {
                spriteBatch.DrawString(smallStandardFont, "AGMENMON", new Vector2(40, 880), Color.White);
            }
            else if (player._projectileType == ProjectileType.Missile)
            {
                spriteBatch.DrawString(smallStandardFont, "WILDCAT", new Vector2(40, 880), Color.White);
            }
            spriteBatch.DrawString(smallStandardFont, "Shield: " + Math.Floor(player.shield), new Vector2(40, 900), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Armour: " + Math.Floor(player.armour), new Vector2(40, 920), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Weapon Type: " + player._projectileType, new Vector2(40, 940), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Ship Speed: "+ player.speed, new Vector2(40,960), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Weapon Damage: " + player.damage, new Vector2(40, 980), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Regeneration: " + player.regenerationRate, new Vector2(40, 1000), Color.White);
        }

        private void DrawBackground(float elapsed)
        {
            spriteBatch.Draw(sideGUI[5], new Rectangle(30, 800, 900, 150), Color.Wheat);
            gameBackground.Update(elapsed * 60);
            gameBackground.Draw(spriteBatch);

        }

        private void DrawPlayer(Player player)
        {
            //spriteBatch.Draw(sideGUI[13], player.boundingBox, Color.Yellow);
            spriteBatch.Draw(player.sprite, player.position, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(playerShip, new Rectangle((int)player.position.X,(int)player.position.Y,1,1)), Color.White);

        }

        private void DrawProjectiles(List<Projectile> projectileList)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                //spriteBatch.Draw(sideGUI[14], projectileList[i].boundingBox, Color.Yellow);
                if (projectileList[i].enemy == true)
                {
                    spriteBatch.Draw(projectileList[i].sprite, projectileList[i].position, null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.FlipVertically, 0f);
                }
                else
                {
                    spriteBatch.Draw(projectileList[i].sprite, projectileList[i].position, null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
                }

            }
        }

        private void DrawSoundEffects(Player player, EventTracker eventTracker)
        {
            if (player.firing)
            {
                if (player._projectileType == ProjectileType.Laser && soundEffectInstanceList[0].State == SoundState.Stopped)
                {
                    soundEffectInstanceList[0].Play();
                }
                else if (player._projectileType == ProjectileType.MassDriver)
                {
                    soundEffects[1].Play();
                }
                else if (player._projectileType == ProjectileType.Missile)
                {
                    soundEffects[2].Play();
                }
            }
            if (player.shield < 10 && soundEffectInstanceList[6].State == SoundState.Stopped)
            {
                soundEffectInstanceList[6].Play();
            }
            if (eventTracker.enemyHitRecorded && soundEffectInstanceList[4].State == SoundState.Stopped)
            {
                soundEffectInstanceList[4].Play();
            }
            if (eventTracker.playerHitRecorded && soundEffectInstanceList[7].State == SoundState.Stopped)
            {
                soundEffectInstanceList[7].Play();
            }
            if (eventTracker.destroyed)
            {
                soundEffects[5].Play();
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
                    //spriteBatch.Draw(sideGUI[15], existingEnemies[i].boundingBox, Color.Yellow);
                    spriteBatch.Draw(existingEnemies[i].sprite, existingEnemies[i].position, null, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.FlipVertically, 0f);
                }
            }
        }

        private void DrawAbilities(Player player)
        {
            for (int i = 0, j = 0; i < player.abilityList.Count; i++, j+=75)
            {
                string ability="";
                Color placeholderColor;
                if (player.abilityList[i].identifier == 1)
                {
                    ability = "Increase damage";
                }
                else if (player.abilityList[i].identifier == 2)
                {
                    ability = "Increase speed";
                }
                else if (player.abilityList[i].identifier == 3)
                {
                    ability = "Repair Armour";
                }
                else if (player.abilityList[i].identifier == 4)
                {
                    ability = "Increase Shield Regen";
                }
                else if (player.abilityList[i].identifier == 5)
                {
                    ability = "Increase Fire Rate";
                }
                if (player.abilityList[i].isActive)
                {
                    placeholderColor = Color.Aqua;
                }
                else if (player.abilityList[i].isAvailable)
                {
                    placeholderColor = Color.Green;
                }
                else
                {
                    placeholderColor = Color.Black;
                }
                spriteBatch.Draw(sideGUI[8], new Rectangle(1500, 100+j, 380, 50), placeholderColor);
                spriteBatch.DrawString(smallStandardFont, ability, new Vector2(1505, 115+j), Color.White);
            }
        }
    }
}
