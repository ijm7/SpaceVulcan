using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Controller;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            var thread = new Thread(() => { levelCatalogue = BuildLevelDictionary(_state); });
            //levelCatalogue = BuildLevelDictionary(_state);
            thread.Start();
            thread.Join();
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
                    timeList.Add(EnemyGenerator.createBasicGrunt(680 + j, -100, 680 + j, 900, 1, 0.1, 1));
                }
                levelCatalogue.Add(1, timeList);
                //WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(590+j, -100, 590 + j, 600, 1, 0.1, 1));
                }
                levelCatalogue.Add(10, timeList);
                //
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(770 + j, -100, 770 + j, 600, 1, 0.1, 1));
                }
                levelCatalogue.Add(15, timeList);
                //
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -100, 500 + j, 600, 1, 0.1, 1));
                }
                levelCatalogue.Add(20, timeList);
                //WAVE 2 5 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j+=180)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(500+j, -100, 500+j, 600, 1, 0.1,1));

                }
                levelCatalogue.Add(30, timeList);
                //WAVE 3 5 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(590 + j, -100, 590 + j, 100, 1, 0.1, 1));

                }
                levelCatalogue.Add(40, timeList);
                //DIAGONALS WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 110)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(300, 100 + j, 1261 - j * 2, 110 + j, 6, 0.1, 1));
                    levelCatalogue.Add(50 + i, timeList);

                }
                //SECOND DIAGONAL WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 130)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(1600, 100 + j, 491 + j * 2, 110 + j, 6, 0.1, 1));
                    levelCatalogue.Add(60 + i, timeList);

                }
                //COMBINED DIAGONAL WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 110)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(300, 100 + j, 1261 - j * 2, 110 + j, 6, 0.1, 1));
                    timeList.Add(EnemyGenerator.createFastGrunt(1600, 100 + j, 491 + j * 2, 110 + j, 6, 0.1, 1));
                    levelCatalogue.Add(70 + i, timeList);
                }
                //MASS WAVE
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createBasicGrunt(500+j, -40-j, 500+j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -500 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -1000 - j, 500 + j, 400, 5, 10.0, 1));
                    if (i<4)
                    {
                        timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2000 - j, 500 + j, 400, 5, 10.0, 1));
                    }
                    levelCatalogue.Add(80 + i, timeList);
                }
                //MEDIUM SHIP
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createEasyMedium(700, -100, 700, 0, 8, 20.0, 1));
                timeList.Add(EnemyGenerator.createEasyMedium(1050, -1000, 1050, 0, 8, 20.0, 1));
                for (int i = 0, j=0; i < 5; i++, j+=160)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(500-j, -250+j, 500+j, 300, 5, 10.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(1310-j, -1250+j, 1310-j, 300, 5, 10.0, 1));
                }
                levelCatalogue.Add(130, timeList);
                //BOSS
                /*timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createEasyBoss());
                levelCatalogue.Add(100, timeList);*/




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
                    timeList.Add(EnemyGenerator.createFastGrunt(0, 100, 671, 40, 6, 0.1,1));
                    timeList.Add(EnemyGenerator.createFastGrunt(2000, 100, 1239, 40, 6, 0.1,1));
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
