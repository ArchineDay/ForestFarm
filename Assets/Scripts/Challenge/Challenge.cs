using System;

namespace IndieFarm
{
    public abstract class Challenge
    {
        public enum States
        {
            NotStart,
            Started,
            Finished
        }

        public States State = States.NotStart;


        public abstract string Name { get; }

        //隔开挑战日期用
        public int StartDate = 0;

        public abstract void OnStart();

        public abstract bool CheckFinish();

        public abstract void OnFinish();
    }

    public class GenericChallenge : Challenge
    {
        public override string Name => mName;
        private string mName;


        public override void OnStart()
        {
            mOnStart?.Invoke(this);
        }

        public override bool CheckFinish()
        {
            return mOnCheckFinish.Invoke(this);
        }

        public override void OnFinish()
        {
            mOnFinish?.Invoke(this);
        }

        public GenericChallenge Key(string name)
        {
            mName = name;
            return this;
        }

        public GenericChallenge OnStart(Action<GenericChallenge> onStart)
        {
            mOnStart = onStart;
            return this;
        }

        public GenericChallenge OnCheckFinish(Func<GenericChallenge, bool> onCheckFinish)
        {
            mOnCheckFinish = onCheckFinish;
            return this;
        }

        public GenericChallenge OnFinish(Action<GenericChallenge> onFinish)
        {
            mOnFinish = onFinish;
            return this;
        }


        private Action<GenericChallenge> mOnStart;
        private Func<GenericChallenge, bool> mOnCheckFinish;
        private Action<GenericChallenge> mOnFinish;
    }
}