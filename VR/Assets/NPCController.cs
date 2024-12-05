using UnityEngine;
using TMPro;
using UnityEngine.AI;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public TMP_Text pedidoText; // Referencia al texto sobre el NPC
    public string pedidoActual; // El pedido asignado al NPC
    public bool atendido = false;
    public FilaController filaController;

    public Transform posicionSalida; // Posición a la que se moverá el cliente al retirarse
    private NavMeshAgent agente;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        GenerarPedido(); // Genera el pedido al inicio
    }
    private void Update()
    {
    if (agente != null && agente.velocity.magnitude > 0.1f)
    {
        Vector3 direccion = agente.velocity.normalized;
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 10f);
    }
    }


    public void GenerarPedido()
    {
        // Generar el pedido (Pastor o Suaperro)
        string[] tiposPedidos = { "TacoPastor", "TacoSuaperro" };
        pedidoActual = tiposPedidos[Random.Range(0, tiposPedidos.Length)];
        pedidoText.text = $"1 {pedidoActual.Replace("Taco", "taco de ")}"; // Actualiza el texto
    }

    public void AtenderPedido()
    {
        if (!atendido)
        {
            atendido = true;
            pedidoText.text = ""; // Limpia el texto del pedido
            filaController.AvanzarFila(); // Llama al siguiente cliente
            Retirarse(); // Llama al método para que el cliente se retire
        }
    }

    public void Retirarse()
    {
        if (posicionSalida != null)
        {
            Debug.Log($"{gameObject.name} se retira hacia la salida.");
            agente.SetDestination(posicionSalida.position); // Mueve al NPC a la salida

            // Cambiar color como indicador visual
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green; // Cambia el color a verde
            }

            StartCoroutine(EsperarParaDestruir(5f)); // Espera 5 segundos antes de destruir
        }
        else
        {
            Debug.LogWarning("Posición de salida no asignada para el NPC.");
            Destroy(gameObject, 1f); // Destruye inmediatamente si no hay posición de salida
        }
    }

    private IEnumerator EsperarParaDestruir(float tiempo)
    {
        while (tiempo > 0)
        {
            Debug.Log($"NPC {gameObject.name} en posición: {transform.position}");
            yield return new WaitForSeconds(1f);
            tiempo -= 1f;
        }

        Destroy(gameObject); // Destruye el NPC después del tiempo especificado
    }
}
