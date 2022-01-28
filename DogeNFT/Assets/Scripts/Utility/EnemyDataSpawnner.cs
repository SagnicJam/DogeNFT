using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDataSpawnner : MonoBehaviour
{
    public EnemyDisplay enemyDisplayPrefab;
    public RectTransform rtParent;
    public ObjectPool<EnemyDisplay> enemyDisplayPool;
    private void Awake()
    {
        enemyDisplayPool = new ObjectPool<EnemyDisplay>(CreateEnemyDisplay, OnTakeEnemyDisplayFromPool
            , OnReturnEnemyDisplayToPool);
    }

    EnemyDisplay CreateEnemyDisplay()
    {
        EnemyDisplay enemyDisplay = Instantiate(enemyDisplayPrefab);
        enemyDisplay.SetPool(enemyDisplayPool);
        return enemyDisplay;
    }

    void OnTakeEnemyDisplayFromPool(EnemyDisplay enemyDisplay)
    {
        enemyDisplay.gameObject.SetActive(true);
        enemyDisplay.transform.SetParent(rtParent);
    }

    void OnReturnEnemyDisplayToPool(EnemyDisplay enemyDisplay)
    {
        enemyDisplay.gameObject.SetActive(false);
        enemyDisplay.transform.SetParent(null);
    }
}
