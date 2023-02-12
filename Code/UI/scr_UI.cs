using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI : MonoBehaviour
{
    [SerializeField] Text lifesText;
    [SerializeField] Text gemText;
    [SerializeField] Text timeText;

    public void UpdateGemText(int gems)
    {
        gemText.text = "Gems: " + gems;
    }

    public void UpdateTimeText(int time)
    {
        timeText.text = "Time: " + time;
    }

    public void UpdateLifeText(int lifes)
    {
        lifesText.text = "Lifes: " + lifes;
    }
}
