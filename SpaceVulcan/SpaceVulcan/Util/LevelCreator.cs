using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Util
{
    public class LevelCreator
    {
        Enemy[] grunt;
        Enemy[] medium;
        Enemy[] boss;
        Dictionary<int, List<Enemy>> levelCatalogue;
        List<Enemy> timeList;

        public LevelCreator()
        {
            grunt = new Enemy[10];
            medium = new Enemy[10];
            boss = new Enemy[3];
            levelCatalogue = new Dictionary<int, List<Enemy>>();
            
        }

        public Level BuildLevelOne()
        {
            levelCatalogue = BuildLevelOneDictionary();
            Level newLevel = new Level(levelCatalogue, Program.game.Content.Load<Texture2D>("Backgrounds/IslandsMap"), Program.game.Content.Load<Song>("Music/Level1"));
            return newLevel;
            
        }

        public Dictionary<int, List<Enemy>> BuildLevelOneDictionary()
        {
            /*for (int i = 5; i < 50; i += 5)
            {
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createGrunt(0, 100, 501, 40, 1, 0.1));
                timeList.Add(EnemyGenerator.createGrunt(2000, 100, 1300, 40, 1, 0.1));
                levelCatalogue.Add(i, timeList);
            }*/
            timeList = new List<Enemy>();
            timeList.Add(EnemyGenerator.createGrunt(0, 100, 501, 40, 1, 0.1));
            levelCatalogue.Add(1, timeList);
            return levelCatalogue;
        }
    }
}
