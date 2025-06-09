using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    
    private JumpConfig jumpConfig;

    private Camera mainCamera;
    private bool onFloor;
    private float nextTimeJump;

    private void Awake()
    {
        mainCamera = Camera.main;
        jumpConfig = ProjectContext.Instance.Container.Resolve<JumpConfig>();
    }

    private void OnValidate()
    {
        body ??= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.time < nextTimeJump || !onFloor)
        {
            return;
        }

        if (mainCamera.IsOutsideView(transform))
        {
            transform.LookAt(mainCamera.transform);
            transform.eulerAngles = Vector3.up * transform.eulerAngles.y;
        }
        
        nextTimeJump = Time.time + jumpConfig.Frequency;
        var angle = Random.Range(-jumpConfig.AngleLimit, jumpConfig.AngleLimit);
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
        var direction = transform.forward + Vector3.up;
        body.AddForce(direction.normalized * jumpConfig.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag($"Floor"))
        {
            onFloor = true;
            nextTimeJump = Time.time + jumpConfig.Frequency;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        onFloor &= !other.transform.CompareTag($"Floor");
    }
}
