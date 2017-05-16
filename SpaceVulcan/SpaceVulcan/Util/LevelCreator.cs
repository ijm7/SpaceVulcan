using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Controller;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Levels;
using System.Collections.Generic;
using System.Threading;

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
                    timeList.Add(EnemyGenerator.createFastGrunt(300, 100 + j, 1271 - j * 2, 110 + j, 6, 0.1, 1));
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
                //DIAGONAL ATTACK
                timeList = new List<Enemy>();
                for (int i = 0, x = 0, y = 0; i < 4; i++, x+=85,y+=62)
                {
                    timeList.Add(EnemyGenerator.createBasicGrunt(2482+x, 2001+y, 482+x, 1+y, 1, 0.1, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(2482 + x+x, 2001 + (y+y), 482 + x, 1 + y + y, 1, 0.1, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(2482 + x+x+x, 2001 + (y+y+y), 482 + x, 1 + y + y + y, 1, 0.1, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(2482 - x, 2001 + y, 1353 - x, 1 + y, 1, 0.1, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(2482 - x - x, 2001 + (y + y), 1353 - x, 1 + y + y, 1, 0.1, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(2482 - x - x - x, 2001 + (y + y + y), 1353 - x, 1 + y + y + y, 1, 0.1, 1));
                }
                levelCatalogue.Add(145, timeList);
                //MASS WAVE 2
                for (int i = 0, j = 0; i < 7; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -40 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -500 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -1000 - j, 500 + j, 400, 5, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2000 - j, 500 + j, 400, 5, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2750 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -3500 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -4500 - j, 500 + j, 400, 5, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2750 - j, 500 + j, 400, 5, 10.0, 1));
                    }
                    levelCatalogue.Add(180 + i, timeList);
                }
                //1 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createEasyLoopingMedium(0, 50, 482, 50, 15, 20.0, 1));
                //timeList.Add(EnemyGenerator.createEasyLoopingMedium(-1500, 250, 482, 250, 15, 20.0, 1));
                //timeList.Add(EnemyGenerator.createEasyLoopingMedium(-3500, 450, 482, 450, 15, 20.0, 1));
                levelCatalogue.Add(255, timeList);
                //Mass WAVE 3
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -40 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -500 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -1000 - j, 500 + j, 400, 5, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2000 - j, 500 + j, 400, 5, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2750 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -3500 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -4500 - j, 500 + j, 400, 5, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2750 - j, 482 + j, 400, 5, 10.0, 1));
                    }
                    levelCatalogue.Add(295 + i, timeList);
                }
                //3 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createEasyLoopingMedium(0, 50, 482, 50, 15, 20.0, 1));
                timeList.Add(EnemyGenerator.createEasyLoopingMedium(-1500, 250, 482, 250, 15, 20.0, 1));
                timeList.Add(EnemyGenerator.createEasyLoopingMedium(-3500, 450, 482, 450, 15, 20.0, 1));
                
                levelCatalogue.Add(380, timeList);
                //4 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createEasyLoopingMedium(0, 50, 482, 50, 15, 20.0, 1));
                timeList.Add(EnemyGenerator.createEasyLoopingMedium(0, 250, 482, 250, 15, 20.0, 1));
                timeList.Add(EnemyGenerator.createEasyLoopingMedium(-480, 50, 482, 50, 15, 20.0, 1));
                timeList.Add(EnemyGenerator.createEasyLoopingMedium(-480, 250, 482, 250, 15, 20.0, 1));
                levelCatalogue.Add(420, timeList);
                //MASS WAVE
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -40 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -500 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -1000 - j, 500 + j, 400, 5, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2000 - j, 500 + j, 400, 5, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2750 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -3500 - j, 500 + j, 400, 2, 2.0, 1));
                    timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -4500 - j, 500 + j, 400, 5, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createBasicGrunt(500 + j, -2750 - j, 482 + j, 400, 5, 10.0, 1));
                    }
                    levelCatalogue.Add(480 + i, timeList);
                }
                //BOSS
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createEasyBoss());
                levelCatalogue.Add(525, timeList);
            }
            else if (_state == GameState.Level2)
            {
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createMediumGrunt(680 + j, -100, 680 + j, 900, 4, 2, 1));
                }
                levelCatalogue.Add(1, timeList);
                //WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createMediumGrunt(590 + j, -100, 590 + j, 600, 4, 2, 1));
                }
                levelCatalogue.Add(15, timeList);
                //
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createMediumGrunt(770 + j, -100, 770 + j, 600, 4, 2, 1));
                }
                levelCatalogue.Add(25, timeList);
                //
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -100, 500 + j, 600, 4, 2, 1));
                }
                levelCatalogue.Add(30, timeList);
                //WAVE 2 5 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -100, 500 + j, 600, 4, 2, 1));

                }
                levelCatalogue.Add(35, timeList);
                //WAVE 3 5 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createMediumGrunt(590 + j, -100, 590 + j, 100, 4, 2, 1));

                }
                levelCatalogue.Add(45, timeList);
                //DIAGONALS WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 110)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(300, 100 + j, 1261 - j * 2, 110 + j, 9, 5, 1));
                    levelCatalogue.Add(55 + i, timeList);

                }
                //SECOND DIAGONAL WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 130)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(1600, 100 + j, 491 + j * 2, 110 + j, 9, 5, 1));
                    levelCatalogue.Add(65 + i, timeList);

                }
                //COMBINED DIAGONAL WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 110)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(300, 100 + j, 1271 - j * 2, 110 + j, 9, 5, 1));
                    timeList.Add(EnemyGenerator.createFastGrunt(1600, 100 + j, 491 + j * 2, 110 + j, 9, 5, 1));
                    levelCatalogue.Add(75 + i, timeList);
                }
                //MASS WAVE
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -40 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -500 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -1000 - j, 500 + j, 400, 8, 15.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2000 - j, 500 + j, 400, 8, 15.0, 1));
                    }
                    levelCatalogue.Add(90 + i, timeList);
                }
                //MEDIUM SHIP
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createMediumMedium(700, -100, 700, 100, 16, 20.0, 2));
                timeList.Add(EnemyGenerator.createMediumMedium(1050, -1000, 1050, 100, 16, 20.0, 2));
                for (int i = 0, j = 0; i < 5; i++, j += 160)
                {
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 - j, -250 + j, 500 + j, 300, 8, 10.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(1310 - j, -1250 + j, 1310 - j, 300, 8, 10.0, 1));
                }
                levelCatalogue.Add(135, timeList);
                //MASS WAVE 2
                for (int i = 0, j = 0; i < 7; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -40 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -500 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -1000 - j, 500 + j, 400, 9, 20.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2000 - j, 500 + j, 9, 4, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2750 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -3500 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -4500 - j, 500 + j, 400, 9, 20.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2750 - j, 500 + j, 400, 9, 20.0, 1));
                    }
                    levelCatalogue.Add(190 + i, timeList);
                }
                //1 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createMediumLoopingMedium(0, 50, 482, 50, 14, 10.0, 3));
                //timeList.Add(EnemyGenerator.createMediumLoopingMedium(-1500, 250, 482, 250, 15, 20.0, 1));
                //timeList.Add(EnemyGenerator.createMediumLoopingMedium(-3500, 450, 482, 450, 15, 20.0, 1));
                levelCatalogue.Add(265, timeList);
                //Mass WAVE 3
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -40 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -500 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -1000 - j, 500 + j, 400, 9, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2000 - j, 500 + j, 400, 5, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2750 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -3500 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -4500 - j, 500 + j, 400, 9, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2750 - j, 482 + j, 400, 9, 10.0, 1));
                    }
                    levelCatalogue.Add(300 + i, timeList);
                }
                //3 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createMediumLoopingMedium(0, 50, 482, 50, 10, 20.0, 2));
                timeList.Add(EnemyGenerator.createMediumLoopingMedium(-1500, 250, 482, 250, 10, 20.0, 2));
                timeList.Add(EnemyGenerator.createMediumLoopingMedium(-3500, 450, 482, 450, 10, 20.0, 2));

                levelCatalogue.Add(385, timeList);
                //4 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createMediumLoopingMedium(0, 50, 482, 50, 10, 20.0, 2));
                timeList.Add(EnemyGenerator.createMediumLoopingMedium(0, 250, 482, 250, 10, 20.0, 2));
                timeList.Add(EnemyGenerator.createMediumLoopingMedium(-480, 50, 482, 50, 10, 20.0, 2));
                timeList.Add(EnemyGenerator.createMediumLoopingMedium(-480, 250, 482, 250, 10, 20.0, 2));
                levelCatalogue.Add(425, timeList);
                //MASS WAVE
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -40 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -500 - j, 500 + j, 400, 4, 4.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -1000 - j, 500 + j, 400, 9, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2000 - j, 500 + j, 400, 9, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2750 - j, 500 + j, 400, 4, 2.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -3500 - j, 500 + j, 400, 4, 2.0, 1));
                    timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -4500 - j, 500 + j, 400, 9, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createMediumGrunt(500 + j, -2750 - j, 482 + j, 400, 9, 10.0, 1));
                    }
                    levelCatalogue.Add(480 + i, timeList);
                }
                //BOSS
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createMediumBoss());
                levelCatalogue.Add(540, timeList);
            }
            else
            {
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createHardGrunt(680 + j, -100, 680 + j, 700, 7, 2, 2));
                }
                levelCatalogue.Add(1, timeList);
                //WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createHardGrunt(590 + j, -100, 590 + j, 600, 7, 2, 2));
                }
                levelCatalogue.Add(10, timeList);
                //
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createHardGrunt(770 + j, -100, 770 + j, 600, 7, 2, 2));
                }
                levelCatalogue.Add(20, timeList);
                //
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 2; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -100, 500 + j, 600, 7, 2, 2));
                }
                levelCatalogue.Add(25, timeList);
                //WAVE 2 5 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -100, 500 + j, 600, 7, 2, 2));

                }
                levelCatalogue.Add(30, timeList);
                //WAVE 3 5 GRUNT
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 180)
                {
                    timeList.Add(EnemyGenerator.createHardGrunt(590 + j, -100, 590 + j, 100, 7, 2, 2));

                }
                levelCatalogue.Add(40, timeList);
                //DIAGONALS WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 110)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(300, 100 + j, 1261 - j * 2, 110 + j, 9, 5, 2));
                    levelCatalogue.Add(50 + i, timeList);

                }
                //SECOND DIAGONAL WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 130)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(1600, 100 + j, 491 + j * 2, 110 + j, 9, 5, 2));
                    levelCatalogue.Add(60 + i, timeList);

                }
                //COMBINED DIAGONAL WAVE
                timeList = new List<Enemy>();
                for (int i = 0, j = 0; i < 4; i++, j += 110)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createFastGrunt(300, 100 + j, 1271 - j * 2, 110 + j, 9, 5, 2));
                    timeList.Add(EnemyGenerator.createFastGrunt(1600, 100 + j, 491 + j * 2, 110 + j, 9, 5, 2));
                    levelCatalogue.Add(70 + i, timeList);
                }
                //MASS WAVE
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -40 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -500 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -1000 - j, 500 + j, 400, 7, 15.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2000 - j, 500 + j, 400, 7, 15.0, 1));
                    }
                    levelCatalogue.Add(85 + i, timeList);
                }
                //MEDIUM SHIP
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createHardMedium(700, -100, 700, 100, 2, 50.0, 1));
                timeList.Add(EnemyGenerator.createHardMedium(1050, -1000, 1050, 100, 2, 50.0, 1));
                for (int i = 0, j = 0; i < 5; i++, j += 160)
                {
                    timeList.Add(EnemyGenerator.createHardGrunt(500 - j, -250 + j, 500 + j, 300, 7, 10.0, 1));
                    timeList.Add(EnemyGenerator.createHardGrunt(1310 - j, -1250 + j, 1310 - j, 300, 7, 10.0, 1));
                }
                levelCatalogue.Add(135, timeList);
                //MASS WAVE 2
                for (int i = 0, j = 0; i < 7; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -40 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -500 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -1000 - j, 500 + j, 400, 7, 20.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2000 - j, 500 + j, 9, 7, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2750 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -3500 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -4500 - j, 500 + j, 400, 7, 20.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2750 - j, 500 + j, 400, 7, 20.0, 1));
                    }
                    levelCatalogue.Add(180 + i, timeList);
                }
                //1 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createHardLoopingMedium(0, 50, 482, 50, 14, 10.0, 3));
                //timeList.Add(EnemyGenerator.createHardLoopingMedium(-1500, 250, 482, 250, 15, 20.0, 1));
                //timeList.Add(EnemyGenerator.createHardLoopingMedium(-3500, 450, 482, 450, 15, 20.0, 1));
                levelCatalogue.Add(280, timeList);
                //Mass WAVE 3
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -40 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -500 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -1000 - j, 500 + j, 400, 7, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2000 - j, 500 + j, 400, 7, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2750 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -3500 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -4500 - j, 500 + j, 400, 7, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2750 - j, 482 + j, 400, 9, 10.0, 1));
                    }
                    levelCatalogue.Add(230 + i, timeList);
                }
                //3 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createHardLoopingMedium(0, 50, 482, 50, 14, 20.0, 3));
                timeList.Add(EnemyGenerator.createHardLoopingMedium(-1500, 250, 482, 250, 14, 20.0, 3));
                timeList.Add(EnemyGenerator.createHardLoopingMedium(-3500, 450, 482, 450, 14, 20.0, 3));

                levelCatalogue.Add(300, timeList);
                //4 Medium
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createHardLoopingMedium(0, 50, 482, 50, 14, 20.0, 3));
                timeList.Add(EnemyGenerator.createHardLoopingMedium(0, 250, 482, 250, 14, 20.0, 3));
                timeList.Add(EnemyGenerator.createHardLoopingMedium(-480, 50, 482, 50, 14, 20.0, 3));
                timeList.Add(EnemyGenerator.createHardLoopingMedium(-480, 250, 482, 250, 14, 20.0, 3));
                levelCatalogue.Add(450, timeList);
                //MASS WAVE
                for (int i = 0, j = 0; i < 8; i++, j += 120)
                {
                    timeList = new List<Enemy>();
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -40 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -500 - j, 500 + j, 400, 9, 4.0, 2));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -1000 - j, 500 + j, 400, 7, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2000 - j, 500 + j, 400, 7, 10.0, 1));
                    }
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2750 - j, 500 + j, 400, 10, 2.0, 3));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -3500 - j, 500 + j, 400, 10, 2.0, 3));
                    timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -4500 - j, 500 + j, 400, 7, 10.0, 1));
                    if (i < 4)
                    {
                        timeList.Add(EnemyGenerator.createHardGrunt(500 + j, -2750 - j, 482 + j, 400, 7, 10.0, 1));
                    }
                    levelCatalogue.Add(510 + i, timeList);
                }
                //BOSS
                timeList = new List<Enemy>();
                timeList.Add(EnemyGenerator.createHardBoss());
                levelCatalogue.Add(675, timeList);
            }
            return levelCatalogue;
        }
    }
}
