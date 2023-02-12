using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_medkits : MonoBehaviour
{
    [SerializeField] int units;

    int maxUnits = 3;

    public void Refill()
    {
        units = maxUnits;
    }

    public void AddUnit()
    {
        units++;
        if (units > maxUnits)
        {
            units = maxUnits;
        }
    }

    public void SubstractUnit()
    {
        units--;
        if (units < 0)
        {
            units = 0;
        }
    }

    public void SetUnits(int unit)
    {
        units = unit;
    }

    public int getUnits()
    {
        return units;
    }
}
