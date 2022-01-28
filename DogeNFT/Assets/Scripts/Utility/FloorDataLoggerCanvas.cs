using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Min_Max_Slider;
using UnityEngine.UI;
using UnityEngine.Pool;
public class FloorDataLoggerCanvas : MonoBehaviour
{
    public TextMeshProUGUI floorText;
    public DogeStats dogeStats;
    public EnemyDataSpawnner enemyDataSpawnner;
    public DogeDataSpawnner dogeDataSpawnner;
    public DungeonEnemySpawner dungeonEnemySpawner;
    public TMP_InputField inputFloorFieldText;
    public TMP_InputField inputDogeFieldText;

    public Slider sliderUITotalEnemyCost;
    public MinMaxSlider minmaxSliderEnemyCost;
    public MinMaxSlider minmaxSliderEnemyCount;

    public TextMeshProUGUI errorText;

    public string[] dogeArr;

    private void Start()
    {
        dungeonEnemySpawner.enemySpawnRandomizer.errorMessage = ReadErrorMessage;
    }

    public void ShowFloorCount()
    {
        //show floor count here
        if (string.IsNullOrEmpty(inputFloorFieldText.text))
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Enter floor number!";
            errorText.color = Color.yellow;
            Debug.LogError("Enter floorText!");
            floorText.text = "--";
            return;
        }
        floorText.text = inputFloorFieldText.text;
    }

    public void ShowDoges()
    {
        if(string.IsNullOrEmpty(inputDogeFieldText.text))
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Enter doges!";
            errorText.color = Color.yellow;
            Debug.LogError("Enter doges!");
            return;
        }
        dogeArr = inputDogeFieldText.text.Split(",");
        //spawn doges here
        if (!IsValidDoges())
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Wrong doge id!";
            errorText.color = Color.red;
            Debug.LogError("Wrong doge id!");
        }
        else
        {
            foreach (DogeDisplay dogeDataSpawnner in dogeDataSpawnnerActiveList)
            {
                dogeDataSpawnner.DestroyToPool();
            }
            dogeDataSpawnnerActiveList.Clear();
            if (dogeArr.Length > 0)
            {
                for (int i = 0; i < dogeArr.Length; i++)
                {
                    DogeDisplay dogeDisplay = dogeDataSpawnner.dogeDisplayPool.Get();
                    dogeDisplay.SpawnNewPrefab(dogeArr[i]);

                    dogeDataSpawnnerActiveList.Add(dogeDisplay);
                }
            }
        }
    }

    bool IsValidDoges()
    {
        for (int i = 0; i < dogeArr.Length; i++)
        {
            if (dogeArr[i].Length > 0)
            {
                if (!(dogeArr[i][0] == 'D' || dogeArr[i][0] == 'd'))
                {
                    Debug.LogError("wrong doge id");
                    return false;
                }

                string substring = dogeArr[i].Substring(1, dogeArr[i].Length-1);
                int dogeId=-1;
                if(int.TryParse(substring,out dogeId))
                {
                    if(!IsValidDogeId(dogeId))
                    {
                        return false;
                    }
                }
                else
                {
                    Debug.LogError("wrong doge id");
                    return false;
                }
            }
            else
            {
                Debug.LogError("wrong doge id");
                return false;
            }
        }
        return true;
    }

    bool IsValidDogeId(int id)
    {
        for (int i = 0; i < dogeStats.doges.Length; i++)
        {
            if(dogeStats.doges[i].id==id)
            {
                return true;
            }
        }
        return false;
    }

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
    public List<DogeDisplay> dogeDataSpawnnerActiveList = new List<DogeDisplay>();
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
                enemyDisplay.SpawnNewPrefab(enemyToSpawn[i]);

                enemyDataSpawnnerActiveList.Add(enemyDisplay);
            }
        }
    }

    void ReadErrorMessage(string errorMessage)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = errorMessage+"!";
        errorText.color = Color.red;
    }
}
