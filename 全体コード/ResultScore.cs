using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScore : MonoBehaviour
{
    private GameObject distance;
    private DistanceScore score;

    private GameObject Player;

    private GameObject Height;
    private GameObject Distance;
    private GameObject TotalScore;

    private TextMeshProUGUI HUI;
    private TextMeshProUGUI DUI;
    private TextMeshProUGUI TUI;

    private player player_cs;

    private float HeightScore;
    private float DistanceScore;

    //âΩèÊ
    private float Multiply=0;
    //äÑÇËéZÇÃíl
    private float split = 1.0f;
    //åÖêî
    private float num;
    // Start is called before the first frame update
    void Start()
    {
        distance = GameObject.Find("distance");
        score=distance.GetComponent<DistanceScore>();

        Player = GameObject.Find("Player(Clone)");
        player_cs = Player.GetComponent<player>();

        Height = GameObject.Find("Tall");
        Distance = GameObject.Find("Meter_text");
        TotalScore = GameObject.Find("Score_text");

        HUI=Height.GetComponent<TextMeshProUGUI>();
        DUI=Distance.GetComponent<TextMeshProUGUI>();
        TUI=TotalScore.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player_cs.inColscript.DeathFlg)
        {
            DistanceScore = score.score.x;
            HeightScore=score.score.y;
        }
        else
        {

            while(split!=0)
            {
                num = 1 * Mathf.Pow(10, Multiply);
                split = HeightScore / num;
                split=Mathf.Floor(split);
                Multiply++;
            }


            HUI.text = HeightScore.ToString();
            DUI.text = DistanceScore.ToString();
            double totalscore = HeightScore + DistanceScore*num*2.0f ;
            TUI.text = totalscore.ToString();
        }
    }
}
