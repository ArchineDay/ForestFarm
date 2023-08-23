using System.Collections.Generic;
using IndieFarm.Tool;
using QFramework;
using Unity.VisualScripting.Dependencies.NCalc;

namespace IndieFarm
{
    public class Config
    {
        public static Item CreateHand()
        {
            return new Item()
            {
                IconName = "ToolHand_0",
                Count = new BindableProperty<int>(1),
                Countable = false,
                IsPlant = false,
                Name = "hand",
                PlantPrefabName = string.Empty,
                Tool = new ToolHand()
            };
        }

        public static Item CreateHandShovel()
        {
            return new Item()
            {
                IconName = "ToolShovel_0",
                Count = new BindableProperty<int>(1),
                Countable = false,
                IsPlant = false,
                Name = "shovel",
                PlantPrefabName = string.Empty,
                Tool = new ToolShovel()
            };
        }

        public static Item CreateSeed(int count = 5)
        {
            return new Item()
            {
                IconName = "ToolSeed_0",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = true,
                Name = "seed",
                PlantPrefabName = "Plant",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            });
        }

        public static Item CreateWateringCan()
        {
            return new Item()
            {
                IconName = "ToolWateringCan_0",
                Count = new BindableProperty<int>(1),
                Countable = false,
                IsPlant = false,
                Name = "watering_can",
                PlantPrefabName = string.Empty,
                Tool = new ToolWateringCan()
            };
        }

        public static Item CreateSeedRadish(int count = 5)
        {
            return new Item()
            {
                IconName = "ToolSeedRadish_0",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = true,
                Name = "seed_radish",
                PlantPrefabName = "PlantRadish",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            });
        }

        public static Item CreateSeedCabbage(int count = 5)
        {
            return new Item()
            {
                IconName = "ToolSeedCabbage_0",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = true,
                Name = "seed_cabbage",
                PlantPrefabName = "PlantCabbage",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            });
        }

        public static Item CreateSeedCarrot(int count = 5)
        {
            return new Item()
            {
                IconName = "CarrotSeedIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = true,
                Name = "seed_carrot",
                PlantPrefabName = "PlantCarrot",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            });
        }

        public static Item CreateCarrot(int count)
        {
            return new Item()
            {
                IconName = "CarrotIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = false,
                Name = "carrot",
                PlantPrefabName = string.Empty,
                Tool = null
            };
        }

        public static List<Item> Items = new List<Item>()
        {
            CreateHand(),
            CreateHandShovel(),
            CreateWateringCan(),
            CreateSeed(),
            CreateSeedRadish(),
            CreateSeedCabbage(),
            CreateSeedCarrot(),
        };
    }
}