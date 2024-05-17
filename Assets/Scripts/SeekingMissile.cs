using UnityEngine;

public class SeekingMissile : MonoBehaviour
{
    [SerializeField] float launchSpeed;
    [SerializeField] float speed;
    [SerializeField] AudioClip missileLaunch;
    [SerializeField] AudioClip missileExplosion;
    private GameObject target;
    private Rigidbody rocketRigidbody;
    private float selfDestructionTimer = 3f;
    public void SetTarget(GameObject targetObject)
    {
        target = targetObject;
    }

    private void Start()
    {
        if (target != null)
        {
            AudioSource.PlayClipAtPoint(missileLaunch, transform.position);
            rocketRigidbody = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        selfDestructionTimer -= Time.deltaTime;
        if (selfDestructionTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.LookAt(target.transform.position);
            rocketRigidbody.AddForce(direction * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(missileExplosion, transform.position);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
