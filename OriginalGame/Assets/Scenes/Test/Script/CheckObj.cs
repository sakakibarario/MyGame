using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckObj : MonoBehaviour
{
    public void DebuffEffect()
    {
        Debug.Log("Debuff");
        Debug.Log(Mino.P1_Turn);
        Debug.Log(Mino.P2_Turn);
        FindObjectOfType<StatusManagement>().DebuffHandle();
    }

    public void BuffEffect()
    {     
        Debug.Log("buff");
        Debug.Log(Mino.P1_Turn);
        Debug.Log(Mino.P2_Turn);
        FindObjectOfType<StatusManagement>().BuffHandle();
    }

    public void RecoveryEffect()
    {     
        Debug.Log("Recovery");
        Debug.Log(Mino.P1_Turn);
        Debug.Log(Mino.P2_Turn);
        FindObjectOfType<StatusManagement>().RecoveryHandle();
    }

    public void AttackEffect()
    {     
        Debug.Log("Attack");
        Debug.Log(Mino.P1_Turn);
        Debug.Log(Mino.P2_Turn);
        FindObjectOfType<StatusManagement>().AttackHandle();
    }

    public void NormalEffect()
    {   
        Debug.Log("Normal");
        Debug.Log(Mino.P1_Turn);
        Debug.Log(Mino.P2_Turn);
        FindObjectOfType<StatusManagement>().NormalHandle();
    }

}
