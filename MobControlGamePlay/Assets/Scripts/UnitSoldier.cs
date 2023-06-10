using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class UnitSoldier : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementOffset=0.1f;
    [SerializeField] private float movementTimer=0.2f;
   
    [SerializeField] private float normalMovementSpeed;
    [SerializeField] private float distancetoTargetCastle=1;
    [SerializeField] private AnimationCurve movementSpeedCurve;
    [SerializeField] private float xOffset = 9;
    [SerializeField] private float zOffset=5;
    [SerializeField] private float waiteTimeToSpawn=0.1f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int MakePositiveOrNegative = 1;

    private List<Transform> targets=new List<Transform>();
    private Vector3 targetDirection;
    private WaitForSeconds spawnTime;
   
    [SerializeField] private Transform currentTargetCastle;

    [SerializeField] private bool switchDirection = false;
    private void Awake()
    {
        targets = new List<Transform>();
        rigidbody = GetComponent<Rigidbody>();
           
    }
    
    private void Start()
    {
        spawnTime = new WaitForSeconds(waiteTimeToSpawn);
    }
    private void OnEnable()
    {
       
        

    }
    public void SetDirection(Vector3 direction)
    {
        targetDirection = direction;
        GameObject[] enemyCastles = GameObject.FindGameObjectsWithTag("EnemyCastle");
        print(enemyCastles.Length);
        for (int i = 0; i < enemyCastles.Length; i++)
        {
            targets.Add(enemyCastles[i].transform);
        }
        //targetDirection = targets[Random.Range(0, targets.Count)].position;
        currentTargetCastle = targets[Random.Range(0, targets.Count)];
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, currentTargetCastle.position) < distancetoTargetCastle)
        {
            switchDirection = true;
        }
        print(Vector3.Distance(transform.localPosition, currentTargetCastle.position));
        //targetDirection = (currentTargetCastle.position - transform.position).normalized;
        //transform.position += targetDirection * movementSpeed * Time.deltaTime;
        //agent.destination= currentTargetCastle.position*movementSpeed*Time.deltaTime;

        if (switchDirection)
        {
            targetDirection = (currentTargetCastle.position - transform.position).normalized;
        }
        movementOffset += Time.deltaTime;
        movementSpeed += movementSpeedCurve.Evaluate(movementOffset + 0.1f);
        if (movementOffset > movementTimer)
        {
            movementSpeed = normalMovementSpeed;
        }

        //if(movementSpeed>= normalMovementSpeed)
        //{
        //    movementSpeed = normalMovementSpeed;
        //}
        Move();
    }
    private void FixedUpdate()
    {
        
    }

    private  void OnTriggerEnter(Collider other)
    {
        TransparentWall transparentWall = other.gameObject.GetComponent<TransparentWall>();
        if (transparentWall != null)
        {
            int soldierToSpawn = transparentWall.GetCharacterIncreaseCount();

            StartCoroutine(SpawnSoliderCoroutine(soldierToSpawn));
           
        }

       
    }

    private IEnumerator  SpawnSoliderCoroutine(int soldierToSpawn)
    {
        yield return spawnTime;
       
        SoldierSpawner.Instance.SpawnSoldier(soldierToSpawn, transform.position, true);
    }
   
    private void Move()
    {
        if (!switchDirection)
        {
           
        }
        rigidbody.MovePosition(rigidbody.position + targetDirection * movementSpeed * Time.fixedDeltaTime);
        //else if(switchDirection)
        //{
        //    targetDirection = (currentTargetCastle.position - transform.position).normalized;
        //    rigidbody.MovePosition(rigidbody.position + targetDirection * movementSpeed * Time.fixedDeltaTime);
        //}



    }
}
