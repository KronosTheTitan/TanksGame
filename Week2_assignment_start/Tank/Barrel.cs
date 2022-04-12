using GXPEngine;
using System;

class Barrel : Sprite 
{
	float lastShot = 0;
	readonly float shotDelay = 1000;
	public Barrel() : base("assets/barrels/t34.png") 
	{
		SetOrigin(width/(width/39f), height/(height/29.5f));
		x += 10;
	}

	public void Update()
	{
		Shoot();
		Vec2 dist = new Vec2(Input.mouseX, Input.mouseY) - new Vec2(parent.x+x,parent.y+y);
		float targetRotation = dist.GetAngleDegrees() - parent.rotation;
		float diff = (targetRotation - rotation + 180) % 360 - 180;
		diff += diff < -180 ? 360 : 0;
		if (Mathf.Abs(diff) < .9f)
        {
			rotation = targetRotation;
			return;
        }
		rotation += diff < 0 ? -.9f : .9f;
	}
	void Shoot()
	{
		if (Input.GetKey(Key.SPACE)&&lastShot+shotDelay<Time.time)
		{
			lastShot = Time.time;
			Vec2 dir = Vec2.GetUnitVectorDeg(parent.rotation+rotation);
			dir = dir.Normalized() * 5;
			Bullet bullet = new Bullet(new Vec2(parent.x-x,parent.y-y),dir);
			MyGame.activeScene.AddChild(bullet);
		}
	}
}
