using System;
using System.Linq;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace IndieFarm
{
    public partial class UIShop : ViewController
    {
        void Start()
        {
            //SetupBtnShowCheck(Global.CarrotCount, BtnSellCarrot, count => count >= 1, gameObject);
            // SetupBtnShowCheck(Global.Coin, BtnBuyCarrotSeed, count => count >= 4, gameObject);
            CreateSellButton(BtnSellCarrot, 8, Global.CarrotCount, "carrot");
            CreateSellButton(BtnSellTomato, 5, Global.TomatoCount, "tomato");
            CreateSellButton(BtnSellPotato, 10, Global.PotatoCount, "potato");
            CreateSellButton(BtnSellPumpkin, 15, Global.PumpkinCount, "pumpkin");
            CreateSellButton(BtnSellBean, 20, Global.BeanCount, "bean");

            CreateBuyButton(BtnBuyCarrotSeed, 3, "seed_carrot", Config.CreateSeedItem);
            CreateBuyButton(BtnBuyTomatoSeed, 2, "seed_tomato", Config.CreateSeedItem);
            CreateBuyButton(BtnBuyPotatoSeed, 4, "seed_potato", Config.CreateSeedItem);
            CreateBuyButton(BtnBuyPumpkinSeed, 6, "seed_pumpkin", Config.CreateSeedItem);
            CreateBuyButton(BtnBuyBeanSeed, 8, "seed_bean", Config.CreateSeedItem);

            void CreateBuyButton(Button button, int money, string toBuyItemName, Func<string, int, Item> toCreateItem)
            {
                button.onClick.AddListener(BtnBuy);

                void BtnBuy()
                {
                    Global.Coin.Value -= money;
                    //购买种子
                    //查询是否有种子，如果没有，创建一个,有的话，数量+1
                    var toBuyItem = Config.Items.FirstOrDefault(i => i.Name == toBuyItemName);
                    if (toBuyItem == null)
                    {
                        toBuyItem = toCreateItem(toBuyItemName, 1);
                        Config.Items.Add(toBuyItem);
                    }
                    else
                    {
                        toBuyItem.Count.Value++;
                    }
                    AudioController.Get.SfxBuy.Play();
                }

                Global.Coin.RegisterWithInitValue(coin =>
                {
                    if (coin >= money)
                    {
                        button.interactable = true;
                    }
                    else
                    {
                        button.interactable = false;
                    }
                }).UnRegisterWhenGameObjectDestroyed(gameObject);
               
            }

            void CreateSellButton(Button button, int price, BindableProperty<int> globalItemName, string sellItemName)
            {
                button.onClick.AddListener(BtnSell);

                void BtnSell()
                {
                    Global.Coin.Value += price;
                    globalItemName.Value -= 1;

                    var sellItem = Config.Items.Single(i => i.Name == sellItemName);
                    sellItem.Count.Value--;

                    if (sellItem.Count.Value == 0)
                    {
                        Config.Items.Remove(sellItem);
                        FindObjectOfType<UIToolBar>().SelectDefault();
                    }
                    AudioController.Get.SfxBuy.Play();
                }

                globalItemName.RegisterWithInitValue(item =>
                {
                    if (item >= 1)
                    {
                        button.interactable = true;
                    }
                    else
                    {
                        button.interactable = false;
                    }
                }).UnRegisterWhenGameObjectDestroyed(gameObject);
            }
        }
    }
}