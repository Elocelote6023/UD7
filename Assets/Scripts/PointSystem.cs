using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    public float counter;
    public float tiempo;
    public float dificultad;
    public float dificultadFake;
    public TMP_Text contador;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        tiempo = 0;
        dificultad = 0.5f;
        dificultadFake = 1;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        tiempo += Time.deltaTime;

        if (Convert.ToInt32(MathF.Floor(counter)) >= 10)
        {
            counter = 0;
            dificultad /= 2;
            dificultadFake++;
        }

        contador.text = ("Tiempo: " + Convert.ToInt32(MathF.Floor(tiempo)) + "\nDificultad: " + dificultadFake);
    }
}
