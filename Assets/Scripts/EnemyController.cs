using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    AnimatorOverrideController aoc; // the animation clip handler
    Animator amebaController; // the monster animator
    public EnemyData data;
    private EnemyMovement movement;
    private BoxCollider2D box;
    [HideInInspector] public int level;
    [HideInInspector] public int score;

    [HideInInspector] public int index;

    private void Awake()
    {
        amebaController = GetComponentInChildren<Animator>();
        movement = GetComponent<EnemyMovement>();
        box = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        Setter(ref data.image, data.size, data.speed, data.radius, data.pos, data.freezeTime, data.name, data.level, data.score,data.boxSize);
        SoundManager.instance.SoundPlayer(5);
    }

    private void Setter(ref AnimationClip img, float size, float speed, float radius, float pos, float freezeTime, string name, int level, int score,Vector2 boxSize)
    {
        SetAnimationController(ref img);
        movement.radius = radius;
        movement.speed = speed;
        movement.basestartpoint = new Vector2(0, pos);
        movement.destination = new Vector2(0, pos);
        transform.localScale = new Vector3(size, size, 1);
        gameObject.name = name;
        movement.freezeTime = freezeTime;
        this.level = level;
        this.score = score;
        box.size = boxSize;

       GameManager.instance.enemies.Add(gameObject);
    }
    private void SetAnimationController(ref AnimationClip clip)
    {

        aoc = new AnimatorOverrideController(amebaController.runtimeAnimatorController);
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();

        anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[0], clip));

        aoc.ApplyOverrides(anims);

        amebaController.runtimeAnimatorController = aoc;


    }
}
