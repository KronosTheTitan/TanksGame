
using System;
using GXPEngine;

// TODO: Fix this mess! - see Assignment 2.2
public class AITank : Sprite
{
	NLineSegment[] nLines;
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

	public AITank(float px, float py) : base("assets/bodies/t34.png")
	{
		_position.x = px;
		_position.y = py;
		_barrel = new Barrel();
		SetOrigin(width / 2, height / 2);
		AddChild(_barrel);
		Vec2[] corners = GetExtentsVec2();
		nLines = new NLineSegment[]{
			new NLineSegment(corners[0],corners[1]),
			new NLineSegment(corners[1],corners[2]),
			new NLineSegment(corners[2],corners[3]),
			new NLineSegment(corners[3],corners[0])
		};
		foreach (NLineSegment nLine in nLines)
			game.AddChild(nLine);
	}

	void UpdateScreenPosition()
	{
		x = _position.x;
		y = _position.y;
	}

	public virtual void Update()
	{
		velocity *= 0.2f;
		// Basic Euler integration:
		_position += velocity;
		UpdateScreenPosition();
		foreach (NLineSegment nLine in nLines)
			game.RemoveChild(nLine);
		Vec2[] corners = GetExtentsVec2();
		nLines = new NLineSegment[]{
			new NLineSegment(corners[0],corners[1]),
			new NLineSegment(corners[1],corners[2]),
			new NLineSegment(corners[2],corners[3]),
			new NLineSegment(corners[3],corners[0])
		};
		foreach (NLineSegment nLine in nLines)
			game.AddChild(nLine);

		foreach (Bullet bullet in MyGame.activeScene.bullets)
		{
			foreach(NLineSegment nLine in nLines)
			{
				Vec2 bulletToLine = bullet.position - nLine.start;
				float bulletDistanceNew = bulletToLine.Dot((nLine.end - nLine.start).Normal());
				Vec2 bulletToLineOld = bullet.oldPosition - nLine.start;
				float bulletDistanceOld = bulletToLineOld.Dot((nLine.end - nLine.start).Normal());
                if (bulletDistanceOld - bullet.radius > 0 && bulletDistanceNew - bullet.radius < 0 && bullet.origin!=this)
                {
					float a = bulletDistanceOld - bullet.radius;
					float b = bulletDistanceOld +(bulletDistanceNew>0 ? bulletDistanceNew : (bulletDistanceNew*-1));
					float t = a/b;
					Vec2 desiredPos = bullet.oldPosition + (bullet.velocity * t);
					Vec2 lineVector = nLine.end - nLine.start;
					float lineLength = lineVector.Length();
					bulletToLine = nLine.start - desiredPos;
					float dotProduct = bulletToLine.Dot(lineVector.Normal());
                    if (dotProduct > 0 && dotProduct < lineLength)
					{						
						Console.WriteLine("Hit! at : " + bullet._position.ToString());
						bullet.velocity = velocity - (1 + 0.99f) * velocity.Normal() * (nLine.end - nLine.start).Normal();
						bullet._position = desiredPos; 
					}
					else
					{
						bullet._position = bullet.oldPosition + bullet.velocity;
					}
                }
			}			
		}
	}
}
