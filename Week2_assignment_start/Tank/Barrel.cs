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
		Vec2 dist = new Vec2(Input.mouseX, Input.mouseY) - new Vec2(parent.x+x,parent.y+y);
		float targetRotation = dist.GetAngleDegrees() - parent.rotation;
		if (targetRotation > 360f)
			for (float i = targetRotation; i > 360; i -= 360) targetRotation = i;
		else if (targetRotation < 0)
			for (float i = targetRotation; i < 0; i += 360) targetRotation = i;
		if(!(rotation > targetRotation-1f&&rotation < targetRotation +1f))
        {
			if (rotation < -90 && targetRotation > rotation+180) rotation -= 1f;
			else if(rotation > 90 && targetRotation < rotation-180) rotation += 1f;
			else if (rotation > targetRotation) rotation-=1f;
			else if (rotation < targetRotation) rotation+=1f;
        }
		if (rotation < -180)
        {
			rotation = 180;
			Console.WriteLine("counter clockwise "+rotation);
		}
		if (rotation > 180) {
			rotation = -180;
			Console.WriteLine("clockwise " + rotation);
		}
		Shoot();
	}
	void Shoot()
	{
		if (Input.GetKey(Key.SPACE)&&lastShot+shotDelay<Time.time)
		{
			lastShot = Time.time;
			Vec2 dir = Vec2.GetUnitVectorDeg(parent.rotation+rotation);
			dir = dir.Normalized() * 5;
			Bullet bullet = new Bullet(new Vec2(parent.x+x,parent.y+y)+dir,dir);
			bullet.rotation = parent.rotation+rotation;
			MyGame.activeScene.AddChild(bullet);
		}
	}
}
