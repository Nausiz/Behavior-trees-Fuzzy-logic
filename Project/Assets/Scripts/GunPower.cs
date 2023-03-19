using UnityEngine;

public class GunPower : MonoBehaviour
{
    [SerializeField]
    private const int GUNPOWER = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<PlayerMovementTest>().gunpower += GUNPOWER;
            Destroy(gameObject);
        }
    }
}
