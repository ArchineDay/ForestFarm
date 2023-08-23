namespace IndieFarm
{
    public class ChallengeHarvest10thCarrot:Challenge
    {
        public override string Name { get; }="收获第10个胡萝卜";
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return ChallengeController.HarvestedCarrotCount>= 10;
        }

        public override void OnFinish()
        {
           
        }
    }
}