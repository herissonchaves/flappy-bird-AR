using UnityEngine;
using System.Collections;

/// <summary>
/// Gerencia a criação procedural de elementos do cenário,
/// como obstáculos no chão e nuvens no céu, em posições e rotações variadas.
/// </summary>
public class GerenciadorDeCenario : MonoBehaviour
{
    [Header("Objetos a Serem Criados")]
    [Tooltip("Lista de prefabs de obstáculos que aparecem no chão (ex: arbusto, pedra).")]
    public GameObject[] prefabsDeObstaculos; 
    [Tooltip("O prefab da nuvem, que terá um tratamento de altura especial.")]
    public GameObject nuvemPrefab; 

    [Header("Referências da Cena")]
    [Tooltip("O objeto vazio que marca a posição inicial de criação.")]
    public Transform pontoDeCriacao; 
    [Tooltip("O objeto pai que agrupará todos os elementos criados, para manter a Hierarquia organizada.")]
    public Transform nodeRootCena;     

    [Header("Configurações de Gameplay")]
    [Tooltip("A altura adicional, a partir do ponto de criação, onde as nuvens devem aparecer.")]
    [SerializeField] private float alturaNuvem = 4.0f;
    [Tooltip("Intervalo em segundos entre a criação de cada novo objeto.")]
    [SerializeField] private float intervaloCriacao = 1.4f;
    [Tooltip("A velocidade com que os objetos se moverão para a frente (use um valor negativo).")]
    [SerializeField] private float velocidade = -75f; 

    // O método Start é chamado uma vez quando o script é habilitado.
    void Start()
    {
        // Inicia a rotina de criação de objetos, que rodará em paralelo ao jogo.
        StartCoroutine(RotinaDeCriarObjetos());
    }

    /// <summary>
    /// Corrotina que executa em um loop infinito para criar objetos de cenário.
    /// </summary>
    private IEnumerator RotinaDeCriarObjetos()
    {
        // Uma validação de segurança para garantir que tudo foi configurado no Inspector.
        // Isso evita erros chatos (NullReferenceException) durante o jogo.
        if (prefabsDeObstaculos.Length == 0 || nuvemPrefab == null || pontoDeCriacao == null || nodeRootCena == null)
        {
            Debug.LogError("ERRO: Uma ou mais referências (Prefabs, Ponto de Criação, Node Root) não foram definidas no Inspector! A criação de objetos foi cancelada.", this.gameObject);
            yield break; // 'yield break' para completamente a execução desta corrotina.
        }

        // Loop infinito que continuará enquanto o objeto estiver ativo na cena.
        while (true)
        {
            // --- 1. Calcular Posições e Rotações Aleatórias ---
            float giroObstaculo = Random.Range(-180f, 180f);
            float giroNuvem = (Random.value > 0.5f) ? 180f : 0f; // Sorteia se a nuvem estará virada ou não.
            float posicaoX = (Random.value > 0.5f) ? 0.45f : -0.45f; // Sorteia uma das duas pistas para a posição X.

            // --- 2. Escolher qual objeto criar ---
            GameObject prefabEscolhido = EscolherObjetoAleatorio();
            
            // --- 3. Calcular a Posição Y Final ---
            float posicaoYFinal;
            if (prefabEscolhido == nuvemPrefab)
            {
                // Se for uma nuvem, ela nasce no alto.
                posicaoYFinal = pontoDeCriacao.position.y + alturaNuvem;
            }
            else
            {
                // Se for um obstáculo, ela nasce no chão.
                posicaoYFinal = pontoDeCriacao.position.y;
            }

            // Monta o vetor da posição final de criação.
            Vector3 posicaoInicial = new Vector3(posicaoX, posicaoYFinal, pontoDeCriacao.position.z);
            
            // --- 4. Criar o Objeto na Cena ---
            GameObject novoObjeto = Instantiate(prefabEscolhido, posicaoInicial, Quaternion.identity, nodeRootCena);

            // --- 5. Aplicar Rotação e Força ---
            if (prefabEscolhido == nuvemPrefab)
            {
                novoObjeto.transform.rotation = Quaternion.Euler(-90, giroNuvem, 0);
            }
            else // Se for um arbusto, pedra, etc.
            {
                novoObjeto.transform.rotation = Quaternion.Euler(-90, giroObstaculo, 0);
            }

            // Pega o componente Rigidbody para aplicar a força.
            Rigidbody rb = novoObjeto.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Usa AddRelativeForce para que a força seja aplicada na "frente" do objeto, respeitando sua rotação.
                rb.AddRelativeForce(new Vector3(0, 0, velocidade), ForceMode.Force);
            }
            
            // --- 6. Pausar a Rotina ---
            // Espera o tempo definido antes de começar a próxima iteração do loop.
            yield return new WaitForSeconds(intervaloCriacao);
        }
    }

    /// <summary>
    /// Sorteia qual prefab será criado, com uma chance de 50% para nuvem e 50% para um obstáculo do chão.
    /// </summary>
    /// <returns>O GameObject do prefab escolhido.</returns>
    private GameObject EscolherObjetoAleatorio()
    {
        // Random.value retorna um float entre 0.0 e 1.0. É uma ótima forma de ter uma chance de 50%.
        if (Random.value > 0.5f)
        {
            return nuvemPrefab;
        }
        else 
        {
            // Sorteia um índice aleatório do array de obstáculos do chão.
            int indice = Random.Range(0, prefabsDeObstaculos.Length);
            return prefabsDeObstaculos[indice];
        }
    }
}