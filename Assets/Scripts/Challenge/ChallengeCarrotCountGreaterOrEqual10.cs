namespace IndieFarm
{
    public class ChallengeCarrotCountGreaterOrEqual10:Challenge
    {
        public override string Name { get; } = "拥有10棵胡萝卜";
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return Global.CarrotCount.Value >= 10;
        }

        public override void OnFinish()
        {
           
        }
    }
}