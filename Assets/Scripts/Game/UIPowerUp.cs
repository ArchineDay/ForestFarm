using System;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace IndieFarm
{
    public partial class UIPowerUp : ViewController
    {
        private void SetupBtnShowCheck(BindableProperty<int> itemCount, Button btn, Func<int, bool> showCondition)
        {
            itemCount.RegisterWithInitValue(count =>
            {
                //满足条件才可交互
                btn.interactable = showCondition(count);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void Start()
        {
            //顺序
            //铁锹
            //花洒
            //手
            //种子
            SetupBtnShowCheck(Global.Coin, BtnShovelRange1, coin => coin >= 20 && !Global.ShovelRange1UnLock);
            SetupBtnShowCheck(Global.Coin, BtnWateringCanRange1,
                coin => coin >= 30 && !Global.WateringCanRange1UnLock && Global.ShovelRange1UnLock);
            SetupBtnShowCheck(Global.Coin, BtnHandRange1,
                coin => coin >= 20 && !Global.HandRange1UnLock && Global.WateringCanRange1UnLock);
            SetupBtnShowCheck(Global.Coin, BtnSeedRange1,
                coin => coin >= 25 && !Global.SeedRange1UnLock && Global.HandRange1UnLock);

            BtnHandRange1.onClick.AddListener(() =>
            {
                Global.HandRange1UnLock = true;
                Global.Coin.Value -= 20;
                AudioController.Get.SfxBuy.Play();
            });

            BtnShovelRange1.onClick.AddListener(() =>
            {
                Global.ShovelRange1UnLock = true;
                Global.Coin.Value -= 20;
                AudioController.Get.SfxBuy.Play();
            });

            BtnWateringCanRange1.onClick.AddListener(() =>
            {
                Global.WateringCanRange1UnLock = true;
                Global.Coin.Value -= 30;
                AudioController.Get.SfxBuy.Play();
            });
            
            BtnSeedRange1.onClick.AddListener(() =>
            {
                Global.SeedRange1UnLock = true;
                Global.Coin.Value -= 25;
                AudioController.Get.SfxBuy.Play();
            });
        }
    }
}