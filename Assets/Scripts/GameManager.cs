
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject missilePrefab;
    public Camera mainCamera;
    public float spawnInterval = 3f;
    public int baseMissiles = 1;
    private float spawnTimer = 0f;
    private float survivalTime = 0f;

    void Start()
    {
        // Find the camera if none is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // if missing
        if (missilePrefab == null || mainCamera == null)
            return;

        // timer for player 
        survivalTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            // add more missile for every 10s
            int difficultyLevel =
                Mathf.FloorToInt(survivalTime / 10f);

            int missileCount = baseMissiles + difficultyLevel;

            // Spawn the missiles for this round
            for (int i = 0; i < missileCount; i++)
            {
                SpawnMissile();
            }
        }
    }

    void SpawnMissile()
    {
        // Choose a random side of the screen
        int edge = Random.Range(0, 4);
        Vector3 viewportPosition;

        if (edge == 0)
            viewportPosition = new Vector3(-0.15f, Random.value, 0f);
        else if (edge == 1)
            viewportPosition = new Vector3(1.15f, Random.value, 0f);
        else if (edge == 2)
            viewportPosition = new Vector3(Random.value, -0.15f, 0f);
        else
            viewportPosition = new Vector3(Random.value, 1.15f, 0f);

        // Find the player
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null)
            return;

        float depth = Vector3.Dot(
            playerObject.transform.position - mainCamera.transform.position,
            mainCamera.transform.forward
        );

        // spawns in front of the camera
        viewportPosition.z = Mathf.Max(depth, 10f);

        // screen position to world position
        Vector3 spawnPosition =
            mainCamera.ViewportToWorldPoint(viewportPosition);

        // spawnn missile at that pos
        Instantiate(
            missilePrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
