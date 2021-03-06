﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVulcan.Model.Players;

namespace SpaceVulcan.View.States
{
    public sealed class DrawIntermission
    {
        private static readonly DrawIntermission instance = new DrawIntermission();
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        SpriteFont menuOptions;
        static DrawIntermission()
        {

        }
        public DrawIntermission()
        {
            this.spriteBatch = Program.game.spriteBatch;
            this.graphicsDevice = Program.game.GraphicsDevice;
            this.menuOptions = Program.game.Content.Load<SpriteFont>("Fonts/MenuOptions");
        }
        public static DrawIntermission Instance
        {
            get
            {
                return instance;
            }
        }
        public void Draw(Player player)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(menuOptions, "LEVEL COMPLETE", new Vector2(655, 400), Color.White);
            spriteBatch.DrawString(menuOptions, "Press ENTER to proceed to next level", new Vector2(185, 600), Color.White);
            spriteBatch.DrawString(menuOptions, "Current Score: " + player.score, new Vector2(500, 800), Color.White);

        }
    }
}
