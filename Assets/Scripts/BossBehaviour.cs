using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] float pushForce;
    [SerializeField] AudioClip bigBoing;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
            AudioSource.PlayClipAtPoint(bigBoing, transform.position);

            if (collision.gameObject.GetComponent<Rigidbody>() != null)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }            
        }
    }
}
