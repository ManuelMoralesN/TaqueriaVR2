using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FilaController : MonoBehaviour
{
    [Header("Configuración")]
    public Transform[] posicionesFila; // Array de posiciones de la fila
    public Transform posicionOrden;    // Posición de la mesa para tomar pedidos

    [Header("Configuración de NPCs")]
    public GameObject prefabNPC;       // Prefab de los NPCs
    public int maximoEnFila = 5;       // Máximo de NPCs en la fila

    private Queue<GameObject> colaNPCs = new Queue<GameObject>(); // Cola de NPCs

    void Start()
    {
        // Inicializa la fila con los NPCs
        for (int i = 0; i < maximoEnFila; i++)
        {
            CrearNPC(i);
        }
    }

    void CrearNPC(int indice)
    {
        // Crea un NPC en la posición correspondiente de la fila
        GameObject nuevoNPC = Instantiate(prefabNPC, posicionesFila[indice].position, Quaternion.identity);
        colaNPCs.Enqueue(nuevoNPC);
        MoverNPCAPosicion(nuevoNPC, posicionesFila[indice]);
    }

    void MoverNPCAPosicion(GameObject npc, Transform posicionObjetivo)
    {
        // Mueve al NPC a la posición objetivo utilizando NavMesh
        NavMeshAgent agente = npc.GetComponent<NavMeshAgent>();
        if (agente != null)
        {
            agente.SetDestination(posicionObjetivo.position);
        }
    }

    public void ProcesarSiguienteNPC()
    {
        if (colaNPCs.Count > 0)
        {
            // Retira al primer NPC de la fila
            GameObject npcActual = colaNPCs.Dequeue();

            // Mueve al NPC a la mesa para tomar el pedido
            MoverNPCAPosicion(npcActual, posicionOrden);

            // Simula que el NPC se retira después de completar el pedido
            StartCoroutine(RemoverNPCDespuesDeRetardo(npcActual));
        }

        // Reorganiza la fila para los NPCs restantes
        ReorganizarFila();
    }
     public void AvanzarFila()
    {
        // Llama a la lógica para procesar al siguiente NPC
        ProcesarSiguienteNPC();
    }
    void ReorganizarFila()
    {
        int indice = 0;
        foreach (GameObject npc in colaNPCs)
        {
            MoverNPCAPosicion(npc, posicionesFila[indice]);
            indice++;
        }
    }

    System.Collections.IEnumerator RemoverNPCDespuesDeRetardo(GameObject npc)
    {
        // Espera un tiempo antes de eliminar al NPC
        yield return new WaitForSeconds(3f);
        Destroy(npc);

        // Agrega un nuevo NPC al final de la fila
        if (colaNPCs.Count < maximoEnFila)
        {
            CrearNPC(posicionesFila.Length - 1);
        }
    }
}
