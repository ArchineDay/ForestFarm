using System;
using System.Linq;
using System.Numerics;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;

namespace IndieFarm
{
    public partial class Player : ViewController
    {
        public Grid Grid;
        public Tilemap Tilemap;

        public Font Font;
        private GUIStyle mLabelStyle;
        private GUIStyle mCoinStyle;
        public Rigidbody2D mRigidbody2D;
        public Animator mAnimator;
        private void Awake()
        {
            Global.Player = this;
            mRigidbody2D = GetComponent<Rigidbody2D>();
            mAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            mLabelStyle = new GUIStyle("Label")
            {
                font = Font
            };

            mCoinStyle = new GUIStyle("Label")
            {
                font = Font,
                normal =
                {
                    textColor = Color.yellow
                }
            };

            Global.Days.Register(day =>
            {
                ChallengeController.CarrotHarvestCountInCurrentDay.Value = 0;
                ChallengeController.RipeAndHarvestCarrotCountInCurrentDay.Value = 0;
                var soilDatas = FindObjectOfType<GridController>().ShowGrid;
                //天数变更小植物成熟
                PlantController.Instance.Plants.ForEach((x, y, plant) =>
                {
                    if (plant != null)
                    {
                        plant.Grow(soilDatas[x, y]);
                        Debug.Log("grow" + PlantController.Instance.Plants[x, y].State);
                    }
                });


                //清空水的状态
                soilDatas.ForEach(soilData =>
                {
                    if (soilData != null)
                    {
                        soilData.Watered = false;
                    }
                });
                //清空水的对象
                foreach (var water in SceneManager.GetActiveScene()
                             .GetRootGameObjects()
                             .Where(gameObj => gameObj.name.StartsWith("Water")))
                {
                    water.DestroySelf();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGUI()
        {
            //显示天数
            IMGUIHelper.SetDesignResolution(640, 360);
            GUILayout.Space(10); //默认是verticle垂直方向，是行间距
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("天数:" + Global.Days.Value, mLabelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("$" + Global.Coin.Value, mCoinStyle);
            GUILayout.EndHorizontal();
            
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("体力" + Global.Power, mLabelStyle);
            GUILayout.EndHorizontal();
            
            //
            // GUILayout.BeginHorizontal();
            // GUILayout.Space(10);
            // GUILayout.Label("胡萝卜:" + Global.CarrotCount.Value, mLabelStyle);
            // GUILayout.EndHorizontal();
            //
            // GUILayout.BeginHorizontal();
            // GUILayout.Space(10);
            // GUILayout.Label("南瓜:" + Global.PumpkinCount.Value, mLabelStyle);
            // GUILayout.EndHorizontal();
            //
            // GUILayout.BeginHorizontal();
            // GUILayout.Space(10);
            // GUILayout.Label("土豆:" + Global.PotatoCount.Value, mLabelStyle);
            // GUILayout.EndHorizontal();
            //
            // GUILayout.BeginHorizontal();
            // GUILayout.Space(10);
            // GUILayout.Label("西红柿:" + Global.TomatoCount.Value, mLabelStyle);
            // GUILayout.EndHorizontal();
            //
            // GUILayout.BeginHorizontal();
            // GUILayout.Space(10);
            // GUILayout.Label("豌豆:" + Global.BeanCount.Value, mLabelStyle);
            // GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            //GUI.Label(new Rect(10, 360 - 24, 200, 24), "[1] 手 [2] 铁锹 [3] 种子 [4] 花洒");
        }

        private void Update()
        {
            //结束游戏
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("GamePass");
            }
            

            //角色移动
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
            
            var direction = new Vector2(horizontalInput, verticalInput).normalized;

            var targetVelocity = direction * 5;
            mRigidbody2D.velocity= Vector2.Lerp(mRigidbody2D.velocity,targetVelocity,1-Mathf.Exp(-Time.deltaTime*10));
            
            //动画播放
            if (horizontalInput==0&&verticalInput==0)
            {
                //静止
                //上一帧是向右走则方向为右
                if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerRightWalk"))
                {
                    mAnimator.Play("PlayerRightIdle");
                }
                else if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerLeftWalk"))
                {
                    mAnimator.Play("PlayerLeftIdle");
                }
            }else 
            {
                if (horizontalInput>=0)
                {
                    mAnimator.Play("PlayerRightWalk");
                }
                else
                {
                    mAnimator.Play("PlayerLeftWalk");
                }
            }
        }

        private void OnDestroy()
        {
            Global.Player = null;
        }
    }
}