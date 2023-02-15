using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    public static GameManager Instance { get; private set; }

    public delegate void OnCollidedWithTransparentWall(int soliderCount, Vector3 spawnPoint);
    public event OnCollidedWithTransparentWall OnCollidedEvent;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
           Destroy(gameObject);
        }
      
    }
    public  void FireOnCollidedWithTransparentWallEvent(int _soliderCount, Vector3 _spawnPoint)
    {
        OnCollidedEvent.Invoke(_soliderCount, _spawnPoint);
        //return Task.Yield();
    }
}
