using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTime : MonoBehaviour {

    public Image Pencil;
    private float StartTime;
    public string time;
    private float Ratio;
    public GameObject Spr;//GameObject where are scripts
    public string NextScene;
    private float kk = 0;
    private float xx = 0;
    private int Ending;
    public AudioSource Sound2;

    void Start()
    {
        StartTime = Time.time;
    }

    void Update()
    {
        float t = Time.time - StartTime + xx;
        Ratio = t / 120;
        Pencil.rectTransform.localScale = new Vector3(1 - Ratio, 1, 1);// making animation looks like shortening the pencil
        Ending = (int)t;
        if (Ending >= 115)
        {
            PlayerPrefs.SetInt("Level", Spr.GetComponent<MainGameSc>().MainPoints);//saving points on ending level time
            SceneManager.LoadScene(NextScene);
        }
        else if (Ending == 110)
        {
            Sound2.Play(); //play sound 5 sec before level ending.
        }

    }
    public void Next()
    {
        kk = Spr.GetComponent<MainGameSc>().ShortValue;// The amount of short pencil for skipping a task
        xx = xx + kk;
    }
}
