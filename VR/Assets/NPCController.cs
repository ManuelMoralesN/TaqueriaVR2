using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    public TMP_Text pedidoText; // Referencia al texto sobre el NPC
    public string pedidoActual; // El pedido asignado al NPC
    public bool atendido = false;
    public FilaController filaController;

    private List<string> tiposTacos = new List<string> { "Pastor", "Suaperro" }; // Solo Pastor y Suaperro

    void Start()
    {
        GenerarPedido(); // Genera el pedido al inicio
    }

    public void GenerarPedido()
    {
        // Elegir un tipo de taco al azar
        string tacoSeleccionado = tiposTacos[Random.Range(0, tiposTacos.Count)];

        // Formar el pedido (siempre será un taco)
        pedidoActual = $"1 taco de {tacoSeleccionado}";
        pedidoText.text = pedidoActual; // Actualiza el texto sobre el NPC
    }

    public void AtenderPedido()
    {
        if (!atendido)
        {
            atendido = true;
            pedidoText.text = ""; // Borra el texto al atender
            filaController.AvanzarFila(); // Llama al siguiente cliente
        }
    }
}
