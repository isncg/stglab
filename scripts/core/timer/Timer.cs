using System;
using System.Collections.Generic;
namespace isn{


    public static class Timer{
        public static TimerImpl DefaultTimer = new TimerImpl();

        public static void Add(int frameDelay, Action action){
            DefaultTimer.Add(frameDelay, action);
        }
        public static void Tick(){
            DefaultTimer.OnTick();
        }
    }



    public class TimerImpl{
        private int tick = 0;
        private MinHeap<TimerTask> taskHeap = new MinHeap<TimerTask>(128);
        public void Add(int frameDelay, Action action){
            var task = new TimerTask();
            task.trigerFrame = tick+frameDelay;
            task.userAction = action;
            taskHeap.Add(task);
        }

        public void OnTick(){
            tick++;
            while(true){
                var top = taskHeap.Top();
                if(top == null || top.trigerFrame>tick)
                    break;
                top.userAction.Invoke();
                taskHeap.Pop();
            }
        }
    }

    public class TimerTask: IComparable<TimerTask>{
        public int trigerFrame;       
        public Action userAction;

        public int CompareTo(TimerTask obj)
        {
            return trigerFrame - obj.trigerFrame;
        }

        public virtual void OnTrigger(){

        }

        public override string ToString(){
            return string.Format("<TTask at:{0}>", trigerFrame);
        }
    }
}