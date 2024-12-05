using UnityEngine;
using System.Collections.Generic;

public class FilaController : MonoBehaviour
{
    public List<Transform> posicionesFila; // Puntos de espera en la fila
    public List<GameObject> npcPrefab; // Prefab de NPC
    private Queue<GameObject> filaNPCs = new Queue<GameObject>();
    private HashSet<int> posicionesOcupadas = new HashSet<int>(); // Control de posiciones ocupadas

    private void Start()
    {
        // Crear solo tres NPCs iniciales
        for (int i = 0; i < Mathf.Min(3, posicionesFila.Count); i++)
        {
            CrearNPC(i);
        }
    }

    private void CrearNPC(int posicion)
    {
        if (posicionesOcupadas.Contains(posicion)) return; // Evitar duplicados

        GameObject npc = Instantiate(npcPrefab[posicion % npcPrefab.Count], posicionesFila[posicion].position, Quaternion.identity);
        posicionesOcupadas.Add(posicion); // Marcar la posición como ocupada

        NPCController npcController = npc.GetComponent<NPCController>();
        if (npcController != null)
        {
            npcController.filaController = this; // Conectar al controlador de fila
            npcController.GenerarPedido(); // Generar el pedido inicial
        }

        filaNPCs.Enqueue(npc);
    }

    public void AvanzarFila()
    {
        if (filaNPCs.Count > 0)
        {
            GameObject npcAtendido = filaNPCs.Dequeue(); // Remueve al primero
            Destroy(npcAtendido, 1f); // Destruye el NPC tras atenderlo

            // Liberar la posición del primer NPC
            posicionesOcupadas.Remove(0);
        }

        // Mueve a los NPCs restantes hacia adelante
        int index = 0;
        foreach (var npc in filaNPCs)
        {
            npc.transform.position = posicionesFila[index].position; // Mover NPC a la nueva posición
            posicionesOcupadas.Remove(index); // Liberar la posición previa
            posicionesOcupadas.Add(index); // Actualizar posición como ocupada
            index++;
        }

        // Crea un nuevo NPC al final de la fila si hay espacio
        if (filaNPCs.Count < posicionesFila.Count)
        {
            int nuevaPosicion = filaNPCs.Count; // La nueva posición será la última disponible
            CrearNPC(nuevaPosicion);
        }
    }
}
