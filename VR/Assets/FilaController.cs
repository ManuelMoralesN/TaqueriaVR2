using UnityEngine;
using System.Collections.Generic;

public class FilaController : MonoBehaviour
{
    public List<Transform> posicionesFila; // Puntos de espera en la fila
    public GameObject npcPrefab; // Prefab de NPC
    public MesaController mesaController; // Referencia al controlador de la mesa
    private Queue<GameObject> filaNPCs = new Queue<GameObject>();

    private void Start()
    {
        // Crear NPCs iniciales
        for (int i = 0; i < posicionesFila.Count; i++)
        {
            CrearNPC(i);
        }

        // Sincronizar el pedido del primer cliente con la mesa
        if (filaNPCs.Count > 0)
        {
            SincronizarPedidoConMesa();
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

        // Sincronizar el pedido del siguiente cliente con la mesa
        if (filaNPCs.Count > 0)
        {
            SincronizarPedidoConMesa();
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

    private void SincronizarPedidoConMesa()
    {
        if (filaNPCs.Count > 0)
        {
            GameObject siguienteNPC = filaNPCs.Peek(); // Obtén el siguiente NPC
            NPCController npcController = siguienteNPC.GetComponent<NPCController>();

            if (npcController != null && mesaController != null)
            {
                mesaController.pedidoEsperado = npcController.pedidoActual; // Actualiza el pedido esperado
            }
        }
    }
}
