using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace IndieFarm
{
    public partial class ChallengeController : ViewController
    {
        public Font Font;
        private GUIStyle mLabelStyle;


        //当天成熟胡萝卜的数量
        public static BindableProperty<int> RipeAndHarvestCarrotCountInCurrentDay = new(0);
        public static BindableProperty<int> RipeAndHarvestTomatoCountInCurrentDay = new(0);
        public static BindableProperty<int> RipeAndHarvestPotatoCountInCurrentDay = new(0);
        public static BindableProperty<int> RipeAndHarvestPumpkinCountInCurrentDay = new(0);
        public static BindableProperty<int> RipeAndHarvestBeanCountInCurrentDay = new(0);

        //当天收割萝卜的数量
        public static BindableProperty<int> CarrotHarvestCountInCurrentDay = new(0);
        public static BindableProperty<int> TomatoHarvestCountInCurrentDay = new(0);
        public static BindableProperty<int> PotatoHarvestCountInCurrentDay = new(0);
        public static BindableProperty<int> PumpkinHarvestCountInCurrentDay = new(0);
        public static BindableProperty<int> BeanHarvestCountInCurrentDay = new(0);

        //收获过的胡萝卜数量
        public static int HarvestedCarrotCount = 0;
        public static int HarvestedTomatoCount = 0;
        public static int HarvestedPotatoCount = 0;
        public static int HarvestedPumpkinCount = 0;
        public static int HarvestedBeanCount = 0;

        public static List<Challenge> Challenges = new List<Challenge>()
        {
            // new ChallengeCoin100(),
            // new ChallengeHarvest10thCarrot(),
            // new ChallengeCarrotCountGreaterOrEqual10(),
            new ChallengeHarvestACarrot(),
        };

        public static List<Challenge> ActiveChallenges = new List<Challenge>();

        public static List<Challenge> FinishedChallenges = new List<Challenge>();

        //挑战完成
        public static EasyEvent<Challenge> OnChallengeFinish = new EasyEvent<Challenge>();

        private void Awake()
        {
            Challenges.Add(new GenericChallenge().Key("结出一个土豆").OnCheckFinish(self =>
                Global.Days.Value != self.StartDate && PotatoHarvestCountInCurrentDay.Value >= 1));
            Challenges.Add(new GenericChallenge().Key("结出一个西红柿").OnCheckFinish(self =>
                Global.Days.Value != self.StartDate && TomatoHarvestCountInCurrentDay.Value >= 1));
            Challenges.Add(new GenericChallenge().Key("结出一个豌豆").OnCheckFinish(self =>
                Global.Days.Value != self.StartDate && BeanHarvestCountInCurrentDay.Value >= 1));
            Challenges.Add(new GenericChallenge().Key("结出一个南瓜").OnCheckFinish(self =>
                Global.Days.Value != self.StartDate && PumpkinHarvestCountInCurrentDay.Value >= 1));
            Challenges.Add(new GenericChallenge().Key("收获第10个土豆").OnCheckFinish(self =>
                HarvestedPotatoCount >= 10));
            Challenges.Add(new GenericChallenge().Key("收获第10个西红柿").OnCheckFinish(self =>
                HarvestedTomatoCount >= 10));
            Challenges.Add(new GenericChallenge().Key("收获第10个豌豆").OnCheckFinish(self =>
                HarvestedBeanCount >= 10));
            Challenges.Add(new GenericChallenge().Key("收获第10个南瓜").OnCheckFinish(self =>
                HarvestedPumpkinCount >= 10));
            Challenges.Add(new GenericChallenge().Key("拥有10个土豆").OnCheckFinish(self =>
                Global.PotatoCount >= 10));
            Challenges.Add(new GenericChallenge().Key("拥有10个西红柿").OnCheckFinish(self =>
                Global.TomatoCount >= 10));
            Challenges.Add(new GenericChallenge().Key("拥有10个豌豆").OnCheckFinish(self =>
                Global.BeanCount >= 10));
            Challenges.Add(new GenericChallenge().Key("拥有10个南瓜").OnCheckFinish(self =>
                Global.PumpkinCount >= 10));
        }

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
                if (plant is PlantCarrot)
                {
                    HarvestedCarrotCount++;
                    CarrotHarvestCountInCurrentDay.Value++;
                    if (plant.RipeDay == Global.Days.Value)
                    {
                        RipeAndHarvestCarrotCountInCurrentDay.Value++;
                    }
                }
                else if (plant is Plant)
                {
                    var plantObj = plant as Plant;
                    if (plantObj.name == "potato")
                    {
                        HarvestedPotatoCount++;
                        PotatoHarvestCountInCurrentDay.Value++;
                        if (plant.RipeDay == Global.Days.Value)
                        {
                            RipeAndHarvestPotatoCountInCurrentDay.Value++;
                        }
                    }
                    else if (plantObj.name == "tomato")
                    {
                        HarvestedTomatoCount++;
                        TomatoHarvestCountInCurrentDay.Value++;
                        if (plant.RipeDay == Global.Days.Value)
                        {
                            RipeAndHarvestTomatoCountInCurrentDay.Value++;
                        }
                    }
                    else if (plantObj.name == "pumpkin")
                    {
                        HarvestedPumpkinCount++;
                        PumpkinHarvestCountInCurrentDay.Value++;
                        if (plant.RipeDay == Global.Days.Value)
                        {
                            RipeAndHarvestPumpkinCountInCurrentDay.Value++;
                        }
                    }
                    else if (plantObj.name == "bean")
                    {
                        HarvestedBeanCount++;
                        BeanHarvestCountInCurrentDay.Value++;
                        if (plant.RipeDay == Global.Days.Value)
                        {
                            RipeAndHarvestBeanCountInCurrentDay.Value++;
                        }
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