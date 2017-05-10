using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller.States
{
    class UpdateEnd
    {
        public UpdateEnd()
        {

        }

        public void Update(KeyboardState keyState, ref GameState _state)
        {
            if (keyState.IsKeyDown(Keys.Enter))
            {
                Program.game.loader();
            }
        }
    }
}
