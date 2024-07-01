using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckObj : MonoBehaviour
{
    public void RecoveryEffect()
    {     
        Debug.Log("Recovery");
        FindObjectOfType<StatusManagement>().RecoveryHandle();
    }

    public void AttackEffect()
    {     
        Debug.Log("Attack");
        FindObjectOfType<StatusManagement>().AttackHandle();
    }

    public void NormalEffect()
    {   
        Debug.Log("Normal");
        FindObjectOfType<StatusManagement>().NormalHandle();
    }
    public void DebuffEffect()
    {
        Debug.Log("Debuff");
        FindObjectOfType<StatusManagement>().DebuffHandle();
    }

    public void BuffEffect()
    {
        Debug.Log("buff");
        Invoke("Buff2", 0.7f);
    }

    public void Buff2()
    {
        FindObjectOfType<StatusManagement>().BuffHandle();
    }
}
