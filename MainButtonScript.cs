using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainButtonScript : MonoBehaviour {

    public int ButLeftClick = 0;
    public Text Score;
    public int ButRightClick = 0;

    private void Update()
    {
        Score.text = ButLeftClick.ToString("f0") + ButRightClick.ToString("f0");

    }
    public void Click1()
    {
        ButLeftClick++;
        if (ButLeftClick == 10) // only 0-9
        {
            ButLeftClick = 0;
        }
    }
    public void Click2()
    {
        ButRightClick++;
        if (ButRightClick == 10) // only 0-9
        {
            ButRightClick = 0;
        }
    }
}
