 using Godot;
using System;

public partial class Player : CharacterBody2D  
{
	public int   health = 10;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready(){
		 GetNode<AnimationPlayer>("AnimationPlayer");  
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity .Y = JumpVelocity;
			if (velocity.Y!=0){
				GetNode<AnimationPlayer>("AnimationPlayer").Play("Jump");
			}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction.X == -1){
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;  
		}
		else if(direction.X == 1){
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false;
		}
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			if (velocity.Y == 0){
				GetNode<AnimationPlayer>("AnimationPlayer").Play("Run");
			}
		}
		else{
			
			if (velocity.Y == 0){
				GetNode<AnimationPlayer>("AnimationPlayer").Play("Idle");
			}
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			
		}
		if (velocity.Y >0){
			GetNode<AnimationPlayer>("AnimationPlayer").Play("Fall"); 
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
