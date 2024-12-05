using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    public TMP_Text pedidoText; // Referencia al pedido que se muestra sobre el NPC
    public string pedidoActual; // El pedido asignado al NPC
    public bool atendido = false;
    public FilaController filaController;

    private List<string> tiposTacos = new List<string> { "Pastor", "Suaperro" };
    private List<string> tiposBebidas = new List<string> { "Coca bien fría", "Caguama" };

    void Start()
    {
        GenerarPedido();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AtenderPedido();
        }
    }

    public void GenerarPedido()
    {
        // Generar cantidades aleatorias entre 1 y 9
        int cantidadTacos = Random.Range(1, 10);
        int cantidadBebidas = Random.Range(1, 10);

        // Elegir un tipo de taco y una bebida al azar
        string tacoSeleccionado = tiposTacos[Random.Range(0, tiposTacos.Count)];
        string bebidaSeleccionada = tiposBebidas[Random.Range(0, tiposBebidas.Count)];

        // Formar el pedido
        pedidoActual = $"{cantidadTacos} tacos de {tacoSeleccionado} y {cantidadBebidas} {bebidaSeleccionada}(s)";
        pedidoText.text = pedidoActual; // Actualiza el texto del pedido sobre el NPC
    }

    public void MoverHacia(Transform destino)
    {
        if(!atendido){
        atendido = false;
        pedidoText.text = ""; // Borra el texto al atender
        filaController.AvanzarFila();
        }
    }
}
