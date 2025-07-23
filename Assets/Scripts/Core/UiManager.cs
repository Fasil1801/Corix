using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Ui Elements")]
    [SerializeField] private TextMeshProUGUI _matchCount;
    [SerializeField] private TextMeshProUGUI _turnCount;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameWonPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _pauseBtn;
    [SerializeField] GameObject container;
    [SerializeField] string NextLevel = "Medium";

    private int _match = 0;
    private int _turnLeft = 0;
    public static UiManager instance;
    private AudioManager audioManager;
    private CardManager cardManager;

    private bool _isOver = false;
    private bool _isWon = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        audioManager = AudioManager.instance;
        cardManager = CardManager.instance;
        StartCoroutine(ActivePauseButton());
    }
    private void Update()
    {
        if (container.transform.childCount == 0)
        {
            Won();
        }
        if (_turnLeft == 0)
        {
            GameOver();
        }
    }
    public void SetMatch()
    {
        _match += 1;
        _matchCount.text = _match.ToString();
    }
    public void SetTurn()
    {
        _turnLeft -= 1;
        _turnCount.text = _turnLeft.ToString();
    }
    public void SetMaxTurn(int maxTurn)
    {
        _turnLeft = maxTurn;
        _turnCount.text = _turnLeft.ToString();
    }
    public void Restart()
    {
        string currentscene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentscene);
    }
    public void Resume()
    {
        _pausePanel.SetActive(false);
        container.SetActive(true);
        _pauseBtn.SetActive(true);
    }
    public void Pause()
    {
        _pausePanel.SetActive(true);
        container.SetActive(false);
        _pauseBtn.SetActive(false);
    }
    public void Next()
    {
        int buildindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildindex+1);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    void GameOver()
    {
        if (_isOver) return;
        _gameOverPanel.SetActive(true);
        container.SetActive(false);
        audioManager.PlayLose();
        _isOver = true;
    }
    void Won()
    {
        if (_isWon) return;
        _gameWonPanel.SetActive(true);
        container.SetActive(false);
        audioManager.PlayWin();
        _isWon = true;
        PlayerPrefs.SetInt(NextLevel, 1);
        PlayerPrefs.Save();
    }
    IEnumerator ActivePauseButton()
    {
        yield return new WaitForSeconds(cardManager.InitialShowTime);
        _pauseBtn.SetActive(true);
    }
}
