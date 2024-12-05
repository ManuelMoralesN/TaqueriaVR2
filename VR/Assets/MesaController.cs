using UnityEngine;

public class MesaController : MonoBehaviour
{
    public FilaController filaController; // Referencia al script FilaController

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto entregado es el pedido correcto (puedes personalizar esta lógica)
        if (other.CompareTag("Pedido"))
        {
            // Llama a AvanzarFila en el FilaController
            filaController.AvanzarFila();

            // Opcional: destruir el objeto del pedido entregado
            Destroy(other.gameObject);
        }
    }
}
