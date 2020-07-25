using Godot;
using System.Collections.Generic;
namespace isn
{
    public interface ITrajectory{
        void GetTransform(float t, ref ProjectileState projTransform);        
    }

    public class TrajectoryOffset{
        public Vector2 origin;
        public float rotation;
        public void GetOffsetTransform(ITrajectory trajectory, 
                                float t, ref ProjectileState projTransform)
        {
            trajectory.GetTransform(t, ref projTransform);
            float px = Mathf.Cos(rotation)*projTransform.position.x - Mathf.Sin(rotation)*projTransform.position.y;
            float py = Mathf.Sin(rotation)*projTransform.position.x + Mathf.Cos(rotation)*projTransform.position.y;
            projTransform.position.x = px;
            projTransform.position.y = py;
            projTransform.rotation += rotation;
        }
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
        public void GetTransform(float t, ref ProjectileState state)
        {
            state.position.x = 0;
            state.position.y = t*state.speed;
            state.rotation = 0;
        }
    }
}