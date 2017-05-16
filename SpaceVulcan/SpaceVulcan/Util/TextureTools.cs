using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceVulcan.Util
{
    static class TextureTools
    {
        // http://stackoverflow.com/questions/13893959/how-to-draw-the-border-of-a-square
        public static void CreateBorder(this Texture2D texture, int borderWidth, Color borderColor)
        {
            Color[] colors = new Color[texture.Width * texture.Height];
            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    bool colored = false;
                    for (int i = 0; i <= borderWidth; i++)
                    {
                        if (x == i || y == i || x == texture.Width - 1 - i || y == texture.Height - 1 - i)
                        {
                            colors[x + y * texture.Width] = borderColor;
                            colored = true;
                            break;
                        }
                    }
                    if (colored == false)
                        colors[x + y * texture.Width] = Color.Transparent;
                }
            }
            texture.SetData(colors);
        }

        //http://stackoverflow.com/questions/5751732/draw-rectangle-in-xna-using-spritebatch
        public static Vector2 CreateRectangle(int w, int h, int x, int y, Color color, float luminosity)
        {
            Texture2D rect = new Texture2D(Program.game.GraphicsDevice, w, h);
            Color[] data = new Color[w * h];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate * luminosity;
            rect.SetData(data);
            Vector2 coor = new Vector2(x, y);
            return coor;
        }
    }
}
