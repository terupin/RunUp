using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceScore : MonoBehaviour
{
    private GameObject player;
    private TextMeshProUGUI scoretext;

    private Vector2 playerInitPos;

    //���U���g�Ɉ����p���ϐ�
    public Vector2 score;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        scoretext = GetComponent<TextMeshProUGUI>();
        playerInitPos.x=player.transform.position.x;
        playerInitPos.y=player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //�X�R�A�ۑ�
        score.x = (player.transform.position.x-playerInitPos.x) * 20.0f;
        score.y = (player.transform.position.y-playerInitPos.y) * 50.0f;

        //�����_�ȉ��؂�̂�
        score.x = Mathf.Ceil(score.x);
        score.y = Mathf.Ceil(score.y);
        //�e�L�X�g�ɑ��
        scoretext.text = score.x.ToString();
    }
}
