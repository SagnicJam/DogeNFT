using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;
/// <summary>
/// Get the required enemies for config
/// </summary>
public class DungeonEnemySpawner : MonoBehaviour
{
    public EnemyStats enemyStats;
    public EnemySpawnRandomizer enemySpawnRandomizer;
    public int totalEnemyCost;

    [MinMaxSlider(0, 100)]
    public Vector2Int enemyCostRange;
    [MinMaxSlider(0, 100)]
    public Vector2Int enemyCountRange;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            enemySpawnRandomizer.SpawnRandomEnemy(enemyCountRange.x
                , enemyCountRange.y, totalEnemyCost, enemyCostRange.x, enemyCostRange.y, enemyStats.enemies,null);
        }
    }
}
