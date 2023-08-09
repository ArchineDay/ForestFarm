using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace IndieFarm
{
    public class Global
    {
        public static ToolController MouseTool=null;
        public static Player Player = null;
        //默认是第一天
        public static BindableProperty<int> Days = new BindableProperty<int>(1);
        
        //果子数量
        public static BindableProperty<int> FruitCount =new BindableProperty<int>(0);
        //萝卜数量
        public static BindableProperty<int> RadishCount =new BindableProperty<int>(0);

        //当前工具
        public static BindableProperty<string> CurrentTool = new BindableProperty<string>(Constant.TOOL_HAND);
        // a：BindableProperty是一个可绑定的属性，可以在属性值发生变化的时候，通知到其他地方。

        //当天成熟果子的数量
        public static BindableProperty<int> RipeAndHarvestCountInCurrentDay = new BindableProperty<int>(0);
        //当天成熟萝卜的数量
        public static BindableProperty<int> RipeAndHarvestRadishCountInCurrentDay = new BindableProperty<int>(0);
        
        //当天收割果子的数量
        public static BindableProperty<int> HarvestCountInCurrentDay = new BindableProperty<int>(0);
        //当天收割萝卜的数量
        public static BindableProperty<int> RadishHarvestCountInCurrentDay = new BindableProperty<int>(0);

        public static List<Challenge> Challenges = new List<Challenge>()
        {
            new ChallengeHarvestAFruit(),
            new ChallengeRipeAndHarvestTwoFruitsInADay(),
            new ChallengeRipeAndHarvestFiveFruitsInADay(),
            new ChallengeHarvestARadish(),
            new ChallengeRipeAndHarvestFruitAndRadishInOneDay()
        };

        public static List<Challenge> ActiveChallenges = new List<Challenge>()
        {

        };

        public static List<Challenge> FinishedChallenges = new List<Challenge>()
        {

        };
        //植物收割
        public static EasyEvent<IPlant> OnPlantHarvest = new EasyEvent<IPlant>();
        //挑战完成
        public static EasyEvent<Challenge> OnChallengeFinish = new EasyEvent<Challenge>();
    }

    public class Constant
    {
        public const string TOOL_HAND = "hand";
        public const string TOOL_SHOVEL = "shovel";
        public const string TOOL_SEED = "seed";
        public const string TOOL_WATERING_SCAN = "watering_scan";
        public const string TOOL_SEED_RADISH ="seed_radish";

        public static string DisplayName(string tool)
        {
            switch (tool)
            {
                case TOOL_HAND:
                    return "手";
                case TOOL_SHOVEL:
                    return "铁锹";
                case TOOL_SEED:
                    return "种子";
                case TOOL_WATERING_SCAN:
                    return "花洒";
                case TOOL_SEED_RADISH:
                    return "萝卜种子";
            }
            return string.Empty;
        }
    }
}