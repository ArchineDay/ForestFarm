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
            ChallengeCotroller.OnChallengeFinish.Register(challenge =>
            {
                AudioController.Get.SfxChallengeFinish.Play();

                if (ChallengeCotroller.Challenges.All(challenges => challenges.State == Challenge.States.Finished))
                {
                    ActionKit.Delay(0.5f, () => { SceneManager.LoadScene("GamePass"); }).Start(this);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
        }
    }
}