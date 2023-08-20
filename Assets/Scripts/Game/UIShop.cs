using System;
using System.Linq;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace IndieFarm
{
    public partial class UIShop : ViewController
    {
        void SetupBtnShowCheck(BindableProperty<int> itemCount,Button btn,Func<int,bool> showCondition)
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
            SetupBtnShowCheck(Global.FruitCount,BtnSellFruit,count=>count>=1);
            SetupBtnShowCheck(Global.RadishCount,BtnSellRadish,count=>count>=1);
            SetupBtnShowCheck(Global.CabbageCount,BtnSellCabbage,count=>count>=1);
            SetupBtnShowCheck(Global.Coin,BtnBuyFruitSeed,count=>count>=1);
            SetupBtnShowCheck(Global.Coin,BtnBuyRadishSeed,count=>count>=2);
            SetupBtnShowCheck(Global.Coin,BtnBuyCabbageSeed,count=>count>=3);

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
        }
    }
}