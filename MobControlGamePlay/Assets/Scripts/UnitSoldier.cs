using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UnitSoldier : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float xOffset = 9;
    [SerializeField] private float zOffset=5;
    [SerializeField] private float waiteTimeToSpawn=0.1f;
    private Vector3 targetDirection;
    [SerializeField] private int MakePositiveOrNegative = 1;
    private List<Vector3> spawnPositions = new List<Vector3>();
    private WaitForSeconds spawnTime;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        spawnTime = new WaitForSeconds(waiteTimeToSpawn);
        //spawnPositions.Add(transform.position);
    }
    public void SetDirection(Vector3 direction)
    {
        targetDirection = direction;
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + targetDirection * movementSpeed * Time.fixedDeltaTime);
    }

    private  void OnTriggerEnter(Collider other)
    {
        TransparentWall transparentWall = other.gameObject.GetComponent<TransparentWall>();
        if (transparentWall != null)
        {
            int soldierToSpawn = transparentWall.GetCharacterIncreaseCount();

            StartCoroutine(SpawnSoliderCoroutine(soldierToSpawn));
           
        }

        print("t");
    }

    private IEnumerator  SpawnSoliderCoroutine(int soldierToSpawn)
    {
        yield return spawnTime;
        //for (int i = 0; i < soldierToSpawn; i++)
        //{
        //    print("soldierToSpawn:" + i);
        //    spawnPositions.Add(transform.position + new Vector3(xOffset * MakePositiveOrNegative, transform.position.y, zOffset));

        //     SpawnSoldiers(i, spawnPositions[i]);
        //    if (i % 2 == 0)
        //    {
        //        xOffset += xOffset;
        //        zOffset += zOffset;

        //    }
        //    else
        //    {
        //        MakePositiveOrNegative *= -1;

        //    }

        //    yield return null;

        //}
        SoldierSpawner.Instance.SpawnSoldier(soldierToSpawn, transform.position, true);
    }

    private void SpawnSoldiers(int soldierCount,Vector3 spawnPosition)
    {
        SoldierSpawner.Instance.SpawnSoldier(soldierCount, spawnPosition);
       
    }
}
