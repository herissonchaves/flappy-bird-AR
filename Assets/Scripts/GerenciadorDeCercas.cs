using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator e corrotinas

public class GerenciadorDeCercas : MonoBehaviour
{
    [Header("Configuracões de Criação de Cercas")]
    public GameObject cercaPrefab; // Referência ao prefab da cerca
    public Transform pontoDeCriacao; // Ponto onde as cercas serão criadas
    public Transform nodeRootCena; // Referência ao GameObject que cont�m os objetos a serem movidos

    [Header("Configura��es de Tempo e Velocidade")]
    [SerializeField] private float intervaloCriacaoCerca = 1.34f; // Intervalo de cria��o de cercas
    [SerializeField] private float velocidadeDaCerca = -75f; // Velocidade com que as cercas se movem

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RotinaDeCriarCercas()); // Inicia a rotina de criação de cercas
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RotinaDeCriarCercas()
    {
        // Verifica se as refer�ncias est�o atribu�das
        if (cercaPrefab == null || pontoDeCriacao == null || nodeRootCena == null)
        {
            Debug.LogError("CercaPrefab, PontoDeCriacao ou NodeRootCena n�o est�o atribu�dos!");
            yield break; // Sai da corrotina se as refer�ncias n�o estiverem atribu�das
        }

        while (true) // Loop infinito para criar cercas continuamente
        {
            GameObject novaCerca = Instantiate(cercaPrefab, pontoDeCriacao.position, Quaternion.identity, nodeRootCena); // Cria uma nova cerca
            novaCerca.transform.rotation = Quaternion.Euler(-90, 0, 0); // Define a rota��o da cerca
            Rigidbody rb = novaCerca.GetComponent<Rigidbody>(); // Obt�m o componente Rigidbody do novo objeto
            if (rb != null)
            {
                rb.AddForce(new Vector3(0, 0, velocidadeDaCerca), ForceMode.Force); // Aplica uma for�a ao Rigidbody para mover a cerca
            }
            else
            {
                Debug.LogWarning("O prefab da cerca n�o possui um Rigidbody!", novaCerca); // Aviso se o Rigidbody n�o estiver presente
            }
            yield return new WaitForSeconds(intervaloCriacaoCerca); // Espera o intervalo definido antes de criar a pr�xima cerca
        }
    }
        
    
}
