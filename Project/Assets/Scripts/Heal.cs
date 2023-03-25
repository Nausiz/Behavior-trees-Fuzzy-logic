using UnityEngine;

public class Heal : MonoBehaviour
{
    private const int HEAL = 25;

    [SerializeField] private SceneMenu sceneMenu;

    void Start()
    {
        sceneMenu.RoundEnded += OnRoundEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<NPC>().AddHealth(HEAL);
            other.gameObject.GetComponent<NPC>().Heals += 1;
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
