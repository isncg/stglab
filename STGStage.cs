using Godot;
using System;
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
	public override void _Ready()
	{
		contentNode = GetNode("ViewportContainer/Viewport/bg/content") as Node2D;
		Event<EventType.Bullet.Create>.Register(CreateBullet);
		
		isn.Timer.Add(100, ()=>{
			Event<EventType.Bullet.Create>.Dispatch();
		});

		isn.Timer.Add(800, ()=>{
			Event<EventType.Bullet.Create>.Dispatch();
		});

		isn.Timer.Add(300, ()=>{
			Event<EventType.Bullet.Create>.Dispatch();
		});
		
		
	}
	public override void _ExitTree(){
		Event<EventType.Bullet.Create>.UnRegister(CreateBullet);
	}

	public void CreateBullet(){
		var bullet = (STGBullet)resBullet.Instance();
		contentNode.AddChild(bullet);
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		isn.Timer.Tick();
	}
}
