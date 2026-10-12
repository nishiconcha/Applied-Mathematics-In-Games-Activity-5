
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float turnSpeed = 3f;
    public float lifeTime = 5f;
    public float hitDistance = 0.5f;
    private Transform target;
    private float timer = 0f;

    void Start()
    {
        // Find the player using its tag
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        // Save the player's position reference
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
    }

    void Update()
    {
        // Count how long the missile has existed
        timer += Time.deltaTime;

        // Destroy the missile after 5 seconds
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        // Destroy the missile if there is no player
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Get the direction and distance to the player
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        // Check if the missile is close enough to hit
        if (distance <= hitDistance)
        {
            // Get the player's script
            Player playerScript = target.GetComponent<Player>();

            // Tell the player that they got hit
            if (playerScript != null)
            {
                playerScript.TakeHit();
            }

            // Destroy the missile after hitting
            Destroy(gameObject);
            return;
        }

        // Find the rotation needed to face the player
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            // Turn smoothly toward the player
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );
        }

        // Move forward in the direction the missile faces
        transform.position += transform.forward
            * moveSpeed * Time.deltaTime;
    }
}
