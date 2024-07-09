using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
    public float previousTime;
    // minoが落ちるタイム
    public static float fallTime = 0.6f;

    // ステージの大きさ
    private static int width = 10;
    private static int height = 20;

    //順番フラグ
    //private bool Enemy_Turn  = false;
    public static bool P1_Turn = true;
    public static bool P2_Turn = false;

    // mino回転
    public Vector3 rotationPoint;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
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
    }


    //プレイヤー１
    private void MinoMovememt1()
    {
        // 左矢印キーで左に動く
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }

        }
        // 右矢印キーで右に動く
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        // 自動で下に移動させつつ、下矢印キーでも移動する
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - previousTime >= fallTime)
        {
            transform.position += new Vector3(0, -1, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(0, -1, 0);

                AddToGrid();
                CheckLines();
                this.enabled = false;

                P2_Turn = true;
                P1_Turn = false;
                FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
            }
            previousTime = Time.time;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // minoを上矢印キーを押して回転させる
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }


        }
    }

    //プレイヤー２
    private void MinoMovememt2()
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
                P1_Turn = true;
                P2_Turn = false;
                FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
            }
            previousTime = Time.time;
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            // minoを上矢印キーを押して回転させる
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
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
                Debug.Log("gameover");
                //HPどちら方０になったら
                if (P2_Turn)
                {
                    Debug.Log("1win");
                    FindObjectOfType<CharacterAnimation>().Player1WinAnime();
                    GameManager.GState = "Title";
                }
                if (P1_Turn)
                {
                    Debug.Log("2win");
                    FindObjectOfType<CharacterAnimation>().Player2WinAnime();
                    GameManager.GState = "Title";
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
