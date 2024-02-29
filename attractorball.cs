using Godot;
using System;
// using System.Timers;
// using Timer = System.Timers.Timer;

public partial class attractorball : RigidBody3D
{
	private int selectedIndex = 0;
	private Node3D attractor;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		attractor = GetNode<Node3D>("../attractor");
		GD.Print(attractor.Position);
		var timer = GetNode<Timer>("../Timer");
		// var timer = GetTree().
		timer.Timeout += OnTimeout;
		
		// var timer = new Timer() { Interval = 10000, AutoReset = true};
		// timer.Start();
		// timer.Elapsed += TimerOnElapsed;

	}

	private void OnTimeout()
	{
		var locations = GetNode<Node3D>("../BoxPositions").GetChildren();
		// var random = new Random().Next(locations.Count - 1);
		attractor = locations[selectedIndex++] as Node3D;
		GD.Print($"Changing to {attractor.Name}");
		GD.Print("chaning");
		if (selectedIndex == 3)
		{
			selectedIndex = 0;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		// Calculate distance and direction to the attractor
		
		var attractorDistance = Position.DistanceTo(attractor.Position);
		var attractorDirection = Position.DirectionTo(attractor.Position);
		var limits = new Vector3(2, 2, 2);
		// GD.Print(attractorDistance);

		// Apply force only when the ball is farther than a threshold
		if (attractorDistance > 0.5) // Adjust the threshold as needed
		{
			GD.Print(attractorDistance);
			// Scale force based on distance for smoother deceleration
			var forceMultiplier = Mathf.Max(0, 1 - attractorDistance / 10); // Adjust for desired range
			var force = attractorDirection * attractorDistance * forceMultiplier;

			// Cap the force to prevent sharp movements
			var maxForce = 10; // Experiment with appropriate limits
			force = force.Clamp(-limits, limits);
			DebugDraw3D.DrawLine(Position, attractor.Position, Colors.Red);
			// force = force.Clamp(maxForce, maxForce);

			ApplyCentralForce(force);
		}
		else
		{
			// Optionally apply a small stopping force if close to the attractor
			// to counteract any remaining momentum
			var stoppingForce = -LinearVelocity * 8f;
			ApplyCentralForce(stoppingForce);
			DebugDraw3D.DrawLine(Position, attractor.Position, Colors.Blue);
		}
		// base._PhysicsProcess(delta);
	}
}
