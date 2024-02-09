using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundTouch : MonoBehaviour
{
    public bool OnGround; // �n�ʂɐG��Ă��邩�̔���
    public float NewGroundY; // �V�����n�_�̍��W�i�[�ϐ�
    public float OldGroundY; // �V�����n�_�̍��W���痣�ꂽ���ɂ��̒l���i�[����ϐ�

    // Start is called before the first frame update
    void Start()
    {
        OnGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player 2")
        {
            OnGround = false;
            OldGroundY = NewGroundY;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name != "Player 2")
        {
            OnGround = true;
            NewGroundY = collision.gameObject.transform.position.y;
        }
    }
}
