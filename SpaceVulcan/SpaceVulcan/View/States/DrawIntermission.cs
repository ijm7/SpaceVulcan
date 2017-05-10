﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.View.States
{
    class DrawIntermission
    {
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteFont menuOptions;
        public DrawIntermission()
        {
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            this.menuOptions = Program.game.Content.Load<SpriteFont>("Fonts/MenuOptions");
        }

        public void Draw()
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(menuOptions, "LEVEL COMPLETE", new Vector2(655, 400), Color.White);
            spriteBatch.DrawString(menuOptions, "Press ENTER to proceed to next level", new Vector2(185, 600), Color.White);

        }
    }
}