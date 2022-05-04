using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _lastScoreText, _highScoreText;

    [SerializeField]
    private Image _soundButton;

    [SerializeField]
    private Color _onSoundColor, _offSoundColor;

    private void Awake()
    {
        _lastScoreText.text = "LAST " + (PlayerPrefs.HasKey(Constants.DATA.LAST_SCORE) ? PlayerPrefs.GetInt(Constants.DATA.LAST_SCORE)
            : 0).ToString();
        _highScoreText.text = "BEST " + (PlayerPrefs.HasKey(Constants.DATA.BEST_SCORE) ? PlayerPrefs.GetInt(Constants.DATA.BEST_SCORE)
            : 0).ToString();

        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ?
            PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND) : 1) == 1;
        _soundButton.color = sound ? _onSoundColor : _offSoundColor;
    }

    public void GamePlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ToggleSound()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ?
           PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND) : 1) == 0;
        PlayerPrefs.SetInt(Constants.DATA.SETTINGS_SOUND, sound ? 1 : 0);
        _soundButton.color = sound ? _onSoundColor : _offSoundColor;

        AudioManager.instance.ToggleSound();
    }
}
