using System;
using System.Collections.Generic;
using IndieFarm.Tool;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace IndieFarm
{
    public partial class UIToolBar : ViewController
    {
        private List<UISlot> ToolbarSlots = new List<UISlot>();

        private void Start()
        {
            // ToolbarSlot1.SetData(new SlotData()
            // {
            //     Icon = ResController.Instance.LoadSprites(Config.Hand.Name),
            //     OnSelect = () => { ChangeTool(Config.Hand.Tool, ToolbarSlot1.Select, ToolbarSlot1.Icon.sprite); }
            // },"1");

            UISlot.IconLoader = (spriteName) => ResController.Instance.LoadSprite(spriteName);

            UISlot.OnItemSelect = slot =>
            {
                if (slot != null)
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


            for (var i = 0; i < ToolbarSlots.Count; i++)
            {
                var slot = ToolbarSlots[i];
                if (i < Config.Items.Count)
                {
                    var item = Config.Items[i];
                    slot.SetData(item, (i + 1).ToString());
                }
            }

            HideAllSelect();
            ToolbarSlots[0].Select.Show();
            Global.MouseTool.Icon.sprite = ToolbarSlots[0].Icon.sprite;

            // foreach (var toolbarSlot in ToolbarSlots)
            // {
            //     var data = toolbarSlot.Data;
            //     toolbarSlot.GetComponent<Button>().onClick.AddListener(() => { data?.OnSelect?.Invoke(); });
            // }

            // Btn1.onClick.AddListener(() => { ChangeTool(Constant.ToolHand,Btn1Seclect,Btn1Icon.sprite); });...
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
            Global.CurrentTool.Value = tool;
            AudioController.Get.SfxTake.Play();

            HideAllSelect();
            selectImage.Show();

            Global.MouseTool.Icon.sprite = toolIcon;
        }

        private void Update()
        {
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