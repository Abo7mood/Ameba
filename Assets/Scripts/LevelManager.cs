using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<GameObject> enemiesList;


    private void Awake()
    {
        init();
    }
    private void init()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
            {
                Destroy(instance);
                instance = this;
            }
            else
            {
                instance = this;
            }

        }
    }
    public void AddEnemy(ref List<GameObject> myList, ref GameObject myObject)
    {
        myList.Add(myObject);
        myList.RemoveAll(x => !x);

    }
    public void ResetGame()
    {

        if (MenuManager.instance.lives >= 0)
        {


            SoundManager.instance.SoundPlayer(3);
            Lose(false, Strings.LIVE_MESSAGE());
        }
        else
        {
            SoundManager.instance.SoundPlayer(2);
            Lose(true, Strings.DIE_MESSAGE);
        }

    }
    private void RemoveAll(ref List<GameObject> list)
    {
        foreach (var item in enemiesList)
        {
            Destroy(item);
        }
        list.RemoveAll(x => x);
    }


    public void ResetScene()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the current scene again to reset it
        SceneManager.LoadScene(currentScene.name);
    }
    private void Lose(bool die, string txt)
    {
        StartCoroutine(Restart(die, 3f));
        Time.timeScale = 0;
        MenuManager.instance.TXT_Panel.SetActive(true);
        MenuManager.instance.TXT_Panel.GetComponentInChildren<TextMeshProUGUI>().text = txt;

    }

    IEnumerator Restart(bool die, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (die)
        {
            ResetScene();
        }
        else
        {
            RemoveAll(ref enemiesList);
            RemoveAll(ref GameManager.instance.enemies);

            GameManager.player.transform.position = new Vector3(0, 0.24f, 0);
            GameManager.player.canContact = true;
            GameManager.instance.data.enemiesAmount = 0;
            MenuManager.instance.TXT_Panel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ApplicationQuit() => Application.Quit();
}
