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
            SetupBtnShowCheck(Global.FruitCount,BtnSellFruit,count=>count>=1,gameObject);
            SetupBtnShowCheck(Global.RadishCount,BtnSellRadish,count=>count>=1,gameObject);
            SetupBtnShowCheck(Global.CabbageCount,BtnSellCabbage,count=>count>=1,gameObject);
            SetupBtnShowCheck(Global.Coin,BtnBuyFruitSeed,count=>count>=1,gameObject);
            SetupBtnShowCheck(Global.Coin,BtnBuyRadishSeed,count=>count>=2,gameObject);
            SetupBtnShowCheck(Global.Coin,BtnBuyCabbageSeed,count=>count>=3,gameObject);
            SetupBtnShowCheck(Global.CarrotCount,BtnSellCarrot,count=>count>=1,gameObject);
            SetupBtnShowCheck(Global.Coin,BtnBuyCarrotSeed,count=>count>=4,gameObject);

            BtnBuyFruitSeed.onClick.AddListener(() =>
            {
                //获取种子的Item进行++操作,single是获取单个元素
                var seedItem = Config.Items.Single(i => i.Name == "seed");
                seedItem.Count.Value += 1;
                Global.Coin.Value -= 1;
                AudioController.Get.SfxBuy.Play();
            });

            BtnBuyRadishSeed.onClick.AddListener(() =>
            {
                var seedItem = Config.Items.Single(i => i.Name == "seed_radish");
                seedItem.Count.Value += 1;
                Global.Coin.Value -= 2;
                AudioController.Get.SfxBuy.Play();
            });
            
            BtnBuyCabbageSeed.onClick.AddListener(() =>
            {
                var seedItem = Config.Items.Single(i => i.Name == "seed_cabbage");
                seedItem.Count.Value += 1;
                Global.Coin.Value -= 3;
                AudioController.Get.SfxBuy.Play();
            });
            
            BtnSellFruit.onClick.AddListener(() =>
            {
                Global.Coin.Value += 3;
                Global.FruitCount.Value -= 1;
                AudioController.Get.SfxBuy.Play();
            });
            
            BtnSellRadish.onClick.AddListener(() =>
            {
                Global.Coin.Value += 5;
                Global.RadishCount.Value -= 1;
                AudioController.Get.SfxBuy.Play();
            });
            
            BtnSellCabbage.onClick.AddListener(() =>
            {
                Global.Coin.Value += 8;
                Global.CabbageCount.Value -= 1;
                AudioController.Get.SfxBuy.Play();
            });
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