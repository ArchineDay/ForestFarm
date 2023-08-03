using System.Collections.Generic;
using QFramework;
using UnityEngine.SceneManagement;

namespace IndieFarm
{
    public class ChallengeRipeAndHarvestTwoFruitsInOneDay : Challenge, IUnRegisterList
    {
        public override void OnStart()
        {
            //监听当前成熟的植物是不是当天成熟收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.RipeDay == Global.Days.Value)
                {
                    Global.RipeAndHarvestCountInCurrentDay.Value++;
                }
            }).AddToUnregisterList(this); //当前类
        }

        public override bool CheckFinish()
        {
            return Global.RipeAndHarvestCountInCurrentDay >= 2;
        }

        public override void OnFinish()
        {
            this.UnRegisterAll();
            // ActionKit.Delay(1.0f, ()
            //     =>
            // { 
            //     SceneManager.LoadScene("GamePass");
            // }).StartGlobal();
        }

        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
    }
}