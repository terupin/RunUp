using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using System.Diagnostics;
using UnityEngine;

// プレイヤーの設定を格納する構造体
[System.Serializable]
public struct PlayerSettings
{
    public float PS_runSpeed; // 走る速さ
    public float PS_stepUpForce; // ジャンプ力
    public float PS_gravity;  // 重力
    public float PlasGravity; // グラビティ付加変数
    public float PlasSpeed;   // スピード付加変数
    public float PlasUpForce; // ジャンプ力付加変数
}

public class ChangeRunSpeed : MonoBehaviour
{
    GameObject Player; // プレイヤーのオブジェクト格納変数
    player PlaInfo; // プレイヤースクリプトの情報格納変数

    public int NowIndex = 0;

    public float PlayerPosX; // プレイヤーのX座標
    public float PlayerPosY; // プレイヤーのY座標

    public bool PS_CheckChange; // プレイヤ－セッティングチェンジフラグ

    public PlayerSettings[] playerSettings; // プレイヤーの設定を格納する配列
    private int currentSettingsIndex = 0; // 現在の設定のインデックス

    // 現在の設定を切り替えるメソッド
    public void SwitchSettings(int newIndex)
    {
        if (newIndex >= 0 && newIndex < playerSettings.Length)
        {
            currentSettingsIndex = newIndex;
        }
    }


    private GameObject SoundManager;
    private SoundList sound;
    float pitch=1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player(Clone)"); // Playerをオブジェクトの名前から取得して格納
        PlaInfo = Player.GetComponent<player>(); // Playerの中のplayerスクリプトを取得して格納
        PlayerPosX = Player.transform.position.x; // PlayerのポジションXを格納
        PlayerPosY = Player.transform.position.y; // PlayerのポジションYを格納
        PS_CheckChange = false;

        SoundManager = GameObject.Find("SoundManagerTest");
        sound = SoundManager.GetComponent<SoundList>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosX = Player.transform.position.x; // PlayerのポジションXを格納
        PlayerPosY = Player.transform.position.y; // PlayerのポジションYを格納

        NowIndex = currentSettingsIndex; // 現在のスピード構造体番号

        if (PS_CheckChange)
        {
            PlaInfo.stepUpForce = playerSettings[currentSettingsIndex].PS_stepUpForce + playerSettings[currentSettingsIndex].PlasUpForce; // ジャンプ力＋ジャンプ力付加
        }
        else
        {
            PlaInfo.stepUpForce = playerSettings[currentSettingsIndex].PS_stepUpForce; // ジャンプ力
        }

        // 重力付加フラグを参照して落下時スピードと重力の調整
        if (PlaInfo.OnGravity)
        {
            if (!PlaInfo.OneJump)
            {
                PlaInfo.rb.gravityScale = playerSettings[currentSettingsIndex].PS_gravity + playerSettings[currentSettingsIndex].PlasGravity; // 重力
                PlaInfo.RunSpeed = (playerSettings[currentSettingsIndex].PS_runSpeed + playerSettings[currentSettingsIndex].PlasSpeed) * Time.deltaTime; // スピード
            }
        }
        else
        {
            PlaInfo.OneJump = true;

            PlaInfo.rb.gravityScale = playerSettings[currentSettingsIndex].PS_gravity;
            PlaInfo.RunSpeed = playerSettings[currentSettingsIndex].PS_runSpeed * Time.deltaTime;
        }

        //DeathFlgがtrueならランスピードを0にする
        if (PlaInfo.inColscript.DeathFlg)
        {
            currentSettingsIndex = 0;
        }
        //---キャラクターのいる高さ(Y座標)によってスピードを変化する---//
        else if (PlayerPosY >= -10.00f && PlayerPosY <= 1.99f) // Element0
        {
            PS_CheckChange = false;
            currentSettingsIndex = 1; // Element1(初期値)

        }
        else if (PlayerPosY >= 2.00f && PlayerPosY < 4.99f) // Element1
        {
            // すぐに切り替わると思わぬ挙動が起きるため変更点到達後少しの間は変更前の設定値にする
            if (PlayerPosY < 2.25f && currentSettingsIndex == 1)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 2; // Element2(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 5.00f && PlayerPosY < 19.99f) // Element2
        {
            if (PlayerPosY < 5.25f && currentSettingsIndex == 2)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 3; // Element3(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 20.00f && PlayerPosY < 29.99f) // Element3
        {
            if (PlayerPosY < 20.25f && currentSettingsIndex == 3)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 4; // Element4(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 30.00f && PlayerPosY < 34.99f) // Element4
        {
            if (PlayerPosY < 30.25f && currentSettingsIndex == 4)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 5; // Element5(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 35.00f && PlayerPosY < 39.99f) // Element5
        {
            if (PlayerPosY < 35.25f && currentSettingsIndex == 5)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 6; // Element6(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 40.00f && PlayerPosY < 44.99f) // Element6
        {
            if (PlayerPosY < 40.25f && currentSettingsIndex == 6)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 7; // Element7(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 45.00f && PlayerPosY < 49.99f) // Element7
        {
            if (PlayerPosY < 45.25f && currentSettingsIndex == 7)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 8; // Element8(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 50.00f && PlayerPosY < 54.99f) // Element8
        {
            if (PlayerPosY < 50.25f && currentSettingsIndex == 8)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 9; // Element9(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 55.00f && PlayerPosY < 59.99f) // Element9
        {
            if (PlayerPosY < 55.25f && currentSettingsIndex == 9)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 10; // Element10(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 60.00f && PlayerPosY < 64.99f) // Element10
        {
            if (PlayerPosY < 60.25f && currentSettingsIndex == 10)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 11; // Element11(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 65.00f && PlayerPosY < 69.99f) // Element11
        {
            if (PlayerPosY < 65.25f && currentSettingsIndex == 11)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 12; // Element12(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 70.00f && PlayerPosY < 74.99f) // Element12
        {
            if (PlayerPosY < 70.25f && currentSettingsIndex == 12)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 13; // Element13(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 75.00f && PlayerPosY < 79.99f) // Element13
        {
            if (PlayerPosY < 75.25f && currentSettingsIndex == 13)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 14; // Element14(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 80.00f && PlayerPosY < 84.99f) // Element14
        {
            if (PlayerPosY < 80.25f && currentSettingsIndex == 14)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 15; // Element15(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 85.00f && PlayerPosY < 89.99f) // Element15
        {
            if (PlayerPosY < 85.25f && currentSettingsIndex == 15)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 16; // Element16(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 90.00f && PlayerPosY < 94.99f) // Element16
        {
            if (PlayerPosY < 90.25f && currentSettingsIndex == 16)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 17; // Element17(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 95.00f && PlayerPosY < 99.99f) // Element17
        {
            if (PlayerPosY < 95.25f && currentSettingsIndex == 17)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 18; // Element18(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 100.00f && PlayerPosY < 109.99f) // Element18
        {
            if (PlayerPosY < 100.25f && currentSettingsIndex == 18)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 19; // Element19(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 110.00f && PlayerPosY < 119.99f) // Element19
        {
            if (PlayerPosY < 110.25f && currentSettingsIndex == 19)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 20; // Element20(一段階UPした値)
            }
        }
        else if (PlayerPosY >= 120.00f && PlayerPosY < 129.99f) // Element20
        {
            if (PlayerPosY < 120.25f && currentSettingsIndex == 20)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 21; // Element21(一段階UPした値)
            }
        }

        if (PS_CheckChange)
        {
            pitch += 0.002f;
            sound.BGMPitch(pitch);
        }
    }
}
