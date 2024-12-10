using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] GameObject brokenBottlePrefab;   // The prefab for the broken bottle
    
    // This function is called when the bottle is clicked
    void OnMouseDown()
    {
        // Call the explode method when the bottle is clicked
        Explode();
    }

    // Method to explode the bottle (create broken bottle prefab and destroy the bottle)
    public void Explode()
    {
        // Instantiate the broken bottle at the position of the original bottle
        Instantiate(brokenBottlePrefab, transform.position, transform.rotation);

        // Destroy the original bottle object
        Destroy(gameObject);
    }
}
