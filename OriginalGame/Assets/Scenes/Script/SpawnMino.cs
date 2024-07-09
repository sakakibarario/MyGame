using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMino : MonoBehaviour
{
    public GameObject[] Minos;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("NewMino", 1.0f);
    }

    public void NewMino()
    {
        //MinoÇÃê∂ê¨
        Instantiate(Minos[Random.Range(0, Minos.Length)], new Vector2(transform.position.x, transform.position.y + 1.0f), Quaternion.identity);
    }
}
