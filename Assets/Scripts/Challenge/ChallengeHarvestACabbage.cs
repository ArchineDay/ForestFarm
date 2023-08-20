namespace IndieFarm
{
    public class ChallengeHarvestACabbage:Challenge
    {
        public override string Name { get; } = "收获一棵白菜";

        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {//隔开挑战日期，当天是startDATe说明不可能成熟
            return Global.Days.Value!=StartDate&&ChallengeController.CabbageHarvestCountInCurrentDay.Value >= 1;
        }

        public override void OnFinish()
        {
            
        }
    }
}