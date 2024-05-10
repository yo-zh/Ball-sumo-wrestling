using UnityEngine;

public class PowerupSpin : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Update()
    {
        transform.position = player.transform.position;
        transform.Rotate(0, 0.25f, 0);
    }
}
