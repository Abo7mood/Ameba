using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [Header("Componenets")]
    public GameObject TXT_Panel, HTP_Panel;
    public TextMeshProUGUI scoreTXT, livesTXT;
    [Space(10)]
    public Image avoid,pauseImage,HTPImage;
    [Header("Values")]
    public int score;
    public int lives = 3;
    public Sprite avoidImage;
    public Sprite pauseSprite,playSprite;
    private bool pause = false;
    private bool howToPlay = false;

    [SerializeField] Button[] buttons;
    private void Awake()
    {
        init();
        Click_Sound();
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
    private void Start()
    {
        ChangeText(ref livesTXT, ref lives, 0);
        ChangeText(ref scoreTXT, ref score, 0);
        ChangeImage(ref avoid, avoidImage);
        TXT_Panel.SetActive(false);
        HTP_Panel.SetActive(false);
    }
    public void ChangeText(ref TextMeshProUGUI txt, ref int value, int amount)
    {

        if (txt == scoreTXT)
        {
            value += amount;
            txt.text = value.ToString();

        }
        else
        {
            value -= amount;
            txt.text = $"X {value}";
        }
    }
    public void ChangeImage(ref Image image, Sprite sprite) => image.sprite = sprite;

    #region Buttons
    public void Pause()
    {
        pause = !pause;

        if (pause)
        {
            Time.timeScale = 0; 
            pauseImage.sprite = playSprite;
            HTPImage.GetComponent<Button>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseImage.sprite = pauseSprite;
            HTPImage.GetComponent<Button>().enabled = true;
        }
    }
    public void HTP()
    {
        
        howToPlay = !howToPlay;
        if (howToPlay) { HTP_Panel.SetActive(true); pauseImage.GetComponent<Button>().enabled = false; Time.timeScale = 0; } else { HTP_Panel.SetActive(false); pauseImage.GetComponent<Button>().enabled = true; Time.timeScale = 1; } 
    }

    #endregion
    private void Click_Sound()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => SoundManager.instance.SoundPlayer(1));
        }
    }

}
