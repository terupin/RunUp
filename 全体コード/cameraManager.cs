using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMa : MonoBehaviour
{
    public GameObject target; // �Ǐ]����Ώۂ����߂�ϐ�
    public Vector3 pos;  // �J�����̏����ʒu���L�����邽�߂̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        pos = Camera.main.gameObject.transform.position; // �J�����̏����ʒu��ϐ�pos�ɓ����
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = target.transform.position; // cameraPos�Ƃ����ϐ������A�Ǐ]����Ώۂ̈ʒu������
        cameraPos.x = target.transform.position.x + 5;  // �J�����̉��ʒu�ɃL�����N�^�[�̈ʒu+5������
        
        // �����Ώۂ̏c�ʒu���O��菬�����ꍇ
        if(target.transform.position.y < 0)
        {
            cameraPos.y = target.transform.position.y;  // �J�����̏c�ʒu��0�ɓ����
        }
        // �����Ώۂ̏c�ʒu��0���傫���ꍇ
        if(target.transform.position.y > 0)
        {
            cameraPos.y = target.transform.position.y; // �J�����̏c�ʒu�ɑΏۂ̈ʒu������
        }

        cameraPos.z = -10; // �J�����̉��s���̈ʒu��-10������
        Camera.main.gameObject.transform.position = cameraPos; // �J�����̈ʒu�ɕϐ�cameraPos�̈ʒu������
    }
}
