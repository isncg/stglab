using Godot;
using isn;
using System;

public class STGBullet : Node2D, IUIBullet
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	
	public STGStage hostStage = null;

	public int BulletID{get;private set;}
	public void Register(){

	}

	public void UnRegister(){

	}

	static Vector2 initPos = Vector2.Left*1000;
	public override void _EnterTree(){
		Position = initPos;
	}

	public override void _Ready()
	{

		//param.lifeTime = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public BulletState state = null;
    //float lifetime = 0;
	public override void _Process(float delta)
	{
		// if(trajectory!=null && state.isRunning){
		// 	param.lifeTime+=delta;
		// 	TrajectoryUtil.CalcProjectilePosAndRot(trajectory, param, ref state);
		// 	this.Position = state.position;
		// 	this.Rotation = state.rotation;
		// 	//Console.WriteLine(string.Format("LT: {0}, pos: {1}", param.lifeTime, state.position));
		//}

        if(state!=null){
            //lifetime+=delta;
            state.Update(isn.Timer.DefaultTimer.Time);
            this.Position = state.BulletPosition;
            this.Rotation = state.BulletRotation;
        }

		//this.Position+=Vector2.Down*delta*20;
	}

	private int regID;
    public void OnBulletRegister(int regID)
    {
        this.regID = regID;
    }

    public void OnBulletRelease()
    {
        this.regID = -1;
    }

	public void OnAlloc(){

	}

	public void OnRecycle(){

	}
}
