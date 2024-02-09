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
        UI = GameObject.Find("ui_high");  //‚‚³‚ÌUI‚ğ’T‚µ‚Ä‚­‚é
        Color = UI.GetComponent<HeightUI>();  //‚‚³‚ÌUI‚ª‹L˜^‚µ‚Ä‚¢‚é•¨‚ğæ‚Á‚Ä‚­‚é
    }

    // Update is called once per frame
    void Update()
    {   
        //‚‚³‚É‡‚í‚¹‚Ä”wŒi‚ÌF‚ğŒˆ’è‚·‚é
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