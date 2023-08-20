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
            return Global.Days.Value != StartDate && ChallengeController.RipeAndHarvestCountInCurrentDay.Value >= 1 &&
                   ChallengeController.RipeAndHarvestRadishCountInCurrentDay >= 1;
        }

        public override void OnFinish()
        {
        }
    }
}