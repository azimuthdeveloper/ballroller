using Godot;
using System;

public partial class BallAttractor : Node3D
{
	private Node3D attractor;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		attractor = GetNode<Node3D>("../attractor");
		GD.Print(attractor.Position);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
