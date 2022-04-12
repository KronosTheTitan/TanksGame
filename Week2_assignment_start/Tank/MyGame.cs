using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
	public static MyGame activeScene;
	public List<Bullet> bullets = new List<Bullet>();

	static void Main() 
	{
		new MyGame().Start();
	}

	public MyGame () : base(1600, 900, false,false)
	{
		UnitTests.RunTests();
		activeScene = this;
		// background:
		AddChild (new Sprite ("assets/desert.png"));
		// tank:
		AddChild (new Tank (width / 2, height / 2));
	}
}