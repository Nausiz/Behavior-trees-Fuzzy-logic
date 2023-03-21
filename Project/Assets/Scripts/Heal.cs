using UnityEngine;

public class Heal : MonoBehaviour
{
    private const int HEAL = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<PlayerMovementTest>().AddHealth(HEAL);
            other.gameObject.GetComponent<PlayerMovementTest>().Heals += 1;
            Destroy(gameObject);
        }
    }
}
