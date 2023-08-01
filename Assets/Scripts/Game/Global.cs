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
    }
}