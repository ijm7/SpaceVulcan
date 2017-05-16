using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;

namespace SpaceVulcan.Controller.States
{
    class UpdateTopMenu
    {
        public void Update(KeyboardState keyState, KeyboardState previousState, ref MenuSelection _menuSelection, ref GameState _state, ref ButtonType _buttonType, GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.Up) & !previousState.IsKeyDown(Keys.Up))
            {
                _buttonType = ButtonType.move;
                if ((int)_menuSelection > 0)
                {
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
                        _buttonType = ButtonType.enter;
                        break;
                    case MenuSelection.Exit:
                        _state = GameState.Exit;
                        break;
                }
            }
        }
    }
}
