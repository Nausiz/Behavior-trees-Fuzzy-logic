using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Button buttonBTvsBT;
    [SerializeField]
    private Button buttonBTvsFL;
    [SerializeField]
    private Button buttonFLvsFL;

    // Start is called before the first frame update
    void Start()
    {
        Button btnBTvsBT = buttonBTvsBT.GetComponent<Button>();
        btnBTvsBT.onClick.AddListener(OnClickBTvsBT);

        Button btnBTvsFL = buttonBTvsFL.GetComponent<Button>();
        btnBTvsFL.onClick.AddListener(OnClickBTvsFL);

        Button btnFLvsFL = buttonFLvsFL.GetComponent<Button>();
        btnFLvsFL.onClick.AddListener(OnClickFLvsFL);
    }

    void OnClickBTvsBT()
    {
        SceneManager.LoadScene("BTvsBT", LoadSceneMode.Single);
    }

    void OnClickBTvsFL()
    {
        SceneManager.LoadScene("BTvsFL", LoadSceneMode.Single);
    }

    void OnClickFLvsFL()
    {
        SceneManager.LoadScene("FLvsFL", LoadSceneMode.Single);
    }

}
