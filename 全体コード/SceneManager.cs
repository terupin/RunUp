using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using static SoundManager;

public class SceneManager : MonoBehaviour
{
    public string NowScene;

    GameObject invasion; // プレイヤーの当たり判定オブジェクトの参照用変数
    public invasionCol inColscript;   // invasionColスクリプトの参照用変数

    private GameObject Player; // インプットシステムのゲームオブジェクト
    private PlayerInput playerInput; // インプットシステムスクリプト

    // シーン情報
    [System.Serializable]
    public class SCENE_Data
    {
        public string SCENE_name; //シーン名
        //public string next_SCENE_name;
        public int FadeInTime;  // フェードイン時間
        public int FadeOutTime; // フェードアウト時間
    }

    [SerializeField]
    private SCENE_Data[] Scene_Datas;

    // 別名(SCENE_name)をキーとした管理用Dictionary
    private Dictionary<string, SCENE_Data> Scene_Dictionary = new Dictionary<string, SCENE_Data>();

    private void Awake()
    {
        // Scene_Dictionaryにセット
        foreach (var sceneData in Scene_Datas)
        {
            Scene_Dictionary.Add(sceneData.SCENE_name, sceneData);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        invasion = GameObject.Find("invasion");
        inColscript = invasion.GetComponent<invasionCol>();
        Player = GameObject.Find("Player(Clone)");
        playerInput = Player.GetComponent<PlayerInput>();
        NowScene = Scene_Datas[0].SCENE_name;　// TITLEシーン
    }

    // Update is called once per frame
    void Update()
    {
        //if(Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.aButton.wasPressedThisFrame || DualSenseGamepadHID.current.xButton.wasPressedThisFrame) // キーボードまたはXboxまたはPS5コントローラ
        //if (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.aButton.wasPressedThisFrame) // XboxまたはPS5コントローラ
        if (Keyboard.current.enterKey.wasPressedThisFrame) // キーボード
        {
            NowScene = Scene_Datas[1].SCENE_name;
        }
        // キャラクターが衝突したとき
        if(inColscript.DeathFlg)
        {
            NowScene = Scene_Datas[2].SCENE_name;
        }
    }
}
