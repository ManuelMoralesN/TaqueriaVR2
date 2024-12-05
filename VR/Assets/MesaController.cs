using UnityEngine;

public class MesaController : MonoBehaviour
{
    public FilaController filaController; // Referencia a la fila
    public string pedidoEsperado; // Pedido esperado

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Taco")) // Si es un taco
        {
            TacoController taco = other.GetComponent<TacoController>();
            if (taco != null && taco.tipoTaco == pedidoEsperado) // Verifica si el pedido es correcto
            {
                filaController.AvanzarFila(); // Llama al siguiente NPC
                Destroy(other.gameObject); // Destruye el taco
            }
        }
    }
}
