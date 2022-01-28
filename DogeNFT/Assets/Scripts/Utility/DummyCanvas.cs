using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Min_Max_Slider;
using UnityEngine.UI;
using UnityEngine.Pool;
public class DummyCanvas : MonoBehaviour
{
    public RectTransform enemyDisplayParent;
    public EnemyDataSpawnner enemyDataSpawnner;
    public DungeonEnemySpawner dungeonEnemySpawner;
    public TMP_InputField inputFieldText;
    
    public Slider sliderUITotalEnemyCost;
    public MinMaxSlider minmaxSliderEnemyCost;
    public MinMaxSlider minmaxSliderEnemyCount;

    public void Randomizer()
    {
        OnWorkDone<List<Enemy>> onWorkDone = OnRandomEnemyCalculated;
        dungeonEnemySpawner.enemySpawnRandomizer.SpawnRandomEnemy(
            Mathf.RoundToInt(minmaxSliderEnemyCount.Values.minValue),
             Mathf.RoundToInt(minmaxSliderEnemyCount.Values.maxValue),
             Mathf.RoundToInt(sliderUITotalEnemyCost.value),
             Mathf.RoundToInt(minmaxSliderEnemyCost.Values.minValue),
             Mathf.RoundToInt(minmaxSliderEnemyCost.Values.maxValue),
             dungeonEnemySpawner.enemyStats.enemies, onWorkDone);
    }

    public List<EnemyDisplay> enemyDataSpawnnerActiveList = new List<EnemyDisplay>();
    void OnRandomEnemyCalculated(List<Enemy>enemyToSpawn)
    {
        foreach (EnemyDisplay enemyDataSpawnner in enemyDataSpawnnerActiveList)
        {
            enemyDataSpawnner.DestroyToPool();
        }
        enemyDataSpawnnerActiveList.Clear();
        if (enemyToSpawn.Count > 0)
        {
            for (int i = 0; i < enemyToSpawn.Count; i++)
            {
                EnemyDisplay enemyDisplay = enemyDataSpawnner.enemyDisplayPool.Get();
                enemyDisplay.SpawnNewPrefab(enemyToSpawn[i], enemyDisplayParent);

                enemyDataSpawnnerActiveList.Add(enemyDisplay);
            }
        }
    }
}
