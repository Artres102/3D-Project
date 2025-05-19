using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.MainMusic);
    }
    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
