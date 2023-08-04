namespace IndieFarm
{
    public class ChallengeHarvesetFirstFruit:Challenge
    {
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return Global.FruitCount.Value > 0;
        }

        public override void OnFinish()
        {
            
        }
    }
}