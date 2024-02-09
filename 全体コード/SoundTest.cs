using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class SoundTest : MonoBehaviour
{
    [SerializeField]
    public SoundManager soundManager; // サウンドマネージャー
    private GameObject MainCamera; // メインカメラ
    private SceneManager sceceManagerCS; // メインカメラスクリプト
    private GameObject Player; // プレイヤーオブジェクト
    private player PlayerCS; // プレイヤースクリプト
    private GameObject Input; // インプットシステムのゲームオブジェクト
    private PlayerInput playerInput; // インプットシステムスクリプト
    private GameObject instOBJ;
    private InstantiateOBJ instOBJ_CS;

    private bool SkySound; // 空中サウンド再生判定
    private bool DeathSound; // 死亡サウンド再生判定
    public int DeathCount; // 衝突後の時間
    public int SetDeathCount; // 衝突設定時間

    public static bool HardDropSEflag = false;  // staticなハードドロップフラグ

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        sceceManagerCS = MainCamera.GetComponent<SceneManager>();
        Player = GameObject.Find("Player(Clone)");
        PlayerCS = Player.GetComponent<player>();
        //Input = GameObject.Find("Input");
        playerInput = Player.GetComponent<PlayerInput>();
        instOBJ = GameObject.Find("InstantiateOBJ");
        instOBJ_CS = instOBJ.GetComponent<InstantiateOBJ>();

        SkySound = false;
        DeathSound = false;
        DeathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //---BGM---//
        if(sceceManagerCS.NowScene == "SCENE-TITLE")
        {
            soundManager.BGM_Play("TITLE");
        }
        if (sceceManagerCS.NowScene == "SCENE-GAME")
        {
            soundManager.BGM_Play("RunGame");
        }
        else if(sceceManagerCS.NowScene == "SCENE-RESULT")
        {
            DeathCount++;
            if(DeathCount == SetDeathCount)
            {
                soundManager.BGM_Play("RESULT");
            }
        }


        //---プレイヤーSE---//
        if (PlayerCS.inColscript.Actjump) // ジャンプ可能フラグがtrueの時
        {
            soundManager.SE_Play("ジャンプ"); // ジャンプ音再生
        }

        if (PlayerCS.inColscript.DeathFlg && !DeathSound) // ゲームオーバーフラグがtrueの時
        {
            if (PlayerCS.NowSky && !SkySound)
            {
                //soundManager.SE_Play("落下");
                soundManager.SE_PlayWithFadeOut("落下", 1.5f);
                SkySound = true;
                //DeathSound = true;
            }
            else
            {
                soundManager.SE_Play("衝突"); // サウンドマネージャーを使用して効果音再生
            }
            DeathSound = true;
        }


        //---ぷよテトSE---//
        // ぷよテト操作
        if (playerInput.actions["Right"].WasPressedThisFrame() || playerInput.actions["Left"].WasPressedThisFrame())
        {
            soundManager.SE_Play2("ぷよテト操作");
        }

        // ハードドロップ時のSE
        if (playerInput.actions["HardDrop"].WasPressedThisFrame() || HardDropSEflag==true)
        {
            soundManager.SE_Play2("ハードドロップ");
            HardDropSEflag = false;
        }

        //---その他SE---//
        //if (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.aButton.wasPressedThisFrame || DualSenseGamepadHID.current.xButton.wasPressedThisFrame) // キーボードまたはXboxまたはPS5コントローラ
        //if (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.aButton.wasPressedThisFrame) // XboxまたはPS5コントローラ
        // ゲームスタート
        if (Keyboard.current.enterKey.wasPressedThisFrame) // エンターキー
        {
            soundManager.SE_Play("決定"); // サウンドマネージャーを使用して効果音再生
        }
        // 


    }
}
