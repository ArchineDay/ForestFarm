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
            ToolbarSlot1.SetData(new SlotData()
            {
                Icon = ResController.Instance.LoadSprites("ToolHand_0"),
                OnSelect = () => { ChangeTool(Constant.ToolHand, ToolbarSlot1.Select, ToolbarSlot1.Icon.sprite); }
            },"1");

            ToolbarSlot2.SetData(new SlotData()
            {
                Icon = ResController.Instance.LoadSprites("ToolShovel_0"),
                OnSelect = () => { ChangeTool(Constant.ToolShovel, ToolbarSlot2.Select, ToolbarSlot2.Icon.sprite); }
            },"2");

            ToolbarSlot3.SetData(new SlotData()
            {
                Icon = ResController.Instance.LoadSprites("ToolSeed_0"),
                OnSelect = () => { ChangeTool(Constant.ToolSeed, ToolbarSlot3.Select, ToolbarSlot3.Icon.sprite); }
            },"3");

            ToolbarSlot4.SetData(new SlotData()
            {
                Icon = ResController.Instance.LoadSprites("ToolWateringCan_0"),
                OnSelect = () =>
                {
                    ChangeTool(Constant.ToolWateringCan, ToolbarSlot4.Select, ToolbarSlot4.Icon.sprite);
                }
            },"4");

            ToolbarSlot5.SetData(new SlotData()
            {
                Icon = ResController.Instance.LoadSprites("ToolSeedRadish_0"),
                OnSelect = () => { ChangeTool(Constant.ToolSeedRadish, ToolbarSlot5.Select, ToolbarSlot5.Icon.sprite); }
            },"5");

            ToolbarSlot6.SetData(new SlotData()
            {
                Icon = ResController.Instance.LoadSprites("ToolSeedCabbage_0"),
                OnSelect = () =>
                {
                    ChangeTool(Constant.ToolSeedCabbage, ToolbarSlot6.Select, ToolbarSlot6.Icon.sprite);
                }
            },"6");
            
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

            foreach (var toolbarSlot in ToolbarSlots)
            {
                var data = toolbarSlot.Data;
                toolbarSlot.GetComponent<Button>().onClick.AddListener(() =>
                {
                    data?.OnSelect?.Invoke();
                });
            }

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
            if (Input.GetKeyDown(KeyCode.Alpha1)) ToolbarSlots[0].Data?.OnSelect?.Invoke();
            //ChangeTool(Constant.ToolHand, ToolbarSlots[0].Select, ToolbarSlots[0].Icon.sprite);
            if (Input.GetKeyDown(KeyCode.Alpha2)) ToolbarSlots[1].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha3)) ToolbarSlots[2].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha4)) ToolbarSlots[3].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha5)) ToolbarSlots[4].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha6)) ToolbarSlots[5].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha7)) ToolbarSlots[6].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha8)) ToolbarSlots[7].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha9)) ToolbarSlots[8].Data?.OnSelect?.Invoke();
            if (Input.GetKeyDown(KeyCode.Alpha0)) ToolbarSlots[9].Data?.OnSelect?.Invoke();
        }
    }
}