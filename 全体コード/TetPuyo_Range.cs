using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TetPuyo_Range : MonoBehaviour
{
    //動かす幅
    private float Speed = 0.5435f;

    //プレイヤーインプット
    private PlayerInput action;
    private GameObject playerinput;
    private player player_cs;

    private bool Right_Collision = false;  //右壁の当たり判定
    public bool Left_Collision = false;  //左壁の当たり判定


    public float prePosX;  //参照用ポジション

    private GameObject ui_range;
    private float UIToTePu;
    private const float rangeMax = 3.9f;
    private const float rangeMin = -4.1f;
    Vector2 pos;

    private GameObject SoundManager;
    private SoundList sound;
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー情報の取得
        playerinput = GameObject.Find("Player(Clone)");
        action = playerinput.GetComponent<PlayerInput>();
        player_cs = playerinput.GetComponent<player>();

        //ポジションの取得
        pos = this.GetComponent<Transform>().position;

        //操作範囲の取得
        ui_range = GameObject.Find("ui_playarea");

        SoundManager = GameObject.Find("SoundManagerTest");
        sound = SoundManager.GetComponent<SoundList>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが倒れたら消す
        if (player_cs.inColscript.DeathFlg)
        {
            Destroy(this);
        }
        else
        {
            UIToTePu = gameObject.transform.position.x - ui_range.gameObject.transform.position.x;//テトぷよのポジションからエリアのポジションを引いたもの

            //右端のポジションより大きい場合
            if (UIToTePu > rangeMax)
            {
                Right_Collision = true;
            }
            else
            {
                Right_Collision = false;
            }
            //エリアの左端のポジションより小さい場合
            if (UIToTePu < rangeMin)
            {
                Left_Collision = true;
            }
            else
            {
                Left_Collision = false;
            }


            //現在の場所を変数に格納する
            pos = this.transform.position;

            prePosX = pos.x;
            //移動

            //サウンド
            if (action.actions["Right"].WasPressedThisFrame() && Right_Collision == false)
            {
                sound.SEPlay(SoundList.SELIST.MOVE);
                pos.x += Speed;
            }
            if (action.actions["Left"].WasPressedThisFrame() && Left_Collision == false)
            {
                sound.SEPlay(SoundList.SELIST.MOVE);
                pos.x -= Speed;
            }
            this.transform.position = pos;
        }
    }
}
    

