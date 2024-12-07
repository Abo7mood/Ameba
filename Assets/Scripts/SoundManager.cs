using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip[] audioClips;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SoundPlayer(0);
        InvokeRepeating(nameof(Water), 3, 3f);
    }
    public void SoundPlayer(int num)
    {
        GameObject audio = new GameObject();
        audio.AddComponent<AudioSource>();
        GameObject soundObject = Instantiate(audio, transform.position, Quaternion.identity, null);
        soundObject.GetComponent<AudioSource>().clip = audioClips[num];
        soundObject.GetComponent<AudioSource>().Play();
        Destroy(soundObject,3f);

    }
    private void Water() => SoundPlayer(6);
}





