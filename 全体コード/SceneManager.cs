using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using static SoundManager;

public class SceneManager : MonoBehaviour
{
    public string NowScene;

    GameObject invasion; // �v���C���[�̓����蔻��I�u�W�F�N�g�̎Q�Ɨp�ϐ�
    public invasionCol inColscript;   // invasionCol�X�N���v�g�̎Q�Ɨp�ϐ�

    private GameObject Player; // �C���v�b�g�V�X�e���̃Q�[���I�u�W�F�N�g
    private PlayerInput playerInput; // �C���v�b�g�V�X�e���X�N���v�g

    // �V�[�����
    [System.Serializable]
    public class SCENE_Data
    {
        public string SCENE_name; //�V�[����
        //public string next_SCENE_name;
        public int FadeInTime;  // �t�F�[�h�C������
        public int FadeOutTime; // �t�F�[�h�A�E�g����
    }

    [SerializeField]
    private SCENE_Data[] Scene_Datas;

    // �ʖ�(SCENE_name)���L�[�Ƃ����Ǘ��pDictionary
    private Dictionary<string, SCENE_Data> Scene_Dictionary = new Dictionary<string, SCENE_Data>();

    private void Awake()
    {
        // Scene_Dictionary�ɃZ�b�g
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
        NowScene = Scene_Datas[0].SCENE_name;�@// TITLE�V�[��
    }

    // Update is called once per frame
    void Update()
    {
        //if(Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.aButton.wasPressedThisFrame || DualSenseGamepadHID.current.xButton.wasPressedThisFrame) // �L�[�{�[�h�܂���Xbox�܂���PS5�R���g���[��
        //if (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.current.aButton.wasPressedThisFrame) // Xbox�܂���PS5�R���g���[��
        if (Keyboard.current.enterKey.wasPressedThisFrame) // �L�[�{�[�h
        {
            NowScene = Scene_Datas[1].SCENE_name;
        }
        // �L�����N�^�[���Փ˂����Ƃ�
        if(inColscript.DeathFlg)
        {
            NowScene = Scene_Datas[2].SCENE_name;
        }
    }
}
