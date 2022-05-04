using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private float _startSpawnY, _spawnOffsetY;

    [SerializeField]
    private List<GameObject> _obstacles;

    [SerializeField]
    private TMP_Text _scoreText, _endScoreText, _highScoreText;

    [SerializeField]
    private GameObject _endPanel;

    private int score;
    private float currentSpawnPosY;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentSpawnPosY = _startSpawnY;
        score = 0;
        _scoreText.text = "00";
        _endPanel.SetActive(false);

        AudioManager.instance.AddButtonSound();
    }

    public void GameOver()
    {
        _endPanel.SetActive(true);
        _endScoreText.text = "SCORE " + score.ToString();

        PlayerPrefs.SetInt(Constants.DATA.LAST_SCORE, score);

        int highScore = (PlayerPrefs.HasKey(Constants.DATA.BEST_SCORE) ? PlayerPrefs.GetInt(Constants.DATA.BEST_SCORE)
            : score);
        if(score > highScore)
        {
            PlayerPrefs.SetInt(Constants.DATA.BEST_SCORE, score);
            highScore = score;
        }

        _highScoreText.text = "BEST " + highScore.ToString(); 
    }

    public void SpawnObstacle()
    {
        GameObject temp = _obstacles[Random.Range(0, _obstacles.Count)];
        Vector3 tempPos = temp.transform.position;
        tempPos.y = currentSpawnPosY;
        Instantiate(temp, tempPos, Quaternion.identity);
        currentSpawnPosY += _spawnOffsetY;

        score++;
        _scoreText.text = (score < 10 ? "0" : "") + score.ToString();
    }

    public void GamePlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
