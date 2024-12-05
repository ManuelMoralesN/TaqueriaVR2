using UnityEngine;
using TMPro;

public class MesaController : MonoBehaviour
{
    public FilaController filaController; // Referencia al controlador de la fila
    public string pedidoEsperado; // Pedido esperado del cliente actual (Tag del taco)
    public TMP_Text contadorText; // Referencia al texto del contador
    public TMP_Text temporizadorText; // Referencia al texto del temporizador

    private int contadorPedidos = 0; // Contador de pedidos exitosos
    private float tiempoRestante = 120f; // Tiempo en segundos (2 minutos)
    private bool tiempoFinalizado = false; // Controla si el tiempo ha terminado

    public AudioClip musicForThisScene;

    private void Start()
    {
        ActualizarContadorUI(); // Inicializa el contador en la UI
        ActualizarTemporizadorUI(); // Inicializa el temporizador en la UI
        MusicManager.instance.PlayMusic(musicForThisScene);
        
    }

    private void Update()
    {
        if (!tiempoFinalizado)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                tiempoFinalizado = true;
                Debug.Log("¡Tiempo finalizado! Ya no se pueden entregar tacos.");
            }

            ActualizarTemporizadorUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tiempoFinalizado)
        {
            Debug.Log("El tiempo ha terminado. No se pueden entregar más pedidos.");
            return; // No hacer nada si el tiempo terminó
        }

        Debug.Log($"Objeto detectado: {other.name}, Tag: {other.tag ?? "Sin Tag"}");

        if (!string.IsNullOrEmpty(other.tag) && other.CompareTag(pedidoEsperado))
        {
            Debug.Log("Pedido correcto, pasando al siguiente cliente.");
            contadorPedidos++; // Incrementa el contador
            ActualizarContadorUI(); // Actualiza el texto en pantalla
            filaController.AvanzarFila(); // Llama al siguiente cliente
            Destroy(other.gameObject); // Destruye el taco entregado
        }
        else
        {
            Debug.Log("Pedido incorrecto o sin Tag válido.");
        }
    }

    private void ActualizarContadorUI()
    {
        if (contadorText != null)
        {
            contadorText.text = $"Pedidos entregados: {contadorPedidos}";
        }
        else
        {
            Debug.LogWarning("No se asignó ContadorText en el inspector.");
        }
    }

    private void ActualizarTemporizadorUI()
    {
        if (temporizadorText != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            temporizadorText.text = $"Tiempo restante: {minutos:00}:{segundos:00}";
        }
        else
        {
            Debug.LogWarning("No se asignó TemporizadorText en el inspector.");
        }
    }
}