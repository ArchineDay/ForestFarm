using System;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace IndieFarm
{
    public partial class UIToolBar : ViewController
    {
        private void Start()
        {
            HideAllSelect();
            Btn1Seclect.Show();
            //Global.Player.ToolIcon.sprite = Btn1Icon.sprite;
            Global.MouseTool.Icon.sprite = Btn1Icon.sprite;
            Btn1.onClick.AddListener(() => { ChangeTool(Constant.TOOL_HAND,Btn1Seclect,Btn1Icon.sprite); });
            Btn2.onClick.AddListener(() => { ChangeTool(Constant.TOOL_SHOVEL,Btn2Seclect,Btn2Icon.sprite); });
            Btn3.onClick.AddListener(() => { ChangeTool(Constant.TOOL_SEED,Btn3Seclect,Btn3Icon.sprite); });
            Btn4.onClick.AddListener(() => { ChangeTool(Constant.TOOL_WATERING_SCAN,Btn4Seclect,Btn4Icon.sprite); });
            Btn5.onClick.AddListener(() => { ChangeTool(Constant.TOOL_SEED_RADISH,Btn5Seclect,Btn5Icon.sprite); });
        }

        void HideAllSelect()
        {
            Btn1Seclect.Hide();
            Btn2Seclect.Hide();
            Btn3Seclect.Hide();
            Btn4Seclect.Hide();
            Btn5Seclect.Hide();
        }
        void ChangeTool(string tool,Image selectImage,Sprite toolIcon)
        {
            Global.CurrentTool.Value = tool;
            AudioController.Get.SfxTake.Play();

            HideAllSelect();
            selectImage.Show();
            
            Global.MouseTool.Icon.sprite = toolIcon;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeTool(Constant.TOOL_HAND,Btn1Seclect,Btn1Icon.sprite);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeTool(Constant.TOOL_SHOVEL,Btn2Seclect,Btn2Icon.sprite);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeTool(Constant.TOOL_SEED,Btn3Seclect,Btn3Icon.sprite);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeTool(Constant.TOOL_WATERING_SCAN,Btn4Seclect,Btn4Icon.sprite);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ChangeTool(Constant.TOOL_SEED_RADISH,Btn5Seclect,Btn5Icon.sprite);
            }
        }
    }
}