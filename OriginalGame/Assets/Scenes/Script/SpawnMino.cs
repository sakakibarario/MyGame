using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMino : MonoBehaviour
{
    public GameObject[] Minos;

    //�N���[���I�u�W�F�N�g�ۑ��p
    private GameObject clonedObject;
    private static GameObject P1clonedObject;
    private static GameObject P2clonedObject;

    //�z�[���h�֘A
    private Mino clonedMino1P;
    private Mino clonedMino2P;

    //�z�[���h���W
    private float P1HoldX = 13.5f;
    private float P1HoldY = 19.0f;
    private float P2HoldX = -4.5f;
    private float P2HoldY = 19.0f;

    //���݂̃z�[���h��
    private static bool P1HoldFlag = false;
    public static bool P2HoldFlag = false;

    // mino��]
    public Vector3 rotationPoint = new Vector3(0,0,0);
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("NewMino", 1.0f);
    }

    public void NewMino()
    {
        if(GameManager.GState == "Playing" || GameManager.GState == "PvE")
        {
            //Mino�̐���
            clonedObject = Instantiate(Minos[Random.Range(0, Minos.Length)], new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f), Quaternion.identity);
        }  
    }

    public void HoldMino()
    {
        if(Mino.P1_Turn )
        {
            //���Ƀz�[���h����Ă邩
            if(P1HoldFlag)
            {
                clonedMino1P = P1clonedObject.GetComponent<Mino>();
                P1clonedObject.transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f);
                clonedMino1P.enabled = true;
            }

            //�z�[���h����Mino�̃X�N���v�g���~�߂�
            clonedMino1P = clonedObject.GetComponent<Mino>();
            clonedMino1P.enabled = false;

            //������߂�
            clonedMino1P.transform.eulerAngles = rotationPoint;

            //�w�肵���z�[���h�̈ʒu�ֈړ�
            clonedObject.transform.position = new Vector2(P1HoldX, P1HoldY);

            //�N���[�����ꂽ�I�u�W�F�N�g���L��
            P1clonedObject = clonedObject;
     
            if (!P1HoldFlag)//�V����Mino�𐶐�
                Instantiate(Minos[Random.Range(0, Minos.Length)], new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f), Quaternion.identity);

            //�z�[���h
            P1HoldFlag = true;
        }
        if(Mino.P2_Turn || Mino.PvE)
        {
            //���Ƀz�[���h����Ă邩
            if (P2HoldFlag)
            {
                clonedMino2P = P2clonedObject.GetComponent<Mino>();
                P2clonedObject.transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f);
                clonedMino2P.enabled = true;
            }

            //�z�[���h����Mino�̃X�N���v�g���~�߂�
            clonedMino2P = clonedObject.GetComponent<Mino>();
            clonedMino2P.enabled = false;

            //������߂�
            clonedMino2P.transform.eulerAngles = rotationPoint;

            //�w�肵���z�[���h�̈ʒu�ֈړ�
            clonedObject.transform.position = new Vector2(P2HoldX, P2HoldY);

            //�N���[�����ꂽ�I�u�W�F�N�g���L��
            P2clonedObject = clonedObject;
      
            if (!P2HoldFlag) //�V����Mino�𐶐�
                Instantiate(Minos[Random.Range(0, Minos.Length)], new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f), Quaternion.identity);

            //�z�[���h
            P2HoldFlag = true;
        } 
    }
}
