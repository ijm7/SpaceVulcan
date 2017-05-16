using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceVulcan.Controller;
using System.Collections.Generic;

namespace SpaceVulcan.View.States
{
    class DrawInstructions
    {
        private ContentManager content = Program.game.Content;

        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private SpriteFont menuOptions;
        private SpriteFont smallStandardFont;
        private SpriteFont mediumStandardFont;
        private SpriteFont largeStandardFont;
        List<SoundEffect> soundEffects;
        public DrawInstructions()
        {
            soundEffects = new List<SoundEffect>();
            soundEffects.Add(content.Load<SoundEffect>("SoundEffects/sfx_menu_select1"));
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            this.menuOptions = content.Load<SpriteFont>("Fonts/MenuOptions");
            this.smallStandardFont = content.Load<SpriteFont>("Fonts/SmallStandard");
            this.mediumStandardFont = content.Load<SpriteFont>("Fonts/MediumStandard");
            this.largeStandardFont = content.Load<SpriteFont>("Fonts/LargeStandard");
        }

        public void Draw(ButtonType _buttonType)
        {
            if (_buttonType == ButtonType.enter)
            {
                soundEffects[0].Play();
            }
            graphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(largeStandardFont, "INSTRUCTIONS", new Vector2(350, 100), Color.White);
            spriteBatch.DrawString(mediumStandardFont, "Controls", new Vector2(100, 420), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Arrow Keys - Switch menu options, Move ship", new Vector2(100, 470), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Spacebar - Shoot projectiles", new Vector2(100, 490), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Enter - Select Menu Options, Pause Game", new Vector2(100, 510), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Escape - Exit Game", new Vector2(100, 530), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Backspace - Back to previous menu", new Vector2(100, 550), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Number keys 1,2,3 - Utilise special abilities", new Vector2(100, 570), Color.White);
            spriteBatch.DrawString(mediumStandardFont, "Instructions", new Vector2(1000, 420), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Shoot at the enemy whilst dodging enemy projectiles.", new Vector2(1000, 470), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Destroy all enemies in a level to advance to the next", new Vector2(1000, 490), Color.White);
            spriteBatch.DrawString(smallStandardFont, "level. Use special abilities to enhance your ship's", new Vector2(1000, 510), Color.White);
            spriteBatch.DrawString(smallStandardFont, "abilities.", new Vector2(1000, 530), Color.White);
            spriteBatch.DrawString(mediumStandardFont, "Credits", new Vector2(100, 650), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Art - Skorpio: CC-BY-SA 3.0, FalcoSun: CC-BY 3.0", new Vector2(100, 700), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Fonts - CRYSTAL, Felipe Munoz: CC-BY, Press Start 2P: SIL Open Font License", new Vector2(100, 720), Color.White);
            spriteBatch.DrawString(smallStandardFont, "Music and Sound Effects - Juhani Junkala: Public Domain", new Vector2(100, 740), Color.White);
            spriteBatch.DrawString(smallStandardFont, "For more information visit https://github.com/ijm7/SpaceVulcan", new Vector2(100, 760), Color.White);
            spriteBatch.DrawString(mediumStandardFont, "Press backspace to return to the main menu", new Vector2(300, 1000), Color.White);
        }
    }
}
