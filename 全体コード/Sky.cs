using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    private float Length;  //�I�u�W�F�N�g�摜�̑傫��
    private float Start_Y; //�ŏ��̃I�u�W�F�N�g�摜��Y���W
    private float Start_Under_Y;  //�ŏ��̃I�u�W�F�N�g�̉E����Y���W
    private float Up_Percent = 2.7f;  //��̏グ��
    private float Half_Length;  //�w�i�摜�̃T�C�Y�̂Q���̈�

    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Start_Y = this.transform.position.y; //�w�i�摜�̍ŏ���Y���W
        Length = this.GetComponent<SpriteRenderer>().bounds.size.y;  //���Y���̉摜�T�C�Y
        Half_Length = Length / 2.0f;  //�摜�T�C�Y�̓񕪂̈�̑傫��
        Start_Under_Y = Start_Y - Half_Length;  //�w�i�摜�̉E����Y���W
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = this.transform.position;  //�v���C���[�̃|�W�V����
        Vector3 CameraRetMix = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));  //�������_��Y���W�̎擾
        Vector3 CameraRetMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));  //���㒸�_��Y���W�̎擾
        Vector3 CameraPos = Camera.main.transform.position; //�J�����̃|�W�V����

        //�J�����̉���Y���W����̉���Y���W���傫���ꍇ
        if (Start_Under_Y < CameraRetMix.y)
        {
            float Camera_Up = CameraRetMix.y - Start_Under_Y;  //�J�����̏オ�蕝
            float Sky_Up = (Pos.y - Half_Length) - Start_Under_Y; //��̏オ�蕝

            Sky_Up = (Camera_Up - 1) / Up_Percent;            //��̏オ�蕝�̒���

            Pos.y = Start_Y + Sky_Up;
        }

        //��̈�ԏ�ɒ�������摜�����̂܂܂��Ă���
        if (CameraRetMax.y > Pos.y + Half_Length)
        {
            Pos.y = CameraRetMax.y - Half_Length;
        }


        this.transform.position = Pos;
    }
}