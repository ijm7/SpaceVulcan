using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;
using SpaceVulcan.Model.Abilities;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Levels;
using SpaceVulcan.Model.Players;
using SpaceVulcan.Model.Projectiles;
using SpaceVulcan.Util;
using SpaceVulcan.View.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller.States
{
    class UpdateShipSelect
    {
        Random rnd = new Random();
        public void Update(KeyboardState keyState, KeyboardState previousState, ref MenuShipSelect _menuShipSelect, ref GameState _state, ref ButtonType _buttonType, GameTime gameTime, ref Player player, ref DrawLevel drawLevel, ref UpdateLevel updateLevel, ref Dictionary<int, List<Enemy>> levelOneGenerator)
        {
            if (keyState.IsKeyDown(Keys.Left) & !previousState.IsKeyDown(Keys.Left))
            {
                _buttonType = ButtonType.move;
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
                _buttonType = ButtonType.move;
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
                LevelCreator levelOneCreator = new LevelCreator();
                _buttonType = ButtonType.enter;
                ProjectileType _projectileType;
                _state = GameState.Level1;
                GameState copyState = _state;
                Level firstLevel=null;
                //Level firstLevel = levelOneCreator.BuildLevel(_state);
                var thread = new Thread(() => { firstLevel = levelOneCreator.BuildLevel(copyState); });
                //var thread = new Thread(() => { levelCatalogue = BuildLevelDictionary(_state); });
                thread.Start();
                thread.Join();
                updateLevel = new UpdateLevel(firstLevel);
                drawLevel = new DrawLevel(firstLevel);
                Vector2 defaultPosition = new Vector2(960,800);
                
                int initialAbilityIndex = rnd.Next(1, 6);
                switch (_menuShipSelect)
                {
                    
                    case MenuShipSelect.Laser:
                        _projectileType = ProjectileType.Laser;
                        
                        player = new Player(defaultPosition, 100, 100, 0, 6, 3, 0.04, 170, 100, _projectileType, false, 0);
                        
                        player.sprite = Program.game.Content.Load<Texture2D>("PlayerSprites/Lasership");
                       
                        break;
                    case MenuShipSelect.Mass:
                        _projectileType = ProjectileType.MassDriver;
                        player = new Player(defaultPosition, 100, 100, 0, 6, 10, 0.3, 170, 100, _projectileType, false, 0);
                        player.sprite = Program.game.Content.Load<Texture2D>("PlayerSprites/massdrivership4");
                        break;  
                    case MenuShipSelect.Missile:
                        _projectileType = ProjectileType.Missile;
                        player = new Player(defaultPosition, 100, 100, 0, 6, 15, 0.6, 170, 100, _projectileType, false, 0);
                        player.sprite = Program.game.Content.Load<Texture2D>("PlayerSprites/missilecruiser2");
                        break;
                }
                player.playerDefault = (Player)player.Clone();
                Ability initialAbility = new Ability(initialAbilityIndex);
                player.abilityList = new List<Ability>();
                player.abilityList.Add(initialAbility);
            }
            if (keyState.IsKeyDown(Keys.Back) & !previousState.IsKeyDown(Keys.Back))
            {
                _buttonType = ButtonType.back;
                _state = GameState.TopMenu;
            }
            
        }
    }
}
