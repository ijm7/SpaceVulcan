using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller.States
{
    class UpdateInstructions
    {
        public UpdateInstructions()
        {
        }

        public void Update(KeyboardState keyState, ref GameState _state, ref ButtonType _buttonType)
        {
            if (keyState.IsKeyDown(Keys.Back))
            {
                _state = GameState.TopMenu;
                _buttonType = ButtonType.back;
            }
        }
    }
}
