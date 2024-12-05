using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public string ordenActual = ""; // La orden que este NPC pedirá
    public bool atendido = false;   // Indica si ya fue atendido
    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    public void MoverHacia(Transform destino)
    {
        // Mueve al NPC hacia el destino especificado
        if (agente != null && destino != null)
        {
            agente.SetDestination(destino.position);
        }
    }

    public void GenerarOrden()
    {
        // Genera una orden aleatoria (por ahora, siempre será un taco)
        ordenActual = "1xTaco"; // Aquí puedes expandir la lógica para órdenes aleatorias
        Debug.Log($"{gameObject.name} ha generado una orden: {ordenActual}");
    }

    public void Atender()
    {
        // Marca al NPC como atendido y detiene su movimiento
        atendido = true;
        if (agente != null)
        {
            agente.isStopped = true;
        }
    }

    public void Retirarse()
    {
        // Lógica para que el NPC se retire (puede ser destruirlo o moverlo fuera de la escena)
        Debug.Log($"{gameObject.name} ha sido atendido y se retira.");
        Destroy(gameObject, 1f); // Destruye el NPC después de 1 segundo
    }
}
