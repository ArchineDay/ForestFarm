namespace IndieFarm
{
    public class ChallengeHarvest10thCabbage:Challenge
    {
        public override string Name { get; } = "收获第10棵白菜";
        public override void OnStart()
        {
            
        }

        public override bool CheckFinish()
        {
            return ChallengeController.HarvestedCabbageCount >= 10;
        }

        public override void OnFinish()
        {
          
        }
    }
}