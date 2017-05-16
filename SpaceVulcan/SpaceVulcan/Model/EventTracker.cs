namespace SpaceVulcan.Model
{
    public class EventTracker
    {
        public EventTracker()
        {
            playerHitRecorded = false;
            enemyHitRecorded = false;
            destroyed = false;
            prevLevel = 0;
        }
        public bool playerHitRecorded { get; set; }
        public bool enemyHitRecorded { get; set; }
        public bool destroyed { get; set; }
        public int prevLevel { get; set; }
    }
}
