using Godot;
using System.Collections.Generic;

namespace isn{
    public class FireControl{

        public List<ProjectileState> projectileStates;

        public void CreateRadiation(ProjectileState origin, int count, float rotationStep){
            projectileStates = new List<ProjectileState>(count);
            for(int i=0;i<count;i++){
                var state = new ProjectileState(origin);
                state.rotation = origin.rotation+rotationStep*i-rotationStep*(count-1);
                projectileStates.Add(state);
            }
        }
    }
}