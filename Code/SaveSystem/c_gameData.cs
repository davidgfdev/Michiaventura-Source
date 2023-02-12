using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class c_gameData
{
    public int[] levels;

    public c_gameData(int[] levels)
    {
        this.levels = levels;
    }
}
