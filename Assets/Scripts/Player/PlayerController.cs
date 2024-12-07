using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int level = 1;

    private int LevelCounter;
    private int sizeCounter;
    public int sizeMax;
    public int LevelMax = 3;


    public Vector3 sizeIncrease;

   public bool canContact = true;

    private void Start()
    {
        canContact = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.ENEMY))
        {
            if (GetEnemy(ref collision).level > level)
            {
                if (canContact)
                    Die(ref collision);
                canContact = false;
            }
            else
            {
                
                Kill(ref collision);
            }
        }
       
    }

    private EnemyController GetEnemy(ref Collider2D collision) => collision.gameObject.GetComponent<EnemyController>();

    private void Kill(ref Collider2D collision)
    {
        MenuManager.instance.ChangeText(ref MenuManager.instance.scoreTXT, ref MenuManager.instance.score, GetEnemy(ref collision).score);
        GameManager.instance.data.enemiesAmount--;
        IncreaseLevel(ref collision);
        IncreaseSize();
        RemoveAt(ref collision);
        Destroy(collision.gameObject);
        SoundManager.instance.SoundPlayer(4);

    }
    public void RemoveAt(ref Collider2D collision)
    {
        for (int i = 0; i < LevelManager.instance.enemiesList.Count; i++)
        {
            if (collision.gameObject == LevelManager.instance.enemiesList[i])
                LevelManager.instance.enemiesList.RemoveAt(i);
        }
        for (int i = 0; i < GameManager.instance.enemies.Count; i++)
        {
            if (collision.gameObject == GameManager.instance.enemies[i])
                GameManager.instance.enemies.RemoveAt(i);
        }
    }
    private void Die(ref Collider2D collision)
    {
        if (MenuManager.instance.lives >= 0)
            MenuManager.instance.ChangeText(ref MenuManager.instance.livesTXT, ref MenuManager.instance.lives, 1);
        LevelManager.instance.ResetGame();
    }

    private int IncreaseLevel(ref Collider2D collision)
    {
        LevelCounter += GetEnemy(ref collision).level;
        if (LevelCounter >= LevelMax)
        {
            LevelCounter = 0;
            GameManager.instance.IncreaseDifficulty();
            LevelMax += IncreasedLevel(ref collision);
            return level++;
        }
        else
        {

            return level;
        }
    }
    private int IncreasedLevel (ref Collider2D collision) => GetEnemy(ref collision).level + 6;
    private void IncreaseSize() {
        sizeCounter++;
        if (sizeCounter >= sizeMax) {
            transform.localScale += sizeIncrease;
            sizeCounter = 0;
 }
    } 



}
