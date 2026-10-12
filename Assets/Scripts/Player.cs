
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 60f;
    public float bankAngle = 25f;
    public int maxHits = 5;
    private int hitCount = 0;
    private float heading = 0f;

    void Update()
    {
        // left rifght input
        float horizontal = Input.GetAxis("Horizontal");
        heading += horizontal * turnSpeed * Time.deltaTime;

        // Tilt like an airplane
        Quaternion targetRotation = Quaternion.Euler(
            0f,
            heading,
            -horizontal * bankAngle
        );

        // smooth rotation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            5f * Time.deltaTime
        );

        // forward
        transform.position += transform.forward
            * moveSpeed * Time.deltaTime;
    }

    public void TakeHit()
    {
        // Add one hit
        hitCount++;

        // debug for hits
        Debug.Log("Hits: " + hitCount + "/" + maxHits);

        // Restart game after 5 hits
        if (hitCount >= maxHits)
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}
