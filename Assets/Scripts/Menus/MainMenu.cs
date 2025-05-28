using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject QuitMenu;
    public GameObject SettingsMenu;
    private bool menuActivated = false;
    private bool menuActivated2 = false;
    // Start is called before the first frame update
    void Start()
    {
        QuitMenu.SetActive(menuActivated);
        SettingsMenu.SetActive(menuActivated2);
    }

    public void Resume()
    {
        menuActivated = !menuActivated;
        QuitMenu.SetActive(menuActivated);
    }
    
    public void Resume2()
    {
        menuActivated2 = !menuActivated2;
        SettingsMenu.SetActive(menuActivated2);
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
