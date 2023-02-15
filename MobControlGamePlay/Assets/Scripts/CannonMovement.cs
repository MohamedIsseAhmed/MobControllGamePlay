using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{

    [SerializeField] private CannonConfigurationSO configurationData;
    [SerializeField] private float planeDistance; 

    private Camera mainCamera;

    private void Start()
    {
        mainCamera=Camera.main;
    }
    private void Update()
    {
        MoveSides();
    }
    private void MoveSides()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, planeDistance);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 point = ray.GetPoint(distance);
                Vector3 targetPosition = new Vector3(point.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, configurationData.CannonMoveSidesSpeed * Time.deltaTime);
                ClampXAxisRange();
            }
        }

    }
    private void ClampXAxisRange()
    {
       
        if (transform.position.x > configurationData.MaxX)
        {
            SetPosionOutOfBounderies(configurationData.MaxX);
        }
        if (transform.position.x < configurationData.MinX)
        {
            SetPosionOutOfBounderies(configurationData.MinX);

        }
    }
    private void SetPosionOutOfBounderies(float XAxis)
    {
        Vector3 MyPostion = new Vector3(XAxis, transform.position.y, transform.position.z);
        transform.position = MyPostion;
    }
}
