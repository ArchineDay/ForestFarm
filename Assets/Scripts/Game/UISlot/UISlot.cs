using System;
using IndieFarm.Tool;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace IndieFarm
{
    public class UISlot : MonoBehaviour
    {
        //接受一个字符串参数并返回一个 Sprite 类型的值，通过IconName获取对应的图标
        //当我们需要加载图标的时候，我们可以通过这个委托来加载图标
        public static Func<string, Sprite> IconLoader = (spriteName) => ResController.Instance.LoadSprite(spriteName);

        //当点击一个物品的时候，我们需要通知外部，这个物品被点击了
        public static Action<UISlot> OnItemSelect;


        public Image Icon;

        public Image Select;

        public Text ShortCut;

        private Item mData;

        public Text Count;

        public Button Button;

        public Item Data => mData;

        private void Awake()
        {
            Icon.sprite = null;
            Icon.Hide();
            Select.Hide();
            ShortCut.text = string.Empty;
            Count.text = string.Empty;

            Button.onClick.AddListener(() =>
            {
                //调用委托，传入slot自己
                //等价OnItemSelect?.Invoke(this);
                OnItemSelect(this);
            });
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (transform.Find("Count"))
            {
                Count = transform.Find("Count").GetComponent<Text>();
            }

            Button = GetComponent<Button>();
        }
#endif

        public void SetData(Item data, string shortCut)
        {
            if (data == null)
            {
                mData = null;
                Icon.sprite = null;
                ShortCut.text = string.Empty;
                Count.text = string.Empty;
                Icon.Hide();
            }
            else
            {
                Icon.Show();
                mData = data;
                Icon.sprite = IconLoader?.Invoke(mData.IconName);
                //Icon.sprite = ResController.Instance.LoadSprite(mData.IconName);
                ShortCut.text = shortCut;
                if (data.Countable)
                {
                    //实时更新数量
                    data.Count.RegisterWithInitValue(count => { Count.text = count.ToString(); })
                        .UnRegisterWhenGameObjectDestroyed(gameObject);
                }
            }
        }
    }
}