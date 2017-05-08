using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Controller;
using SpaceVulcan.Model;
using SpaceVulcan.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.View.States
{
    public class DrawShipSelect
    {
        private ContentManager content = Program.game.Content;
        
        private GraphicsDevice graphicsDevice;
        ScrollingBackground menuBackground;
        private SpriteBatch spriteBatch;
        private SpriteFont menuOptions;
        private SpriteFont smallStandardFont;
        private SpriteFont mediumStandardFont;
        private SpriteFont largeStandardFont;
        private Texture2D background;
        private Texture2D[] backSquares;
        private Texture2D[] ships;
        //private Song song; REMEMBER THAT YOU WILL HAVE TO MAKE THE SONG FOR FIRST LEVEL PLAY HERE TO CUT OFF MEDIAPLAYER
        List<SoundEffect> soundEffects;
        public DrawShipSelect()
        {
            soundEffects = new List<SoundEffect>();
            this.spriteBatch = Program.game.spriteBatch;
            this.menuOptions = content.Load<SpriteFont>("Fonts/MenuOptions");
            this.smallStandardFont = content.Load<SpriteFont>("Fonts/SmallStandard");
            this.mediumStandardFont = content.Load<SpriteFont>("Fonts/MediumStandard");
            this.largeStandardFont = content.Load<SpriteFont>("Fonts/LargeStandard");
            this.background = content.Load<Texture2D>("Backgrounds/Stars2");
            //this.song = content.Load<Song>("Music/TitleScreen");
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_menu_select1"));
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_menu_move1"));
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_menu_select2"));
            this.graphicsDevice = Program.game.GraphicsDevice;
            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;
            backSquares = new Texture2D[3];
            ships = new Texture2D[3];
            ships[0] = content.Load<Texture2D>("PlayerSprites/lasership");
            ships[1] = content.Load<Texture2D>("PlayerSprites/massdrivership4");
            ships[2] = content.Load<Texture2D>("PlayerSprites/missilecruiser2");
            for (int i = 0; i < 3; i++)
            {
                backSquares[i] = new Texture2D(graphicsDevice, 480, 600);
                backSquares[i].CreateBorder(5, Color.White);
            }
            menuBackground = new ScrollingBackground();
            menuBackground.Load(graphicsDevice, background);
        }
        public void Draw(MenuShipSelect _menuShipSelect, ButtonType _buttonType, GameTime gameTime, float elapsed)
        {

            menuBackground.Update(elapsed * 25);
            menuBackground.Draw(spriteBatch);
            /*Texture2D rect = new Texture2D(graphicsDevice, 80, 30);
            Color[] data = new Color[80 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate * 0.5f;
            rect.SetData(data);
            Vector2 coor = new Vector2(10, 20);
            spriteBatch.Draw(rect, coor, Color.White);*/
            spriteBatch.DrawString(largeStandardFont, "SHIP SELECT", new Vector2(375, 100), Color.White);
            spriteBatch.DrawString(menuOptions, "VINDICATOR", new Vector2(160, 320), Color.White);
            spriteBatch.DrawString(menuOptions, "AGEMEMNON", new Vector2(780, 320), Color.White);//735
            spriteBatch.DrawString(menuOptions, "WILDCAT", new Vector2(1425, 320), Color.White);//1335
            spriteBatch.DrawString(mediumStandardFont, "Laser Guns", new Vector2(195, 645), Color.Red);
            spriteBatch.DrawString(mediumStandardFont, "Giga Cannons", new Vector2(770, 645), Color.Red);
            spriteBatch.DrawString(mediumStandardFont, "Missiles", new Vector2(1440, 645), Color.Red);
            //LASER SHIP                                
            spriteBatch.DrawString(smallStandardFont, "Laser weaponry has a very ", new Vector2(150, 700), Color.White);
            spriteBatch.DrawString(smallStandardFont, "high fire rate, but also  ", new Vector2(150, 730), Color.White);
            spriteBatch.DrawString(smallStandardFont, "puts out a low amount of ", new Vector2(150, 760), Color.White);
            spriteBatch.DrawString(smallStandardFont, "damage per second.", new Vector2(150, 790), Color.White);
            //CANNONS                                  Laser ship has a very high
            spriteBatch.DrawString(smallStandardFont, "Giga Cannons provide a", new Vector2(750, 700), Color.White);
            spriteBatch.DrawString(smallStandardFont, "good balance between damage", new Vector2(750, 730), Color.White);
            spriteBatch.DrawString(smallStandardFont, "output and fire rate. Thus", new Vector2(750, 760), Color.White);
            spriteBatch.DrawString(smallStandardFont, "they are well suited for", new Vector2(750, 790), Color.White);
            spriteBatch.DrawString(smallStandardFont, "beginners.", new Vector2(750, 820), Color.White);
            //MISSILES                                 Laser ship has a very high
            
            spriteBatch.DrawString(smallStandardFont, "Missiles do very high", new Vector2(1350, 700), Color.White);
            spriteBatch.DrawString(smallStandardFont, "damage but have a low fire", new Vector2(1350, 730), Color.White);
            spriteBatch.DrawString(smallStandardFont, "rate. Missiles are more ", new Vector2(1350, 760), Color.White);
            spriteBatch.DrawString(smallStandardFont, "suited to experienced", new Vector2(1350, 790), Color.White);
            spriteBatch.DrawString(smallStandardFont, "players." + elapsed, new Vector2(1350, 820), Color.White);
            for (int i = 0, j = 0; i < 3 ; i++)
            {
                if (i == 1)
                {
                    spriteBatch.Draw(ships[i], new Vector2(300 + j, 410));//135
                }
                else
                {
                    spriteBatch.Draw(ships[i], new Vector2(275 + j, 385));//135
                }
                j += 600;
            }
            switch (_menuShipSelect)
            {
                case MenuShipSelect.Laser:
                    backSquares[0].CreateBorder(10, Color.Red);
                    backSquares[1].CreateBorder(5, Color.White);
                    backSquares[2].CreateBorder(5, Color.White);
                    break;
                case MenuShipSelect.Mass:
                    backSquares[0].CreateBorder(5, Color.White);
                    backSquares[1].CreateBorder(10, Color.Red);
                    backSquares[2].CreateBorder(5, Color.White);
                    break;
                case MenuShipSelect.Missile:
                    backSquares[0].CreateBorder(5, Color.White);
                    backSquares[1].CreateBorder(5, Color.White);
                    backSquares[2].CreateBorder(10, Color.Red);
                    break;
            }
            for (int i = 0,j = 0; i < 3; i++)
            {
                spriteBatch.Draw(backSquares[i], new Vector2(120 + j, 300), Color.White);
                j += 600;
            }
            switch (_buttonType)
            {
                case ButtonType.enter:
                    soundEffects[0].Play();
                    break;
                case ButtonType.move:
                    soundEffects[1].Play();
                    break;
                case ButtonType.back:
                    soundEffects[2].Play();
                    break;
            }

        }
    }
}
