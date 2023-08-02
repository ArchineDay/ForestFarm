using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace IndieFarm
{
    public class Global
    {
        //默认是第一天
        public static BindableProperty<int> Days = new BindableProperty<int>(1);
        
        //果子数量
        public static BindableProperty<int> FruitCount =new BindableProperty<int>(0);

        public static BindableProperty<string> CurrentTool = new BindableProperty<string>(Constant.TOOL_HAND);
        // a：BindableProperty是一个可绑定的属性，可以在属性值发生变化的时候，通知到其他地方。
    }

    public class Constant
    {
        public const string TOOL_HAND = "hand";
        public const string TOOL_SHOVEL = "shovel";
        public const string TOOL_SEED = "seed";

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
            }
            return string.Empty;
        }
    }
}