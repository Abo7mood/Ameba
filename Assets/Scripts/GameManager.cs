using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Abilities")]
    public GameObject abilityPrefab;
    [Header("Data")]
    public Data data;
    [Space(15)]
    [SerializeField] EnemyData[] dataList; // 0 is the smallest one , 14 is the bigger one


    public static GameManager instance;

    [SerializeField] GameObject enemyPrefab;


    float enemiesTime;

    public static PlayerController player;

    public List<GameObject> enemies;

    [SerializeField] float distance = 2;
    private void Awake()
    {
        init();
    }
    private void Start()
    {
        Debug.Log(data.enemiesLevel + " L1 ");

        Time.timeScale = 1;

        InvokeRepeating(nameof(CreateAbility), 60, 60);
    }

    private void CreateAbility() {

        GameObject ability = Instantiate(abilityPrefab, new Vector3(0, 2.2f, 0), Quaternion.identity, null);
        Destroy(ability,8);
    }
    private void Update()
    {
        enemiesTime += Time.deltaTime;

        if (CanCreateEnemy)
        {
            CreateEnemy(enemyPrefab, dataList[EnemyLevel()]);
            enemiesTime = 0;
            data.enemiesAmount++;

        }
    }

    public void CreateEnemy(GameObject enemy, EnemyData data)
    {
        this.data.random = Random.insideUnitCircle;


        GameObject EnemyObject = Instantiate(enemy, randomizePosition(data), Quaternion.identity, null);

        EnemyObject.GetComponent<EnemyController>().data = data;
        EnemyObject.GetComponent<EnemyMovement>().start = randomizeVector2(data);
        LevelManager.instance.AddEnemy(ref LevelManager.instance.enemiesList, ref EnemyObject);

        float distance = Vector2.Distance(player.transform.position, randomizeVector2(data));
        Debug.Log(distance + "-Distance");
        if (distance < this.distance)
        {
            this.data.enemiesAmount--;
            RemoveAt(ref EnemyObject);
            Destroy(EnemyObject);
        }
    }

    private void RemoveAt(ref GameObject obj)
    {
        for (int i = 0; i < LevelManager.instance.enemiesList.Count; i++)
        {
            if (obj.gameObject == LevelManager.instance.enemiesList[i])
                LevelManager.instance.enemiesList.RemoveAt(i);
        }
        for (int i = 0; i < GameManager.instance.enemies.Count; i++)
        {
            if (obj.gameObject == GameManager.instance.enemies[i])
                GameManager.instance.enemies.RemoveAt(i);
        }
    }
    public int IncreaseDifficulty()
    {


        if (data.enemiesLevel < dataList.Length)
            MenuManager.instance.ChangeImage(ref MenuManager.instance.avoid, dataList[data.enemiesLevel].sprite);

        data.enemiesLevelUpCounter++;
        Debug.Log(data.enemiesLevel + " L1 ");
        if (data.enemiesLevelUpCounter >= data.maxEnemiesLevelUp && data.enemiesLevel < dataList.Length)
        {
            data.enemiesLevelUpCounter = 0;
            data.maxEnemiesInTheField++;
            CreateEnemy(enemyPrefab, dataList[data.enemiesLevel]);
            enemiesTime = 0;
            data.enemiesAmount++;
            Debug.Log(data.enemiesLevel + " L2 ");

            return data.enemiesLevel++;

        }
        else
            return data.enemiesLevel;
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
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    int EnemyLevel()
    {
        Debug.Log(EnemiesToAvoidCount() + "Avoid");
        Debug.Log(EnemiesNotToAvoidCount() + "NotAvoid");

        int balance = Random.Range(0, 2);

        if (EnemiesToAvoidCount() == 0)
        {
            Debug.Log("One");

            return data.enemiesLevel - 1;
        }
        else if (EnemiesToAvoidCount() > 0 && EnemiesNotToAvoidCount() > 0)
        {
            Debug.Log("Two");

            return Random.Range(MinimumLevel(), data.enemiesLevel);

        }
        if (EnemiesNotToAvoidCount() <= 0)
        {
            Debug.Log("Three");

            return MinimumLevel();

        }
        else
        {
            Debug.Log("Four");
            return Random.Range(MinimumLevel(), data.enemiesLevel - 1);

        }




    }
    private int EnemiesToAvoidCount()
    {
        if (enemies.Count <= 0) return 0;
        int avoidCount = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyController>().level == data.enemiesLevel)
                avoidCount++;
        }
        Debug.Log(avoidCount + "A");
        return avoidCount;
    }
    private int EnemiesNotToAvoidCount()
    {
        if (enemies.Count <= 0) return 0;
        int avoidCount = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyController>().level != data.enemiesLevel)
                avoidCount++;
        }
        Debug.Log(avoidCount + "A2");
        return avoidCount;
    }
    private int MinimumLevel()
    {
        if (data.enemiesLevel < data.enemiesLevelMin) return 0;
        else return data.enemiesLevel - data.enemiesLevelMin;

    }
    private bool CanCreateEnemy => enemiesTime > data.maxEnemiesTimeToCreate && data.enemiesAmount < data.maxEnemiesInTheField;
    private Vector3 randomizePosition(EnemyData data)
    {
        return new Vector3(randomizeVector2(data).x, randomizeVector2(data).y, 0);
    }
    private Vector2 randomizeVector2(EnemyData data) => this.data.random * data.radius + new Vector2(0, data.pos);

}
[System.Serializable]
public struct Data
{
    [Header("MaximumEnemies")]
    public int maxEnemiesInTheField; // how many enemies can we have in the map at the same time 
    public float maxEnemiesTimeToCreate; // how many seconds we need to create them
    public int maxEnemiesLevel; //how many enemies type can we create at the same time 
    public int maxEnemiesLevelUp; // difficulty for the game 
    public int maxEnemiesLevelRange => 2;

    [Header("Values")]
    public int enemiesLevel; // what is the current difficulty for enemies
    public int enemiesLevelMin; // what is the current difficulty for enemies
    public int enemiesAmount; // how many enemies we have 
    public int enemiesLevelUpCounter; // enemy level up counter


    [HideInInspector] public Vector2 random;
}