using UnityEngine;

public class SeekingMissile : MonoBehaviour
{
    [SerializeField] float launchSpeed;
    [SerializeField] float speed;
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
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
