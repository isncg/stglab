using Godot;
using System;
using System.Collections.Generic;
using isn;
public class STGStage : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	[Export]
	PackedScene resBullet;
	Node2D contentNode;

	FireControl fc = new FireControl();
	TrajectoryLib tl = new TrajectoryLib();
	public override void _Ready()
	{
		contentNode = GetNode("ViewportContainer/Viewport/bg/content") as Node2D;
		Event<EventType.Bullet.Create>.Register(CreateBullet);
		
		var paramList = fc.CreateRadiation(new ProjectileState(){
			position = Vector2.Zero,
			rotation = 0,			
		}, 10, 0.1f);

		FireBullet(tl.Get(TrajectoryType.DIRECT), paramList);


		// isn.Timer.Add(100, ()=>{
		// 	Event<EventType.Bullet.Create>.Dispatch();
		// });

		// isn.Timer.Add(800, ()=>{
		// 	Event<EventType.Bullet.Create>.Dispatch();
		// });

		// isn.Timer.Add(300, ()=>{
		// 	Event<EventType.Bullet.Create>.Dispatch();
		// });
		
		
	}
	public override void _ExitTree(){
		Event<EventType.Bullet.Create>.UnRegister(CreateBullet);
	}

	public void CreateBullet(){
		var bullet = (STGBullet)resBullet.Instance();
		contentNode.AddChild(bullet);
	}


	public void FireBullet(ITrajectory trajectory, List<TrajectoryParam> paramList){
		foreach(var param in paramList){
			var bullet = AllocBullet();
			bullet.trajectory = trajectory;
			bullet.param = param;
		}
	}

	Queue<STGBullet> bulletPool = new Queue<STGBullet>();
	Queue<STGBullet> bulletAllocated = new Queue<STGBullet>();
	

	int creationCount = 0;
	public STGBullet AllocBullet(){
		// if(bulletPool.Count>0)
		// {
		// 	var bullet =  bulletPool.Dequeue();
		// 	bullet.OnAlloc();
		// 	bulletAllocated.Enqueue(bullet);
		// 	return bullet;
		// }
		STGBullet bullet = null;

		if(bulletPool.Count<1){
			int count = bulletAllocated.Count;
			if(count<20)
				count = 20;
			for(int i=0;i<count;i++){
				bullet = (STGBullet)resBullet.Instance();
				bullet.Name = string.Format("bullet_{0}", creationCount);
				creationCount++;
				contentNode.AddChild(bullet);
				bulletPool.Enqueue(bullet);
			}			
		}

		bullet =  bulletPool.Dequeue();
		bullet.OnAlloc();
		bulletAllocated.Enqueue(bullet);
		return bullet;
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		isn.Timer.Tick();
	}
}
