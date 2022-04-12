using GXPEngine;

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

	// private fields:
	Vec2 _position;

	public Bullet(Vec2 pPosition, Vec2 pVelocity) : base("assets/bullet.png") 
	{
		_position = pPosition;
		velocity = pVelocity;
		rotation = velocity.GetAngleDegrees();
		SetOrigin(width / 2, height / 2);
		radius = height / 2;
	}

	void UpdateScreenPosition() 
	{
		x = _position.x;
		y = _position.y;
	}

	void Update() 
	{
		_position += velocity;
		UpdateScreenPosition ();
	}
}
