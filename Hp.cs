using Godot;
using System;

public partial class Hp : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Player p = GetNode<Player>("../../Player/Player");
		this.Text = "HP: " + p.health.ToString();
	}
}
