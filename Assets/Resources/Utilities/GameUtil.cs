using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil
{
    public static float GetAngle(float hor, float ver)
    {
        float angle = 0;
        if (ver == 0)
        {
            if (hor > 0)
            {
                angle = -90;
            }
            else
            {
                angle = 90;
            }
        }
        else
        {
            angle = Mathf.Atan(Mathf.Abs(hor) / Mathf.Abs(ver)) * 180 / Mathf.PI;
            if (hor >= 0 && ver >= 0)
            {
                angle = -angle;
            }
            else if (hor >= 0 && ver <= 0)
            {
                angle = -180 + angle;
            }
            else if (hor <= 0 && ver <= 0)
            {
                angle = -180 - angle;
            }
            else
            {

            }
        }

        return angle;
    }



}
