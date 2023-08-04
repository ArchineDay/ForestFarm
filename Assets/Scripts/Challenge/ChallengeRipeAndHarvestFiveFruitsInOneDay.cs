namespace IndieFarm
{
    public class ChallengeRipeAndHarvestFiveFruitsInOneDay:Challenge
    {
        public override string Name { get; } = "同一天收获五个当天成熟的果子";

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