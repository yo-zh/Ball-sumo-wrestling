using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void GameState();
    public static event GameState GameOver;

    [SerializeField] float speed;
    [SerializeField] GameObject cameraFocus;
    [SerializeField] float powerupStrength = 1.0f;
    [SerializeField] GameObject rocketPrefab;

    private float forwardInput;
    private Rigidbody playerRigidBody;

    public PowerupType currentPowerUp = PowerupType.None;
    public void SetCurrentPowerup(PowerupType value) { currentPowerUp = value; }

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        SetCurrentPowerup(PowerupType.None);
    }

    void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical");
        playerRigidBody.AddForce(forwardInput * speed * cameraFocus.transform.forward);
    }

    private void Update()
    {
        if (transform.position.y < -5) 
        {
            GameOver?.Invoke();
        }
        if (currentPowerUp == PowerupType.Cannon)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject rocketTarget = FindClosestEnemy();
                if (rocketTarget != null)
                {
                    GameObject newRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
                    Debug.Log("Pew pew pew", newRocket);
                    newRocket.GetComponent<SeekingMissile>().SetTarget(rocketTarget);
                }
            }
        }
        else if (currentPowerUp == PowerupType.GroundPound)
        {
            StartCoroutine(GroundPound());
        }
    }

    IEnumerator GroundPound()
    {
        SetCurrentPowerup(PowerupType.None);
        playerRigidBody.AddForce(speed * Vector3.up, ForceMode.Impulse);
        yield return new WaitForSecondsRealtime(0.5f);
        playerRigidBody.isKinematic = true;
        yield return new WaitForSecondsRealtime(2f);
        playerRigidBody.isKinematic = false;
        playerRigidBody.AddForce(speed * Vector3.down * 5, ForceMode.Impulse);
        PushEnemiesAway();
        yield return new WaitForSecondsRealtime(0.25f);
        GetComponent<PowerupPickUp>().AdjustPowerupIndicator(PowerupType.None);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerupType.Bouncy) // TODO переделать под новый список усилений
        {
            PushAway(collision.gameObject);
        }
    }

    private void PushAway(GameObject target)
    {
        Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (target.transform.position - transform.position).normalized;
        targetRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in aliveEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        return closestEnemy;
    }
    private void PushEnemiesAway()
    {
        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in aliveEnemies)
        {
            Vector3 pushDirection = (enemy.transform.position - transform.position).normalized;
            enemy.GetComponent<Rigidbody>().AddForce(pushDirection * 1000, ForceMode.Impulse);
        }
    }
}
