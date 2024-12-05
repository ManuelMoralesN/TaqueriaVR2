using UnityEngine;
using TMPro;

public class NPCController : MonoBehaviour
{
    public TMP_Text pedidoText; // Referencia al pedido que se muestra sobre el NPC
    public string pedidoActual; // El pedido asignado al NPC
    private bool atendido = false;

    public void SetPedido(string pedido)
    {
        pedidoActual = pedido;
        pedidoText.text = pedido; // Actualiza el texto del pedido sobre el NPC
    }

    public void AtenderPedido()
    {
        atendido = true;
        pedidoText.text = ""; // Borra el texto al atender
    }

    public bool EsAtendido()
    {
        return atendido;
    }
}
