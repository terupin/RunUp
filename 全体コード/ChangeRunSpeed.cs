using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using System.Diagnostics;
using UnityEngine;

// �v���C���[�̐ݒ���i�[����\����
[System.Serializable]
public struct PlayerSettings
{
    public float PS_runSpeed; // ���鑬��
    public float PS_stepUpForce; // �W�����v��
    public float PS_gravity;  // �d��
    public float PlasGravity; // �O���r�e�B�t���ϐ�
    public float PlasSpeed;   // �X�s�[�h�t���ϐ�
    public float PlasUpForce; // �W�����v�͕t���ϐ�
}

public class ChangeRunSpeed : MonoBehaviour
{
    GameObject Player; // �v���C���[�̃I�u�W�F�N�g�i�[�ϐ�
    player PlaInfo; // �v���C���[�X�N���v�g�̏��i�[�ϐ�

    public int NowIndex = 0;

    public float PlayerPosX; // �v���C���[��X���W
    public float PlayerPosY; // �v���C���[��Y���W

    public bool PS_CheckChange; // �v���C���|�Z�b�e�B���O�`�F���W�t���O

    public PlayerSettings[] playerSettings; // �v���C���[�̐ݒ���i�[����z��
    private int currentSettingsIndex = 0; // ���݂̐ݒ�̃C���f�b�N�X

    // ���݂̐ݒ��؂�ւ��郁�\�b�h
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
        Player = GameObject.Find("Player(Clone)"); // Player���I�u�W�F�N�g�̖��O����擾���Ċi�[
        PlaInfo = Player.GetComponent<player>(); // Player�̒���player�X�N���v�g���擾���Ċi�[
        PlayerPosX = Player.transform.position.x; // Player�̃|�W�V����X���i�[
        PlayerPosY = Player.transform.position.y; // Player�̃|�W�V����Y���i�[
        PS_CheckChange = false;

        SoundManager = GameObject.Find("SoundManagerTest");
        sound = SoundManager.GetComponent<SoundList>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosX = Player.transform.position.x; // Player�̃|�W�V����X���i�[
        PlayerPosY = Player.transform.position.y; // Player�̃|�W�V����Y���i�[

        NowIndex = currentSettingsIndex; // ���݂̃X�s�[�h�\���̔ԍ�

        if (PS_CheckChange)
        {
            PlaInfo.stepUpForce = playerSettings[currentSettingsIndex].PS_stepUpForce + playerSettings[currentSettingsIndex].PlasUpForce; // �W�����v�́{�W�����v�͕t��
        }
        else
        {
            PlaInfo.stepUpForce = playerSettings[currentSettingsIndex].PS_stepUpForce; // �W�����v��
        }

        // �d�͕t���t���O���Q�Ƃ��ė������X�s�[�h�Əd�͂̒���
        if (PlaInfo.OnGravity)
        {
            if (!PlaInfo.OneJump)
            {
                PlaInfo.rb.gravityScale = playerSettings[currentSettingsIndex].PS_gravity + playerSettings[currentSettingsIndex].PlasGravity; // �d��
                PlaInfo.RunSpeed = (playerSettings[currentSettingsIndex].PS_runSpeed + playerSettings[currentSettingsIndex].PlasSpeed) * Time.deltaTime; // �X�s�[�h
            }
        }
        else
        {
            PlaInfo.OneJump = true;

            PlaInfo.rb.gravityScale = playerSettings[currentSettingsIndex].PS_gravity;
            PlaInfo.RunSpeed = playerSettings[currentSettingsIndex].PS_runSpeed * Time.deltaTime;
        }

        //DeathFlg��true�Ȃ烉���X�s�[�h��0�ɂ���
        if (PlaInfo.inColscript.DeathFlg)
        {
            currentSettingsIndex = 0;
        }
        //---�L�����N�^�[�̂��鍂��(Y���W)�ɂ���ăX�s�[�h��ω�����---//
        else if (PlayerPosY >= -10.00f && PlayerPosY <= 1.99f) // Element0
        {
            PS_CheckChange = false;
            currentSettingsIndex = 1; // Element1(�����l)

        }
        else if (PlayerPosY >= 2.00f && PlayerPosY < 4.99f) // Element1
        {
            // �����ɐ؂�ւ��Ǝv��ʋ������N���邽�ߕύX�_���B�㏭���̊Ԃ͕ύX�O�̐ݒ�l�ɂ���
            if (PlayerPosY < 2.25f && currentSettingsIndex == 1)
            {
                PS_CheckChange = true;
            }
            else
            {
                PS_CheckChange = false;
                currentSettingsIndex = 2; // Element2(��i�KUP�����l)
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
                currentSettingsIndex = 3; // Element3(��i�KUP�����l)
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
                currentSettingsIndex = 4; // Element4(��i�KUP�����l)
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
                currentSettingsIndex = 5; // Element5(��i�KUP�����l)
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
                currentSettingsIndex = 6; // Element6(��i�KUP�����l)
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
                currentSettingsIndex = 7; // Element7(��i�KUP�����l)
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
                currentSettingsIndex = 8; // Element8(��i�KUP�����l)
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
                currentSettingsIndex = 9; // Element9(��i�KUP�����l)
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
                currentSettingsIndex = 10; // Element10(��i�KUP�����l)
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
                currentSettingsIndex = 11; // Element11(��i�KUP�����l)
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
                currentSettingsIndex = 12; // Element12(��i�KUP�����l)
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
                currentSettingsIndex = 13; // Element13(��i�KUP�����l)
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
                currentSettingsIndex = 14; // Element14(��i�KUP�����l)
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
                currentSettingsIndex = 15; // Element15(��i�KUP�����l)
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
                currentSettingsIndex = 16; // Element16(��i�KUP�����l)
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
                currentSettingsIndex = 17; // Element17(��i�KUP�����l)
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
                currentSettingsIndex = 18; // Element18(��i�KUP�����l)
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
                currentSettingsIndex = 19; // Element19(��i�KUP�����l)
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
                currentSettingsIndex = 20; // Element20(��i�KUP�����l)
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
                currentSettingsIndex = 21; // Element21(��i�KUP�����l)
            }
        }

        if (PS_CheckChange)
        {
            pitch += 0.002f;
            sound.BGMPitch(pitch);
        }
    }
}
