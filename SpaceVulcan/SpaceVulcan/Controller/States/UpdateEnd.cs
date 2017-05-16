using Microsoft.Xna.Framework.Input;

namespace SpaceVulcan.Controller.States
{
    class UpdateEnd
    {
        public void Update(KeyboardState keyState, ref GameState _state)
        {
            if (keyState.IsKeyDown(Keys.Enter))
            {
                Program.game.loader();
            }
        }
    }
}
