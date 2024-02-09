using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceScore : MonoBehaviour
{
    private GameObject player;
    private TextMeshProUGUI scoretext;

    private Vector2 playerInitPos;

    //リザルトに引き継ぐ変数
    public Vector2 score;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        scoretext = GetComponent<TextMeshProUGUI>();
        playerInitPos.x=player.transform.position.x;
        playerInitPos.y=player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //スコア保存
        score.x = (player.transform.position.x-playerInitPos.x) * 20.0f;
        score.y = (player.transform.position.y-playerInitPos.y) * 50.0f;

        //小数点以下切り捨て
        score.x = Mathf.Ceil(score.x);
        score.y = Mathf.Ceil(score.y);
        //テキストに代入
        scoretext.text = score.x.ToString();
    }
}
