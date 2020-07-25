using Godot;


namespace isn
{
    public class ProjectileState{
        public Vector2 position = Vector2.Zero;
        public float rotation = 0;
        public float speed = 1;

        public ProjectileState(){}

        public ProjectileState( ProjectileState copyfrom){
            position = copyfrom.position;
            rotation = copyfrom.rotation;
            speed = copyfrom.speed;
        }
    }


    public class Projectile{
        public TrajectoryOffset offset;
        public ProjectileState transform;
        public ITrajectory trajectory;
        public float lifeStart;
        public float lifeStop;

        public float speed;
        public void Simulate(float time){
            float t = Mathf.Min(Mathf.Max(time, lifeStart), lifeStop);
            offset.GetOffsetTransform(trajectory, t - lifeStart, ref transform);
        }

    }
}