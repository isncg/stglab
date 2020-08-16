using Godot;
using System.Collections.Generic;

namespace isn{
    public class FireControl{

        // public List<TrajectoryParam> CreateRadiation(ProjectileState origin, int count, float rotationStep, float speed){
        //     var result = new List<TrajectoryParam>(count);
        //     var halfRotation = rotationStep*(count-1)/2;
        //     for(int i=0;i<count;i++){
        //         var param = new TrajectoryParam();
        //         param.translation = origin.position;
        //         param.rotation = origin.rotation+rotationStep*i-halfRotation;
        //         param.speed = speed;
        //         result.Add(param);
        //     }
        //     return result;
        // }

        public List<BulletState> CreateRadiation(
            ITrajectory trajectory, 
            int count, 
            float radiationStep, 
            float speed,
            Vector2 trajPosition,
            float trajRotation,
            double currentStageTime,
            double firstFireDelay,
            double lastFireDelay

        ){
            var result = new List<BulletState>(count);
            var halfRotation = radiationStep*(count-1)/2;
            for(int i=0;i<count;i++){
                var param = new BulletState();
                param.SetTrajectoryAndTransform(trajectory, trajPosition, trajRotation + radiationStep*i - halfRotation);
                param.SetTimeAndSpeed(speed, 0, 100);
                result.Add(param);
            }
            result[0].timeBegin = currentStageTime+firstFireDelay;
            if(count>1){
                for(int i=1;i<count;i++){
                    result[i].timeBegin = currentStageTime + (lastFireDelay - firstFireDelay)*i/(count-1);
                }
            }

            return result;
        }

        // public List<ProjectileState> projectileStates;

        // public void CreateRadiation(ProjectileState origin, int count, float rotationStep){
        //     projectileStates = new List<ProjectileState>(count);
        //     for(int i=0;i<count;i++){
        //         var state = new ProjectileState(origin);
        //         state.rotation = origin.rotation+rotationStep*i-rotationStep*(count-1);
        //         projectileStates.Add(state);
        //     }
        // }
    }
}