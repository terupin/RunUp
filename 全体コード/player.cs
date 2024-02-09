using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using static Unity.VisualScripting.Member;

public class player : MonoBehaviour
{
    public float RunSpeed; // 走る速さ
    public float jumpcount; // 連続でジャンプしてしまったかどうかの判定変数
    public float stepUpForce; // 段差を上昇するための力(ジャンプ力)
    public  bool OnGravity; // グラビティ発動フラグ
    public float DeathPosY; // 死亡判定高さ
    public bool OneJump; // 一度ジャンプした判定
    public bool NowSky; // 空中判定

    GameObject UI;   // UI
    GameObject HighICON;

    GameObject GroundTouch; // プレイヤーの股下コライダー
    groundTouch GroundTouchFlg;

    GameObject invasion; // プレイヤーの当たり判定オブジェクトの参照用変数
    public invasionCol inColscript;   // invasionColスクリプトの参照用変数

    private Animator anim; // アニメーター
    public Rigidbody2D rb;
    public BoxCollider2D boxColl; // ボックスコライダーへの参照

    public bool Finish_DeathAnim;  //死亡アニメーションが終わったか

    private GameObject SoundManager;
    private SoundList sound;
    private int hitflag = 0;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UI");
        HighICON = UI.transform.GetChild(2).gameObject;

        invasion = GameObject.Find("invasion"); // invasionをオブジェクトの名前から取得
        inColscript = invasion.GetComponent<invasionCol>(); // invasionの中にあるinvasionColスクリプトを取得して変数に格納
        anim = GetComponent<Animator>();

        GroundTouch = GameObject.Find("groundTouch"); // プレイヤーの股下コライダー
        GroundTouchFlg = GroundTouch.GetComponent<groundTouch>();

        rb = GetComponent<Rigidbody2D>();
        jumpcount = 0;

        anim = GetComponent<Animator>(); // アニメーターの取得

        OnGravity = false;
        OneJump = false;

        NowSky = false;

        Finish_DeathAnim = false;

        SoundManager = GameObject.Find("SoundManagerTest");
        sound = SoundManager.GetComponent<SoundList>();
    }

    // Update is called once per frame
    void Update()
    {
        // 水平方向の移動
        Vector2 autoMoveDirection = Vector2.right; // 右に移動
        transform.Translate(autoMoveDirection *  RunSpeed);

        // ジャンプ可能フラグがtrueの時かつゲームオーバーフラグがfalseの時にジャンプする
        if (inColscript.Actjump && !inColscript.DeathFlg)
        {
            sound.SEPlay2(SoundList.SELIST.JUMP);

            float jumpForce = CalculateJumpForce(stepUpForce);
            OneJump = true;
            rb.AddForce(Vector2.up * stepUpForce, ForceMode2D.Impulse);
            inColscript.Actjump = false;  // Actjumpフラグをfalseにする
            inColscript.PUYOTETOcount = 0;  // ぷよテトに当たったカウントを0にする
        }
        else if (inColscript.DeathFlg)
        {
            if (hitflag == 0)
            {
                if (NowSky == false)
                    sound.SEPlay2(SoundList.SELIST.WALL);
                else
                    sound.SEPlay2(SoundList.SELIST.FALL);
            }
            hitflag++;

            if(NowSky)
            {
                Debug.Log("ok");
                sound.SE_Fall_volume();
            }

            anim.SetTrigger("Death"); // デスアニメーション
            // 子オブジェクト解除
            UI.transform.parent = null;
            Destroy(HighICON.GetComponent<HeightUI>());
        }

        // ジャンプに必要な力を計算するメソッド
        float CalculateJumpForce(float jumpHeight)
        {
            // ジャンプ高さに基づいて必要なジャンプ力を計算
            // 高さ H に到達するために必要なジャンプ力は、2 * g * H で計算できます (g は重力の値)
            float gravity = Mathf.Abs(Physics2D.gravity.y);
            stepUpForce = 2.0f * gravity * jumpHeight;

            return stepUpForce;
        }

        // 高所から落下した際の次位置ブロックジャンプを行うための処理
        if (inColscript.NowJamp)
        {
            OnGravity = true; // グラビティ付加フラグ
        }
        else
        {
            OnGravity = false;
        }

        // groundTouchのNewGroundY < OldGroundYを比べる
        if(GroundTouch.transform.position.y < GroundTouchFlg.OldGroundY)
        {
            OneJump = false;
        }

        // 一定の高さから落ちた時の死亡フラグ発生
        if (GroundTouch.transform.position.y + DeathPosY < GroundTouchFlg.OldGroundY)
        {
            inColscript.DeathFlg = true;
            NowSky = true;
        }
    }
    public void OnAnimationEnd()
    {
        sound.BGMPlay(SoundList.BGMLIST.RESULT);
        Finish_DeathAnim = true;
    }
}
