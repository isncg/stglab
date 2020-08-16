using Godot;
namespace isn{
    public class BulletState{
        private ITrajectory trajectory;
        private Vector2 trajTranslation;
        private float trajRotation;
        private Vector2 bulletPosition;
        public Vector2 BulletPosition{get{return bulletPosition;}}
        private float bulletRotation;
        public float BulletRotation{get{return bulletRotation;}}
        public double timeBegin = 0;
        public double timeEnd = 100;
        private double speed = 100;

        public void Update(double stageTime){
            if(stageTime>=timeBegin && stageTime<=timeEnd){
                Calc(stageTime - timeBegin);
            }
            else{
                bulletPosition = initPos;
            }
        }
        
        Vector2 _pos_1 = new Vector2();
        Vector2 _pos_2 = new Vector2();

        bool isTrajRotDirty = true;
        float sinr = 0;
        float cosr = 1;
        float _rot_1 = 0;
        float _rot_2 = 0;

        static Vector2 initPos = Vector2.Left*1000;

        public void SetTrajectoryAndTransform(ITrajectory trajectory, Vector2 translation, float rotation){
            this.trajectory = trajectory;
            trajTranslation = translation;
            trajRotation = rotation;
            sinr = Mathf.Sin(trajRotation);
            cosr = Mathf.Cos(trajRotation);
        }

        public void SetTimeAndSpeed(float speed, float timeBegin, float timeEnd){
            this.timeBegin = timeBegin;
            this.timeEnd = timeEnd;
            this.speed = speed;
        }
        private void Calc(double trajTime){
            trajectory.Calc(trajTime*speed, ref _pos_1, ref _rot_1);
            _pos_2.x = cosr*_pos_1.x - sinr*_pos_1.y;
            _pos_2.y = sinr*_pos_1.x + cosr*_pos_1.y;
            _pos_2 += trajTranslation;
            _rot_2 = _rot_1+trajRotation;

            bulletPosition = _pos_2;
            bulletRotation = _rot_2;
        }
    }
}