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
        public int StartDate=0;

        public abstract void OnStart();

        public abstract bool CheckFinish();
        
        public abstract void OnFinish();
    }
}