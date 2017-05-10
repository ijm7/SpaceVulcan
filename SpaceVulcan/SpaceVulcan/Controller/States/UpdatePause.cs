using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller.States
{
    class UpdatePause
    {
        public UpdatePause()
        {

        }

        public void Update(KeyboardState keyState, KeyboardState prevState, ref GameState _state, EventTracker eventTracker)
        {
            if (keyState.IsKeyDown(Keys.Enter) & !prevState.IsKeyDown(Keys.Enter))
            {
                if (eventTracker.prevLevel==0)
                {
                    _state = GameState.Level1;
                }
                else if (eventTracker.prevLevel == 1)
                {
                    _state = GameState.Level2;
                }
                else
                {
                    _state = GameState.Level3;
                }
            }

        }
    }
}
