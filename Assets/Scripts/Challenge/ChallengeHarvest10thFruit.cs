namespace IndieFarm
{
    public class ChallengeHarvest10thFruit:Challenge
    {
        public override string Name { get; } = "收获第10个水果";
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return ChallengeController.HarvestedFruitCount>=10;
        }

        public override void OnFinish()
        {
           
        }
    }
}