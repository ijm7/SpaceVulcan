using Microsoft.Xna.Framework.Input;

namespace SpaceVulcan.Controller.States
{
    class UpdateInstructions
    {
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
