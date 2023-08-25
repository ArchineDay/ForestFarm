using System;
using System.Linq;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace IndieFarm
{
    public partial class UIShop : ViewController
    {
        public static void SetupBtnShowCheck(BindableProperty<int> itemCount, Button btn, Func<int, bool> showCondition,
            GameObject gameObject)
        {
            itemCount.RegisterWithInitValue(count =>
            {
                //if (count >= 1)
                if (showCondition(count))
                {
                    btn.interactable = true;
                    //btn.Show();
                }
                else
                {
                    btn.interactable = false;
                    //btn.Hide();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void Start()
        {
            SetupBtnShowCheck(Global.CarrotCount, BtnSellCarrot, count => count >= 1, gameObject);
            SetupBtnShowCheck(Global.Coin, BtnBuyCarrotSeed, count => count >= 4, gameObject);

            CreateSellButton(BtnSellCarrot, 8, Global.CarrotCount, "carrot");

            BtnBuyCarrotSeed.onClick.AddListener(() =>
            {
                Global.Coin.Value -= 4;
                //购买胡萝卜种子
                //查询是否有胡萝卜种子，如果没有，创建一个,有的话，数量+1
                var seedItem = Config.Items.FirstOrDefault(i => i.Name == "seed_carrot");
                if (seedItem == null)
                {
                    seedItem = Config.CreateSeedCarrot(1);
                    Config.Items.Add(seedItem);
                }
                else
                {
                    seedItem.Count.Value++;
                }

                AudioController.Get.SfxBuy.Play();
            });

            // void CreateBuyButton(BindableProperty<int> money,string toBuyItemName,Item toCreateItem)
            // {
            //     money.Value -= 4;
            //     //购买种子
            //     //查询是否有种子，如果没有，创建一个,有的话，数量+1
            //     var toBuyItem = Config.Items.FirstOrDefault(i => i.Name == toBuyItemName);
            //     if (toBuyItem == null)
            //     {
            //         toBuyItem = Config.CreateSeedCarrot(1);
            //         Config.Items.Add(toBuyItem);
            //     }
            //     else
            //     {
            //         toBuyItem.Count.Value++;
            //     }
            //
            //     AudioController.Get.SfxBuy.Play();
            // }

            void CreateSellButton(Button button, int price, BindableProperty<int> globalItemName, string sellItemName)
            {
                button.onClick.AddListener(BtnSell);

                void BtnSell()
                {
                    Global.Coin.Value += price;
                    globalItemName.Value -= 1;
                    AudioController.Get.SfxBuy.Play();

                    var sellItem = Config.Items.Single(i => i.Name == sellItemName);
                    sellItem.Count.Value--;

                    if (sellItem.Count.Value == 0)
                    {
                        Config.Items.Remove(sellItem);
                        FindObjectOfType<UIToolBar>().SelectDefault();
                    }
                }
            }
        }
    }
}