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

        public static Item CreateSeedPumpkin(int count = 5)
        {
            return new Item()
            {
                IconName = "PumpkinSeedIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = true,
                Name = "seed_pumpkin",
                PlantPrefabName = "PlantPumpkin",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            });
        }

        public static Item CreateSeedPotato(int count = 5)
        {
            return new Item()
            {
                IconName = "PotatoSeedIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = true,
                Name = "seed_potato",
                PlantPrefabName = "PlantPotato",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            });
        }

        public static Item CreateSeedTomato(int count = 5)
        {
            return new Item()
            {
                IconName = "TomatoSeedIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = true,
                Name = "seed_tomato",
                PlantPrefabName = "PlantTomato",
                Tool = new ToolSeed()
            }.Self(item => item.Tool = new ToolSeed()
            {
                Item = item
            });
        }

        public static Item CreateSeedBean(int count = 5)
        {
            return new Item()
            {
                IconName = "BeanSeedIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = true,
                Name = "seed_bean",
                PlantPrefabName = "PlantBean",
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

        public static Item CreatePumpkin(int count)
        {
            return new Item()
            {
                IconName = "PumpkinIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = false,
                Name = "pumpkin",
                PlantPrefabName = string.Empty,
                Tool = null
            };
        }

        public static Item CreatePotato(int count)
        {
            return new Item()
            {
                IconName = "PotatoIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = false,
                Name = "potato",
                PlantPrefabName = string.Empty,
                Tool = null
            };
        }

        public static Item CreateTomato(int count)
        {
            return new Item()
            {
                IconName = "TomatoIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = false,
                Name = "tomato",
                PlantPrefabName = string.Empty,
                Tool = null
            };
        }

        public static Item CreateBean(int count)
        {
            return new Item()
            {
                IconName = "BeanIcon",
                Count = new BindableProperty<int>(count),
                Countable = true,
                IsPlant = false,
                Name = "bean",
                PlantPrefabName = string.Empty,
                Tool = null
            };
        }

        public static List<Item> Items = new List<Item>()
        {
            CreateHand(),
            CreateHandShovel(),
            CreateWateringCan(),
         
            CreateSeedCarrot(),
            CreateSeedPumpkin(),
            CreateSeedPotato(),
            CreateSeedTomato(),
            CreateSeedBean()
        };

        public static Item CreateItem(string name, int count)
        {
            return name switch
            {
                "tomato" => CreateTomato(count),
                "bean" => CreateBean(count),
                "carrot" => CreateCarrot(count),
                "pumpkin" => CreatePumpkin(count),
                "potato" => CreatePotato(count),
                _ => null
            };
        }
    }
}