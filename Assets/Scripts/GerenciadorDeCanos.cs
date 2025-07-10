using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator e corrotinas

public class GerenciadorDeCanos : MonoBehaviour
{
    [Header("Configurações de Criação de Canos")]
    public GameObject canoPrefab; // Referência ao prefab do cano
    public Transform pontoDeCriacao; // Ponto onde os canos ser�o criados
    public Transform nodeRootCena; // Referência ao GameObject que cont�m os objetos a serem movidos

    [Header("Configurações de Tempo e Velocidade")]
    [SerializeField] private float intervaloCriacaoCano = 5.13f; // Intervalo de cria��o de canos
    [SerializeField] private float velocidadeDoCano = -75f; // Velocidade com que os canos se movem
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RotinaDeCanos()); // Inicia a rotina de cria��o de canos
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RotinaDeCanos()
    {
        if (canoPrefab == null || pontoDeCriacao == null || nodeRootCena == null)
        {
            Debug.LogError("canoPrefab, PontoDeCriacao ou NodeRootCena n�o est�o atribu�dos!");
            yield break; // Sai da corrotina se as refer�ncias n�o estiverem atribu�das
        }
        while (true)
        {
            var randCano = Random.Range(2f, -1f);
            GameObject novoCano = Instantiate(canoPrefab, pontoDeCriacao.position, Quaternion.identity, nodeRootCena);
            Rigidbody rb = novoCano.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.position = new Vector3(pontoDeCriacao.position.x, randCano, pontoDeCriacao.position.z);
               // print(randCano);
                rb.AddForce(new Vector3(0, 0, velocidadeDoCano), ForceMode.Force);
            }
            else
            {
                Debug.LogWarning("O prefab do cano n�o possui um Rigidbody!", novoCano);
            }
            yield return new WaitForSeconds(intervaloCriacaoCano);

        }

    }
}
