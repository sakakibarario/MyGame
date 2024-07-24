using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRandom : MonoBehaviour
{
    //ìGÇÃèÛë‘ÉtÉâÉO
    public static bool EnemyMoveFlag = false;

    public enum Move
    {
        Attack,
        Recovery,
        Debuff,     
    }  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyMove()
    {
        Mino.EnemyMoveCount = 3;
        int rnd = Random.Range(2, 3);
        Mino.PvE = false;
        EnemyMoveFlag = true;
        switch (rnd)
        {
            case (int)Move.Attack:
                FindObjectOfType<StatusManagement>().AttackHandle();
                break;      
            case (int)Move.Recovery:
                FindObjectOfType<StatusManagement>().RecoveryHandle();
                break;
            case (int)Move.Debuff:
                FindObjectOfType<StatusManagement>().DebuffHandle();
                break;
        }
    }
}
