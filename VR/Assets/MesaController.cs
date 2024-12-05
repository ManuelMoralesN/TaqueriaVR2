using UnityEngine;

public class MesaController : MonoBehaviour
{
    public FilaController filaController; // Referencia al controlador de la fila
    public string pedidoEsperado; // Pedido esperado del cliente actual (Tag del taco)

    private void OnTriggerEnter(Collider other)
{
    Debug.Log($"Objeto detectado: {other.name}, Tag: {other.tag ?? "Sin Tag"}");

    if (!string.IsNullOrEmpty(other.tag) && other.CompareTag(pedidoEsperado))
    {
        Debug.Log("Pedido correcto, pasando al siguiente cliente.");
        filaController.AvanzarFila(); // Llama al siguiente cliente
        Destroy(other.gameObject); // Destruye el taco entregado
    }
    else
    {
        Debug.Log("Pedido incorrecto o sin Tag válido.");
    }
}
}
