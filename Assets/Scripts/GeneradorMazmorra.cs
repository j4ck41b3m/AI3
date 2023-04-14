using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorMazmorra : MonoBehaviour
{
    public class Celda {

        public bool visitada = false;
        public bool[] status = new bool[4]; // 0 top, 1 ,down , 2 left, 3 right

    }
    public Vector2Int size;
    public int posicionInicio = 0;
    public GameObject[] sala;
    public Vector2Int dimension;
    List<Celda> tablero;
    public int numsala, colorsala;

    void Start()
    {
        numsala = 0;

        GeneradorLaberinto();
    }

    // Update is called once per frame
   

    void GeneraMazmorra()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                
                    colorsala = Mathf.RoundToInt(((i + j) * 7)/20);
                    var nuevaSala = Instantiate(sala[colorsala], new Vector3(i * dimension.x, 0, j * dimension.y), Quaternion.identity, transform).GetComponent<ComportamientoSala>();
                    nuevaSala.UpdateSala(tablero[i + j * size.x].status);
                
                //int h = Random.Range(0, 7);
                
            }
        }
    }

    void GeneradorLaberinto()
    {
        tablero = new List<Celda>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                tablero.Add(new Celda());

            }
        }

        int celdaActual = posicionInicio;
        Stack<int> camino = new Stack<int>();

        int num = 0;

        while (num < 20)
        {
            num++;

            tablero[celdaActual].visitada = true;

            List<int> vecinos = CompruebaVecino(celdaActual);

            if (vecinos.Count == 0)
            {
                if (camino.Count == 0)
                {
                    break;
                }
                else
                {
                    celdaActual = camino.Pop();
                }
            }
           
            else
            {
                camino.Push(celdaActual);
                int nuevaCelda = vecinos[UnityEngine.Random.Range(0, vecinos.Count)];

                if (nuevaCelda > celdaActual)
                {
                    if (nuevaCelda - 1 == celdaActual)
                    {
                        tablero[celdaActual].status[3] = true;
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[2] = true;
                    }
                    else
                    {
                        tablero[celdaActual].status[0] = true;
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[1] = true;
                    }
                }

                else
                {
                    if (nuevaCelda + 1 == celdaActual)
                    {
                        tablero[celdaActual].status[2] = true;
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[3] = true;
                    }
                    else
                    {
                        tablero[celdaActual].status[1] = true;
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[0] = true;
                    }
                }
            }
        }
        GeneraMazmorra();

    }

    List<int> CompruebaVecino(int celda)
    {
        List<int> vecinos = new List<int>();

        if (celda - size.x >= 0 && !tablero[celda - size.x].visitada)
        {
            vecinos.Add(celda - size.x);
        }

        if (celda + size.x < tablero.Count && !tablero[celda + size.x].visitada)
        {
            vecinos.Add(celda + size.x);
        }

        if (celda % size.x != 0 && !tablero[celda -1].visitada)
        {
            vecinos.Add(celda - 1);
        }

        if ((celda + 1) % size.x != 0 && !tablero[celda +1 ].visitada)
        {
            vecinos.Add(celda + 1);
        }
        
        return vecinos;
    }
}
