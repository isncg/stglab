using System;
using System.Collections.Generic;
namespace isn{


    public static class Timer{
        public static TimerImpl DefaultTimer = new TimerImpl();

        public static void FrameDelay(int frameDelay, Action action){
            DefaultTimer.FrameDelay(frameDelay, action);
        }
        public static void Tick(double dt){
            DefaultTimer.OnTick(dt);
        }
    }



    public class TimerImpl{
        private int tick = 0;
        public int Tick{get{return tick;}}
        private double time = 0;
        public double Time{get{return time;}}
        private MinHeap<FrameTimerTask> frameTaskHeap = new MinHeap<FrameTimerTask>(128);
        private MinHeap<TimerTask> taskHeap = new MinHeap<TimerTask>(128);
        public void FrameDelay(int frameDelay, Action action){
            var task = new FrameTimerTask();
            task.trigerFrame = tick+frameDelay;
            task.userAction = action;
            frameTaskHeap.Add(task);
        }

        public void Delay(double timeDelay, Action action){
            var task = new TimerTask();
            task.trigerTime = time + timeDelay;
            task.userAction = action;
            taskHeap.Add(task);
        }

        public void OnTick(double dt){
            tick++;
            time+=dt;
            while(true){
                var top = frameTaskHeap.Top();
                if(top == null || top.trigerFrame>tick)
                    break;
                top.userAction.Invoke();
                frameTaskHeap.Pop();
            }

            while(true){
                var top = taskHeap.Top();
                if(top == null || top.trigerTime>time)
                    break;
                top.userAction.Invoke();
                frameTaskHeap.Pop();
            }
        }
    }

    public class FrameTimerTask: IComparable<FrameTimerTask>{
        public int trigerFrame;       
        public Action userAction;

        public int CompareTo(FrameTimerTask obj)
        {
            return trigerFrame - obj.trigerFrame;
        }

        public virtual void OnTrigger(){

        }

        public override string ToString(){
            return string.Format("<TTask at:{0}>", trigerFrame);
        }
    }

    public class TimerTask : IComparable<TimerTask>
    {
        public double trigerTime;
        public Action userAction;

        public int CompareTo(TimerTask other)
        {
            if(trigerTime>other.trigerTime)
                return 1;
            else if(trigerTime<other.trigerTime)
                return -1;
            return 0;
        }
    }
}