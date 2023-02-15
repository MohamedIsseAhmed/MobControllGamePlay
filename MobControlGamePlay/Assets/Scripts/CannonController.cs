using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Material material;
    [SerializeField] private float spwanTimer;
   
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float planeDistance=5;
   
    private bool hasToResize = false;

    [SerializeField] private CannonConfigurationSO configurationData;
    [SerializeField] private SoldierSpawnConfiguration soldierSpawnData;
    [SerializeField] private Transform spawnPoint;
    private Camera camera;
 
    private void Awake()
    {
        material=GetComponent<MeshRenderer>().material;  
    }
    private void Start()
    {
        GameManager.Instance.OnCollidedEvent += Instance_OnCollidedEvent;
        spwanTimer = Time.time;
        //SpawnSoldier(5, spawnPoint.position);
    }

    private void Instance_OnCollidedEvent(int soliderCount, Vector3 _spawnPoint)
    {
        print("count" + soliderCount);
        SoldierSpawner.Instance.SpawnSoldier(soliderCount, _spawnPoint); 
    }
    void Update()
    {
        SpawnASoldier();
       
    }
    private void OnDisable()
    {
        TransparentWall.Instance.OnCollidedEvent -= Instance_OnCollidedEvent;
    }
    private void SpawnASoldier()
    {
        if (Input.GetMouseButton(0))
        {
            //spwanSpeed += Time.time;
            if(spwanTimer < Time.time)
            {
                spwanTimer = Time.time + configurationData.SpawnTimeMax;
                hasToResize = true;
                StartCoroutine(ResizseCannon());
            }
        
        }
        if (Input.GetMouseButtonUp(0))
        {
            material.SetFloat("_VerticalUvScale", configurationData.MinUVCannonScale);
            hasToResize = false;
            spwanTimer = 0;
        }
    }

    IEnumerator ResizseCannon()
    {
        float t = 0;
        while (hasToResize)
        {          
            t += Time.deltaTime + configurationData.SpawnTimeOffset;        
            material.SetFloat("_VerticalUvScale", t);
            if (t > configurationData.MaxUVCannonScale)
            {
                spwanTimer = 0;
                t=0;
                hasToResize = false;
                if (soldierSpawnData.IsThisSpawner)
                {
                   SoldierSpawner.Instance.SpawnSoldier(1,spawnPoint.position);
                }
              
                yield break;
            }
            yield return null;
        }
    }
    
    //public void FormaCharactersOrderly()
    //{
    //    for (int i = 0; i < unitLists.Count; i++)
    //    {
    //        var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
    //        var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

    //        var NewPos = new Vector3(x, 0, z);

    //        unitLists[i].transform.DOLocalMove(NewPos, tweenTime).SetEase(Ease.InOutBack);
    //    }
    //}
}

