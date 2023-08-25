using System;
using System.Collections.Generic;
using System.Linq;
using IndieFarm.Tool;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace IndieFarm
{
    public partial class UIToolBar : ViewController
    {
        public List<UISlot> ToolbarSlots = new List<UISlot>();

        private void Start()
        {
            //注册委托
            UISlot.OnItemSelect = slot =>
            {
                if (slot.Data != null)
                {
                    ChangeTool(slot.Data.Tool, slot.Select, slot.Icon.sprite);
                }
            };

            ToolbarSlots.Add(ToolbarSlot1);
            ToolbarSlots.Add(ToolbarSlot2);
            ToolbarSlots.Add(ToolbarSlot3);
            ToolbarSlots.Add(ToolbarSlot4);
            ToolbarSlots.Add(ToolbarSlot5);
            ToolbarSlots.Add(ToolbarSlot6);
            ToolbarSlots.Add(ToolbarSlot7);
            ToolbarSlots.Add(ToolbarSlot8);
            ToolbarSlots.Add(ToolbarSlot9);
            ToolbarSlots.Add(ToolbarSlot10);


            HideAllSelect();
            ToolbarSlots[0].Select.Show();
            Global.MouseTool.Icon.sprite = ToolbarSlots[0].Icon.sprite;
        }

        void HideAllSelect()
        {
            foreach (var toolbarSlot in ToolbarSlots)
            {
                toolbarSlot.Select.Hide();
            }
            // Btn1Seclect.Hide();...
        }

        void ChangeTool(ITool tool, Image selectImage, Sprite toolIcon)
        {
            if (tool != null)
            {
                Global.CurrentTool.Value = tool;
                AudioController.Get.SfxTake.Play();

                HideAllSelect();
                selectImage.Show();

                Global.MouseTool.Icon.sprite = toolIcon;
            }
        }

        public void SelectDefault()
        {
            UISlot.OnItemSelect(ToolbarSlot1);
        }

        private void Update()
        {
            for (var i = 0; i < ToolbarSlots.Count; i++)
            {
                if (i < Config.Items.Count)
                {
                    var item = Config.Items[i];
                    ToolbarSlots[i].SetData(item, (i + 1).ToString());

                    //i往后的slot都设置为空
                    for (int j = i + 1; j < ToolbarSlots.Count; j++)
                    {
                        ToolbarSlots[j].SetData(null, string.Empty);
                    }
                }
            }

            //ChangeTool(Constant.ToolHand, ToolbarSlots[0].Select, ToolbarSlots[0].Icon.sprite);
            //if (Input.GetKeyDown(KeyCode.Alpha1)) ToolbarSlots[0].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha1)) UISlot.OnItemSelect(ToolbarSlot1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) UISlot.OnItemSelect(ToolbarSlot2);
            if (Input.GetKeyDown(KeyCode.Alpha3)) UISlot.OnItemSelect(ToolbarSlot3);
            if (Input.GetKeyDown(KeyCode.Alpha4)) UISlot.OnItemSelect(ToolbarSlot4);
            if (Input.GetKeyDown(KeyCode.Alpha5)) UISlot.OnItemSelect(ToolbarSlot5);
            if (Input.GetKeyDown(KeyCode.Alpha6)) UISlot.OnItemSelect(ToolbarSlot6);
            if (Input.GetKeyDown(KeyCode.Alpha7)) UISlot.OnItemSelect(ToolbarSlot7);
            if (Input.GetKeyDown(KeyCode.Alpha8)) UISlot.OnItemSelect(ToolbarSlot8);
            if (Input.GetKeyDown(KeyCode.Alpha9)) UISlot.OnItemSelect(ToolbarSlot9);
            if (Input.GetKeyDown(KeyCode.Alpha0)) UISlot.OnItemSelect(ToolbarSlot10);
        }
    }
}