using UnityEngine;

public class GunPower : MonoBehaviour
{
    private const int GUNPOWER = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<NPC>().GunPower += GUNPOWER;
            other.gameObject.GetComponent<NPC>().PowerUps += 1;
            gameObject.SetActive(false);
        }
    }
}
