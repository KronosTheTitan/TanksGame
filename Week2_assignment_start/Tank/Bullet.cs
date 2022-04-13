using GXPEngine;
using System;

public class Bullet : Sprite 
{

	// public fields & properties:
	public Vec2 position 
	{
		get 
		{
			return _position;
		}
	}
	public Vec2 velocity;

	public float radius;
	public AITank origin;

	// private fields:
	public Vec2 _position;
	public Vec2 oldPosition;

	public Bullet(Vec2 pPosition, Vec2 pVelocity,AITank pOrigin) : base("assets/bullet.png") 
	{
		_position = pPosition;
		oldPosition = _position;
		velocity = pVelocity;
		rotation = velocity.GetAngleDegrees();
		SetOrigin(width / 2, height / 2);
		radius = height / 2;
		MyGame.activeScene.bullets.Add(this);
		origin=pOrigin;
	}

	void UpdateScreenPosition() 
	{
		x = _position.x;
		y = _position.y;
	}

	void Update() 
	{
		oldPosition = _position;
		_position += velocity;
		UpdateScreenPosition ();
	}
}
