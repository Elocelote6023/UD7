using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject PointSystem;
    public GameObject proyectilPrefab;
    public Transform spawnPoint;
    public Transform objetoATeletransportar;

    [Header("Parámetros de dificultad")]
    public float dificultad;
    public float intervaloTeletransporte;
    public float tiempoEntreBalas;

    [Header("Disparo")]
    public float fuerza = 10f;

    [Header("Cámara")]
    public Camera camara;
    public float margen = 0.5f;

    private float rangoXMinimo;
    private float rangoXMaximo;

    private void Start()
    {
        if (camara == null)
            camara = Camera.main;

        // Calcular límites iniciales según la cámara
        ActualizarLimitesCamara();

        // Iniciar corrutinas
        StartCoroutine(TP());
        StartCoroutine(EnviarBala());
    }

    private void Update()
    {
        // Actualizar dificultad desde PointSystem
        dificultad = PointSystem.GetComponent<PointSystem>().dificultad;
        intervaloTeletransporte = dificultad;
        tiempoEntreBalas = dificultad;

        // Si la cámara cambia (por ejemplo, aspect ratio), recalculamos los límites
        ActualizarLimitesCamara();
    }

    private void ActualizarLimitesCamara()
    {
        // Calcular los límites visibles en coordenadas del mundo
        float mitadAlto = camara.orthographicSize;
        float mitadAncho = mitadAlto * camara.aspect;

        rangoXMinimo = camara.transform.position.x - mitadAncho + margen;
        rangoXMaximo = camara.transform.position.x + mitadAncho - margen;
    }

    private IEnumerator TP()
    {
        while (true)
        {
            TeletransportarObjeto();
            yield return new WaitForSeconds(intervaloTeletransporte);
        }
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
        // Genera una posición X aleatoria dentro del rango visible de la cámara
        float posicionXAleatoria = Random.Range(rangoXMinimo, rangoXMaximo);

        // Actualiza la posición del objeto en X
        Vector3 nuevaPosicion = objetoATeletransportar.position;
        nuevaPosicion.x = posicionXAleatoria;

        // Teletransporta el objeto
        objetoATeletransportar.position = nuevaPosicion;
    }

    private void DispararProyectil()
    {
        // Instanciar un nuevo proyectil en el punto de origen
        GameObject newProyectil = Instantiate(proyectilPrefab, spawnPoint.position, spawnPoint.rotation);

        // Aplicar fuerza si tiene Rigidbody
        Rigidbody rb = newProyectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(spawnPoint.forward * fuerza, ForceMode.Impulse);
        }
    }
}

