using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMino : MonoBehaviour
{
    public GameObject[] Minos;

    //クローンオブジェクト保存用
    private GameObject clonedObject;
    private static GameObject P1clonedObject;
    private static GameObject P2clonedObject;

    //ホールド関連
    private Mino clonedMino1P;
    private Mino clonedMino2P;

    //ホールド座標
    private float P1HoldX = 13.5f;
    private float P1HoldY = 19.0f;
    private float P2HoldX = -4.5f;
    private float P2HoldY = 19.0f;

    //現在のホールド状況
    private static bool P1HoldFlag = false;
    public static bool P2HoldFlag = false;

    // mino回転
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
            //Minoの生成
            clonedObject = Instantiate(Minos[Random.Range(0, Minos.Length)], new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f), Quaternion.identity);
        }  
    }

    public void HoldMino()
    {
        if(Mino.P1_Turn )
        {
            //既にホールドされてるか
            if(P1HoldFlag)
            {
                clonedMino1P = P1clonedObject.GetComponent<Mino>();
                P1clonedObject.transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f);
                clonedMino1P.enabled = true;
            }

            //ホールドするMinoのスクリプトを止める
            clonedMino1P = clonedObject.GetComponent<Mino>();
            clonedMino1P.enabled = false;

            //向きを戻す
            clonedMino1P.transform.eulerAngles = rotationPoint;

            //指定したホールドの位置へ移動
            clonedObject.transform.position = new Vector2(P1HoldX, P1HoldY);

            //クローンされたオブジェクトを記憶
            P1clonedObject = clonedObject;
     
            if (!P1HoldFlag)//新しいMinoを生成
                Instantiate(Minos[Random.Range(0, Minos.Length)], new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f), Quaternion.identity);

            //ホールド
            P1HoldFlag = true;
        }
        if(Mino.P2_Turn || Mino.PvE)
        {
            //既にホールドされてるか
            if (P2HoldFlag)
            {
                clonedMino2P = P2clonedObject.GetComponent<Mino>();
                P2clonedObject.transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f);
                clonedMino2P.enabled = true;
            }

            //ホールドするMinoのスクリプトを止める
            clonedMino2P = clonedObject.GetComponent<Mino>();
            clonedMino2P.enabled = false;

            //向きを戻す
            clonedMino2P.transform.eulerAngles = rotationPoint;

            //指定したホールドの位置へ移動
            clonedObject.transform.position = new Vector2(P2HoldX, P2HoldY);

            //クローンされたオブジェクトを記憶
            P2clonedObject = clonedObject;
      
            if (!P2HoldFlag) //新しいMinoを生成
                Instantiate(Minos[Random.Range(0, Minos.Length)], new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f), Quaternion.identity);

            //ホールド
            P2HoldFlag = true;
        } 
    }
}
