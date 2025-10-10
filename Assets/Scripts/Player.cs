using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f; // Velocidad de movimiento de la barra
    public float limitePosX = 34.0f;         // Límite máximo de la posición en el eje X
    private bool invencibilidad;
    public int VidaActual, VidaMaxima;
    public GameObject Jugador;

    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;

    public GameObject corazonVacio1;
    public GameObject corazonVacio2;
    public GameObject corazonVacio3;


    private float tiempoTranscurrido = 0f;
    private float tiempoTotal = 3f; // 3 segundos en total

    private void Start()
    {
        VidaMaxima = 3;

        invencibilidad = false;
        VidaActual = VidaMaxima;

        corazon1.SetActive(true);
        corazon2.SetActive(true);
        corazon3.SetActive(true);

        corazonVacio1.SetActive(false);
        corazonVacio2.SetActive(false);
        corazonVacio3.SetActive(false);

    }

    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float movimientoX = inputHorizontal * velocidadMovimiento * Time.deltaTime;
        float nuevaPosX = transform.position.x + movimientoX;
        nuevaPosX = Mathf.Clamp(nuevaPosX, -limitePosX, limitePosX);
        transform.position = new Vector3(nuevaPosX, transform.position.y, transform.position.z);

        if (VidaActual <= 0)
        {
            Debug.Log("Has perdido");
            Jugador.SetActive(false);
        }

        if(invencibilidad)
        {
            tiempoTranscurrido += Time.deltaTime;
        }

        if (tiempoTranscurrido >= tiempoTotal)
        {
            invencibilidad = false;
            Debug.Log("Invencivilidad Acabada");
            tiempoTranscurrido= 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto entrante tiene la etiqueta "Enemigo"
        if (other.CompareTag("Enemigo") && !invencibilidad)
        {
            Debug.Log("Colision");
            VidaActual--;
            invencibilidad = true;
            tiempoTranscurrido += Time.deltaTime;

            corazon1.SetActive(false);
            corazonVacio1.SetActive(true);

            if (VidaActual == 1)
            {
                corazon2.SetActive(false);
                corazonVacio2.SetActive(true);
            }
            else if (VidaActual == 0)
            {
                corazon3.SetActive(false);
                corazonVacio3.SetActive(true);
            }

        }
    }
}


