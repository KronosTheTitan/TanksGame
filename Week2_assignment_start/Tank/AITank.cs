
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
			new NLineSegment(corners[1],corners[0]),
			new NLineSegment(corners[0],corners[3]),
			new NLineSegment(corners[3],corners[2]),
			new NLineSegment(corners[2],corners[1])
		};
		foreach (NLineSegment nLine in nLines)
			game.AddChild(nLine);

		foreach (Bullet bullet in MyGame.activeScene.bullets)
		{
			if(bullet.origin != this)
			{
				foreach (NLineSegment nLine in nLines)
				{
					Vec2 ltb = bullet.position - nLine.start;
					float ballDistance = ltb.Dot((nLine.end - nLine.start).Normal());   //HINT: it's NOT 10000

					//compare distance with ball radius
					if (ballDistance < bullet.radius)
					{
						float a = (bullet.oldPosition - nLine.start).Dot((nLine.end - nLine.start).Normal()) - bullet.radius;
						float b = -bullet.velocity.Dot((nLine.end - nLine.start).Normal());
						float t = a / b;
						//bullet.position = bullet.oldPosition + (bullet.velocity * t);
						Vec2 desiredPos = bullet.oldPosition + (bullet.velocity * t);
						Vec2 lineVector = nLine.end - nLine.start;
						float lineLength = lineVector.Length();
						Vec2 bulletToLine = desiredPos - nLine.start;
						float dotProduct = bulletToLine.Dot(lineVector.Normalized());
						if (dotProduct > 0 && dotProduct < lineLength && !(b <= 0 && a < 0))
						{
							bullet.SetColor(1, 0, 0);
							bullet._position = desiredPos;
							bullet.velocity = bullet.velocity.Reflect((nLine.end - nLine.start).Normal(), 1f);
                            //if (notbounce)
							//{
							//	LateDestroy();
							//	bullet.LateDestroy();
							//}
							bullet.rotation = bullet.velocity.GetAngleDegrees();
							Console.WriteLine(t + " : " + bullet.velocity.ToString()+" : "+MyGame.activeScene.bullets.Count);
							bullet._position = bullet.oldPosition + (bullet.velocity * (1 - t));

						}
						else
						{
							bullet._position = bullet.oldPosition + bullet.velocity;
						}
					}
					else
					{
						bullet.SetColor(0, 1, 0);
					}
				}
			}			
		}
	}
}
