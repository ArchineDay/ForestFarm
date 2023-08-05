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
            //挑战开始，随机添加一个挑战
            var randomItem = Global.Challenges.GetRandomItem();
            Global.ActiveChallenges.Add(randomItem);


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
                AudioController.Get.SfxChallengeFinish.Play();
                
                if (Global.Challenges.All(challenges => challenges.State == Challenge.States.Finished))
                {
                    ActionKit.Delay(0.5f, () => { SceneManager.LoadScene("GamePass"); }).Start(this);
                    /*
                     * 移除所有完成的挑战
                     */
                    //Global.FinishedChallenges.RemoveAll(c => c.State == Challenge.States.Finished);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            var hasFinishChallenge = false;
            foreach (var challenge in Global.ActiveChallenges)
            {
                if (challenge.State == Challenge.States.NotStart)
                {
                    challenge.StartDate= Global.Days.Value;
                    challenge.OnStart();
                    challenge.State = Challenge.States.Started;
                }

                if (challenge.State == Challenge.States.Started)
                {
                    if (challenge.CheckFinish())
                    {
                        challenge.OnFinish();
                        challenge.State = Challenge.States.Finished;
                        Global.OnChallengeFinish.Trigger(challenge);
                        Global.FinishedChallenges.Add(challenge);
                        hasFinishChallenge = true;
                    }
                }
            }

            if (hasFinishChallenge)
            {
                Global.ActiveChallenges.RemoveAll(challenge =>
                    challenge.State == Challenge.States.Finished); //移除已经完成的挑战
            }

            //一个挑战完成后，随机添加另一个挑战
             if (Global.ActiveChallenges.Count==0&&Global.FinishedChallenges.Count!=Global.Challenges.Count)
             {
                var randomItem = Global.Challenges.Where(c => c.State == Challenge.States.NotStart).ToList()
                     .GetRandomItem();
                 Global.ActiveChallenges.Add(randomItem);
             }
        }
    }
}