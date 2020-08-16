using Godot;
using System;
using System.Collections.Generic;
namespace isn
{


    public interface ITrajectory{
        void Calc(double t, ref Vector2 position, ref float rotation);     
    }
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
        public void Calc(double t, ref Vector2 position, ref float rotation)
        {
            position.x=0;
            position.y=(float)t;
            rotation = 0;
        }
    }

    public class DirectTrajectoryAcc : ITrajectory
    {
        public void Calc(double t, ref Vector2 position, ref float rotation)
        {
            throw new NotImplementedException();
        }
    }
}