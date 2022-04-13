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

	Bullet _ball;

	EasyDraw _text;

	NLineSegment _lineSegment;

	public MyGame () : base(1600, 900, false,false)
	{
		UnitTests.RunTests();
		activeScene = this;
		// tank:
		Tank tank = new Tank(width / 2, height / 2);
		AddChild (tank);
		AddChild(new AITank(width / 4, height / 4));
	}
}