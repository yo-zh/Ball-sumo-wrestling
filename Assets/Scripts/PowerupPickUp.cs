using System.Collections;
using UnityEngine;

public class PowerupPickUp : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject powerupIndicator;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        powerupIndicator = GameObject.Find("PowerupIndicator");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && playerController.currentPowerUp == PowerupType.None)
        {
            PowerupType powerupName = other.GetComponent<Powerup>().powerupType;
            playerController.SetCurrentPowerup(powerupName);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
            AdjustPowerupIndicator(powerupName);
        }
    }

    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSecondsRealtime(10);
        playerController.SetCurrentPowerup(PowerupType.None);
        AdjustPowerupIndicator(PowerupType.None);
    }

    public void AdjustPowerupIndicator(PowerupType powerupType)
    {
        switch (powerupType)
        {
            case PowerupType.None:
                for (int i = 0; i < powerupIndicator.transform.childCount; i++)
                {
                    powerupIndicator.transform.GetChild(i).gameObject.SetActive(false);
                }
                break;
            case PowerupType.Bouncy:
                for (int i = 0; i < powerupIndicator.transform.childCount; i++)
                {
                    GameObject currentChild = powerupIndicator.transform.GetChild(i).gameObject;
                    if (currentChild.name == "Bouncy")
                    {
                        currentChild.gameObject.SetActive(true);
                    }
                }
                break;
            case PowerupType.Cannon:
                for (int i = 0; i < powerupIndicator.transform.childCount; i++)
                {
                    GameObject currentChild = powerupIndicator.transform.GetChild(i).gameObject;
                    if (currentChild.name == "Cannon")
                    {
                        currentChild.gameObject.SetActive(true);
                    }
                }
                break;
            case PowerupType.GroundPound:
                for (int i = 0; i < powerupIndicator.transform.childCount; i++)
                {
                    GameObject currentChild = powerupIndicator.transform.GetChild(i).gameObject;
                    if (currentChild.name == "GroundPound")
                    {
                        currentChild.gameObject.SetActive(true);
                    }
                }
                break;
        }
    }
}
