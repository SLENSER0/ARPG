using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Ray _lastRay;
    private bool _isMoving;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMoving = true;
            MoveToCursor();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMoving = false;
        }

        if (_isMoving)
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent.enabled)
            {
                agent.destination = hit.point;
                agent.updateRotation = false;
                Vector3 lookDirection = hit.point - transform.position;
                lookDirection.y = 0f;
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 40f);
            }
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
    
    
}