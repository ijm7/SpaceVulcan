using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Levels;
using SpaceVulcan.Model.Players;
using SpaceVulcan.Model.Projectiles;
using SpaceVulcan.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpaceVulcan.Controller.States
{
    class UpdateLevel
    {
        int levelStartTime;
        float trackerTime;
        int marker;
        Dictionary<int, List<Enemy>> levelDictionary;
        Random rnd;
        LevelCreator levelCreator;
        List<Enemy> newEnemies;
        public UpdateLevel(Level currentLevel)
        {
            levelStartTime = 0;
            marker = 0;
            levelDictionary = currentLevel.enemyDictionary;
            rnd = new Random();
            levelCreator = new LevelCreator();
        }

        public void Update(ref Player player, KeyboardState keyState, KeyboardState prevKeyState, float elapsed, ref List<Projectile> projectileList, GameTime gameTime, ref List<Enemy> existingEnemies, ref GameState _state, ref EventTracker eventTracker, ref UpdateLevel updateLevel)
        {
            
            eventTracker = new EventTracker();
            if (_state == GameState.Level1)
            {
                eventTracker.prevLevel = 0;
            }
            else if (_state == GameState.Level2)
            {
                eventTracker.prevLevel = 1;
            }
            else
            {
                eventTracker.prevLevel = 2;
            }
                trackerTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            levelStartTime = (int)Math.Floor(trackerTime);
            if (keyState.IsKeyDown(Keys.Right))
            {
                if (player.boundingBox.Right + player.speed < GameArea.RIGHT)
                {
                    player.position = new Vector2(player.position.X + player.speed, player.position.Y);
                }
                else
                {
                    player.position = new Vector2(GameArea.RIGHT - player.boundingBox.Width, player.position.Y);
                }
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                if (player.boundingBox.Left - player.speed > GameArea.LEFT)
                {
                    player.position = new Vector2(player.position.X - player.speed, player.position.Y);
                }
                else
                {
                    player.position = new Vector2(481, player.position.Y);
                }
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                if (player.boundingBox.Top - player.speed > GameArea.TOP)
                {
                    player.position = new Vector2(player.position.X, player.position.Y - player.speed);
                }
                else
                {
                    player.position = new Vector2(player.position.X, 1);
                }
            }
            
            if (keyState.IsKeyDown(Keys.Down))
            {
                if (player.boundingBox.Bottom + player.speed < GameArea.BOTTOM)
                {
                    player.position = new Vector2(player.position.X, player.position.Y + player.speed);
                }
                else
                {
                    player.position = new Vector2(player.position.X, GameArea.BOTTOM - player.boundingBox.Height);
                }
            }
            if (keyState.IsKeyDown(Keys.Enter) & !prevKeyState.IsKeyDown(Keys.Enter))
            {
                _state = GameState.Pause;
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                float canShoot = elapsed - player.lastShot;
                if (canShoot > player.fireRate)
                {
                    player.firing = true;
                    player.lastShot = elapsed;
                    NewPlayerProjectile(player, ref projectileList);
                }
                else
                {
                    player.firing = false;
                }
            }
            else
            {
                player.firing = false;
            }
            updateAbilities(ref player, keyState);
            updateShield(ref player, keyState, ref _state);
            spawnEnemies(ref existingEnemies);
            if (existingEnemies.Count != 0)
            {
                moveEnemies(ref existingEnemies);
            }
            newEnemyProjectile(ref projectileList, existingEnemies);
            updateProjectiles(ref projectileList, player);
            checkProjectileCollisions(ref projectileList, ref existingEnemies, ref player, ref eventTracker);
            checkEnd(ref _state, existingEnemies, ref eventTracker, ref updateLevel, ref player, ref projectileList);
        }

        public void NewPlayerProjectile(Player player, ref List<Projectile> projectileList)
        {
            Projectile[] projectile;
            Vector2[] newProjectilePosition;
            Vector2 initialProjectilePosition = new Vector2(0);
            switch (player._projectileType)
            {
                case ProjectileType.Laser:
                    projectile = new Projectile[1];
                    newProjectilePosition = new Vector2[1];
                    projectile[0] = new Projectile(initialProjectilePosition, player.damage, 15, ProjectileType.Laser, ProjectileDirection.North,false);
                    projectile[0].sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
                    newProjectilePosition[0] = new Vector2(player.position.X + player.boundingBox.Width/2 - projectile[0].boundingBox.Width/2, player.boundingBox.Top - projectile[0].boundingBox.Height);
                    projectile[0].position = newProjectilePosition[0];
                    projectileList.Add(projectile[0]);
                    break;
                case ProjectileType.MassDriver:
                    projectile = new Projectile[2];
                    newProjectilePosition = new Vector2[2];
                    projectile[0] = new Projectile(initialProjectilePosition, player.damage, 15, ProjectileType.MassDriver, ProjectileDirection.North, false);
                    projectile[1] = new Projectile(initialProjectilePosition, player.damage, 15, ProjectileType.MassDriver, ProjectileDirection.North, false);
                    for (int i=0; i < projectile.Length; i++)
                    {
                        projectile[i].sprite = Program.game.Content.Load<Texture2D>("Projectiles/MassDriver");
                    }
                    newProjectilePosition[0] = new Vector2(player.position.X + 10, player.boundingBox.Top - projectile[0].boundingBox.Height);
                    newProjectilePosition[1] = new Vector2(player.boundingBox.Right - 10 - projectile[1].boundingBox.Width, player.boundingBox.Top - projectile[1].boundingBox.Height);
                    for (int i = 0; i < projectile.Length; i++)
                    {
                        projectile[i].position = newProjectilePosition[i];
                        projectileList.Add(projectile[i]);
                    }
                    break;
                case ProjectileType.Missile:
                    projectile = new Projectile[3];
                    newProjectilePosition = new Vector2[3];
                    projectile[0] = new Projectile(initialProjectilePosition, player.damage, 15, ProjectileType.Missile, ProjectileDirection.North, false);
                    projectile[1] = new Projectile(initialProjectilePosition, player.damage, 15, ProjectileType.Missile, ProjectileDirection.North, false);
                    projectile[2] = new Projectile(initialProjectilePosition, player.damage, 15, ProjectileType.Missile, ProjectileDirection.North, false);
                    for (int i = 0; i < projectile.Length; i++)
                    {
                        projectile[i].sprite = Program.game.Content.Load<Texture2D>("Projectiles/Missile");
                    }
                    newProjectilePosition[0] = new Vector2(player.position.X, player.boundingBox.Top - projectile[0].boundingBox.Height);
                    newProjectilePosition[1] = new Vector2(player.position.X + player.boundingBox.Width / 2 - projectile[1].boundingBox.Width / 2, player.boundingBox.Top - projectile[1].boundingBox.Height);
                    newProjectilePosition[2] = new Vector2(player.boundingBox.Right - projectile[2].boundingBox.Width, player.boundingBox.Top - projectile[2].boundingBox.Height);
                    for (int i = 0; i < projectile.Length; i++)
                    {
                        projectile[i].position = newProjectilePosition[i];
                        projectileList.Add(projectile[i]);
                    }
                    break;
            }
        }

        public void updateProjectiles(ref List<Projectile> projectileList, Player player)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                switch (projectileList[i]._projectileDirection)
                {
                    case ProjectileDirection.North:
                        projectileList[i].position = new Vector2(projectileList[i].position.X, projectileList[i].position.Y - projectileList[i].speed);
                        break;
                    case ProjectileDirection.South:
                        projectileList[i].position = new Vector2(projectileList[i].position.X, projectileList[i].position.Y + projectileList[i].speed);
                        break;
                }
            }
            /*if (CollisionChecker.checkProjectileBounds(projectileList[i]))
            {
                projectileList.Remove(projectileList[i]);
            }*/
          
        }

        private void updateShield(ref Player player, KeyboardState keyState, ref GameState _state)
        {
            if (player.armour < 0)
            {
                _state = GameState.GameOver;
            }
            if (player.shield < 170 && player.firing==false)
            {
                player.shield += player.regenerationRate;
            }
        }

        private void spawnEnemies(ref List<Enemy> existingEnemies)
        {
            if (levelStartTime != marker)
            {
                if (levelDictionary.ContainsKey(levelStartTime))
                {
                    newEnemies = levelDictionary[levelStartTime];
                    existingEnemies = existingEnemies.Concat(newEnemies).ToList();
                    levelDictionary.Remove(levelStartTime);
                }
                marker = levelStartTime;
            }
        }

        private void moveEnemies(ref List<Enemy> existingEnemies)
        {
            for (int i = 0; i < existingEnemies.Count; i++)
            {
                if (existingEnemies[i].firstDestination == false || existingEnemies[i].position != existingEnemies[i].destination)
                {
                    if (existingEnemies[i].position.X < existingEnemies[i].destination.X)
                    {
                        if (existingEnemies[i].position.X + existingEnemies[i].speed > existingEnemies[i].destination.X)
                        {
                            existingEnemies[i].position = new Vector2(existingEnemies[i].destination.X, existingEnemies[i].position.Y);
                        }
                        else
                        {
                            existingEnemies[i].position = new Vector2(existingEnemies[i].position.X + existingEnemies[i].speed, existingEnemies[i].position.Y);
                        }
                    }
                    else if (existingEnemies[i].position.X > existingEnemies[i].destination.X)
                    {
                        if (existingEnemies[i].position.X - existingEnemies[i].speed < existingEnemies[i].destination.X)
                        {
                            existingEnemies[i].position = new Vector2(existingEnemies[i].destination.X, existingEnemies[i].position.Y);
                        }
                        else
                        {
                            existingEnemies[i].position = new Vector2(existingEnemies[i].position.X - existingEnemies[i].speed, existingEnemies[i].position.Y);
                        }
                    }
                    if (existingEnemies[i].position.Y < existingEnemies[i].destination.Y)
                    {
                        if (existingEnemies[i].position.Y + existingEnemies[i].speed > existingEnemies[i].destination.Y)
                        {
                            existingEnemies[i].position = new Vector2(existingEnemies[i].position.X, existingEnemies[i].destination.Y);
                        }
                        else
                        {
                            existingEnemies[i].position = new Vector2(existingEnemies[i].position.X, existingEnemies[i].position.Y + existingEnemies[i].speed);
                        }
                    }
                    else if (existingEnemies[i].position.Y > existingEnemies[i].destination.Y)
                    {
                        if (existingEnemies[i].position.Y - existingEnemies[i].speed < existingEnemies[i].destination.Y)
                        {
                            existingEnemies[i].position = new Vector2(existingEnemies[i].position.X, existingEnemies[i].destination.Y);
                        }
                        else
                        {
                            existingEnemies[i].position = new Vector2(existingEnemies[i].position.X, existingEnemies[i].position.Y - existingEnemies[i].speed);
                        }
                    }
                }
                if (existingEnemies[i].looping && existingEnemies[i].destination == existingEnemies[i].position)
                {
                    Vector2 destCopy = existingEnemies[i].destination;
                    existingEnemies[i].destination = existingEnemies[i].secondaryDestination;
                    existingEnemies[i].secondaryDestination = destCopy;
                }
            }
        }

        private void checkProjectileCollisions(ref List<Projectile> projectileList, ref List<Enemy> existingEnemies, ref Player player, ref EventTracker eventTracker)
        {
            List<int> projectilesToRemove = new List<int>();
            Stack enemiesToRemove = new Stack();
            for (int i = 0; i < projectileList.Count; i++)
            {
                for (int j = 0; j < existingEnemies.Count; j++)
                {
                    if (projectileList[i].boundingBox.Intersects(existingEnemies[j].boundingBox) && projectileList[i].enemy==false)
                    {
                        existingEnemies[j].hp -= (int)projectileList[i].damage;
                        eventTracker.enemyHitRecorded = true;
                        //projectileList.Remove(projectileList[i]);
                        if (!projectilesToRemove.Contains(i))
                        {
                            projectilesToRemove.Add(i);
                        }

                    }
                    if (existingEnemies[j].hp <= 0)
                    {
                        eventTracker.destroyed = true;
                        //existingEnemies.Remove(existingEnemies[j]);
                        if (!enemiesToRemove.Contains(j))
                        {
                            player.score += existingEnemies[j].score;
                            enemiesToRemove.Push(j);
                        }
                    }
                }
            }
            for (int i = projectilesToRemove.Count - 1; i >= 0; i--)
            {
                projectileList.RemoveAt(projectilesToRemove.ElementAt(i));
            }
            projectilesToRemove.Clear();
            //for (int i = enemiesToRemove.Count - 1; i >= 0; i--)
            for (int i = 0; i<enemiesToRemove.Count; i++)
            {
                existingEnemies.RemoveAt((int)enemiesToRemove.Pop());
                
            }
            for (int i = 0; i < projectileList.Count; i++)
            {
                if (player.boundingBox.Intersects(projectileList[i].boundingBox) && projectileList[i].enemy == true)
                {
                    eventTracker.playerHitRecorded = true;
                    if (player.shield - projectileList[i].damage < 0)
                    {
                        double difference = player.shield - projectileList[i].damage;
                        player.shield -= projectileList[i].damage;
                        player.shield -= difference;
                        player.armour += difference * 2;
                    }
                    else
                    {
                        player.shield -= projectileList[i].damage;
                    }
                    //projectileList.Remove(projectileList[i]);
                    if (!projectilesToRemove.Contains(i))
                    {
                        projectilesToRemove.Add(i);
                    }
                }
            }
            for (int i = projectilesToRemove.Count - 1; i >= 0; i--)
            {
                projectileList.RemoveAt(projectilesToRemove.ElementAt(i));
            }
            projectilesToRemove.Clear();
            for (int i = 0; i<projectileList.Count(); i++)
            {
                if (!CollisionChecker.checkProjectileBounds(projectileList[i]))
                {
                    //projectileList.Remove(projectileList[i]);
                    if (!projectilesToRemove.Contains(i))
                    {
                        projectilesToRemove.Add(i);
                    }
                }
            }
            for (int i = projectilesToRemove.Count - 1; i >= 0; i--)
            {
                projectileList.RemoveAt(projectilesToRemove.ElementAt(i));
            }
            
            for (int i = 0; i < existingEnemies.Count; i++)
            {
                if (player.boundingBox.Intersects(existingEnemies[i].boundingBox))
                {
                    eventTracker.playerHitRecorded = true;
                    player.armour -= 0.1;
                    if (player.shield >= 0.2)
                    {
                        player.shield -= 0.2;
                    }
                }
            }
            projectilesToRemove.Clear();
            enemiesToRemove.Clear();
        }

        private void newEnemyProjectile(ref List<Projectile> projectileList, List<Enemy> existingEnemies)
        {
            Vector2 initialProjectilePosition = new Vector2(0);
            Projectile[] projectile;
            Vector2[] newProjectilePosition;
            for (int i = 0; i < existingEnemies.Count; i++)
            {
                if (trackerTime + existingEnemies[i].fireRate/10 > existingEnemies[i].nextSpawn && existingEnemies[i].boundingBox.Left>GameArea.LEFT && existingEnemies[i].boundingBox.Right<GameArea.RIGHT)
                {
                    if (existingEnemies[i].shots == 1)
                    {
                        projectile = new Projectile[1];
                        newProjectilePosition = new Vector2[1];
                        projectile[0] = (Projectile)existingEnemies[i].projectile.Clone();
                        newProjectilePosition[0] = new Vector2(existingEnemies[i].position.X + existingEnemies[i].boundingBox.Width / 2, existingEnemies[i].boundingBox.Bottom);
                        projectile[0].position = newProjectilePosition[0];
                        if (CollisionChecker.checkProjectileBounds(projectile[0]))
                        {
                            existingEnemies[i].nextSpawn = trackerTime + existingEnemies[i].fireRate;
                            existingEnemies[i].lastSpawn = trackerTime;
                            projectileList.Add(projectile[0]);
                        }
                    }
                    else if (existingEnemies[i].shots == 2)
                    {
                        projectile = new Projectile[2];
                        newProjectilePosition = new Vector2[2];
                        for (int j = 0; j < projectile.Length; j++)
                        {
                            projectile[j] = (Projectile)existingEnemies[i].projectile.Clone();
                        }
                        newProjectilePosition[0] = new Vector2(existingEnemies[i].position.X + 10, existingEnemies[i].boundingBox.Bottom);
                        newProjectilePosition[1] = new Vector2(existingEnemies[i].boundingBox.Right - 10 - projectile[1].boundingBox.Width, existingEnemies[i].boundingBox.Bottom);
                        for (int j = 0; j < projectile.Length; j++)
                        {
                            projectile[j].position = newProjectilePosition[j];
                            if (CollisionChecker.checkProjectileBounds(projectile[0]))
                            {
                                existingEnemies[i].nextSpawn = trackerTime + existingEnemies[i].fireRate;
                                existingEnemies[i].lastSpawn = trackerTime;
                                projectileList.Add(projectile[j]);
                            }
                        }
                    }
                    else if (existingEnemies[i].shots == 3)
                    {
                        projectile = new Projectile[3];
                        newProjectilePosition = new Vector2[3];
                        for (int j = 0; j < projectile.Length; j++)
                        {
                            projectile[j] = (Projectile)existingEnemies[i].projectile.Clone();
                        }
                        newProjectilePosition[0] = new Vector2(existingEnemies[i].position.X + 10, existingEnemies[i].boundingBox.Bottom);
                        newProjectilePosition[1] = new Vector2(existingEnemies[i].position.X + existingEnemies[i].boundingBox.Width / 2, existingEnemies[i].boundingBox.Bottom);
                        newProjectilePosition[2] = new Vector2(existingEnemies[i].boundingBox.Right - projectile[2].boundingBox.Width, existingEnemies[i].boundingBox.Bottom);
                        for (int j = 0; j < projectile.Length; j++)
                        {
                            projectile[j].position = newProjectilePosition[j];
                            if (CollisionChecker.checkProjectileBounds(projectile[0]))
                            {
                                existingEnemies[i].nextSpawn = trackerTime + existingEnemies[i].fireRate;
                                existingEnemies[i].lastSpawn = trackerTime;
                                projectileList.Add(projectile[j]);
                            }
                        }
                    }
                }
            }
        }

        private void checkEnd(ref GameState _state, List<Enemy> existingEnemies, ref EventTracker eventTracker, ref UpdateLevel updateLevel, ref Player player, ref List<Projectile> projectileList)
        {
            int dictChecker = 0;
            //THIS WORKS PROBABLY
            if (levelDictionary.Count!=0 || existingEnemies.Count!=0)
            {
                dictChecker++;
            }
            
            if (dictChecker==0)
            {
                double timeBonus;
                timeBonus = 200 / levelStartTime;
                projectileList.Clear();
                eventTracker.playerHitRecorded = false;
                eventTracker.enemyHitRecorded = false;
                eventTracker.destroyed = false;
                player.firing = false;
                player.armour = 170;
                player.shield = 170;
                player.score = player.score + (int)Math.Floor(timeBonus*100);

                for (int i = 0; i < player.abilityList.Count; i++)
                {
                    if (player.abilityList[i].identifier == 1)
                    {
                        player.damage = player.playerDefault.damage;
                    }
                    else if (player.abilityList[i].identifier == 2)
                    {
                        player.speed = player.playerDefault.speed;
                    }
                    else if (player.abilityList[i].identifier == 3)
                    {
                        player.armour = player.playerDefault.armour;
                    }
                    else if (player.abilityList[i].identifier == 4)
                    {
                        player.regenerationRate = player.playerDefault.regenerationRate;
                    }
                    else if (player.abilityList[i].identifier == 5)
                    {
                        player.fireRate = player.playerDefault.fireRate;
                    }
                    int x = 0;
                    x = player.abilityList[i].identifier;
                    player.abilityList[i].setup(x);
                }
                if (_state == GameState.Level1)
                {
                    eventTracker.prevLevel = 1;
                    _state = GameState.Intermission;
                }
                else if (_state == GameState.Level2)
                {
                    eventTracker.prevLevel = 2;
                    _state = GameState.Intermission;
                }
                else
                {
                    _state = GameState.End;
                }
                    
            }

        }

        private void updateAbilities(ref Player player, KeyboardState keyState)
        {
            for (int i = 0; i < player.abilityList.Count; i++)
            {
                
            }
            int keyPressed=0;
            if (keyState.IsKeyDown(Keys.D1) || keyState.IsKeyDown(Keys.NumPad1))
            {
                keyPressed = 1;
            }
            else if (keyState.IsKeyDown(Keys.NumPad2) | keyState.IsKeyDown(Keys.D2))
            {
                keyPressed = 2;
            }
            else if (keyState.IsKeyDown(Keys.NumPad3) | keyState.IsKeyDown(Keys.D3))
            {
                keyPressed = 3;
            }
            keyPressed -= 1;
            if (keyPressed > -1 && player.abilityList.Count>keyPressed)
            {
                if (player.abilityList[keyPressed].isAvailable)
                {
                    player.abilityList[keyPressed].isActive = true;
                    player.abilityList[keyPressed].isAvailable = false;
                    player.abilityList[keyPressed].lastUsed = trackerTime;
                    if (player.abilityList[keyPressed].identifier==1)
                    {
                        player.damage = player.abilityList[keyPressed].doubleDamage(player.damage);
                    }
                    else if (player.abilityList[keyPressed].identifier == 2)
                    {
                        player.speed = player.abilityList[keyPressed].doubleSpeed(player.speed);
                    }
                    else if (player.abilityList[keyPressed].identifier == 3)
                    {
                        player.armour = player.abilityList[keyPressed].armourRepair(player.armour);
                    }
                    else if (player.abilityList[keyPressed].identifier == 4)
                    {
                        player.regenerationRate = player.abilityList[keyPressed].increaseShieldRegenerationRate(player.regenerationRate);
                    }
                    if (player.abilityList[keyPressed].identifier == 5)
                    {
                        player.fireRate = player.abilityList[keyPressed].increaseFireRate(player.fireRate);
                    }
                }
            }
            for (int i = 0; i < player.abilityList.Count; i++)
            {
                if (player.abilityList[i].isActive)
                {
                    if (trackerTime - player.abilityList[i].lastUsed > player.abilityList[i].abilityTime)
                    {
                        player.abilityList[i].isActive = false;
                        player.abilityList[i].isAvailable = false;
                        player.abilityList[i].lastUsed = player.abilityList[i].lastUsed + player.abilityList[i].abilityTime;
                        if (player.abilityList[i].identifier == 1)
                        {
                            player.damage = player.playerDefault.damage;
                        }
                        else if (player.abilityList[i].identifier == 2)
                        {
                            player.speed = player.playerDefault.speed;
                        }
                        else if (player.abilityList[i].identifier == 3)
                        {
                            player.armour = player.playerDefault.armour;
                        }
                        else if (player.abilityList[i].identifier == 4)
                        {
                            player.regenerationRate = player.playerDefault.regenerationRate;
                        }
                        else if (player.abilityList[i].identifier == 5)
                        {
                            player.fireRate = player.playerDefault.fireRate;
                        }
                    }
                }
                else if (!player.abilityList[i].isAvailable)
                {
                    if (trackerTime - player.abilityList[i].lastUsed > player.abilityList[i].coolDown)
                    {
                        player.abilityList[i].isAvailable = true;
                    }
                }
            }
        }
    }
}
