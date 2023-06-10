using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public Transform[] paths;
    public float waitTime=0.1f;
    void Start()
    {
        StartCoroutine(FollowPatshs(paths));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FollowPatshs(Transform[] pathLists)
    {
        
        int currentIndex = 0;
        Vector3 currentPaths = pathLists[currentIndex].position;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPaths, 10 * Time.deltaTime);
           
            if(currentPaths == transform.position)
            {
                currentIndex=(currentIndex + 1)% pathLists.Length;
                currentPaths = pathLists[currentIndex].position;
                // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentPaths - transform.position), Time.deltaTime);
                transform.forward = (currentPaths - transform.position).normalized;
                    
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
