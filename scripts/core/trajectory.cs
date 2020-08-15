using Godot;
using System;
using System.Collections.Generic;
namespace isn
{


    public interface ITrajectory{
        //void Calc(float t, ref ProjectileState result);   
        void Calc(float t, ref Vector2 position, ref float rotation);     
    }



    // public class TrajectoryModifier{
    //     public Vector2 origin;
    //     public float rotation;
    //     public TrajectoryParam Calc(ITrajectory trajectory, 
    //                             float t, ref TrajectoryParam projTransform)
    //     {
    //         TrajectoryParam result = new TrajectoryParam();
    //         trajectory.Calc(t, ref projTransform);
    //         float px = Mathf.Cos(rotation)*projTransform.offsetOrigin.x - Mathf.Sin(rotation)*projTransform.offsetOrigin.y;
    //         float py = Mathf.Sin(rotation)*projTransform.offsetOrigin.x + Mathf.Cos(rotation)*projTransform.offsetOrigin.y;
    //         result.offsetOrigin.x = px;
    //         result.offsetOrigin.y = py;
    //         result.offsetRotation =projTransform.offsetRotation+ rotation;
            
    //         return result;
    //     }
    // }

    public class TrajectoryParam{
        public Vector2 translation;
        public float rotation;
        public float lifeTime;
        public float speed;
    }


    // public static class TrajectoryUtil{
    //     public static void CalcProjectilePosAndRot(ITrajectory trajectory, TrajectoryParam param, ref ProjectileState result)
    //     {
    //         trajectory.Calc(param.lifeTime* param.speed, ref result.position, ref result.rotation);
    //         //Console.WriteLine(string.Format("Calc pos: {0}", result.position));
    //         float px = Mathf.Cos(param.rotation)*result.position.x - Mathf.Sin(param.rotation)*result.position.y;
    //         float py = Mathf.Sin(param.rotation)*result.position.x + Mathf.Cos(param.rotation)*result.position.y;
    //         result.position.x = px;
    //         result.position.y = py;
    //         result.rotation+=param.rotation;            
    //     }
    // }

    public enum TrajectoryType{
        DIRECT
    }

    public class TrajectoryLib{
        private Dictionary<int, ITrajectory> traj;

        public ITrajectory Get(TrajectoryType trajectoryType){
            ITrajectory trajectory=null;
            traj.TryGetValue((int)trajectoryType, out trajectory);
            return trajectory;
        }
        public TrajectoryLib(){
            traj = new Dictionary<int, ITrajectory>();
            traj.Add((int)TrajectoryType.DIRECT, new DirectTrajectory());
        }

    }

    public class DirectTrajectory : ITrajectory
    {
        // public void Calc(float t, ref v)
        // {
        //     result.position.x = 0;
        //     result.position.y = t;
        //     result.rotation = 0;
        //     // return new ProjectileState(){
        //     //     position = new Vector2(0,t),
        //     //     rotation = 0
        //     // };          
        // }

        public void Calc(float t, ref Vector2 position, ref float rotation)
        {
            position.x=0;
            position.y=t;
            rotation = 0;
        }
    }
}