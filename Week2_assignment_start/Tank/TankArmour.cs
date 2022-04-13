using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class TankArmour
{
    public NLineSegment front;
    public NLineSegment back;
    public NLineSegment left;
    public NLineSegment right;
    
    public TankArmour(AITank tank)
    {
        front = new NLineSegment(new Vec2(500, 100), new Vec2(500,200));        
    }
    public void Update()
    {
    }
}
