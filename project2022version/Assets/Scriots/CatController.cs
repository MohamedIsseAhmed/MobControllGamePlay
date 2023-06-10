using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float minDisatnce = 0.10f;
    [SerializeField] private float smoothJoystickSpeed = 0.10f;
    [SerializeField] private float angelSpeed = 10f;
    [SerializeField] private float axis = 10f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private DynamicJoystick dynamicJoystick; 

    private Animator animator;
    public string currentState;
    public LayerMask groundLayerMask;
    float angel;
    private CharacterController characterController;
    private bool startAction;
    Vector3 desiredPos;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            ProcessInput();
        }
    }
    private void ProcessInput()
    {
        float horizontal = dynamicJoystick.Horizontal * smoothJoystickSpeed;
        float vertical = dynamicJoystick.Vertical * smoothJoystickSpeed;
        if (!startAction)
        {
            desiredPos = new Vector3(horizontal, 0, vertical);
        }
      
        //float yVelocity = Mathf.SmoothDamp(rigidbody.velocity.y, rigidbody.velocity.y, ref yVelocityTarget, smoothTime);
        //desiredPosition = new Vector3(horizontal, 0, vertical) + new Vector3(0, yVelocity, 0);
        //float inputMagnitude = desiredPosition.magnitude;
        ////smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothVelocity, smoothSpeed);
        //float angÝnDeg = Mathf.Atan2(desiredPosition.x, desiredPosition.z) * Mathf.Rad2Deg;
      
        if (desiredPos.magnitude > 0)
        {
            animator.CrossFade(currentState, 0);
        }
        Ray ray = new Ray(transform.position, -transform.up * 10);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            if (hit.collider.gameObject.CompareTag("Climp") /*|| hit.collider.gameObject.CompareTag("Jump")*/)
            {
                print(dynamicJoystick.Horizontal);
                Vector3 vert = Quaternion.Euler(axis, 0, 0) * hit.normal;
                desiredPos = vert; 
                Debug.DrawLine(hit.point, vert * 10, Color.red);
            }
            else
            {
                Debug.DrawLine(ray.origin, transform.forward * 10, Color.black);
                print("null");
            }


        }
        float angelInDeg = Mathf.Atan2(desiredPos.x, desiredPos.z) * Mathf.Rad2Deg;
        float inputMagnitude = desiredPos.magnitude;
        angel = Mathf.LerpAngle(angel, angelInDeg, angelSpeed * Time.deltaTime);

        //transform.position = Vector3.MoveTowards(transform.position, desiredPos, moveSpeed * Time.deltaTime);
        //characterController.Move(desiredPos * moveSpeed * Time.deltaTime);

        transform.rotation = Quaternion.AngleAxis(angel, Vector3.up);
        print(characterController.isGrounded);
        transform.Translate(desiredPos * moveSpeed * Time.deltaTime, Space.World);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (desiredPos.magnitude > 0 && other.CompareTag("jumpTrigger"))
        {
            currentState = "Jump";
            animator.CrossFade(currentState, 0);
        }
    }
    private void TryDoSomeThing()
    {
        int i = 0;
        string tag = GetParkurTransform().tag;
        switch (tag)
        {
            case "Climp":

                    break;
            case "Jump":
                break;

            default:
                break;
        }
    }
    private Transform GetParkurTransform()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray,out RaycastHit hit))
        {
            if(hit.collider.CompareTag("Climp") || hit.collider.CompareTag("Jump"))
            {
                return hit.collider.transform;
            }
            
        }
        return null;
       
    } 
    private void Climp(Transform targetParkour)
    {
        startAction = true;
        
        //desiredPos =new Vector3(dynamicJoystick.Horizontal,0,)
    }
}
