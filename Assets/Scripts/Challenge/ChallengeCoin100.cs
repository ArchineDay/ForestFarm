namespace IndieFarm
{
    public class ChallengeCoin100:Challenge
    {
        public override string Name { get; } = "赚取100金币";
        public override void OnStart()
        {
        
        }

        public override bool CheckFinish()
        {
           return  Global.Coin.Value >= 100;
        }

        public override void OnFinish()
        {
           
        }
    }
}