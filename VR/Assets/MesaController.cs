using UnityEngine;

public class MesaController : MonoBehaviour
{
    public FilaController filaController; // Referencia al controlador de la fila
    public string pedidoEsperado; // Pedido esperado del cliente actual

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto entregado es un taco
        if (other.CompareTag("Taco"))
        {
            TacoController taco = other.GetComponent<TacoController>();
            if (taco != null && taco.tipoTaco == pedidoEsperado) // Verifica si el pedido es correcto
            {
                Debug.Log("Pedido correcto, pasando al siguiente cliente.");
                filaController.AvanzarFila(); // Llama al siguiente cliente
                Destroy(other.gameObject); // Destruye el taco entregado
            }
            else
            {
                Debug.Log("Pedido incorrecto.");
            }
        }
    }
}
