using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneMenu : MonoBehaviour
{
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonReset;
    [SerializeField] private TextMeshProUGUI textRounds;
    [SerializeField] private TextMeshProUGUI textTime;

    [SerializeField] private TextMeshProUGUI textWinsNPC1;
    [SerializeField] private TextMeshProUGUI textPowerUpsNPC1;
    [SerializeField] private TextMeshProUGUI textHealsNPC1;

    [SerializeField] private TextMeshProUGUI textWinsNPC2;
    [SerializeField] private TextMeshProUGUI textPowerUpsNPC2;
    [SerializeField] private TextMeshProUGUI textHealsNPC2;

    [SerializeField] private NPC npc1;
    [SerializeField] private NPC npc2;

    [SerializeField] private GameObject[] healsPacksNPC1;
    [SerializeField] private GameObject[] healsPacksNPC2;
    
    [SerializeField] private GameObject[] gunPowersPacksNPC1;
    [SerializeField] private GameObject[] gunPowersPacksNPC2;

    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        Button btnBack = buttonBack.GetComponent<Button>();
        btnBack.onClick.AddListener(OnClickBack);

        Button btnReset = buttonReset.GetComponent<Button>();
        btnReset.onClick.AddListener(OnClickReset);

        textRounds.text = "0";
        textTime.text = "00:00";

        ResetPowerUps();
    }

    void Update()
    {
        //Time
        timer += Time.deltaTime;

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.RoundToInt(timer % 60).ToString("00");

        textTime.text = string.Format("{0}:{1}", minutes, seconds);

        //Rounds
        if ((npc1.Wins + npc2.Wins + 1).ToString() != textRounds.text)
        {
            textRounds.text = (npc1.Wins + npc2.Wins + 1).ToString();
            ResetPowerUps();
        }

        //NPC1
        if (npc1.Wins.ToString()!= textWinsNPC1.text)
            textWinsNPC1.text = npc1.Wins.ToString();
        if (npc1.PowerUps.ToString() != textPowerUpsNPC1.text)
            textPowerUpsNPC1.text = npc1.PowerUps.ToString();
        if (npc1.Heals.ToString() != textHealsNPC1.text)
            textHealsNPC1.text = npc1.Heals.ToString();

        //NPC2
        if (npc2.Wins.ToString() != textWinsNPC2.text)
            textWinsNPC2.text = npc2.Wins.ToString();
        if (npc2.PowerUps.ToString() != textPowerUpsNPC2.text)
            textPowerUpsNPC2.text = npc2.PowerUps.ToString();
        if (npc2.Heals.ToString() != textHealsNPC2.text)
            textHealsNPC2.text = npc2.Heals.ToString();
    }

    void OnClickBack()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    void OnClickReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    void ResetHealsPacks(GameObject[] healsPacks)
    {
        foreach (GameObject healPack in healsPacks)
        {
            healPack.SetActive(false);
        }


        int firstRandomPackIndex = Random.Range(0, healsPacks.Length);
        healsPacks[firstRandomPackIndex].SetActive(true);

        int secondRandomPackIndex;
        do
        {
            secondRandomPackIndex = Random.Range(0, healsPacks.Length);

        } while (firstRandomPackIndex == secondRandomPackIndex);
        healsPacks[secondRandomPackIndex].SetActive(true);
    }

    void ResetGunPowersPacks(GameObject[] gunPowersPacks)
    {
        foreach (GameObject gunPowerPack in gunPowersPacks)
        {
            gunPowerPack.SetActive(false);
        }


        int firstRandomPackIndex = Random.Range(0, gunPowersPacks.Length);
        gunPowersPacks[firstRandomPackIndex].SetActive(true);

        int secondRandomPackIndex;
        do
        {
            secondRandomPackIndex = Random.Range(0, gunPowersPacks.Length);

        } while (firstRandomPackIndex == secondRandomPackIndex);
        gunPowersPacks[secondRandomPackIndex].SetActive(true);
    }

    void ResetPowerUps()
    {
        ResetHealsPacks(healsPacksNPC1);
        ResetHealsPacks(healsPacksNPC2);

        ResetGunPowersPacks(gunPowersPacksNPC1);
        ResetGunPowersPacks(gunPowersPacksNPC2);
    }
}
