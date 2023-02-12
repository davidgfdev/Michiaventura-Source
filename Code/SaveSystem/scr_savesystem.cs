using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_savesystem : MonoBehaviour
{
    public int[] StartNewGame()
    {
        int[] _levels = new int[13];
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i] = 0;
        }
        _levels[0] = 1;
        SaveGame(_levels);
        return _levels;
    }
    public void SaveGame(int[] levels)
    {
        savesystem_core.SaveData(levels);
    }

    public int[] LoadGame()
    {
        //Cargamos el archivo
        c_gameData data = savesystem_core.LoadPlayer();
        return data.levels;
    }
}
