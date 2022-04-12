using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class UnitTests
{
    public static void RunTests()
    {
		Vec2 myVec = new Vec2(2, 3);
		Vec2 result = myVec * 3;
		Console.WriteLine("Scalar multiplication right ok ?: " +
		 (result.x == 6 && result.y == 9 && myVec.x == 2 && myVec.y == 3));
		Vec2 result2 = 4 * myVec;
		Console.WriteLine("Scalar multiplication left ok ?: " +
		 (result2.x == 8 && result2.y == 12 && myVec.x == 2 && myVec.y == 3));
		Vec2 result3 = myVec;
		result3.SetXY(3, 4);
		Console.WriteLine("SetXY ok?: " + (result3.x == 3 && result3.y == 4));
		Vec2 result4 = new Vec2(12, 9);
		Console.WriteLine("Length ok?: " + (result4.Length() == 15));
		Vec2 result5 = new Vec2(-3f, 4f);
		result5.Normalize();
		Console.WriteLine("Normalize ok?: " + (result5.x == -0.6f && result5.y == 0.8f));
		Vec2 result6 = new Vec2(6, 8).Normalized();
		Console.WriteLine("Normalized ok?: " + (result6.x == 0.6f && result6.y == 0.8f));

		Vec2 question = new Vec2(5, 5).Normalized();
		question *= 10;
		Console.WriteLine(question.x + "," + question.y);
		Console.WriteLine(question.Length() == 10);

		Console.WriteLine("Deg2Rad : " + (Approximate(Vec2.Deg2Rad(90), .5f * Mathf.PI)));
		// test Vec2.Rad2Deg
		Console.WriteLine("Rad2Deg : " + (Approximate(Vec2.Rad2Deg(1.5f * Mathf.PI), 270f)));
		// test Vec2.GetUnitVectorDegrees
		Vec2 v = Vec2.GetUnitVectorDeg(180f);
		v.Normalize();
		Console.WriteLine("GetUnitVectorDegrees : " + (Approximate(v, new Vec2(-1, 0))) + " " + v.ToString());
		// test Vec2.GetUnitVectorRadiansv = Vec2.GetUnitVectorRad(1);
		v = Vec2.GetUnitVectorRad(0.5f * Mathf.PI);
		v.Normalize();
		Console.WriteLine("GetUnitVectorRadians : " + (Approximate(v, new Vec2(0, 1))) + " " + v.ToString());

		// Week 2 instance:

		// test v.GetAngleDegrees
		v = new Vec2(5, 5);
		Console.WriteLine("GetAngleDegrees : " + (Approximate(v.GetAngleDegrees(), 45f)) + " " + v.GetAngleDegrees());
		// test v.GetAngleRadians
		v = new Vec2(0, 5);
		Console.WriteLine("GetAngleRadians : " + (Approximate(v.GetAngleRadians(), 0.5f * Mathf.PI)) + " " + v.GetAngleRadians());
		// test v.SetAngleDegrees
		v = new Vec2(1, 0);
		v.SetAngleDegrees(90f);
		Console.WriteLine("SetAngleDegrees : " + (Approximate(v, new Vec2(0, 1))) + " " + v.ToString());
		// test v.SetAngleRadians
		v.SetAngleRadians(1 * Mathf.PI);
		Console.WriteLine("SetAngleRadians : " + (Approximate(v, new Vec2(-1, 0))) + " " + v.ToString());

		// test v.RotateDegrees
		v = new Vec2(10, 0);
		v.RotateDegrees(90);
		Console.WriteLine("RotateDegrees : " + Approximate(v, new Vec2(0, 10)) + " " + v.ToString());
		// test v.RotateRadians
		v = new Vec2(10, 0);
		v.RotateRadians(1f * Mathf.PI);
		Console.WriteLine("RotateRadians : " + Approximate(v, new Vec2(-10, 0)) + " " + v.ToString());
		// test v.RotateAroundDegrees
		v.SetXY(1, 0);
		v.RotateAroundDegrees(new Vec2(1, 1), 90f);
		Console.WriteLine("RotateAroundDegrees : " + Approximate(v, new Vec2(2, 1)) + " " + v.ToString());
		// test v.
		v.SetXY(1, 0);
		v.RotateAroundRadians(new Vec2(1, 1), 0.5f * Mathf.PI);
		Console.WriteLine("RotateAroundRadians : " + Approximate(v, new Vec2(2, 1)) + " " + v.ToString());

	}
	public static bool Approximate(Vec2 a, Vec2 b, float errorMargin = 0.01f)
	{
		return Approximate(a.x, b.x, errorMargin) && Approximate(a.y, b.y, errorMargin);
	}

	/// <summary>
	/// A helper method for unit testing:
	/// Returns true if and only if [a] and [b] differ by at most [errorMargin].
	/// </summary>
	public static bool Approximate(float a, float b, float errorMargin = 0.01f)
	{
		return Math.Abs(a - b) < errorMargin;
	}
}
