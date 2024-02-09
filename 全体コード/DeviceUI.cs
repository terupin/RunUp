using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class DeviceUI : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g
    private GameObject Hard;
    private GameObject Move;
    private GameObject Rotate;

    //�e�I�u�W�F�N�g�X�v���C�g�����_���[
    private SpriteRenderer Hsprite;
    private SpriteRenderer Msprite;
    private SpriteRenderer Rsprite;

    //�e��X�v���C�g�i�[�ϐ�
    public Sprite[] sprite;

    // Start is called before the first frame update
    void Start()
    {
        Hard = GameObject.Find("hard_con");
        Move = GameObject.Find("move_con");
        Rotate = GameObject.Find("rotate_con");

        Hsprite=Hard.GetComponent<SpriteRenderer>();
        Msprite=Move.GetComponent<SpriteRenderer>();
        Rsprite=Rotate.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //�f�o�C�X�ɉ�����UI�ύX
        if (Gamepad.current == null)
        {
            Hsprite.sprite = sprite[1];
            Msprite.sprite = sprite[3];
            Rsprite.sprite = sprite[6];
        }
        else
        {
            if (Gamepad.current.ToString() == "XInputControllerWindows:/XInputControllerWindows")
            {
                Hsprite.sprite = sprite[0];
                Msprite.sprite = sprite[2];
                Rsprite.sprite = sprite[5];
            }
            else if (Gamepad.current.ToString() == "DualSenseGamepadHID:/DualSenseGamepadHID")
            {
                Hsprite.sprite = sprite[0];
                Msprite.sprite = sprite[2];
                Rsprite.sprite = sprite[4];
            }
        }
    }
}
