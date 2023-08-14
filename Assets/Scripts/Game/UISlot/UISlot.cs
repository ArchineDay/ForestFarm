using System;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace IndieFarm
{
    public class UISlot : MonoBehaviour
    {
        public Image Icon;

        public Image Select;

        public Text ShortCut;
        
        private ISlotData mData;

        public ISlotData Data => mData;

        private void Awake()
        {
            Icon.sprite=null;
            Select.Hide();
            ShortCut.text = string.Empty;
        }

        public void SetData(ISlotData data,string shortCut)
        {
            mData = data;
            Icon.sprite= mData.Icon;
            ShortCut.text = shortCut;
        }
    }
    public interface ISlotData
    {
        public Sprite Icon { get; set; }
        public Action OnSelect { get; set; }
    }

    public class SlotData : ISlotData
    {
        public Sprite Icon { get; set; }
        public Action OnSelect { get; set; }
    }
}