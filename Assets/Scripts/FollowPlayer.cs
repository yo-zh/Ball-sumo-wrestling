using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private SpawnManager spawnManager;
    private Rigidbody enemyRigidbody;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] AudioClip smallBoing;
    [SerializeField] AudioClip barrierBounce;
    void Start()
    {
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>();
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && smallBoing)
        {
            AudioSource.PlayClipAtPoint(smallBoing, transform.position);
        }
        else if (collision.gameObject.CompareTag("Barrier"))
        {
            AudioSource.PlayClipAtPoint(barrierBounce, transform.position);
        }
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
