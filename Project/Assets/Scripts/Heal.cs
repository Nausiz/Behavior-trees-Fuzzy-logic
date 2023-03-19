using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private const int HEAL = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<PlayerMovementTest>().hp += HEAL;
            Destroy(gameObject);
        }
    }
}
