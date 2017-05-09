using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model
{
    public class EventTracker
    {
        public EventTracker()
        {
            playerHitRecorded = false;
            enemyHitRecorded = false;
            destroyed = false;
        }
        public bool playerHitRecorded { get; set; }
        public bool enemyHitRecorded { get; set; }
        public bool destroyed { get; set; }
    }
}
