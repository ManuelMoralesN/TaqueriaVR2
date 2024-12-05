using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public GameObject tacoPrefab; // Prefab del taco
    public Transform[] puntosRespawn; // Array de posiciones donde se pueden generar los tacos
    public float tiempoRespawn = 5f; // Tiempo entre respawns de tacos

    private void Start()
    {
        // Inicia el proceso de respawn automático
        InvokeRepeating(nameof(RespawnTacos), 0f, tiempoRespawn);
    }

    private void RespawnTacos()
    {
        foreach (Transform punto in puntosRespawn)
        {
            // Verifica si ya hay un taco en el punto
            if (Physics.CheckSphere(punto.position, 0.5f))
            {
                Debug.Log($"Punto ocupado en {punto.position}. No se generó un taco.");
                continue;
            }

            // Genera un nuevo taco en la posición del punto
            Instantiate(tacoPrefab, punto.position, Quaternion.identity);
            Debug.Log($"Taco generado en {punto.position}");
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja esferas en las posiciones de respawn en el editor
        Gizmos.color = Color.green;
        foreach (Transform punto in puntosRespawn)
        {
            Gizmos.DrawSphere(punto.position, 0.5f);
        }
    }
}
