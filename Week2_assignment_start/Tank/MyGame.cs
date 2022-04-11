using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
	public static MyGame activeScene;

	static void Main() 
	{
		new MyGame().Start();
	}

	public MyGame () : base(1600, 900, false,false)
	{
		activeScene = this;
		// background:
		AddChild (new Sprite ("assets/desert.png"));
		// tank:
		AddChild (new Tank (width / 2, height / 2));
	}
}