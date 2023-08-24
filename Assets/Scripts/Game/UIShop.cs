using System;
using System.Linq;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace IndieFarm
{
    public partial class UIShop : ViewController
    {
       public static void SetupBtnShowCheck(BindableProperty<int> itemCount,Button btn,Func<int,bool> showCondition,GameObject gameObject)
        {
            itemCount.RegisterWithInitValue(count =>
            {
                //if (count >= 1)
                if (showCondition(count))
                {
                    btn.Show();
                }
                else
                {
                    btn.Hide();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        
        void Start()
        {
            
            SetupBtnShowCheck(Global.CarrotCount,BtnSellCarrot,count=>count>=1,gameObject);
            SetupBtnShowCheck(Global.Coin,BtnBuyCarrotSeed,count=>count>=4,gameObject);

            // BtnSellFruit.onClick.AddListener(() =>
            // {
            //     Global.Coin.Value += 3;
            //     Global.FruitCount.Value -= 1;
            //     AudioController.Get.SfxBuy.Play();
            // });
      
            BtnSellCarrot.onClick.AddListener(() =>
            {
                Global.Coin.Value += 8;
                Global.CarrotCount.Value -= 1;
                AudioController.Get.SfxBuy.Play();
                
                var carrotItem = Config.Items.Single(i => i.Name == "carrot");
                carrotItem.Count.Value--;
                
                if (carrotItem.Count.Value == 0)
                {
                    Config.Items.Remove(carrotItem);
                    FindObjectOfType<UIToolBar>().SelectDefault();
                }
            });
            
            BtnBuyCarrotSeed.onClick.AddListener(() =>
            {
                Global.Coin.Value -= 4;
               //购买胡萝卜种子
               //查询是否有胡萝卜种子，如果没有，创建一个,有的话，数量+1
                var seedItem = Config.Items.FirstOrDefault(i => i.Name == "seed_carrot");
                if (seedItem==null)
                {
                    seedItem= Config.CreateSeedCarrot(1);
                    Config.Items.Add(seedItem);
                }
                else
                {
                    seedItem.Count.Value++;
                }
                AudioController.Get.SfxBuy.Play();
                
            });
            
        }
    }
}