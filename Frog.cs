using Godot;
using System;

public partial class Frog : CharacterBody2D
{
	public const float Speed = 50.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public Player player;
	public bool chase = false;
	
	public override void _Ready(){
		 GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
	}
	
	public override void _PhysicsProcess(double delta){
		// Gravity for Frog.  
		Vector2 velocity = Velocity;
		velocity.Y += gravity * (float)delta;
		
		player = GetNode<Player>("../../Player/Player");
		Vector2 direction = (player.Position - GlobalPosition).Normalized();
		if (chase == true){
			if (GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation != "Death"){
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Jump");
				if (direction.X > 0){
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
				}
				else{
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false;
				}
			}
			velocity.X = direction.X * Speed;
		}  
		else{
			if (GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation != "Death"){
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
			}
			velocity.X = 0;
		}
		Velocity = velocity;
		MoveAndSlide();
	}
	
	private void _on_player_detection_body_entered(Node2D body)
	{
		if(body is Player player){
			chase = true;
		}
	}
	private void _on_player_detection_body_exited(Node2D body)
	{
		if(body is Player player){
			chase = false;
		}
	}
	private void _on_player_death_body_entered(Node2D body)
	{
		if (body is Player player){
			chase = false; 
			var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			var animationPlayer = animatedSprite.GetNode<AnimationPlayer>("AnimationPlayer");

			// Play the death animation
			animatedSprite.Play("Death");

			// Connect to the animation_finished signal
			Godot.Callable callback = new Godot.Callable(this, nameof(OnDeathAnimationFinished));
			animationPlayer.Connect("animation_finished", callback);
		}
	}
	private void OnDeathAnimationFinished(string animName)
	{
		if (animName == "Death")
		{
			// Animation finished, perform cleanup or further actions
			this.QueueFree();
		}
	}


	private void _on_player_death_body_exited(Node2D body)
	{
		if (body is Player player){
			
		}
	}
}



