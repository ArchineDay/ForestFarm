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

        //果子数量
        public static BindableProperty<int> FruitCount = new BindableProperty<int>(0);
        //硬币
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);

     

        //萝卜数量
        public static BindableProperty<int> RadishCount = new BindableProperty<int>(0);
      
        
        //白菜数量
        public static BindableProperty<int> CabbageCount = new BindableProperty<int>(0);
  

        //当前工具
        public static BindableProperty<ITool> CurrentTool = new BindableProperty<ITool>(Config.Items[0].Tool);
        // a：BindableProperty是一个可绑定的属性，可以在属性值发生变化的时候，通知到其他地方。

        //植物收割
        public static EasyEvent<IPlant> OnPlantHarvest = new EasyEvent<IPlant>();
    }
}