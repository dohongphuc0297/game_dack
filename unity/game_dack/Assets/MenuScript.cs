using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject SettingPanel;
    public GameObject Audio;
    public UnityEngine.UI.InputField Volume;
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("Scenes/levels/map_1", LoadSceneMode.Single);
    }
    public void ContinueButtonClicked()
    {
        SaveLoad.Load();
        switch (SaveLoad.Level)
        {
            case 0:
                SceneManager.LoadScene("Scenes/levels/map_" + (SaveLoad.Level + 1), LoadSceneMode.Single);
                break;
            //case 1:
            //    SceneManager.LoadScene("Scenes/levels/map_" + (SaveLoad.Level + 1), LoadSceneMode.Single);
            //    break;
            //case 2:
            //    SceneManager.LoadScene("Scenes/levels/map_" + (SaveLoad.Level + 1), LoadSceneMode.Single);
            //    break;
            default:
                //SceneManager.LoadScene("Scenes/levels/map_1", LoadSceneMode.Single);
                Debug.Log("Level not exist");
                break;
        }
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void SettingButtonClicked()
    {
        SettingPanel.SetActive(true);
    }
    public void CloseButtonClicked()
    {
        SettingPanel.SetActive(false);
    }
    public void SettingApplyClicked()
    {
        int volume = int.Parse(Volume.text);
        AudioSource m_MyAudioSource = Audio.GetComponent<AudioSource>();
        if (volume <= 0)
        {
            PlayerPrefs.SetInt("Volume", 0);
            Volume.text = 0.ToString();
            m_MyAudioSource.volume = 0;
        }
        else if (volume >= 100)
        {
            PlayerPrefs.SetInt("Volume", 100);
            Volume.text = 100.ToString();
            m_MyAudioSource.volume = 1;
        }
        else
        {
            PlayerPrefs.SetInt("Volume", volume);
            Volume.text = volume.ToString();
            m_MyAudioSource.volume = (volume * 1f) / 100;
        }
    }

    void Start()
    {
        AudioSource m_MyAudioSource = Audio.GetComponent<AudioSource>();
        SettingPanel.SetActive(false);
        int volume = PlayerPrefs.GetInt("Volume", -1);
        if(volume < 0)
        {
            PlayerPrefs.SetInt("Volume", 25);
            Volume.text = (25).ToString();
            m_MyAudioSource.volume = 0.25f;
        }
        else
        {
            Volume.text = volume.ToString();
            m_MyAudioSource.volume = (volume * 1f) / 100;
        }
    }
}
