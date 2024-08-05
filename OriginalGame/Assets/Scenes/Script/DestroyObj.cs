using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyObject()
    {
        GameObject[] objectscell = GameObject.FindGameObjectsWithTag("Cell");
        GameObject[] objectsmino = GameObject.FindGameObjectsWithTag("Mino");
        //MinoとCellが付いたタグをすべて削除
        foreach(GameObject gameObject in objectscell)
        {
            Destroy(gameObject);
        }
        foreach (GameObject gameObject in objectsmino)
        {
            Destroy(gameObject);
        }
    }
}
