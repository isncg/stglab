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
	TrajectoryLib trajectoryLib = new TrajectoryLib();
	public override void _Ready()
	{
		contentNode = GetNode("ViewportContainer/Viewport/bg/content") as Node2D;
		Event<EventType.Bullet.TestFire>.Register(CreateBullet);
		Event<EventType.Stage.ClearAllBullets>.Register(ClearAllBullets);
		
		


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

	public override void _Input(InputEvent @event){
		if(@event is InputEventKey key){
			if(key.Pressed && key.Scancode == (uint)KeyList.Z){
				Event<EventType.Bullet.TestFire>.Dispatch();
			}

			if(key.Pressed && key.Scancode == (uint)KeyList.X){
				Event<EventType.Stage.ClearAllBullets>.Dispatch();
			}
		}
	}
	public override void _ExitTree(){
		Event<EventType.Bullet.TestFire>.UnRegister(CreateBullet);
		Event<EventType.Stage.ClearAllBullets>.UnRegister(ClearAllBullets);
	}

	public void CreateBullet(){
		var paramList = fc.CreateRadiation(new ProjectileState(){
			position = Vector2.Zero,
			rotation = 0,		
		}, 10, 0.1f, 800);

		FireBullet(trajectoryLib.Get(TrajectoryType.DIRECT), paramList);
	}


	public void FireBullet(ITrajectory trajectory, List<TrajectoryParam> paramList){
		if(!fireEnable){
			Console.WriteLine("[stage] fire bullet while !fireEnable");
			return;
		}
		foreach(var param in paramList){
			var bullet = AllocBullet();
			bullet.trajectory = trajectory;
			bullet.param = param;
			bullet.state.isRunning = true;
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
			if(count > 100)
				count = 100;
			for(int i=0;i<count;i++){
				bullet = (STGBullet)resBullet.Instance();
				bullet.hostStage = this;
				bullet.Name = string.Format("bullet_{0}", creationCount);
				Console.WriteLine("[stage] create bullet instance:"+bullet.Name);
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

	bool fireEnable = true;
	public void ClearAllBullets(){
		fireEnable = false;
		foreach(var bullet in bulletAllocated){
			bullet.state.isRunning = false;
		}

		isn.Timer.Add(1, ()=>{
			while(bulletAllocated.Count>0){
				var bullet = bulletAllocated.Dequeue();
				bullet.Position = Vector2.Up*1000;
				bulletPool.Enqueue(bullet);

			}
			fireEnable = true;
		});
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		isn.Timer.Tick();		
	}
}
