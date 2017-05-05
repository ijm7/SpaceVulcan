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
        public void Update(KeyboardState keyState, KeyboardState previousState, ref MenuSelection _menuSelection, ref GameState _state, ref ButtonType _buttonType, GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.Up) & !previousState.IsKeyDown(Keys.Up))
            {
                _buttonType = ButtonType.move;
                if ((int)_menuSelection > 0)
                {
                    //menuList.mainMenu--;
                    _menuSelection = (MenuSelection)(int)_menuSelection - 1;
                }
                else
                {
                    _menuSelection = (MenuSelection)2;
                }
            }
            if (keyState.IsKeyDown(Keys.Down) & !previousState.IsKeyDown(Keys.Down))
            {
                _buttonType = ButtonType.move;
                if ((int)_menuSelection < 2)
                {
                    //menuList.mainMenu++;
                    _menuSelection = (MenuSelection)(int)_menuSelection + 1;
                }
                else
                {
                    _menuSelection = (MenuSelection)0;
                }
            }
            if (keyState.IsKeyDown(Keys.Enter) & !previousState.IsKeyDown(Keys.Enter))
            {
                _buttonType = ButtonType.enter;
                switch (_menuSelection)
                {
                    case MenuSelection.Play:
                        _state = GameState.ShipSelect;
                        break;
                    case MenuSelection.Controls:
                        _state = GameState.Controls;
                        break;
                    case MenuSelection.Exit:
                        _state = GameState.Exit;
                        break;
                }
            }
            
            //System.Diagnostics.Debug.WriteLine(menuList.mainMenu);
        }
    }
}
