using System;
using System.Linq;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

namespace IndieFarm
{
    public partial class GameController : ViewController
    {
        void Start()
        {
            
            //监听当前成熟的植物是不是当天成熟收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant.RipeDay == Global.Days.Value)
                {
                    Global.RipeAndHarvestCountInCurrentDay.Value++;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject); //当前类
            
            
           Global.OnChallengeFinish.Register(challenge =>
           {
               Debug.Log("@@@"+challenge.GetType().Name+"挑战完成");

               if (Global.Challenges.All(challenges=>challenges.State==Challenge.States.Finished))
               {
                   ActionKit.Delay(0.5f, () =>
                   {
                       SceneManager.LoadScene("GamePass");
                   }).Start(this);
               }
           } ).UnRegisterWhenGameObjectDestroyed(gameObject);
           
        }

        private void Update()
        {
            foreach (var challenge in Global.Challenges.Where(challenge =>challenge.State!=Challenge.States.Finished))
            {
                if (challenge.State == Challenge.States.NotStart)
                {
                    challenge.OnStart();
                    challenge.State= Challenge.States.Started;
                }

                if (challenge.State == Challenge.States.Started)
                {
                    if (challenge.CheckFinish())
                    {
                        challenge.OnFinish();
                        challenge.State = Challenge.States.Finished;
                        Global.OnChallengeFinish.Trigger(challenge);
                    }
                    
                }
            }
        }
    }
}