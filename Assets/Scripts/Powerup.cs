using UnityEngine;

public enum PowerupType
{
    None,
    Bouncy,
    Cannon,
    GroundPound
}

public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;
}
