using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace IndieFarm
{
    public partial class UIMessageQueue : ViewController
    {
        public static void Push(Sprite icon, string message)
        {
            mInstance.UIMessageItemTemplate.InstantiateWithParent(mInstance.MessageRoot).Self(self =>
            {
                self.Icon.sprite = icon;
                self.TextWithIcon.text = message;
                self.Text.Hide();
                self.Icon.Show();
                self.TextWithIcon.Show();
                self.SetAlpha(0);
                self.Show();

                ActionKit.Sequence().Append(ActionKit.Lerp(0, 1, 0.5f, percent => { self.SetAlpha(percent); }))
                    .Delay(3.0f)
                    .Lerp(1, 0, 2.0f, self.SetAlpha)
                    .Start(self, self.DestroyGameObj);
            }).Show();
        }

        public static void Push(string message)
        {
            mInstance.UIMessageItemTemplate.InstantiateWithParent(mInstance.MessageRoot).Self(self =>
            {
                self.TextWithIcon.Hide();
                self.Icon.Hide();
                self.Text.text = message;
                self.Text.Show();
                self.SetAlpha(0);
                self.Show();

                //将透明度从0渐变到1，动画持续时间为0.5秒。
                //将透明度从1渐变到0，动画持续时间为2秒。
                ActionKit.Sequence().Append(ActionKit.Lerp(0, 1, 0.5f, self.SetAlpha))
                    .Delay(3.0f)
                    .Lerp(1, 0, 2.0f, self.SetAlpha)
                    .Start(self, self.DestroyGameObj);
            }).Show();
        }

        private static UIMessageQueue mInstance;

        private void Start()
        {
            UIMessageItemTemplate.Hide();

        }

        private void Awake()
        {
            mInstance = this;
        }

        private void OnDestroy()
        {
            mInstance = null;
        }
    }
}