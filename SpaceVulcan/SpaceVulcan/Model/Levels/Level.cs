﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpaceVulcan.Model.Enemies;
using System.Collections.Generic;

namespace SpaceVulcan.Model.Levels
{
    public class Level
    {
        public Level(Dictionary<int, List<Enemy>> enemyDictionary, Texture2D background, Song song)
        {
            this.enemyDictionary = enemyDictionary;
            this.background = background;
            this.song = song;
        }
        public Dictionary<int, List<Enemy>> enemyDictionary { get; set; }
        public Texture2D background { get; set; }
        public Song song { get; set; }
    }
}
