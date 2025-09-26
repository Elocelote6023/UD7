using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float tiempoVida = 10f; // Tiempo de vida del proyectil en segundos

    private void DestruirProyectil()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto con el que colisionó tiene el tag "Desaparecer"
        if (other.CompareTag("Desaparecer"))
        {
            // Destruye el proyectil cuando colisiona con un objeto con el tag "Desaparecer"
            Destroy(gameObject);
        }
        if (other.CompareTag("CuentaAtras"))
        {
            Invoke("DestruirProyectil", tiempoVida);
        }
    }

}
