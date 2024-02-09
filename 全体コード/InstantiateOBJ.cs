using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InstantiateOBJ : MonoBehaviour
{
    //�Ղ�e�g�i�[�z��
    public GameObject[] puyoteto;

    //�Ղ�e�g�����I�u�W�F�N�g
    private GameObject nowPuyoTeto;
    private TetPuyo_Range nowRange;
    private Tet_Rotate nowRotate;
    private InstantiateSkelton nowSkelton;

    //�n�[�h�h���b�v��̂Ղ�e�g
    private GameObject prePuyoTeto;

    //���ɂ�����Ղ�e�g
    private GameObject nextPuyoTeto;
    private GameObject nextUI;
    private TetPuyo_Range nextRange;
    private Tet_Rotate nextRotate;
    private InstantiateSkelton nextSkelton;

    //x���W�␳�ϐ�
    private float offsetX;
    //�����ʒu�̌��̍��W
    private Vector3 BaseLocalPosition;
    private Vector3 BasePosition;

    //�v���C���[�I�u�W�F�N�g
    private GameObject Player;
    private player player_cs;
    // Start is called before the first frame update
    void Start()
    {
        //�l�N�X�g�\���n
        nextUI = GameObject.Find("ui_hold");
        nextPuyoTeto = Instantiate(puyoteto[9], nextUI.transform.position, Quaternion.identity);
        nextPuyoTeto.gameObject.transform.parent = nextUI.gameObject.transform;
        nextRange = nextPuyoTeto.GetComponent<TetPuyo_Range>();
        nextRotate= nextPuyoTeto.GetComponent<Tet_Rotate>();
        nextSkelton = nextPuyoTeto.GetComponent<InstantiateSkelton>();
        nextRange.enabled = false;
        nextRotate.enabled = false;
        nextSkelton.enabled = false;

        //�ŏ��ɏo���Ղ�e�g
        nowPuyoTeto = Instantiate(puyoteto[11], gameObject.transform.position, Quaternion.identity);
        nowRange = nowPuyoTeto.GetComponent<TetPuyo_Range>();
        nowRotate = nowPuyoTeto.GetComponent<Tet_Rotate>();
        nowSkelton = nowPuyoTeto.GetComponent<InstantiateSkelton>();
        nowRange.enabled = true;
        nowRotate.enabled = true;
        nowSkelton.enabled = true;

        //�n�[�h�h���b�v��̃I�u�W�F�N�g�i�[
        prePuyoTeto = nowPuyoTeto;

        //�����鐶���ʒu�̍��W�擾
        BaseLocalPosition = gameObject.transform.localPosition;

        //�v���C���[�I�u�W�F�N�g����
        Player = GameObject.Find("Player(Clone)");
        player_cs = Player.GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        //���S������X�N���v�g�폜���ē����Ȃ��悤�ɂ���
        if (player_cs.inColscript.DeathFlg)
        {
            Destroy(nowRange);
            Destroy(nowRotate);
            Destroy(nowSkelton);
        }
        else
        {
            //�Ղ�e�g�����ɗ�������
            BasePosition = gameObject.transform.position;
            if (nowSkelton.hardDropFlag)
            {
                //�u������Ɋi�[
                prePuyoTeto = nowPuyoTeto;
                prePuyoTeto.gameObject.AddComponent<InvisibleDestroy>();

                //�����ʒu��␳����
                float posx = OffsetPosition();

                //���W�␳
                Vector3 offsetPos = new Vector3(posx, BasePosition.y, 0.0f);

                //�l�N�X�g�\��������������悤�ɂ���
                ChangeNow(offsetPos);

                //�l�N�X�g�\������
                InstatiateNext();

                //���������ʒu�ɖ߂�
                gameObject.transform.localPosition = BaseLocalPosition;
            }

            //���������Ă���Ղ�e�g�͐����ʒu��Y���W�̈ʒu�ɏ�u��
            Vector3 nowpos = nowPuyoTeto.transform.position;
            nowPuyoTeto.transform.position = new Vector3(nowpos.x, BasePosition.y, 0.0f);
        }
    }

    private float OffsetPosition()
    {
        offsetX = 0.5435f * 2.0f;
        //�␳�ʒu�v�Z
        float PosX = prePuyoTeto.transform.position.x + offsetX;
        if (PosX > BasePosition.x)
        {
            offsetX = BasePosition.x - prePuyoTeto.gameObject.transform.position.x;
            offsetX = Mathf.Ceil(offsetX / 0.5435f);
            offsetX = offsetX * 0.5435f;
            PosX = prePuyoTeto.transform.position.x + offsetX;
        }
        return PosX;
    }

    private void ChangeNow(Vector3 pos)
    {
        //�l�N�X�g�\���̐e�q�֌W����
        nextPuyoTeto.transform.parent = null;
        //�l�N�X�g�\���̈ʒu���ړ�������
        nextPuyoTeto.transform.position = pos;
        //���ׂĂ��I���ɂ�����Ԃō��̃I�u�W�F�N�g�Ɋi�[
        nowPuyoTeto = nextPuyoTeto;
        //�g���Ȃ��悤�ɂ��Ă����R���|�[�l���g���N��������
        nowRange = nowPuyoTeto.GetComponent<TetPuyo_Range>();
        nowRotate = nowPuyoTeto.GetComponent<Tet_Rotate>();
        nowSkelton = nowPuyoTeto.GetComponent<InstantiateSkelton>();
        nowRange.enabled = true;
        nowRotate.enabled = true;
        nowSkelton.enabled = true;
    }

    private void InstatiateNext()
    {
        //�Ղ�e�g�̔���
        int puyoORteto = Random.Range(0, 3);
        //�Ղ�e�g�̎�ޔ���
        int index;
        switch (puyoORteto)
        {
            //�Ղ�Ղ�
            case 0:
                index = Random.Range(0, 5);
                nextPuyoTeto = Instantiate(puyoteto[index], nextUI.transform.position, Quaternion.identity);
                break;
            //�e�g���X
            case 1:
            case 2:
                index = Random.Range(5, 12);
                nextPuyoTeto = Instantiate(puyoteto[index], nextUI.transform.position, Quaternion.identity);
                break;
        }
        nextPuyoTeto.gameObject.transform.parent = nextUI.gameObject.transform;
        nextRange = nextPuyoTeto.GetComponent<TetPuyo_Range>();
        nextRotate = nextPuyoTeto.GetComponent<Tet_Rotate>();
        nextSkelton = nextPuyoTeto.GetComponent<InstantiateSkelton>();
        nextRange.enabled = false;
        nextRotate.enabled = false;
        nextSkelton.enabled = false;
    }
}
