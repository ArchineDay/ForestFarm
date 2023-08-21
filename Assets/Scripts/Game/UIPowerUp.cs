using UnityEngine;
using QFramework;

namespace IndieFarm
{
    public partial class UIPowerUp : ViewController
    {
        void Start()
        {
            UIShop.SetupBtnShowCheck(Global.Coin, BtnHandRange1, coin => coin >= 20 && !Global.HandRange1UnLock,
                gameObject);
            UIShop.SetupBtnShowCheck(Global.Coin, BtnShovelRange1, coin => coin >= 20 && !Global.ShovelRange1UnLock,
                gameObject);
            UIShop.SetupBtnShowCheck(Global.Coin, BtnWateringCanRange1,
                coin => coin >= 30 && !Global.WateringCanRange1UnLock, gameObject);

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
        }
    }
}