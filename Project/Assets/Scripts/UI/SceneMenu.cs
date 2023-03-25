using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float timer;

    public event Action RoundEnded;


    // Start is called before the first frame update
    void Start()
    {
        Button btnBack = buttonBack.GetComponent<Button>();
        btnBack.onClick.AddListener(OnClickBack);

        Button btnReset = buttonReset.GetComponent<Button>();
        btnReset.onClick.AddListener(OnClickReset);

        textRounds.text = "0";
        textTime.text = "00:00";
    }

    void Update()
    {
        //Time
        timer += Time.deltaTime;

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.RoundToInt(timer % 60).ToString("00");

        textTime.text = string.Format("{0}:{1}", minutes, seconds);

        //Rounds
        //if ((npc1.Wins + npc2.Wins).ToString() != textRounds.text)
        //    {
        //         textRounds.text = (npc1.Wins + npc2.Wins).ToString();
        //         RoundEnded?.Invoke();

        //NPC1
        if (npc1.Wins.ToString()!= textWinsNPC1.text)
            textWinsNPC1.text = npc1.Wins.ToString();
        if (npc1.PowerUps.ToString() != textPowerUpsNPC1.text)
            textPowerUpsNPC1.text = npc1.PowerUps.ToString();
        if (npc1.Heals.ToString() != textHealsNPC1.text)
            textHealsNPC1.text = npc1.Heals.ToString();

        //NPC2
        //if (npc2.Wins.ToString() != textWinsNPC2.text)
        //    textWinsNPC2.text = npc2.Wins.ToString();
        //if (npc2.PowerUps.ToString() != textPowerUpsNPC2.text)
        //    textPowerUpsNPC2.text = npc2.PowerUps.ToString();
        //if (npc2.Heals.ToString() != textHealsNPC2.text)
        //    textHealsNPC2.text = npc2.Heals.ToString();
    }

    void OnClickBack()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    void OnClickReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
