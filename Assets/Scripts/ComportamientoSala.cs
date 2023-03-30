using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoSala : MonoBehaviour
{
    public GameObject[] muros; // 0 - top, 1 - down, 2 - left, 3 - right
    // Start is called before the first frame update
    public GameObject[] puertas;
    public bool conectada;

    void Start()
    {
    }

    // Update is called once per frame
    public void UpdateSala(bool[] status)
    {
        conectada = false;
        for (int i = 0; i < status.Length; i++)
        {
            puertas[i].SetActive(status[i]);
            muros[i].SetActive(!status[i]);
            if (status[i])
            {
                conectada = true;
            }
        }
        if (!conectada)
        {
            Destroy(gameObject);
        }
    }
}
