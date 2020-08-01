using Godot;
using isn;
using System;

public class STGBullet : Node2D, IUIBullet
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.

	public int BulletID{get;private set;}
	public void Register(){

	}

	public void UnRegister(){

	}

	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		this.Position+=Vector2.Down*delta*20;
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
}
