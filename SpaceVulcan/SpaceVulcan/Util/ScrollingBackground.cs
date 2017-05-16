using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceVulcan.Util
{
    //ADAPTED FROM https://msdn.microsoft.com/en-us/library/bb203868.aspx
    public class ScrollingBackground
    {
        private Vector2 screenpos, origin, texturesize;
        private Texture2D mytexture;
        private int screenheight;
        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            mytexture = backgroundTexture;
            screenheight = device.Viewport.Height;
            int screenwidth = device.Viewport.Width;
            origin = new Vector2(mytexture.Width / 2, 0);
            screenpos = new Vector2(screenwidth / 2, screenheight / 2);
            texturesize = new Vector2(0, mytexture.Height);
        }
        public void Update(float deltaY)
        {
            screenpos.Y += deltaY;
            screenpos.Y = screenpos.Y % mytexture.Height;
        }
        public void Draw(SpriteBatch batch)
        {
            if (screenpos.Y < screenheight)
            {
                batch.Draw(mytexture, screenpos, null,
                     Color.White, 0, origin, 1, SpriteEffects.None, 0f);
            }
            batch.Draw(mytexture, screenpos - texturesize, null,
                 Color.White, 0, origin, 1, SpriteEffects.None, 0f);
        }
    }
}
