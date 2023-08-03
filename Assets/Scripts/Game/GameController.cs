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
           Global.OnChallengeFinish.Register(challenge =>
           {
               Debug.Log("@@@"+challenge.GetType().Name+"挑战完成");
           } ).UnRegisterWhenGameObjectDestroyed(gameObject);
           
           // Global.FruitCount.Register(fruitCount =>
           // {
           //     if (fruitCount >= 1)
           //     {
           //         ActionKit.Delay(1.0f, () =>
           //         {
           //             SceneManager.LoadScene("GamePass");
           //         }).Start(this);
           //
           //     }
           // }).UnRegisterWhenGameObjectDestroyed(this);
           // //this 关键字指的是当前 GameController 类的实例对象
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