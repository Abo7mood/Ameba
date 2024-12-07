using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Monster")]

public class EnemyData : ScriptableObject
{
    [Header("UI")]
    public string txt;
    public Sprite sprite;
    [Space(5)]
    public new string name;
    [Space(5)]
    public int level;
    public AnimationClip image;
    public float size;
    [Space(15)]
    public int score;
    [Header("Movement")]
    public float speed;
    public float radius;
    public float pos;
    public float freezeTime;
    [Header("Size")]
    public Vector2 boxSize=new Vector2(.6f,1.5f);
}
