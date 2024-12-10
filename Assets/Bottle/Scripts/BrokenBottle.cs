using UnityEngine;

public class BrokenBottle : MonoBehaviour
{
    [SerializeField] GameObject[] pieces;  // Array of the broken bottle pieces
    [SerializeField] float velMultiplier = 2f;  // Velocity multiplier for the explosion effect
    [SerializeField] float timeBeforeDestroying = 60f;  // Time before the broken pieces get destroyed

    void Start()
    {
        // Destroy the broken bottle after a certain amount of time
        Destroy(this.gameObject, timeBeforeDestroying);
    }

    public void RandomVelocities()
    {
        // Apply random velocities to each broken piece
        for (int i = 0; i < pieces.Length; i++)
        {
            // Generate random velocity components
            float xVel = UnityEngine.Random.Range(0f, 1f);
            float yVel = UnityEngine.Random.Range(0f, 1f);
            float zVel = UnityEngine.Random.Range(0f, 1f);
            Vector3 velocity = new Vector3(velMultiplier * xVel, velMultiplier * yVel, velMultiplier * zVel);

            // Apply the random velocity to each piece's Rigidbody
            pieces[i].GetComponent<Rigidbody>().linearVelocity = velocity;
        }
    }
}
