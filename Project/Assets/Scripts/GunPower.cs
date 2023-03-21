using UnityEngine;

public class GunPower : MonoBehaviour
{
    private const int GUNPOWER = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<PlayerMovementTest>().GunPower += GUNPOWER;
            other.gameObject.GetComponent<PlayerMovementTest>().PowerUps += 1;
            Destroy(gameObject);
        }
    }
}
