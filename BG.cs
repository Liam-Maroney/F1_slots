using Godot;
using System;

public partial class BG : ParallaxBackground
{
	public const float ScrollingSpeed = 100.0f;
	
	public override void _Process(double delta){
		Vector2 so = ScrollOffset;
		so.X -= ScrollingSpeed * (float)delta;
		ScrollOffset = so;
	}
}
