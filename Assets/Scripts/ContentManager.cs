using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    public EnemyData[] enemyDatas;

    public GameObject cardPrefab;
    private void Start()
    {
        for (int i = 0; i < enemyDatas.Length; i++)
        {
            GameObject card = Instantiate(cardPrefab, this.transform.position, Quaternion.identity, this.transform);
            card.GetComponent<Card>().image.sprite = enemyDatas[i].sprite;
            card.GetComponent<Card>().txt.text = enemyDatas[i].txt;

        }
    }
}
