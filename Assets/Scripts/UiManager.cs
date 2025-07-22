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
    [SerializeField] GameObject container;

    private int _match = 0;
    private int _turnLeft = 0;
    public static UiManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if (container.transform.childCount == 0)
        {
            _gameWonPanel.SetActive(true);
            container.SetActive(false);
        }
        if (_turnLeft == 0)
        {
            _gameOverPanel.SetActive(true);
            container.SetActive(false);
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
    }
    public void Pause()
    {
        _pausePanel.SetActive(true);
        container.SetActive(false);
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
}
