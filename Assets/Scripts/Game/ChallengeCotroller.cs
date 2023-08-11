using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace IndieFarm
{
    public partial class ChallengeCotroller : ViewController
    {
        public Font Font;
        private GUIStyle mLabelStyle;


        //当天成熟果子的数量
        public static BindableProperty<int> RipeAndHarvestCountInCurrentDay = new(0);

        //当天成熟萝卜的数量
        public static BindableProperty<int> RipeAndHarvestRadishCountInCurrentDay = new(0);

        //当天收割果子的数量
        public static BindableProperty<int> HarvestCountInCurrentDay = new(0);

        //当天收割萝卜的数量
        public static BindableProperty<int> RadishHarvestCountInCurrentDay = new(0);

        //收获过的果实数量
        public static int HarvestedFruitCount = 0;
        //收获过的萝卜数量
        public static int HarvestedRadishCount = 0;

        public static List<Challenge> Challenges = new List<Challenge>()
        {
            new ChallengeHarvestAFruit(),
            new ChallengeRipeAndHarvestTwoFruitsInADay(),
            new ChallengeRipeAndHarvestFiveFruitsInADay(),
            new ChallengeHarvestARadish(),
            new ChallengeRipeAndHarvestFruitAndRadishInOneDay(),
            new ChallengeHarvest10thFruit(),
            new ChallengeHarvest10thRadish(),
            new ChallengeFruitCountGreaterOrEqual10(),
            new ChallengeRadishCountGreaterOrEqual10()
        };

        public static List<Challenge> ActiveChallenges = new List<Challenge>();

        public static List<Challenge> FinishedChallenges = new List<Challenge>();

        //挑战完成
        public static EasyEvent<Challenge> OnChallengeFinish = new EasyEvent<Challenge>();

        void Start()
        {
            mLabelStyle = new GUIStyle("Label")
            {
                font = Font
            };

            //挑战开始，随机添加一个挑战
            var randomItem = Challenges.GetRandomItem();
            ActiveChallenges.Add(randomItem);
            //监听当前成熟的植物是不是当天成熟收割的
            Global.OnPlantHarvest.Register(plant =>
            {
                if (plant is Plant)
                {
                    HarvestedFruitCount++;
                    HarvestCountInCurrentDay.Value++;
                    if (plant.RipeDay == Global.Days.Value)
                    {
                        RipeAndHarvestCountInCurrentDay.Value++;
                    }
                }

                if (plant is PlantRadish)
                {
                    HarvestedRadishCount++;
                    RadishHarvestCountInCurrentDay.Value++;
                    if (plant.RipeDay == Global.Days.Value)
                    {
                        RipeAndHarvestRadishCountInCurrentDay.Value++;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject); //当前类
        }

        private void OnGUI()
        {
            IMGUIHelper.SetDesignResolution(960, 540);

            GUI.Label(new Rect(960 - 300, 0, 300, 24), "@@ 挑战 @@", mLabelStyle);
            for (int i = 0; i < ActiveChallenges.Count; i++)
            {
                var challenge = ActiveChallenges[i];

                GUI.Label(new Rect(960 - 300, 24 + i * 24, 300, 24), challenge.Name, mLabelStyle);
            }

            for (int i = 0; i < FinishedChallenges.Count; i++)
            {
                var challenge = FinishedChallenges[i];

                GUI.Label(new Rect(960 - 300, 24 + (i + ActiveChallenges.Count) * 24, 300, 24),
                    "<color=green>" + challenge.Name + "</color>", mLabelStyle);
            }
        }


        private void Update()
        {
            var hasFinishChallenge = false;
            foreach (var challenge in ActiveChallenges)
            {
                if (challenge.State == Challenge.States.NotStart)
                {
                    challenge.StartDate = Global.Days.Value;
                    challenge.OnStart();
                    challenge.State = Challenge.States.Started;
                }

                if (challenge.State == Challenge.States.Started)
                {
                    if (challenge.CheckFinish())
                    {
                        challenge.OnFinish();
                        challenge.State = Challenge.States.Finished;
                        OnChallengeFinish.Trigger(challenge);
                        FinishedChallenges.Add(challenge);
                        hasFinishChallenge = true;
                    }
                }
            }

            if (hasFinishChallenge)
            {
                ActiveChallenges.RemoveAll(challenge =>
                    challenge.State == Challenge.States.Finished); //移除已经完成的挑战
            }

            //一个挑战完成后，随机添加另一个挑战
            if (ActiveChallenges.Count == 0 && FinishedChallenges.Count != Challenges.Count)
            {
                var randomItem = Challenges.Where(c => c.State == Challenge.States.NotStart).ToList()
                    .GetRandomItem();
                ActiveChallenges.Add(randomItem);
            }
        }
    }
}