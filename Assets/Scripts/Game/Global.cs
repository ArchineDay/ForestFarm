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

        public static BindableProperty<string> CurrentToolName = new BindableProperty<string>("手");
        // a：BindableProperty是一个可绑定的属性，可以在属性值发生变化的时候，通知到其他地方。
    }
}