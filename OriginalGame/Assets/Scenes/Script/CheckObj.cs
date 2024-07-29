using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckObj : MonoBehaviour
{
    public void RecoveryEffect()
    {
        Debug.Log("Recovery");
        if (GameManager.GState == "Playing" || GameManager.GState == "PvE")
            FindObjectOfType<StatusManagement>().RecoveryHandle();
    }

    public void AttackEffect()
    {
        Debug.Log("Attack");
        if (GameManager.GState == "Playing" )
            FindObjectOfType<StatusManagement>().AttackHandle();
        else if(GameManager.GState == "PvE")
            FindObjectOfType<StatusManagement>().EnemyAttackHandle();
    }

    public void NormalEffect()
    {
        Debug.Log("Normal");
        if (GameManager.GState == "Playing" || GameManager.GState == "PvE")
            FindObjectOfType<StatusManagement>().NormalHandle();
    }
    public void DebuffEffect()
    {
        Debug.Log("Debuff");
        if (GameManager.GState == "Playing" || GameManager.GState == "PvE")
            FindObjectOfType<StatusManagement>().Invoke("DebuffHandle", 0.5f);
    }

    public void BuffEffect()
    {
        Debug.Log("buff");
        if (GameManager.GState == "Playing" || GameManager.GState == "PvE")
            FindObjectOfType<StatusManagement>().Invoke("BuffHandle", 0.7f);
    }
}
