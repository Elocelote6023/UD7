using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject proyectilPrefab; // Referencia al prefab del proyectil
    public Transform spawnPoint; // Punto de origen del disparo
    public float fuerza = 10f; // Fuerza aplicada al proyectil

    public Transform objetoATeletransportar; // Objeto que se teletransportará
    public float intervaloTeletransporte = 2f; // Intervalo de tiempo entre teletransportes
    private float rangoXMinimo = -34f; // Valor mínimo de X
    private float rangoXMaximo = 34f; // Valor máximo de X

    public float tiempoEntreBalas = 1f; // Tiempo en segundos entre cada mensaje

    private void Start()
    {
        InvokeRepeating("TeletransportarObjeto", 0f, intervaloTeletransporte);
        StartCoroutine(EnviarBala());
    }

    private IEnumerator EnviarBala()
    {
        while (true)
        {
            DispararProyectil();
            yield return new WaitForSeconds(tiempoEntreBalas);
        }
    }

    private void TeletransportarObjeto()
    {
        // Genera una posición X aleatoria dentro del rango especificado
        float posicionXAleatoria = Random.Range(rangoXMinimo, rangoXMaximo);

        // Obtiene la posición actual del objeto
        Vector3 nuevaPosicion = objetoATeletransportar.position;

        // Actualiza la posición X con el valor aleatorio
        nuevaPosicion.x = posicionXAleatoria;

        // Teletransporta el objeto a la nueva posición
        objetoATeletransportar.position = nuevaPosicion;
    }

    void DispararProyectil()
    {
        // Instanciar un nuevo proyectil en el punto de origen
        GameObject newProyectil = Instantiate(proyectilPrefab, spawnPoint.position, spawnPoint.rotation);

        // Obtener el componente Rigidbody del proyectil
        Rigidbody rb = newProyectil.GetComponent<Rigidbody>();

        // Verificar si existe un Rigidbody y aplicarle una fuerza usando AddForce
        if (rb != null)
        {
            rb.AddForce(spawnPoint.forward * fuerza, ForceMode.Impulse);
        }
    }
}
