using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPool : MonoBehaviour
{
    public static SoldierPool instance;
    [SerializeField] private int maxSoldierCount;
    [SerializeField] private SoldierSpawnConfiguration soldierSpawnData;
    private List<GameObject> poolList = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
      
    }
    private void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        for (int i = 0; i < maxSoldierCount; i++)
        {
            GameObject newSoldier=  Instantiate(soldierSpawnData.soldierPrefab);
            poolList.Add(newSoldier);
            newSoldier.transform.parent = transform;
            newSoldier.SetActive(false);

        }
    }
    public GameObject GetAnItemFromPool(int soldierCount)
    {
     
        for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].gameObject.activeInHierarchy)
            {
                poolList[i].SetActive(true);
               
                return poolList[i];

            }
        }
        print("null");
        return null;
       
    }
    public void ReturnToPoolAnItem(GameObject soldier)
    {
        if (poolList.Contains(soldier))
        {
            soldier.SetActive(false);
        }
    }
}
