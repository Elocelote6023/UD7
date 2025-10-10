using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 5.0f;
    public float margen = 0.1f; // espacio opcional respecto al borde
    private Camera camara;

    [Header("Vida")]
    private bool invencibilidad;
    public int VidaActual, VidaMaxima;
    public GameObject Jugador;

    [Header("Corazones llenos")]
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;

    [Header("Corazones vacíos")]
    public GameObject corazonVacio1;
    public GameObject corazonVacio2;
    public GameObject corazonVacio3;

    private float tiempoTranscurrido = 0f;
    private float tiempoTotal = 3f; // 3 segundos de invencibilidad

    void Start()
    {
        camara = Camera.main;

        VidaMaxima = 3;
        VidaActual = VidaMaxima;
        invencibilidad = false;

        corazon1.SetActive(true);
        corazon2.SetActive(true);
        corazon3.SetActive(true);

        corazonVacio1.SetActive(false);
        corazonVacio2.SetActive(false);
        corazonVacio3.SetActive(false);
    }

    void Update()
    {
        // --- Movimiento horizontal ---
        float inputHorizontal = Input.GetAxis("Horizontal");
        float movimientoX = inputHorizontal * velocidadMovimiento * Time.deltaTime;
        float nuevaPosX = transform.position.x + movimientoX;

        // Calcular límites visibles según la cámara actual
        float mitadAlto = camara.orthographicSize;
        float mitadAncho = mitadAlto * camara.aspect;

        // Limitar posición dentro de la cámara visible
        nuevaPosX = Mathf.Clamp(
            nuevaPosX,
            camara.transform.position.x - mitadAncho + margen,
            camara.transform.position.x + mitadAncho - margen
        );

        transform.position = new Vector3(nuevaPosX, transform.position.y, transform.position.z);

        // --- Vida y daño ---
        if (VidaActual <= 0)
        {
            Debug.Log("Has perdido");
            Jugador.SetActive(false);
        }

        if (invencibilidad)
        {
            tiempoTranscurrido += Time.deltaTime;

            if (tiempoTranscurrido >= tiempoTotal)
            {
                invencibilidad = false;
                tiempoTranscurrido = 0f;
                Debug.Log("Invencibilidad acabada");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo") && !invencibilidad)
        {
            Debug.Log("Colisión con enemigo");
            VidaActual--;
            invencibilidad = true;
            tiempoTranscurrido = 0f;

            // Actualizar corazones según vida restante
            if (VidaActual == 2)
            {
                corazon1.SetActive(false);
                corazonVacio1.SetActive(true);
            }
            else if (VidaActual == 1)
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



