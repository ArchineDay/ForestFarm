namespace IndieFarm
{
    public class ChallengeHarvest10thRadish:Challenge
    {
        public override string Name { get; }="收获第10个萝卜";
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return ChallengeController.HarvestedRadishCount>= 10;
        }

        public override void OnFinish()
        {
           
        }
    }
}