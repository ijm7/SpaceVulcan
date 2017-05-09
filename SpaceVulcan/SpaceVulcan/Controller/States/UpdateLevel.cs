﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVulcan.Model;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Levels;
using SpaceVulcan.Model.Players;
using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller.States
{
    class UpdateLevel
    {
        int levelStartTime;
        float trackerTime;
        int marker;
        int projectileMarker;
        Dictionary<int, List<Enemy>> levelDictionary;
        Random rnd;


        List<Enemy> newEnemies;
        public UpdateLevel(GameTime start, Level currentLevel)
        {
            levelStartTime = 0;
            marker = 0;
            this.levelDictionary = currentLevel.enemyDictionary;
            rnd = new Random();
        }

        public void Update(ref Player player, KeyboardState keyState, float elapsed, ref List<Projectile> projectileList, GameTime gameTime, ref List<Enemy> existingEnemies, ref GameState _state, ref EventTracker eventTracker)
        {
            eventTracker = new EventTracker();
            trackerTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            levelStartTime = (int)Math.Floor(trackerTime);

            System.Diagnostics.Debug.WriteLine(levelStartTime);
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
                //player.position.Y += 10;
                if (player.boundingBox.Bottom + player.speed < GameArea.BOTTOM)
                {
                    player.position = new Vector2(player.position.X, player.position.Y + player.speed);
                }
                else
                {
                    player.position = new Vector2(player.position.X, GameArea.BOTTOM - player.boundingBox.Height);
                }
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

            updateShield(ref player, keyState);
            spawnEnemies(ref existingEnemies);
            if (existingEnemies.Count != 0)
            {
                moveEnemies(ref existingEnemies);
            }
            newEnemyProjectile(ref projectileList, existingEnemies);
            updateProjectiles(ref projectileList, player);
            checkProjectileCollisions(ref projectileList, ref existingEnemies, ref player, ref eventTracker);
            checkEnd(ref _state, existingEnemies);
        }

        public void NewPlayerProjectile(Player player, ref List<Projectile> projectileList)
        {
            Projectile projectile;
            Projectile secondaryProjectile;
            Projectile tertiaryProjectile;
            Vector2 initialProjectilePosition = new Vector2(0);
            switch (player._projectileType)
            {
                case ProjectileType.Laser:
                    projectile = new Projectile(initialProjectilePosition, player.damage, 5, ProjectileType.Laser, ProjectileDirection.North);
                    projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
                    Vector2 newLaserProjectilePosition = new Vector2(player.position.X + player.boundingBox.Width/2 - projectile.boundingBox.Width/2, player.boundingBox.Top - projectile.boundingBox.Height);
                    projectile.position = newLaserProjectilePosition;
                    projectileList.Add(projectile);
                    break;
                case ProjectileType.MassDriver:
                    projectile = new Projectile(initialProjectilePosition, player.damage, 5, ProjectileType.MassDriver, ProjectileDirection.North);
                    secondaryProjectile = new Projectile(initialProjectilePosition, player.damage, 5, ProjectileType.MassDriver, ProjectileDirection.North);
                    projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/MassDriver");
                    secondaryProjectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/MassDriver");
                    Vector2 newMassProjectilePosition = new Vector2(player.position.X + 10, player.boundingBox.Top - projectile.boundingBox.Height);
                    Vector2 newSecondaryMassProjectilePosition = new Vector2(player.boundingBox.Right - 10 - projectile.boundingBox.Width, player.boundingBox.Top - projectile.boundingBox.Height);
                    projectile.position = newMassProjectilePosition;
                    secondaryProjectile.position = newSecondaryMassProjectilePosition;
                    projectileList.Add(projectile);
                    projectileList.Add(secondaryProjectile);
                    break;
                case ProjectileType.Missile:
                    projectile = new Projectile(initialProjectilePosition, player.damage, 5, ProjectileType.Missile, ProjectileDirection.North);
                    secondaryProjectile = new Projectile(initialProjectilePosition, player.damage, 5, ProjectileType.Missile, ProjectileDirection.North);
                    tertiaryProjectile = new Projectile(initialProjectilePosition, player.damage, 5, ProjectileType.Missile, ProjectileDirection.North);

                    projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Missile");
                    secondaryProjectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Missile");
                    tertiaryProjectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Missile");
                    Vector2 newMissileProjectilePosition = new Vector2(player.position.X, player.boundingBox.Top - projectile.boundingBox.Height);
                    Vector2 newSecondaryMissileProjectilePosition = new Vector2(player.position.X + player.boundingBox.Width / 2 - projectile.boundingBox.Width / 2, player.boundingBox.Top - projectile.boundingBox.Height);
                    Vector2 newTertiaryMissileProjectilePosition = new Vector2(player.boundingBox.Right - projectile.boundingBox.Width, player.boundingBox.Top - projectile.boundingBox.Height);
                    projectile.position = newMissileProjectilePosition;
                    secondaryProjectile.position = newSecondaryMissileProjectilePosition;
                    tertiaryProjectile.position = newTertiaryMissileProjectilePosition;
                    projectileList.Add(projectile);
                    projectileList.Add(secondaryProjectile);
                    projectileList.Add(tertiaryProjectile);
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
                        projectileList[i].position = new Vector2(projectileList[i].position.X, projectileList[i].position.Y - 20);
                        break;
                    case ProjectileDirection.South:
                        projectileList[i].position = new Vector2(projectileList[i].position.X, projectileList[i].position.Y + 20);
                        break;
                }
            }
            /*if (CollisionChecker.checkProjectileBounds(projectileList[i]))
            {
                projectileList.Remove(projectileList[i]);
            }*/
          
        }

        private void updateShield(ref Player player, KeyboardState keyState)
        {
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
                if (existingEnemies[i].secondaryDestination.X != 0)
                {
                    if (existingEnemies[i].position == existingEnemies[i].destination)
                    {
                        existingEnemies[i].firstDestination = true;
                    }
                    if (existingEnemies[i].position != existingEnemies[i].secondaryDestination)
                    {
                        if (existingEnemies[i].position.X < existingEnemies[i].secondaryDestination.X)
                        {
                            if (existingEnemies[i].position.X + existingEnemies[i].speed > existingEnemies[i].secondaryDestination.X)
                            {
                                existingEnemies[i].position = new Vector2(existingEnemies[i].secondaryDestination.X, existingEnemies[i].position.Y);
                            }
                            else
                            {
                                existingEnemies[i].position = new Vector2(existingEnemies[i].position.X + existingEnemies[i].speed, existingEnemies[i].position.Y);
                            }
                        }
                        else if (existingEnemies[i].position.X > existingEnemies[i].secondaryDestination.X)
                        {
                            if (existingEnemies[i].position.X - existingEnemies[i].speed < existingEnemies[i].secondaryDestination.X)
                            {
                                existingEnemies[i].position = new Vector2(existingEnemies[i].secondaryDestination.X, existingEnemies[i].position.Y);
                            }
                            else
                            {
                                existingEnemies[i].position = new Vector2(existingEnemies[i].position.X - existingEnemies[i].speed, existingEnemies[i].position.Y);
                            }
                        }
                        if (existingEnemies[i].position.Y < existingEnemies[i].secondaryDestination.Y)
                        {
                            if (existingEnemies[i].position.Y + existingEnemies[i].speed > existingEnemies[i].secondaryDestination.Y)
                            {
                                existingEnemies[i].position = new Vector2(existingEnemies[i].position.X, existingEnemies[i].secondaryDestination.Y);
                            }
                            else
                            {
                                existingEnemies[i].position = new Vector2(existingEnemies[i].position.X, existingEnemies[i].position.Y + existingEnemies[i].speed);
                            }
                        }
                        else if (existingEnemies[i].position.Y > existingEnemies[i].secondaryDestination.Y)
                        {
                            if (existingEnemies[i].position.Y - existingEnemies[i].speed < existingEnemies[i].secondaryDestination.Y)
                            {
                                existingEnemies[i].position = new Vector2(existingEnemies[i].position.X, existingEnemies[i].secondaryDestination.Y);
                            }
                            else
                            {
                                existingEnemies[i].position = new Vector2(existingEnemies[i].position.X, existingEnemies[i].position.Y - existingEnemies[i].speed);
                            }
                        }
                    }
                }
            }
        }

        private void checkProjectileCollisions(ref List<Projectile> projectileList, ref List<Enemy> existingEnemies, ref Player player, ref EventTracker eventTracker)
        {
            List<int> projectilesToRemove = new List<int>();
            List<int> enemiesToRemove = new List<int>();
            for (int i = 0; i < projectileList.Count; i++)
            {
                for (int j = 0; j < existingEnemies.Count; j++)
                {
                    if (projectileList[i].boundingBox.Intersects(existingEnemies[j].boundingBox))
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
                            enemiesToRemove.Add(j);
                        }
                    }
                }
            }
            for (int i = projectilesToRemove.Count - 1; i >= 0; i--)
            {
                projectileList.RemoveAt(projectilesToRemove.ElementAt(i));
            }
            projectilesToRemove.Clear();
            for (int i = enemiesToRemove.Count - 1; i >= 0; i--)
            {
                existingEnemies.RemoveAt(enemiesToRemove.ElementAt(i));
                
            }
            for (int i = 0; i < projectileList.Count; i++)
            {
                if (player.boundingBox.Intersects(projectileList[i].boundingBox))
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
                    player.shield -= 0.2;

                }
            }
            projectilesToRemove.Clear();
            enemiesToRemove.Clear();
        }

        private void newEnemyProjectile(ref List<Projectile> projectileList, List<Enemy> existingEnemies)
        {
            Vector2 initialProjectilePosition = new Vector2(0);
            for (int i = 0; i < existingEnemies.Count; i++)
            {
                //int test = rnd.Next(1, 15);
                //if (test == 3)
                if (trackerTime + existingEnemies[i].fireRate/10 > existingEnemies[i].nextSpawn)
                {
                    Projectile projectile = (Projectile)existingEnemies[i].projectile.Clone();
                    Vector2 newProjectilePosition = new Vector2(existingEnemies[i].position.X + existingEnemies[i].boundingBox.Width/2, existingEnemies[i].boundingBox.Bottom);
                    projectile.position = newProjectilePosition;
                    if (CollisionChecker.checkProjectileBounds(projectile))
                    {
                        existingEnemies[i].nextSpawn = trackerTime + existingEnemies[i].fireRate;
                        existingEnemies[i].lastSpawn = trackerTime;
                        projectileList.Add(projectile);
                    }
                }
            }
        }

        private void checkEnd(ref GameState _state, List<Enemy> existingEnemies)
        {
            int dictChecker = 0;
            //THIS WORKS PROBABLY
            if (levelDictionary.Count!=0 || existingEnemies.Count!=0)
            {
                dictChecker++;
            }
            
            if (dictChecker==0 && _state == GameState.Level1)
            {
                _state = GameState.Level2;
            }
        }
    }
}