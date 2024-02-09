using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resalt_Color : MonoBehaviour
{
    public Sprite[] sprite;  
    public SpriteRenderer sr;

    private GameObject UI;  
    private HeightUI Color;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>(); 
        UI = GameObject.Find("ui_high");  //高さのUIを探してくる
        Color = UI.GetComponent<HeightUI>();  //高さのUIが記録している物を取ってくる
    }

    // Update is called once per frame
    void Update()
    {   
        //高さに合わせて背景の色を決定する
        switch (Color.heightstate)
        {
            case HeightUI.HEIGHTSTATE.HOUSE:
                sr.sprite = sprite[0];
                break;

            case HeightUI.HEIGHTSTATE.BILL:
                sr.sprite = sprite[1];
                break;

            case HeightUI.HEIGHTSTATE.MOUNTAIN:
                sr.sprite = sprite[2];
                break;

            case HeightUI.HEIGHTSTATE.SKY:
                sr.sprite = sprite[3];
                break;

            case HeightUI.HEIGHTSTATE.SPACE:
                sr.sprite = sprite[4];
                break;
        }

    }
}