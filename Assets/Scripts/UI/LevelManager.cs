using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _voiceBtn;
    [SerializeField] private GameObject _sfxBtn;
    [SerializeField] private GameObject _vibBtn;
    [SerializeField] private bool muted;
    [SerializeField] private bool sfxmuted;
    [SerializeField] private bool vibBool;
    [SerializeField] private Sprite[] _whichBtn;
    [SerializeField] private Sprite[] _whichBtnSfx;
    [SerializeField] private Sprite[] _whichBtnVib;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceSfx;
    [SerializeField] private GameManager _gameManager;
    
    void Start()
    {
        Time.timeScale = 1;
        
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetFloat("muted",0.7f);
        }
        if (!PlayerPrefs.HasKey("sfxmuted"))
        {
            PlayerPrefs.SetFloat("sfxmuted",0.7f);
        }
        if (!PlayerPrefs.HasKey("vibmuted"))
        {
            PlayerPrefs.SetInt("vibmuted",100);
        }
        _audioSource.volume = PlayerPrefs.GetFloat("muted");
        _audioSource.volume = PlayerPrefs.GetFloat("sfxmuted");
        //_gameManager.Set_VibInt(PlayerPrefs.GetInt("vibmuted"));
        
        if (PlayerPrefs.GetInt("muted") == 0)
        {
            muted = true;
            _voiceBtn.GetComponent<Image>().sprite = _whichBtn[1];
        }
        if (PlayerPrefs.GetInt("muted") == 1)
        {
            muted = false;
            _voiceBtn.GetComponent<Image>().sprite = _whichBtn[0];
        }
        
        if (PlayerPrefs.GetInt("sfxmuted") == 0)
        {
            sfxmuted = true;
            _sfxBtn.GetComponent<Image>().sprite = _whichBtnSfx[1];
        }
        if (PlayerPrefs.GetInt("sfxmuted") == 1)
        {
            sfxmuted = false;
            _sfxBtn.GetComponent<Image>().sprite = _whichBtnSfx[0];
        }
        
        if (PlayerPrefs.GetInt("vibmuted") == 0)
        {
            vibBool = true;
            _vibBtn.GetComponent<Image>().sprite = _whichBtnVib[1];
        }
        if (PlayerPrefs.GetInt("vibmuted") == 1)
        {
            vibBool = false;
            _vibBtn.GetComponent<Image>().sprite = _whichBtnVib[0];
        }

    }
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public void Home()
    {
        SceneManager.LoadScene("Opening");
    }
    
    public void Pause()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void MenuMusicVolume()
    {
        muted = !muted;
        PlayerPrefs.SetInt("muted", muted?1:0);
        if (muted == true)
        {
            _voiceBtn.GetComponent<Image>().sprite = _whichBtn[1];
            PlayerPrefs.SetFloat("muted", 0);
            _audioSource.volume = 0;
        }

        if (muted == false)
        {
            _voiceBtn.GetComponent<Image>().sprite = _whichBtn[0];
            PlayerPrefs.SetFloat("muted", 0.7f);
            _audioSource.volume = 0.7f; 
        }
    }
    
    public void MenuSfxVolume()
    {
        sfxmuted = !sfxmuted;
        PlayerPrefs.SetInt("sfxmuted", muted?1:0);
        if (sfxmuted == true)
        {
            _sfxBtn.GetComponent<Image>().sprite = _whichBtnSfx[1];
            PlayerPrefs.SetFloat("sfxmuted", 0);
            _audioSourceSfx.volume = 0;
        }

        if (sfxmuted == false)
        {
            _sfxBtn.GetComponent<Image>().sprite = _whichBtnSfx[0];
            PlayerPrefs.SetFloat("sfxmuted", 0.7f);
            _audioSourceSfx.volume = 0.7f; 
        }
    }
    
    public void VibrationSet()
    {
        vibBool = !vibBool;
        PlayerPrefs.SetInt("vibmuted", vibBool?1:0);
        if (vibBool == true)
        {
            _vibBtn.GetComponent<Image>().sprite = _whichBtnVib[1];
            PlayerPrefs.SetInt("vibmuted", 0);
            //_gameManager.Set_VibInt(0);
        }

        if (vibBool == false)
        {
            _vibBtn.GetComponent<Image>().sprite = _whichBtnVib[0];
            PlayerPrefs.SetInt("vibmuted", 100);
            //_gameManager.Set_VibInt(100);
        }
    }
}
