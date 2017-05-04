using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller.States
{
    class UpdateTopMenu
    {
        public UpdateTopMenu()
        {

        }
        public void Update(KeyboardState state, KeyboardState previousState, ref MenuSelection _menuSelection, GameTime gameTime)
        {
            if (state.IsKeyDown(Keys.Up) & !previousState.IsKeyDown(Keys.Up))
            {
                if ((int)_menuSelection > 0)
                {
                    //menuList.mainMenu--;
                    _menuSelection = (MenuSelection)(int)_menuSelection - 1;
                }
            }
            if (state.IsKeyDown(Keys.Down) & !previousState.IsKeyDown(Keys.Down))
            {
                if ((int)_menuSelection < 2)
                {
                    //menuList.mainMenu++;
                    _menuSelection = (MenuSelection)(int)_menuSelection + 1;
                }
            }
            //System.Diagnostics.Debug.WriteLine(menuList.mainMenu);
        }
    }
}
