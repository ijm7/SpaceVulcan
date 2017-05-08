using SpaceVulcan.Model.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model.Levels
{
    public class Level
    {
        public Level(List<Enemy> enemies, Backgrounds _backgrounds)
        {
            this.enemies = enemies;
            this._backgrounds = _backgrounds;
        }
        public List<Enemy> enemies { get; set; }
        public Backgrounds _backgrounds { get; set; }
    }
}
