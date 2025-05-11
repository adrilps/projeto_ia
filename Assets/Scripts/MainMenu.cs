using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int level = 0;

    public void LevelSelector(){
        level = 1;
        SceneManager.LoadSceneAsync(1);
    }
    public void SelectLevel1()
    {
        level = 2;
        SceneManager.LoadSceneAsync(2);
    }

    public void SelectLevel2()
    {
        level = 3;
        SceneManager.LoadSceneAsync(3);
    }

    public void SelectLevel3()
    {
        level = 4;
        SceneManager.LoadSceneAsync(4);
    }

    public void LevelUp()
    {
        if(level < 4)
        {
        SceneManager.LoadSceneAsync(++level);
        }
    }

}
