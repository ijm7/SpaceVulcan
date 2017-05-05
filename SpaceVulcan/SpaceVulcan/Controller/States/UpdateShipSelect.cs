using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;
using SpaceVulcan.Model.Players;
using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller.States
{
    class UpdateShipSelect
    {
        public void Update(KeyboardState keyState, KeyboardState previousState, ref MenuShipSelect _menuShipSelect, ref GameState _state, GameTime gameTime, ref Player player)
        {
            if (keyState.IsKeyDown(Keys.Left) & !previousState.IsKeyDown(Keys.Left))
            {
                if ((int)_menuShipSelect > 0)
                {
                    //menuList.mainMenu--;
                    _menuShipSelect = (MenuShipSelect)(int)_menuShipSelect - 1;
                }
                else
                {
                    _menuShipSelect = (MenuShipSelect)2;
                }
            }
            if (keyState.IsKeyDown(Keys.Right) & !previousState.IsKeyDown(Keys.Right))
            {
                if ((int)_menuShipSelect < 2)
                {
                    //menuList.mainMenu++;
                    _menuShipSelect = (MenuShipSelect)(int)_menuShipSelect + 1;
                }
                else
                {
                    _menuShipSelect = (MenuShipSelect)0;
                }
            }
            if (keyState.IsKeyDown(Keys.Enter) & !previousState.IsKeyDown(Keys.Enter))
            {
                ProjectileType _projectileType;
                switch (_menuShipSelect)
                {
                    case MenuShipSelect.Laser:
                        _projectileType = ProjectileType.Laser;
                        player = new Model.Players.Player(500, 500, 0, 15, 5, 1.0, 25, 25, _projectileType);
                        break;
                    case MenuShipSelect.Mass:
                        _projectileType = ProjectileType.MassDriver;
                        player = new Model.Players.Player(500, 500, 0, 15, 10, 0.5, 25, 25, _projectileType);
                        break;
                    case MenuShipSelect.Missile:
                        _projectileType = ProjectileType.Missile;
                        player = new Model.Players.Player(500, 500, 0, 15, 50, 0.1, 25, 25, _projectileType);
                        break;
                }
                _state = GameState.Level1;
            }
        }
    }
}
