using Godot;


namespace isn
{
    public class ProjectileState{
        public Vector2 position = Vector2.Zero;
        public float rotation = 0;

        public bool isRunning = false;

        public ProjectileState(){}

        public ProjectileState( ProjectileState copyfrom){
            position = copyfrom.position;
            rotation = copyfrom.rotation;
        }
    }


    // public class Projectile{
    //     //public TrajectoryModifier trajModifier;
    //     public ProjectileState state;
    //     public ITrajectory trajectory;
    //     public float lifeStart;
    //     public float lifeStop;

    //     public float speed;
    //     public void Simulate(float time){
    //         float t = Mathf.Min(Mathf.Max(time, lifeStart), lifeStop);
    //         trajModifier.CalcState(trajectory, t - lifeStart, ref state);
    //     }

    // }
}