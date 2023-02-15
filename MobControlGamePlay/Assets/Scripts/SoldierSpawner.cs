using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    public static SoldierSpawner Instance { get; private set; }
    [SerializeField] private float xOffset = 9;
    [SerializeField] private float zOffset = 5;
    [SerializeField] private SoldierSpawnConfiguration soldierSpawnData;

    [SerializeField] private bool collidedWithTransapentWall;
    private void Awake()
    {
        Instance = this;

    }
    public void SpawnSoldier(int soldierCount, Vector3 _spawnPoint,bool _collidedWithTransapentWall=false)
    {
         this.collidedWithTransapentWall = _collidedWithTransapentWall;
         SpawnSoldierCoroutine(soldierCount, _spawnPoint, collidedWithTransapentWall);
    }
    private void SpawnSoldierCoroutine(int soldierCount,Vector3 _spawnPoint, bool collidedWithTransapentWall = false)
    {

        for (int i = 0; i < soldierCount; i++)
        {
           
            //if (i % 2 == 0 && i > 0)
            //{
            //    xOffset += xOffset;
            //    //zOffset += zOffset;
            //}
            //else
            //{
            //    if (i!=0)
            //    {
            //        print("Make+-");
            //        //xOffset *= xOffset>0?-1:-1;
            //    }


            //}
            GameObject soldier = SoldierPool.instance.GetAnItemFromPool(i);
            if (collidedWithTransapentWall)
            {
                soldier.transform.position = _spawnPoint + new Vector3(xOffset, 0, zOffset);
            }
            else
            {
                soldier.transform.position = _spawnPoint;
            }

            soldier.GetComponent<Rigidbody>().AddForce(Vector3.forward * soldierSpawnData.SpwnThrowForce);
            soldier.GetComponent<UnitSoldier>().SetDirection(Vector3.forward);
            if (collidedWithTransapentWall)
            {
                xOffset += xOffset;
            }
            print("i" + i);
            print("xOffset" + xOffset);
        }

        // int i = 0;
        //while (i < soldierCount)
        //{

        //    //GameObject soldier = SoldierPool.instance.GetAnItemFromPool()[i];
        //    GameObject soldier = SoldierPool.instance.GetAnItemFromPool(i);
        //    soldier.transform.position = _spawnPoint;
        //    soldier.GetComponent<Rigidbody>().AddForce(Vector3.forward * soldierSpawnData.SpwnThrowForce);
        //    soldier.GetComponent<UnitSoldier>().SetDirection(Vector3.forward);
        //    i++;
        //    print("i" + i);
        //    //yield return null;
        //}
        ResetXAndZOffsetValues();

    }

    public void ResetXAndZOffsetValues()
    {
        this.collidedWithTransapentWall = false;
        xOffset = 9;
        zOffset = 9;
    }
}
