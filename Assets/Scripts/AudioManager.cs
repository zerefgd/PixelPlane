using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource _musicSource, _effectSource;

    [SerializeField]
    private AudioClip _clickSound;

    private bool IsSoundMuted
    {
        get
        {
            int result =
            PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND) : 1;
            return result == 0;
        }

        set
        {
            PlayerPrefs.SetInt(Constants.DATA.SETTINGS_SOUND, value ? 0 : 1);
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerPrefs.SetInt(Constants.DATA.SETTINGS_SOUND, IsSoundMuted ? 0 : 1);
        _effectSource.mute = IsSoundMuted;
        _musicSource.mute = IsSoundMuted;
        if(!IsSoundMuted)
        {
            _musicSource.Play();
        }

        AddButtonSound();
    }

    public void AddButtonSound()
    {
        var buttons = FindObjectsOfType<Button>(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => 
            {
                PlaySound(_clickSound);
            });
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    public void ToggleSound()
    {
        _musicSource.mute = IsSoundMuted;
        _effectSource.mute = IsSoundMuted;
    }
}
