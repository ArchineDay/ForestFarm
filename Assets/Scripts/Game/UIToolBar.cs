using System;
using UnityEngine;
using QFramework;

namespace IndieFarm
{
    public partial class UIToolBar : ViewController
    {
        private void Start()
        {
            Btn1.onClick.AddListener(() => { ChangeTool(Constant.TOOL_HAND); });
            Btn2.onClick.AddListener(() => { ChangeTool(Constant.TOOL_SHOVEL); });
            Btn3.onClick.AddListener(() => { ChangeTool(Constant.TOOL_SEED); });
            Btn4.onClick.AddListener(() => { ChangeTool(Constant.TOOL_WATERING_SCAN); });
        }

        void ChangeTool(string tool)
        {
            Global.CurrentTool.Value = tool;
            AudioController.Get.SfxTake.Play();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeTool(Constant.TOOL_HAND);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeTool(Constant.TOOL_SHOVEL);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeTool(Constant.TOOL_SEED);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeTool(Constant.TOOL_WATERING_SCAN);
            }
        }
    }
}