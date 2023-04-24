using UnityEngine;

public class Heal : MonoBehaviour
{
    private const int HEAL = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<NPC>().AddHealth(HEAL);
            other.gameObject.GetComponent<NPC>().Heals += 1;
            gameObject.SetActive(false);
        }
    }
}
