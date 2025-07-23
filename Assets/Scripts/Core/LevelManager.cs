using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] Button easyButton;
    [SerializeField] Button mediumButton;
    [SerializeField] Button hardButton;

    [SerializeField] GameObject mediumLockIcon;
    [SerializeField] GameObject hardLockIcon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeLevels();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateButtons();
    }
    private void InitializeLevels()
    {
        if (!PlayerPrefs.HasKey("Easy")) PlayerPrefs.SetInt("Easy", 1); 
        if (!PlayerPrefs.HasKey("Medium")) PlayerPrefs.SetInt("Medium", 0);
        if (!PlayerPrefs.HasKey("Hard")) PlayerPrefs.SetInt("Hard", 0);
        PlayerPrefs.Save();
    }

    public void UpdateButtons()
    {
        easyButton.interactable = true;
        mediumButton.interactable = PlayerPrefs.GetInt("Medium",0) == 1;
        mediumLockIcon.SetActive(!mediumButton.interactable);

        hardButton.interactable = PlayerPrefs.GetInt("Hard",0) == 1;
        hardLockIcon.SetActive(!hardButton.interactable);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("Easy");
        PlayerPrefs.DeleteKey("Medium");
        PlayerPrefs.DeleteKey("Hard");
        InitializeLevels();
        UpdateButtons();
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
