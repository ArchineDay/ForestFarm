using System;
using System.Collections;
using System.Collections.Generic;
using IndieFarm.Tool;
using QFramework;
using UnityEngine;

namespace IndieFarm
{
    public class Global
    {
        public static ToolController MouseTool = null;

        public static Player Player = null;

        //默认是第一天
        public static BindableProperty<int> Days = new BindableProperty<int>(1);
        //硬币
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);

        //植物数量
        public static BindableProperty<int> CarrotCount = new BindableProperty<int>(0);
        public static BindableProperty<int> PumpkinCount = new BindableProperty<int>(0);
        public static BindableProperty<int> PotatoCount = new BindableProperty<int>(0);
        public static BindableProperty<int> TomatoCount = new BindableProperty<int>(0);
        public static BindableProperty<int> BeanCount = new BindableProperty<int>(0);

        //当前工具
        public static BindableProperty<ITool> CurrentTool = new BindableProperty<ITool>(Config.Items[0].Tool);
        // a：BindableProperty是一个可绑定的属性，可以在属性值发生变化的时候，通知到其他地方。

        //植物收割
        public static EasyEvent<IPlant> OnPlantHarvest = new EasyEvent<IPlant>();

        //为什么用static
        //全局的，如果不用static，那么就需要在每个场景中都创建一份这样的数据，这样就会造成数据的冗余
        public static bool HandRange1UnLock = false;
        public static bool ShovelRange1UnLock = false;
        public static bool WateringCanRange1UnLock = false;
        public static bool SeedRange1UnLock = false;
    }
}