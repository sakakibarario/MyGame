using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
    public float previousTime;
    // mino��������^�C��
    public static float fallTime = 0.6f;

    // �X�e�[�W�̑傫��
    private static int width = 10;
    private static int height = 20;

    //���ԃt���O
    //private bool Enemy_Turn  = false;
    public static bool P1_Turn = true;
    public static bool P2_Turn = false;

    // mino��]
    public Vector3 rotationPoint;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        //���ʎ���
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
            if (P1_Turn)//�v���C���[�P�Ăяo��
                MinoMovememt1();
            if (P2_Turn)//�v���C���[�Q�Ăяo��
                MinoMovememt2();
        }
    }


    //�v���C���[�P
    private void MinoMovememt1()
    {
        // �����L�[�ō��ɓ���
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }

        }
        // �E���L�[�ŉE�ɓ���
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        // �����ŉ��Ɉړ������A�����L�[�ł��ړ�����
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
            // mino������L�[�������ĉ�]������
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }


        }
    }

    //�v���C���[�Q
    private void MinoMovememt2()
    {
        // �����L�[�ō��ɓ���
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }

        }
        // �E���L�[�ŉE�ɓ���
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        // �����ŉ��Ɉړ������A�����L�[�ł��ړ�����
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
            // mino������L�[�������ĉ�]������
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
    }

    // ���C�������邩�H�m�F
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

    //  �񂪂�����Ă��邩�m�F
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

    // ���C��������
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }

    }

    // ���������
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

            // height-1 = 19�̂Ƃ���܂Ńu���b�N��������GameOver
            if (roundY >= height - 1)
            {
                // GameOver���\�b�h���Ăяo��
                Debug.Log("gameover");
                //HP�ǂ�����O�ɂȂ�����
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

    // mino�̈ړ��͈͂̐���
    bool ValidMovement()
    {

        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            // mino���X�e�[�W���͂ݏo���Ȃ��悤�ɐ���
            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                //Debug.Log("������܂���");
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
