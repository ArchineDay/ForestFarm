using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace IndieFarm
{
    public interface IPlant
    {
        GameObject GameObject { get; }
        PlantStates State { get; }
        void SetState(PlantStates state);
        void Grow(SoliData soilData);

        int RipeDay { get; }

        public int XCell { get; set; }
        public int YCell { get; set; }
    }

    public static class PlantExtentions
    {
        public static void ClearSoilDigState(this IPlant self)
        {
            //清空地块
            Object.FindObjectOfType<GridController>().Soil.SetTile(new Vector3Int(self.XCell, self.YCell), null);
        }
    }

    public partial class Plant : ViewController, IPlant
    {
        //可以在Unity的Inspector面板中显示和编辑
        [System.Serializable]
        public class PlantState
        {
            public PlantStates state;
            public Sprite sprite;
            public bool ShowDigState = false;
            public int Days = 1;
        }

        public string name;
        public List<PlantState> states = new List<PlantState>();

        public int RipeDay { get; private set; }

        public GameObject GameObject => gameObject;

        public int XCell { get; set; }
        public int YCell { get; set; }

        private PlantStates mState = PlantStates.Seed;
        public PlantStates State => mState; //get方法

        public void SetState(PlantStates newState)
        {
            //设置不同阶段的状态
            if (newState != mState)
            {
                //挑战用判断
                if (mState == PlantStates.Small && newState == PlantStates.Ripe)
                {
                    RipeDay = Global.Days.Value;
                }

                //拿取新阶段的状态
                var plantState = states.FirstOrDefault(s => s.state == newState);
                mState = newState;

                if (!plantState.ShowDigState)
                {
                    this.ClearSoilDigState();
                }
                
                GetComponent<SpriteRenderer>().sprite = plantState.sprite;

                //同步到SoilData中
                FindObjectOfType<GridController>().ShowGrid[XCell, YCell].PlantState = newState;
            }
        }

        //当前状态的天数
        private int mDaysInCurrenrState = 0;

        public void Grow(SoliData soilData)
        {
            if (mState == PlantStates.Ripe) return;//防止索引越界

            if (soilData.Watered)
            {
                //阶段切换
                mDaysInCurrenrState++;
                var plantState = states.FirstOrDefault(s => s.state == mState);
                if (mDaysInCurrenrState >= plantState.Days)
                {
                    var currentStateIndex = states.FindIndex(s => s.state == mState);
                    //获取后一个阶段
                    currentStateIndex++;
                    var nextState = states[currentStateIndex];
                    SetState(nextState.state);
                    mDaysInCurrenrState = 0;
                }
            }
            
        }
    }
}