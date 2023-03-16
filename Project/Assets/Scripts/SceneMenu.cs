
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenu : MonoBehaviour
{
    [SerializeField]
    private Button buttonBack;
    [SerializeField]
    private Button buttonReset;

    // Start is called before the first frame update
    void Start()
    {
        Button btnBack = buttonBack.GetComponent<Button>();
        btnBack.onClick.AddListener(OnClickBack);

        Button btnReset = buttonReset.GetComponent<Button>();
        btnReset.onClick.AddListener(OnClickReset);
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
