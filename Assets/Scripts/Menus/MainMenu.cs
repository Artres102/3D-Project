using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.MainMusic);
    }
    public void Newgame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
