using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private SpawnManager spawnManager;
    private Rigidbody enemyRigidbody;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    void Start()
    {
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>();
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRigidbody.AddForce(lookDirection * speed);
    }

    private void OnDestroy()
    {
        spawnManager.ReduceAliveEnemies();
    }
}
