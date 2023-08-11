namespace IndieFarm
{
    public class ChallengeRadishCountGreaterOrEqual10:Challenge
    {
        public override string Name { get; } = "拥有10个萝卜";
        public override void OnStart()
        {
         
        }

        public override bool CheckFinish()
        {
            return Global.RadishCount.Value >= 10;
        }

        public override void OnFinish()
        {
           
        }
    }
}