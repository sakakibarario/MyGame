using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
    public float previousTime;
    // mino��������^�C��
    public static float fallTime = 0.6f;

    public static bool HoldFlag = true;//�z�[���h�p

    // �X�e�[�W�̑傫��
    private static int width = 10;
    private static int height = 21;

    //���ԃt���O
    public static bool PvE     = false;
    public static bool P1_Turn = false;
    public static bool P2_Turn = false;

    //�G�������o������
    public static int EnemyMoveCount = 3;

    // mino��]
    public Vector3 rotationPoint;

    //X��
    private float hori = 0;
    private bool Xaxiscontrol = false;
   
    //Y��
    private float Verti = 0;
    private bool Yaxiscontrol = false;

    private bool HardDropFlag = false;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        HardDropFlag = true;
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
        if (GameManager.GState == "PvE")
        {
            if (PvE)
                MinoMovememt2();
        }
    }


    //�v���C���[�P
    private void MinoMovememt1()
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

                //�T�E���h�֐��Ăяo��
                FindObjectOfType<SoundMino>().MinoSound();

                //�^�[���̓���ւ�
                P2_Turn = true;
                P1_Turn = false;

                //�z�[���h�t���O��������
                HoldFlag = true;
                //�V����Mino�̐���
                FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
            }
            previousTime = Time.time;
        }
        //��{�^���Ńn�[�h�h���b�v
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

                    //�T�E���h�֐��Ăяo��
                    FindObjectOfType<SoundMino>().MinoSound();

                    //�^�[���̓���ւ�
                    P2_Turn = true;
                    P1_Turn = false;

                    //�z�[���h�t���O��������
                    HoldFlag = true;
                    //�V����Mino�̐���
                    FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
                    break;//���[�v�I��
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // mino������L�[�������ĉ�]������
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // mino������L�[�������ĉ�]������
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
    
   

    //�v���C���[�Q
    private void MinoMovememt2()
    {
        if (hori == 0)
            Xaxiscontrol = true;
        if (Verti == 0)
            Yaxiscontrol = true;

        hori  = Input.GetAxisRaw("Joystick_H");
        Verti = Input.GetAxisRaw("Joystick_V");


        //string AAA = Input.GetJoystickNames();
        // �����L�[�ō��ɓ���
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
        // �E���L�[�ŉE�ɓ���
        else if ( hori > 0 && Xaxiscontrol )
        {
            Xaxiscontrol = false;

            transform.position += new Vector3(1, 0, 0);

            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        // �����ŉ��Ɉړ������A�����{�^���ł��ړ�����
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

                //�T�E���h�֐��Ăяo��
                FindObjectOfType<SoundMino>().MinoSound();

                //�^�[���̓���ւ�
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
                //�z�[���h�t���O��������
                HoldFlag = true;
                //�V����Mino�̐���
                FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
            }
            previousTime = Time.time;
        }
        //��{�^���Ńn�[�h�h���b�v
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

                    //�T�E���h�֐��Ăяo��
                    FindObjectOfType<SoundMino>().MinoSound();

                    //�^�[���̓���ւ�
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
                    //�z�[���h�t���O��������
                    HoldFlag = true;
                    //�V����Mino�̐���
                    FindObjectOfType<SpawnMino>().Invoke("NewMino", 1.0f);
                    break;//���[�v�I��
                }
            }

        }

        else if (Input.GetKeyDown("joystick button 4"))
        {
            // mino��LB�������č���]������
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (!ValidMovement())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        else if(Input.GetKeyDown("joystick button 5"))
        {
            // mino��RB�������ĉE��]������
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
                GameManager.GState = "Title";
                //Debug.Log("gameover");
                //HP�ǂ�����O�ɂȂ�����
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
