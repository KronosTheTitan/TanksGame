using GXPEngine;

// TODO: Fix this mess! - see Assignment 2.2
class Tank : Sprite 
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

	// private fields:
	Vec2 _position;
	Barrel _barrel;

	public Tank(float px, float py) : base("assets/bodies/t34.png") 
	{
		_position.x = px;
		_position.y = py;
		_barrel = new Barrel ();
		SetOrigin(width / 2, height / 2);
		AddChild (_barrel);
	}

	void Controls() 
	{
		if (Input.GetKey (Key.LEFT)) 
		{
			rotation--;
		}
		if (Input.GetKey (Key.RIGHT)) 
		{
			rotation++;
		}
		if (Input.GetKey (Key.UP)) 
		{
			velocity += Vec2.GetUnitVectorDeg(rotation).Normalized() * 3f;
		}
		if (Input.GetKey (Key.DOWN)) 
		{
			velocity -= Vec2.GetUnitVectorDeg(rotation).Normalized() * 3f;
		}
	}

	void UpdateScreenPosition() 
	{
		x = _position.x;
		y = _position.y;
	}

	public void Update() 
	{
		velocity *= 0.2f;
		Controls ();
		// Basic Euler integration:
		_position += velocity;
		UpdateScreenPosition ();
	}
}
