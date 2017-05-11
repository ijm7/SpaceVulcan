using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Controller;
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

        public Level BuildLevel(GameState _state)
        {
            Texture2D background = null;
            Song song = null;
            levelCatalogue = BuildLevelDictionary(_state);
            if (_state == GameState.Level1)
            {
                background = Program.game.Content.Load<Texture2D>("Backgrounds/IslandsMap");
                song = Program.game.Content.Load<Song>("Music/Level1");
            }
            else if (_state == GameState.Level2)
            {
                background = Program.game.Content.Load<Texture2D>("Backgrounds/Stars2");
                song = Program.game.Content.Load<Song>("Music/Level2");
            }
            else
            {
                background = Program.game.Content.Load<Texture2D>("Backgrounds/merged-nebula");
                song = Program.game.Content.Load<Song>("Music/Level3");
            }
            Level newLevel = new Level(levelCatalogue, background, song);        
            return newLevel;
        }

        



        public Dictionary<int, List<Enemy>> BuildLevelDictionary(GameState _state)
        {
            if (_state == GameState.Level1)
            {
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(820 + j, -400, 820 + j, 100, 1, 0.1, 1));
                }
                levelCatalogue.Add(3, timeList);
                //WAVE 2 5 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 5; i++, j+=180)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(500+j, -400, 500+j, 100, 1, 0.1,1));

                }
                levelCatalogue.Add(10, timeList);
                //WAVE 3 5 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 5; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(590 + j, -400, 590 + j, 100, 1, 0.1, 1));

                }
                levelCatalogue.Add(15, timeList);
                //WAVE 4 10 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 10; i++, j += 90)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(1420 - j - 84, -400, 1400-j-84, 180, 1, 0.1, 1));

                }
                levelCatalogue.Add(25, timeList);
                //WAVE 5 10 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 10; i++, j += 90)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(1420 - j - 84, -400, 1400 - j - 84, 40, 1, 0.1, 1));

                }
                levelCatalogue.Add(40, timeList);
            }
            else if (_state == GameState.Level2)
            {
                for (int i = 5; i < 50; i += 5)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createBasicGrunt(0, 100, 501, 40, 1, 0.1,1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(2000, 100, 1300, 40, 1, 0.1,1));
                    levelCatalogue.Add(i, timeList);
                }
            }
            else
            {
                for (int i = 5; i < 50; i += 5)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createBasicGrunt(0, 100, 501, 40, 1, 0.1,1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(2000, 100, 1300, 40, 1, 0.1,1));
                    levelCatalogue.Add(i, timeList);
                }
            }
            /*timeList = new List<Enemy>();
            timeList.Add(EnemyGenerator.createBasicGrunt(0, 100, 501, 40, 1, 0.1));
            levelCatalogue.Add(1, timeList);*/
            return levelCatalogue;
        }
    }
}
