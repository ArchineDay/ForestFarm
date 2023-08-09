using Unity.VisualScripting;

namespace IndieFarm
{
    public class ChallengeRipeAndHarvestFruitAndRadishInOneDay : Challenge
    {
        public override string Name { get; } = "同一天收获当天成熟的果子和萝卜";

        public override void OnStart()
        {
        }

        public override bool CheckFinish()
        {
            return Global.Days.Value != StartDate && Global.RipeAndHarvestCountInCurrentDay.Value >= 1 &&
                   Global.RipeAndHarvestRadishCountInCurrentDay >= 1;
        }

        public override void OnFinish()
        {
        }
    }
}