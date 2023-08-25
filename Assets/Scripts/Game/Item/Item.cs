using System;
using IndieFarm.Tool;
using QFramework;
using UnityEngine;

namespace IndieFarm
{
    [System.Serializable]
    public class Item
    {
        public string Name;
        public string IconName;
        public BindableProperty<int> Count;

        public bool Countable = false;
        public ITool Tool;
        public bool IsPlant;
        public string PlantPrefabName;

        public static Item CreateItem(string iconName, int count, bool countable, bool isPlant, string name,
            string plantPrefabName, ITool tool)
        {
            var item = new Item()
            {
                IconName = iconName,
                Count = new BindableProperty<int>(count),
                Countable = countable,
                IsPlant = isPlant,
                Name = name,
                PlantPrefabName = plantPrefabName,
                Tool = tool
            };
            if (isPlant)
            {
                item.Self(i => item.Tool = new ToolSeed()
                {
                    Item = i
                });
            }

            return item;
        }

        public Item Self(Action<Item> onSelf) //回调函数
        {
            onSelf.Invoke(this);
            return this;
        }
    }
}