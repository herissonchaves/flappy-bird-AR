using UnityEngine;
using System.Collections; // Necess�rio para usar IEnumerator e corrotinas

public class actionCenaPrincipal : MonoBehaviour
{
    // usar [SerializeField] para expor vari�veis privadas no Inspector do Unity.  � uma boa pr�tica para manter vari�veis privadas mas vis�veis no Inspector.
    [SerializeField] private float scrollSpeed = -0.5f;
    [SerializeField] private float velocidadeObjeto = -75f;
    [SerializeField] private float posicaoZInicialObjetos = 2.5f; // Posi��o Z inicial dos objetos
    [SerializeField] private float intervaloCriacaoCerca = 1f; // Intervalo de cria��o de cercas

    [Tooltip("Material do piso para o efeito de rolagem")] // Adiciona uma dica no Inspector. Coment�rio para descrever o prop�sito da vari�vel
    public Material materialPiso; // Material que ser� aplicado ao GameObject
    [Tooltip("Objeto pai para organizar as cercas criadas.")]
    public GameObject nodeRootCena; // Refer�ncia ao GameObject que cont�m os objetos a serem movidos
    [Tooltip("Prefab da cerca que ser� criado.")]
    public GameObject cercaPrefab; // Refer�ncia ao GameObject da cerca
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1; // Garante que o tempo esteja em escala normal
        StartCoroutine(RotinaDeCriarCercas()); // Inicia a rotina de cria��o de cercas


    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed; // Calcula o deslocamento da textura com base no tempo e na velocidade de rolagem   
        materialPiso.SetTextureOffset("_BaseMap", new Vector2(offset, 0)); // Aplica o deslocamento � textura principal do material
    }

    // Corrotinas s�o m�todos do tipo IEnumerator que permitem pausas na execu��o
    IEnumerator RotinaDeCriarCercas()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de come�ar a criar cercas

        while (true) // Loop infinito para criar cercas continuamente
        {
            GameObject novoObjeto = Instantiate(cercaPrefab, nodeRootCena.transform); // Cria uma nova cerca como filho do nodeRootCena
            novoObjeto.transform.position = new Vector3(-0.75f, 0, posicaoZInicialObjetos); // Define a posi��o inicial da cerca
            novoObjeto.transform.rotation = Quaternion.Euler(-90, 0, 0); // Define a rota��o da cerca para zero

            Rigidbody rb = novoObjeto.GetComponent<Rigidbody>(); // Obt�m o componente Rigidbody do novo objeto
            if (rb != null)
            {
                rb.AddForce(new Vector3(0, 0, velocidadeObjeto), ForceMode.Force); // Aplica uma for�a ao Rigidbody para mover a cerca
            }
            else
            {
                Debug.LogWarning("O prefab da cerca n�o possui um Rigidbody!", novoObjeto); // Aviso se o Rigidbody n�o estiver presente
            }
            yield return new WaitForSeconds(intervaloCriacaoCerca); // Espera o intervalo definido antes de criar a pr�xima cerca
        }
    }
}