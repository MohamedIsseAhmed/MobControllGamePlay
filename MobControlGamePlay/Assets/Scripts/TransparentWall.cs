using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class TransparentWall : MonoBehaviour
{
    public static TransparentWall Instance { get; private set; }
    [SerializeField] private TextMeshPro yearText;
    [SerializeField] private int soldierToSpawn;
    [SerializeField] private Vector3 spawnPointOffset;
   
    public delegate void OnCollided(int soliderCount, Vector3 spawnPoint);
    public event OnCollided OnCollidedEvent;
    private void Awake()
    {
        Instance= this;
    }
    private void Start()
    {
        yearText.text = "X" + soldierToSpawn.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }
    public int GetCharacterIncreaseCount()
    {
        return soldierToSpawn;
    }
}
