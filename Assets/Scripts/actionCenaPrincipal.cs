using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator e corrotinas

public class actionCenaPrincipal : MonoBehaviour
{
    // usar [SerializeField] para expor variáveis privadas no Inspector do Unity.  é uma boa prática para manter variáveis privadas mas visíveis no Inspector.
    [SerializeField] private float scrollSpeed = -0.5f;
    [SerializeField] private float velocidadeObjeto = -75f;
    [SerializeField] private float posicaoZInicialObjetos = 2.5f; // Posição Z inicial dos objetos
    [SerializeField] private float intervaloCriacaoCerca = 1f; // Intervalo de criação de cercas

    [Tooltip("Material do piso para o efeito de rolagem")] // Adiciona uma dica no Inspector. Comentário para descrever o propósito da variável
    public Material materialPiso; // Material que será aplicado ao GameObject
    [Tooltip("Objeto pai para organizar as cercas criadas.")]
    public GameObject nodeRootCena; // Referência ao GameObject que contém os objetos a serem movidos
    [Tooltip("Prefab da cerca que será criado.")]
    public GameObject cercaPrefab; // Referência ao GameObject da cerca
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1; // Garante que o tempo esteja em escala normal
        StartCoroutine(RotinaDeCriarCercas()); // Inicia a rotina de criação de cercas


    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed; // Calcula o deslocamento da textura com base no tempo e na velocidade de rolagem   
        materialPiso.SetTextureOffset("_BaseMap", new Vector2(offset, 0)); // Aplica o deslocamento à textura principal do material
    }

    // Corrotinas são métodos do tipo IEnumerator que permitem pausas na execução
    IEnumerator RotinaDeCriarCercas()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de começar a criar cercas

        while (true) // Loop infinito para criar cercas continuamente
        {
            GameObject novoObjeto = Instantiate(cercaPrefab, nodeRootCena.transform); // Cria uma nova cerca como filho do nodeRootCena
            novoObjeto.transform.position = new Vector3(-0.75f, 0, posicaoZInicialObjetos); // Define a posição inicial da cerca
            novoObjeto.transform.rotation = Quaternion.Euler(-90, 0, 0); // Define a rotação da cerca para zero

            Rigidbody rb = novoObjeto.GetComponent<Rigidbody>(); // Obtém o componente Rigidbody do novo objeto
            if (rb != null)
            {
                rb.AddForce(new Vector3(0, 0, velocidadeObjeto), ForceMode.Force); // Aplica uma força ao Rigidbody para mover a cerca
            }
            else
            {
                Debug.LogWarning("O prefab da cerca não possui um Rigidbody!", novoObjeto); // Aviso se o Rigidbody não estiver presente
            }
            yield return new WaitForSeconds(intervaloCriacaoCerca); // Espera o intervalo definido antes de criar a próxima cerca
        }
    }
}