using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
    public float previousTime;
    // minoが落ちるタイム
    public static float fallTime = 0.6f;

    public static bool HoldFlag = true;//ホールド用

    // ステージの大きさ
    private static int width = 10;
    private static int height = 21;

    //順番フラグ
    public static bool PvE     = false;
    public static bool P1_Turn = false;
    public static bool P2_Turn = false;

    //敵が動き出す時間
    public static int EnemyMoveCount = 3;

    // mino回転
    public Vector3 rotationPoint;

    //X軸
    private float hori = 0;
    private bool Xaxiscontrol = false;
   
    //Y軸
    private float Verti = 0;
    private bool Yaxiscontrol = false;

    private bool HardDropFlag = false;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        HardDropFlag = true;
        //効果時間
        if (StatusManagement.DebuffTime > 0)
            FindObjectOfType<StatusManagement>().DebuffCount();
        if (StatusManagement.BuffTime > 0)
            FindObjectOfType<StatusManagement>().BuffCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GState == "Playing")
        {
            if (P1_Turn)//プレイヤー１呼び出す
                MinoMovememt1();
            if (P2_Turn)//プレイヤー２呼び出す
                MinoMovememt2();
        }
        if (GameManager.GState == "PvE")
        {
            if (PvE)
                MinoMovememt2();
        }
    }


    //プレイヤー１
    private void MinoMovememt1()
    {
        // 左矢印キーで左に動く
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }

        }
        // 右矢印キーで右に動く
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        // 自動で下に移動させつつ、下矢印キーでも移動する
        else if (Input.GetKeyDown(KeyCode.S) || Time.time - previousTime >= fallTime)
        {
            transform.position += new Vector3(0, -1, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(0, -1, 0);

                AddToGrid();
                CheckLines();
                this.enabled = false;

                //サウンド関数呼び出し
                FindObjectOfType<SoundMino>().MinoSound();

                //ターンの入れ替え
                P2_Turn = true;
                P1_Turn = false;

                //ホールドフラグをあげる
                HoldFlag = true;
                //新しいMinoの生成
                FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
            }
            previousTime = Time.time;
        }
        //上ボタンでハードドロップ
        else if (Input.GetKeyDown(KeyCode.W) && HardDropFlag)
        {

           // Debug.Log("hard");
            HardDropFlag = false;

            while (true)
            {
                transform.position += new Vector3(0, -1, 0);

                if (!ValidMovement())
                {
                    transform.position -= new Vector3(0, -1, 0);

                    AddToGrid();
                    CheckLines();
                    this.enabled = false;

                    //サウンド関数呼び出し
                    FindObjectOfType<SoundMino>().MinoSound();

                    //ターンの入れ替え
                    P2_Turn = true;
                    P1_Turn = false;

                    //ホールドフラグをあげる
                    HoldFlag = true;
                    //新しいMinoの生成
                    FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
                    break;//ループ終了
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // minoを上矢印キーを押して回転させる
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // minoを上矢印キーを押して回転させる
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }

        else if(Input.GetKeyDown(KeyCode.UpArrow) && HoldFlag)
        {
            HoldFlag = false;
            FindObjectOfType<SpawnMino>().HoldMino();
        }
    }
    
   

    //プレイヤー２
    private void MinoMovememt2()
    {
        if (hori == 0)
            Xaxiscontrol = true;
        if (Verti == 0)
            Yaxiscontrol = true;

        hori  = Input.GetAxisRaw("Joystick_H");
        Verti = Input.GetAxisRaw("Joystick_V");


        //string AAA = Input.GetJoystickNames();
        // 左矢印キーで左に動く
        if ( hori < 0 && Xaxiscontrol)
        {
            Xaxiscontrol = false;

            //Xcontrol = false;
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }

        }
        // 右矢印キーで右に動く
        else if ( hori > 0 && Xaxiscontrol )
        {
            Xaxiscontrol = false;

            transform.position += new Vector3(1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        // 自動で下に移動させつつ、したボタンでも移動する
        else if ((Verti < 0 && Yaxiscontrol) || Time.time - previousTime >= fallTime)
        {
            Yaxiscontrol = false;

            transform.position += new Vector3(0, -1, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(0, -1, 0);

                AddToGrid();
                CheckLines();
                this.enabled = false;

                //サウンド関数呼び出し
                FindObjectOfType<SoundMino>().MinoSound();

                //ターンの入れ替え
                if (GameManager.GState == "Playing")
                {
                    P1_Turn = true;
                    P2_Turn = false;
                }
                else
                {
                    EnemyMoveCount--;
                    if (EnemyMoveCount == 0)
                        FindObjectOfType<EnemyMoveRandom>().EnemyMove();
                }
                //ホールドフラグをあげる
                HoldFlag = true;
                //新しいMinoの生成
                FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
            }
            previousTime = Time.time;
        }
        //上ボタンでハードドロップ
        else if ((Verti > 0 && Yaxiscontrol)&& HardDropFlag)
        {
            Yaxiscontrol = false;

            //Debug.Log("hard");
            HardDropFlag = false;

            while (true)
            {
                transform.position += new Vector3(0, -1, 0);

                if (!ValidMovement())
                {
                    transform.position -= new Vector3(0, -1, 0);

                    AddToGrid();
                    CheckLines();
                    this.enabled = false;

                    //サウンド関数呼び出し
                    FindObjectOfType<SoundMino>().MinoSound();

                    //ターンの入れ替え
                    if (!PvE)
                    {
                        P1_Turn = true;
                        P2_Turn = false;
                    }
                    else
                    {
                        EnemyMoveCount--;
                        if (EnemyMoveCount == 0)
                            FindObjectOfType<EnemyMoveRandom>().EnemyMove();
                    }
                    //ホールドフラグをあげる
                    HoldFlag = true;
                    //新しいMinoの生成
                    FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
                    break;//ループ終了
                }
            }

        }

        else if (Input.GetKeyDown("joystick button 4"))
        {
            // minoをLBを押して左回転させる
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        else if(Input.GetKeyDown("joystick button 5"))
        {
            // minoをRBを押して右回転させる
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }

        else if (Input.GetKeyDown("joystick button 9") && HoldFlag)
        {
            HoldFlag = false;
            FindObjectOfType<SpawnMino>().HoldMino();
        }
    }

    // ラインがあるか？確認
    public void CheckLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    //  列がそろっているか確認
    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
            //Debug.Log(j);
        }

    
        //FindObjectOfType<GameManagement>().AddScore();
        return true;
    }

    // ラインを消す
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }

    }

    // 列を下げる
    public void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundX, roundY] = children;

            // height-1 = 19のところまでブロックがきたらGameOver
            if (roundY >= height - 1)
            {
                // GameOverメソッドを呼び出す
                GameManager.GState = "Title";
                //Debug.Log("gameover");
                //HPどちら方０になったら
                if (P1_Turn)
                {
                    //Debug.Log("1win");
                   
                    FindObjectOfType<CharacterAnimation>().Player1WinAnime();
                   
                }
                if (P2_Turn)
                {
                    //Debug.Log("2win");
                    FindObjectOfType<CharacterAnimation>().Player2WinAnime();
                }
                if (PvE)
                {
                    //Debug.Log("lose");
                    FindObjectOfType<CharacterAnimation>().PlayerloseAnime();
                }
               // FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);
            }
        }
    }

    // minoの移動範囲の制御
    bool ValidMovement()
    {

        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            // minoがステージよりはみ出さないように制御
            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                //Debug.Log("当たりました");
                return false;
            }

            if (grid[roundX, roundY] != null)
            {
                return false;
            }
        }
        return true;
    }
}
