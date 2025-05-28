using UnityEngine;

public class MaterialOffset : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // Units per second
    public float scrollSpeed2 = 0.05f;
    private Material material;
    private Vector2 currentOffset = new Vector2(1f, 1f);

    void Start()
    {
        material = GetComponent<Renderer>().material;
        
        material.mainTextureOffset = currentOffset;
    }

    void Update()
    {
        // Increase both offsets
        currentOffset.x += scrollSpeed * Time.deltaTime;
        currentOffset.y += scrollSpeed2 * Time.deltaTime;

        // Reset if either exceeds or hits 5
        if (currentOffset.x >= 5f) currentOffset.x = 1f;
        if (currentOffset.y >= 5f) currentOffset.y = 1f;

        // Apply the updated offset
        material.mainTextureOffset = currentOffset;
    }
}