namespace IndieFarm
{
    public class ChallengeCabbageCountGreaterOrEqual10:Challenge
    {
        public override string Name { get; } = "拥有10棵白菜";
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
         return Global.CabbageCount.Value >= 10;
        }

        public override void OnFinish()
        {
           
        }
    }
}