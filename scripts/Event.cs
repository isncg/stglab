using System;
using System.Collections.Generic;

namespace isn{
    public class EventType{
        public class Bullet{
            public class TestFire{}
        }
    }

    public static class Event<EventTypeCls>{
        static HashSet<Action> handlerSet = new HashSet<Action>();
        public static bool Register(Action handler){
            if(!handlerSet.Contains(handler)){
                handlerSet.Add(handler);
                return true;
            }
            return false;
        }

        public static bool UnRegister(Action handler){
            if(handlerSet.Contains(handler)){
                handlerSet.Remove(handler);
                return true;
            }
            return false;
        }

        public static void Dispatch(){
            foreach(var handler in handlerSet)
                handler.Invoke();
        }

        public static void Clear(){
            handlerSet.Clear();
        }
    }

    public static class Event<EventTypeCls, ParamCls>{
        static HashSet<Action<ParamCls>> handlerSet = new HashSet<Action<ParamCls>>();
        public static bool Register(Action<ParamCls> handler){
            if(!handlerSet.Contains(handler)){
                handlerSet.Add(handler);
                return true;
            }
            return false;
        }

        public static bool UnRegister(Action<ParamCls> handler){
            if(handlerSet.Contains(handler)){
                handlerSet.Remove(handler);
                return true;
            }
            return false;
        }


         public static void Dispatch(ParamCls param){
            foreach(var handler in handlerSet)
                handler.Invoke(param);
        }

        public static void Clear(){
            handlerSet.Clear();
        }
    }
}