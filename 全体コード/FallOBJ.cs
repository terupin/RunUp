using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class FallOBJ : MonoBehaviour
{
    //一番下の段のY座標
    private float BasePosition;
    //動く1マス分
    private Vector3 fallPosition;
    //状態
    private enum STATE
    {
        DOWN,
        UP,
        NONE,
    }
    STATE state;

    //プレイヤーオブジェクト
    private GameObject player;
    //オブジェクトを落とす座標の補正
    public float offsetY;

    private Tet_Rotate tetRot_cs;
    private TetPuyo_Range tetRange_cs;

    //最高地点
    public static  float highest;
    public static float nowposY;
    private GameObject fallarea;

    private float offsetHigh;

    private string objectname;
    // Start is called before the first frame update
    void Start()
    {
        fallPosition = new Vector3(0.0f, 0.5435f, 0.0f);
        player = GameObject.Find("Player(Clone)");
        tetRot_cs =GetComponent<Tet_Rotate>();
        tetRange_cs=GetComponent <TetPuyo_Range>();

        BasePosition = -3.507f;
        
        //初期はベースポジションに配置
        if (nowposY == 0)
        {
            highest = BasePosition;
        }

        //オブジェクトの名前取得
        objectname = gameObject.name;

        fallarea = GameObject.Find("highestarea");
    }

    // Update is called once per frame
    void Update()
    {
        //テトリスが空中にある時
        switch (state)
        {
            //一段下げる
            case STATE.DOWN:
                gameObject.transform.position -= fallPosition;
                break;
            //一段上げる
            case STATE.UP:
                gameObject.transform.position += fallPosition;
                state = STATE.NONE;
                break;
        }

        //横に動くか回転したらもう一度落下予測位置を計算する
        if (gameObject.transform.position.x != tetRange_cs.prePosX || tetRot_cs.angle != tetRot_cs.preangle)
        {
            state = STATE.DOWN;
        }

        //ぷよぷよとテトリスが回転したときに一番下に落ちなくなるのを改善する
        SetOffsetHeight();

        //最高到達点を更新する
        if (nowposY != 0)   //はじめは通らないようにする
        {
            //現在のポジションが最高地点を超えていたら
            if (highest < nowposY)
            {
                highest = nowposY;
            }
            fallarea.transform.position = new Vector3(fallarea.transform.position.x, highest - 1.087f, 0.0f);
        }

        //一番高い位置により下にいかないようにする
        if (gameObject.transform.position.y <= highest + offsetHigh)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, highest + offsetHigh, 0.0f);
        }

        //一番下のラインにいるなら
        if (gameObject.transform.position.y <= BasePosition + offsetY + offsetHigh)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, BasePosition + offsetY + offsetHigh, 0.0f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //１マスあげる
        if (collision.gameObject.tag == "TETRIS" || collision.gameObject.tag == "Puyo")
        {
            state = STATE.UP;
        }
    }

    //ぷよテトの角度によっての一番下のラインを修正
    void SetOffsetHeight()
    {
        //最低地点の修正
        offsetY = Mathf.Ceil((player.transform.position.y - BasePosition) / 0.5435f);
        offsetY = (offsetY - 1) * 0.5435f;

        if (gameObject.tag == "Puyo")
        {
            if (tetRot_cs.angle == Tet_Rotate.ROTATE_ANGLE.LEFT)
            {
                offsetHigh = -0.5435f;
            }
            else if (tetRot_cs.angle == Tet_Rotate.ROTATE_ANGLE.DOWN)
            {
                offsetHigh = -1.087f;
            }
            else
            {
                offsetHigh = 0.0f;
            }
        }

        if (gameObject.tag == "TETRIS")
        {
            if (objectname == "Imino_s(Clone)")
            {
                if (tetRot_cs.angle == Tet_Rotate.ROTATE_ANGLE.DOWN)
                {
                    offsetHigh = -1.087f;
                }
                else if(tetRot_cs.angle==Tet_Rotate.ROTATE_ANGLE.RIGHT)
                {
                    offsetHigh = 0.5435f;
                }
                else { offsetHigh = 0.0f; }
            }
            else if (objectname == "Omino_s(Clone)")
            {
                if (tetRot_cs.angle == Tet_Rotate.ROTATE_ANGLE.DOWN)
                {
                    offsetHigh = -0.5435f;
                }
                else if (tetRot_cs.angle == Tet_Rotate.ROTATE_ANGLE.LEFT)
                {
                    offsetHigh = -0.5435f;
                }
                else { offsetHigh = 0.0f; }
            }
            else if (objectname == "Tmino_s(Clone)" ||
                objectname == "Smino_s(Clone)" ||
                objectname == "Zmino_s(Clone)" ||
                objectname == "Jmino_s(Clone)" ||
                objectname == "Lmino_s(Clone)")
            {
                if (tetRot_cs.angle == Tet_Rotate.ROTATE_ANGLE.DOWN)
                {
                    offsetHigh = -0.5435f;
                }
                else
                {
                    offsetHigh = 0.0f;
                }
            }
        }
    }

}
