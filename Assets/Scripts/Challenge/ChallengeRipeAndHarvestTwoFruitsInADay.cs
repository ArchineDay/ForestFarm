using System.Collections.Generic;
using QFramework;
using UnityEngine.SceneManagement;

namespace IndieFarm
{
    public class ChallengeRipeAndHarvestTwoFruitsInADay : Challenge
    {
        public override string Name { get; } = "同一天收获两个当天成熟的果子";

        public override void OnStart()
        {
        }

        public override bool CheckFinish()
        {
            return Global.Days.Value!=StartDate&&Global.RipeAndHarvestCountInCurrentDay.Value >= 2;
        }

        public override void OnFinish()
        {
            //this.UnRegisterAll();
            // ActionKit.Delay(1.0f, ()
            //     =>
            // { 
            //     SceneManager.LoadScene("GamePass");
            // }).StartGlobal();
        }

        //public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
    }
}