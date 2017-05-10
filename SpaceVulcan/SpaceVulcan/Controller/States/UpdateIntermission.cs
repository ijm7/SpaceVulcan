using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;
using SpaceVulcan.Model.Levels;
using SpaceVulcan.Util;
using SpaceVulcan.View.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller.States
{
    class UpdateIntermission
    {
        LevelCreator levelCreator;
        public UpdateIntermission()
        {
            levelCreator = new LevelCreator();
        }

        public void Update(KeyboardState keyState, KeyboardState prevState, EventTracker eventTracker, ref GameState _state, ref UpdateLevel updateLevel, ref DrawLevel drawLevel)
        {
            if (keyState.IsKeyDown(Keys.Enter) & !prevState.IsKeyDown(Keys.Enter))
            {
                if (eventTracker.prevLevel == 1)
                {
                    Level level = levelCreator.BuildLevel(GameState.Level2);
                    updateLevel = new UpdateLevel(level);
                    drawLevel = new DrawLevel(level);
                    _state = GameState.Level2;
                }
                else
                {
                    Level level = levelCreator.BuildLevel(GameState.Level3);
                    updateLevel = new UpdateLevel(level);
                    drawLevel = new DrawLevel(level);
                    _state = GameState.Level3;
                }
            }
        }
    }
}
