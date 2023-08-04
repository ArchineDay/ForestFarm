namespace IndieFarm
{
    public class ChallengeRipeAndHarvestFiveFruitsInOneDay:Challenge
    {
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return Global.RipeAndHarvestCountInCurrentDay.Value >= 5;
        }

        public override void OnFinish()
        {
            
        }
    }
}