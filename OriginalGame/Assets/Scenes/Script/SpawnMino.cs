using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMino : MonoBehaviour
{
    public GameObject[] Minos;
    // Start is called before the first frame update
    void Start()
    {
        if (TitleScene.Startdelay)
        {
            Invoke("NewMino", 1.0f);
            TitleScene.Startdelay = false;
        }        
        else
            NewMino();
    }

    public void NewMino()
    {
        //MinoÇÃê∂ê¨
        Instantiate(Minos[Random.Range(0, Minos.Length)], transform.position, Quaternion.identity);
    }
}
