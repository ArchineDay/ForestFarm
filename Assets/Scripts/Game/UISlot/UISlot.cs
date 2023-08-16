using System;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace IndieFarm
{
    public class UISlot : MonoBehaviour
    {
        //接受一个字符串参数并返回一个 Sprite 类型的值，通过IconName获取对应的图标
        //当我们需要加载图标的时候，我们可以通过这个委托来加载图标
        public static Func<string, Sprite> IconLoader;

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
            Select.Hide();
            ShortCut.text = string.Empty;
            Count.text = string.Empty;

            Button.onClick.AddListener(() =>
            {
                //当前UISlot被点击了，我们需要通知外部
                OnItemSelect?.Invoke(this);
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
            mData = data;
            Icon.sprite = IconLoader?.Invoke(mData.IconName);
            ShortCut.text = shortCut;
            if (data.Countable)
            {
                Count.text = mData.Count.ToString();
                Count.Show();
            }
            
        }
    }
}