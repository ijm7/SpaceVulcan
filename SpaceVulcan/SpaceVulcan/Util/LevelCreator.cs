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
            timeList = new List<Enemy>();
        }

        public Dictionary<int, List<Enemy>> BuildLevelOne()
        {
            timeList.Add(EnemyGenerator.createGrunt(-20, -20, 500,40));
            timeList.Add(EnemyGenerator.createGrunt(2000, -20, 1200, 40));
            levelCatalogue.Add(5, timeList);
            return levelCatalogue;
            
        }
    }
}
