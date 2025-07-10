using UnityEngine;
// Exige que o GameObject tenha um componente Renderer para funcionar corretamente
[RequireComponent(typeof(Renderer))]    
public class ScrollTexturaPiso : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Velocidade de rolagem da textura
    private Renderer pisoRenderer; // Referência ao componente Renderer do GameObject

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       pisoRenderer = GetComponent<Renderer>(); // Obtém o componente Renderer do GameObject 
    }

    // Update is called once per frame
    void Update()
    {
        // Calculamos o deslocamento (offset) baseado no tempo e na velocidade.
        // Usamos Mathf.Repeat para o loop ser mais est�vel e n�o depender de n�meros gigantes.
        float offset = Mathf.Repeat(Time.time * scrollSpeed, 1); // Calcula o deslocamento da textura com base no tempo e na velocidade de rolagem 
        // ERRO: "_MainTex" não é a propriedade correta no shader URP Lit.
        //pisoRenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0)); // Aplica o deslocamento � textura principal do material
        // CORRE��O: No shader URP Lit, a propriedade da textura principal se chama "_BaseMap".
        pisoRenderer.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0)); // Aplica o deslocamento � textura principal do material


    }
}
