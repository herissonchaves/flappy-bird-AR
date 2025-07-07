using UnityEngine;

public class actionCenaPrincipal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float scrollSpeed;
    public Material materialPiso;
    void Start()
    {
        Time.timeScale = 1f; // Ensure the game runs at normal speed
        scrollSpeed = -0.5f; // Set the speed of the scrolling texture
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed; // Calculate the offset based on time and speed
        materialPiso.SetTextureOffset("_MainTex", new Vector2(offset,0)); // Update the texture offset

    }
}
