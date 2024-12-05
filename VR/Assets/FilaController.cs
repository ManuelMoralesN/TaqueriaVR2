using UnityEngine;
using System.Collections.Generic;

public class FilaController : MonoBehaviour
{
    public List<Transform> posicionesFila; // Puntos de espera en la fila
    public GameObject npcPrefab; // Prefab de NPC
    private Queue<GameObject> filaNPCs = new Queue<GameObject>();

    private void Start()
    {
        // Crear NPCs iniciales
        for (int i = 0; i < posicionesFila.Count; i++)
        {
            CrearNPC(i);
        }
    }

    private void CrearNPC(int posicion)
{
    GameObject npc = Instantiate(npcPrefab, posicionesFila[posicion].position, Quaternion.identity);

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
        }

        // Mueve a los NPCs restantes hacia adelante
        int index = 0;
        foreach (var npc in filaNPCs)
        {
            npc.transform.position = posicionesFila[index].position;
            index++;
        }

        // Crea un nuevo NPC al final de la fila y genera su pedido
        CrearNPC(posicionesFila.Count - 1);
    }
}
