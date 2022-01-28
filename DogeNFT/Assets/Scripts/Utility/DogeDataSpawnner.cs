using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DogeDataSpawnner : MonoBehaviour
{
    public DogeDisplay dogeDisplayPrefab;
    public RectTransform rtParent;
    public ObjectPool<DogeDisplay> dogeDisplayPool;
    private void Awake()
    {
        dogeDisplayPool = new ObjectPool<DogeDisplay>(CreateDogeDisplay, OnTakeDogeDisplayFromPool
            , OnReturnDogeDisplayToPool);
    }

    DogeDisplay CreateDogeDisplay()
    {
        DogeDisplay dogeDisplay = Instantiate(dogeDisplayPrefab);
        dogeDisplay.SetPool(dogeDisplayPool);
        return dogeDisplay;
    }

    void OnTakeDogeDisplayFromPool(DogeDisplay dogeDisplay)
    {
        dogeDisplay.gameObject.SetActive(true);
        dogeDisplay.transform.SetParent(rtParent);
    }

    void OnReturnDogeDisplayToPool(DogeDisplay dogeDisplay)
    {
        dogeDisplay.gameObject.SetActive(false);
        dogeDisplay.transform.SetParent(null);
    }
}
