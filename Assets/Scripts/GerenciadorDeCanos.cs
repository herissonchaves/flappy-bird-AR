using UnityEngine;
using System.Collections; // Necessário para usar IEnumerator e corrotinas

public class GerenciadorDeCanos : MonoBehaviour
{
    [Header("Configurações de Criação de Canos")]
    public GameObject canoPrefab; // Referência ao prefab do cano
    public Transform pontoDeCriacao; // Ponto onde os canos serão criados
    public Transform nodeRootCena; // Referência ao GameObject que contém os objetos a serem movidos

    [Header("Configurações de Tempo e Velocidade")]
    [SerializeField] private float intervaloCriacaoCano = 5.13f; // Intervalo de criação de canos
    [SerializeField] private float velocidadeDoCano = -75f; // Velocidade com que os canos se movem
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RotinaDeCanos()); // Inicia a rotina de criação de canos
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RotinaDeCanos()
    {
        if (canoPrefab == null || pontoDeCriacao == null || nodeRootCena == null)
        {
            Debug.LogError("canoPrefab, PontoDeCriacao ou NodeRootCena não estão atribuídos!");
            yield break; // Sai da corrotina se as referências não estiverem atribuídas
        }
        while (true)
        {
            var randCano = Random.Range(-1f, 0.0f);
            GameObject novoCano = Instantiate(canoPrefab, pontoDeCriacao.position, Quaternion.identity, nodeRootCena);
            Rigidbody rb = novoCano.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.position = new Vector3(pontoDeCriacao.position.x, randCano, pontoDeCriacao.position.z);
                
                rb.AddForce(new Vector3(0, 0, velocidadeDoCano), ForceMode.Force);
            }
            else
            {
                Debug.LogWarning("O prefab do cano não possui um Rigidbody!", novoCano);
            }
            yield return new WaitForSeconds(intervaloCriacaoCano);

        }

    }
}
