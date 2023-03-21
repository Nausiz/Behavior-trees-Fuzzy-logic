using UnityEngine;

public class GunPower : MonoBehaviour
{
    private const int GUNPOWER = 15;

    [SerializeField] private SceneMenu sceneMenu;

    void Start()
    {
        sceneMenu.RoundEnded += OnRoundEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<PlayerMovementTest>().GunPower += GUNPOWER;
            other.gameObject.GetComponent<PlayerMovementTest>().PowerUps += 1;
            gameObject.SetActive(false);
        }
    }

    private void OnRoundEnd()
    {
        gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        sceneMenu.RoundEnded -= OnRoundEnd;
    }
}
