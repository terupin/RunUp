using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HeightUI : MonoBehaviour
{
    //UIのテクスチャ用
    private GameObject Frame;
    private GameObject Next;
    private GameObject Score;
    private SpriteRenderer UIHeight;
    private SpriteRenderer UIFrame;
    private SpriteRenderer UINext;
    private SpriteRenderer UIScore;

    //子オブジェクト
    private GameObject puyo;
    //プレイヤー
    private GameObject player;

    //各UI画像
    public Sprite[] UI_Height;
    public Sprite[] UI_Frame;
    public Sprite[] UI_Next;
    public Sprite[] UI_Score;

    //高さ種類
    public enum HEIGHTSTATE
    {
        HOUSE,
        BILL,
        MOUNTAIN,
        SKY,
        SPACE
    };
  public HEIGHTSTATE heightstate;

    //各種高さ
    private const float height1 = 12.6f;
    private const float height2 = 33.7f;
    private const float height3 = 58.1f;
    private const float height4 = 92.3f;
    private const float height5 = 203.2f;
    private const float playerbottom = -3.273734f;

    //各種変数
    private float height;
    private float playerheight;
    private float UIheight;
    private float multiply;
    // Start is called before the first frame update
    void Start()
    {
        puyo = transform.GetChild(0).gameObject;
        player = GameObject.Find("Player(Clone)");

        //テクスチャ検索
        {
            Frame = GameObject.Find("ui_playarea");
            Next = GameObject.Find("ui_hold");
            Score = GameObject.Find("score");
            UIHeight = GetComponent<SpriteRenderer>();
            UIFrame = Frame.GetComponent<SpriteRenderer>();
            UINext = Next.GetComponent<SpriteRenderer>();
            UIScore = Score.GetComponent<SpriteRenderer>();
        }

        //最初の状態を設定
        heightstate = HEIGHTSTATE.HOUSE;
    }

    // Update is called once per frame
    void Update()
    {
        //各種高さによる制限
        {
            if (player.transform.position.y <= height1)
            {
                HeightSetting(height1, playerbottom);
                //エリアの状態をセット
                heightstate = HEIGHTSTATE.HOUSE;
            }
            else if (height1 < player.transform.position.y && player.transform.position.y <= height2)
            {
                HeightSetting(height2, height1);
                //エリアの状態をセット
                heightstate = HEIGHTSTATE.BILL;
            }
            else if (height2 < player.transform.position.y && player.transform.position.y <= height3)
            {
                HeightSetting(height3, height2);
                //エリアの状態をセット
                heightstate = HEIGHTSTATE.MOUNTAIN;
            }
            else if (height3 < player.transform.position.y && player.transform.position.y <= height4)
            {
                HeightSetting(height4, height3);
                //エリアの状態をセット
                heightstate = HEIGHTSTATE.SKY;
            }
            else if (height4 < player.transform.position.y&&player.transform.position.y<=height5)
            {
                HeightSetting(height5, height4);
                //エリアの状態セット
                heightstate = HEIGHTSTATE.SPACE;
            }
        }

        //場所に応じたテクスチャセット
        switch (heightstate)
        {
            case HEIGHTSTATE.HOUSE:
                UIHeight.sprite = UI_Height[0];
                UIFrame.sprite = UI_Frame[0];
                UINext.sprite = UI_Next[0];
                UIScore.sprite = UI_Score[0];
                break;
            case HEIGHTSTATE.BILL:
                UIHeight.sprite = UI_Height[1];
                UIFrame.sprite = UI_Frame[1];
                UINext.sprite = UI_Next[1];
                UIScore.sprite = UI_Score[1];
                break;
            case HEIGHTSTATE.MOUNTAIN:
                UIHeight.sprite = UI_Height[2];
                UIFrame.sprite = UI_Frame[2];
                UINext.sprite = UI_Next[2];
                UIScore.sprite = UI_Score[2];
                break;
            case HEIGHTSTATE.SKY:
                UIHeight.sprite = UI_Height[3];
                UIFrame.sprite = UI_Frame[3];
                UINext.sprite = UI_Next[3];
                UIScore.sprite = UI_Score[3];
                break;
            case HEIGHTSTATE.SPACE:
                UIHeight.sprite = UI_Height[4];
                UIFrame.sprite = UI_Frame[4];
                UINext.sprite = UI_Next[4];
                UIScore.sprite = UI_Score[4];
                break;
        }

        //子オブジェクトを移動させる
        UIheight = multiply * (2.0f * playerheight - height);
        puyo.transform.localPosition = new Vector3(puyo.transform.localPosition.x, UIheight, 0.0f);
    }



    private void HeightSetting(float top, float bottom)
    {
        //エリアの高さ
        height = top - bottom;
        //プレイヤーのエリア最低面からの高さ
        playerheight = height - (top - player.transform.position.y);

        //倍率を計算する
        multiply = 7.6f / height;
    }
}        