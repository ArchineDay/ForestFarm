namespace IndieFarm
{
    public class ChallengeFruitCountGreaterOrEqual10 : Challenge

    {
        public override string Name { get; } = "拥有10个果实";

        public override void OnStart()
        {
        }

        public override bool CheckFinish()
        {
            return Global.FruitCount.Value >= 10;
        }

        public override void OnFinish()
        {
        }
    }
}