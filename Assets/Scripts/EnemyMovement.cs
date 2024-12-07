using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] SpriteRenderer enemySprite,circleSprite;

    private BoxCollider2D box;

    public float radius = 40.0f;
    public float speed = 5.0f;


    public float basePosition = 2f;
    // The point we are going around in circles
    [HideInInspector] public Vector2 basestartpoint;

    // Destination of our current move
    [HideInInspector] public Vector2 destination;

    // Start of our current move
    [HideInInspector] public Vector2 start;

    // Current move's progress
    private float progress = 0.0f;

    bool reached = false;
    private bool canStart;

    private float maxProgress = 1;

    [HideInInspector] public float freezeTime = 1;


    // Use this for initialization
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        enemySprite.enabled = false;
        box.enabled = false;

        progress = 0.0f;

        PickNewRandomDestination();

        Invoke(nameof(CanStart), freezeTime);
    }
    private void CanStart() {
        circleSprite.enabled = false;
        enemySprite.enabled = true;
        canStart = true;
        box.enabled = true;


    } 
    // Update is called once per frame
    void Update()
    {
        if (!canStart) return;
        reached = false;

        // Update our progress to our destination
        progress += speed * Time.deltaTime;

        // Check for the case when we overshoot or reach our destination
        if (progress >= maxProgress)
        {
            progress = maxProgress;
            reached = true;
            canStart = true;
        }


        // Update out position based on our start postion, destination and progress.
        transform.localPosition = (destination * progress) + start * (1 - progress);
        // If we have reached the destination, set it as the new start and pick a new random point. Reset the progress
        if (reached)
        {
            start = destination;
            PickNewRandomDestination();
            progress = 0.0f;
        }
    }

    void PickNewRandomDestination()
    {
        // We add basestartpoint to the mix so that is doesn't go around a circle in the middle of the scene.
        destination = Random.insideUnitCircle * radius + basestartpoint;
    }

    private void OnDrawGizmos()
    {
        Vector2 a = new Vector2(0, basePosition);
        Gizmos.DrawSphere(a, radius);
    }

}
