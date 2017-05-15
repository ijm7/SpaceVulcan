using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;
using SpaceVulcan.Model.Abilities;
using SpaceVulcan.Model.Levels;
using SpaceVulcan.Model.Players;
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
        Random rnd = new Random();
        public UpdateIntermission()
        {
            levelCreator = new LevelCreator();
        }

        public void Update(KeyboardState keyState, KeyboardState prevState, EventTracker eventTracker, ref GameState _state, ref UpdateLevel updateLevel, ref DrawLevel drawLevel, ref Player player)
        {
            if (keyState.IsKeyDown(Keys.Enter) & !prevState.IsKeyDown(Keys.Enter))
            {
                bool good = true;
                int initialAbilityIndex = 0;
                do
                {
                    good = true;
                    initialAbilityIndex = rnd.Next(1, 6);
                    for (int i = 0; i < player.abilityList.Count; i++)
                    {
                        if (player.abilityList[i].identifier == initialAbilityIndex)
                        {
                            good = false;
                        }
                    }
                } while (good != true);
                Ability initialAbility = new Ability(initialAbilityIndex);
                player.abilityList.Add(initialAbility);
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
