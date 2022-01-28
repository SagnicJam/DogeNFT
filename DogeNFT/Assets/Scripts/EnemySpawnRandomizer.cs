using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnWorkDone<T>(T data);
public class EnemySpawnRandomizer : MonoBehaviour
{
    public Dictionary<int, List<Enemy>> enemyCostToEnemies = new Dictionary<int, List<Enemy>>();

    public List<Enemy> toSpawnEnemies = new List<Enemy>();

    [Header("Live Data")]
    public int enemyCount;
    public int[] costArr;
    public int totalCost;

    int minEnemyCost;
    int maxEnemyCost;
    int minEnemyCount;
    int maxEnemyCount;
    int totalEnemyCost;
    
    Enemy[] enemies;
    
    public void SpawnRandomEnemy(int minEnemyCount, int maxEnemyCount, int totalEnemyCost, int minEnemyCost, int maxEnemyCost, Enemy[] enemies, OnWorkDone<List<Enemy>> onWorkDone)
    {
        this.minEnemyCount = minEnemyCount;
        this.maxEnemyCount = maxEnemyCount;
        this.totalEnemyCost = totalEnemyCost;
        this.minEnemyCost = minEnemyCost;
        this.maxEnemyCost = maxEnemyCost;
        this.enemies = enemies;
        if (ValidityCheck())
        {
            EnemyToSpawn();
            onWorkDone?.Invoke(toSpawnEnemies);
        }
        else
        {
            Debug.LogError("invalid stats");
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        ProcessPartitioning();
    //    }
    //}

    bool ValidityCheck()
    {
        if (maxEnemyCount > enemies.Length)
        {
            Debug.LogError("EnemyCountMax range is larger that total enemies in stats");
            return false;
        }
        if (totalEnemyCost < maxEnemyCost)
        {
            Debug.LogError("max enemy cost range is greater than the total enemy cost ");
            return false;
        }
        if (totalEnemyCost < maxEnemyCost)
        {
            Debug.LogError("max enemy cost range is greater than the total enemy cost ");
            return false;
        }
        if((minEnemyCost*minEnemyCount)>totalEnemyCost)
        {
            Debug.LogError("combo of min enemy cost and minimum enemmy count is too large for the total cost you have");
            return false;
        }
       
        InitialiseListToDic();
        for (int i = minEnemyCost; i <=maxEnemyCost; i++)
        {
            if (!enemyCostToEnemies.ContainsKey(i))
            {
                Debug.LogError("range item is missing in the stats");
                return false;
            }
        }
        int maxEnemyCountTemp=maxEnemyCount;
        for (int i = minEnemyCount; i <= maxEnemyCountTemp; i++)
        {
            if ((i * minEnemyCost) <= totalEnemyCost)
            {
                continue;
            }
            else
            {
                maxEnemyCount = i-1;
                break;
            }
        }
        return true;
    }

    void InitialiseListToDic()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemyCostToEnemies.ContainsKey(enemies[i].enemyCost))
            {
                List<Enemy> newEnemyList = new List<Enemy>();
                newEnemyList.Add(enemies[i]);
                enemyCostToEnemies.Add(enemies[i].enemyCost, newEnemyList);
            }
            else
            {
                List<Enemy> updateEnemyList = new List<Enemy>(enemyCostToEnemies[enemies[i].enemyCost]);
                updateEnemyList.Add(enemies[i]);
                enemyCostToEnemies.Remove(enemies[i].enemyCost);
                enemyCostToEnemies.Add(enemies[i].enemyCost, updateEnemyList);
            }
        }
    }

    void EnemyToSpawn()
    {        
        enemyCount = Random.Range(minEnemyCount, maxEnemyCount + 1);
        costArr = new int[enemyCount];
        totalCost = 0;
        for (int i = 0; i < enemyCount; i++)
        {
            costArr[i] = Random.Range(minEnemyCost, maxEnemyCost + 1);
            totalCost += costArr[i];
        }
        if (totalCost > totalEnemyCost)
        {
            includeSet = new List<int>();
            for (int i = 0; i < costArr.Length; i++)
            {
                if (costArr[i] != minEnemyCost)
                {
                    includeSet.Add(i);
                }
            }

            ProcessPartitioning();
        }

        toSpawnEnemies = new List<Enemy>();
        for (int i = 0; i < costArr.Length; i++)
        {
            if (enemyCostToEnemies.ContainsKey(costArr[i]))
            {
                if (enemyCostToEnemies[costArr[i]].Count > 0)
                {
                    List<Enemy> enemies = enemyCostToEnemies[costArr[i]];
                    Enemy enemyToAdd = enemies[Random.Range(0, enemies.Count)];
                    toSpawnEnemies.Add(enemyToAdd);
                }
            }
        }
    }

    public List<int> includeSet = new List<int>();

    void ProcessPartitioning()
    {
        while (totalCost > totalEnemyCost)
        {
            if (includeSet.Count > 0)
            {
                int index = includeSet[Random.Range(0, includeSet.Count)];
                if ((costArr[index] - 1) >= minEnemyCost)
                {
                    costArr[index]--;
                    totalCost--;
                    if (costArr[index] == minEnemyCost)
                    {
                        includeSet.Remove(index);
                    }
                }
            }
        }
    }
}
