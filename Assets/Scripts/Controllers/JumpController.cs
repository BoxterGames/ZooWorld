using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private BoxCollider boxCollider;
    
    private JumpConfig jumpConfig;
    private GameModel gameModel;
    
    private const float epsilon = 0.001f;
    private Camera mainCamera;
    private float nextTimeJump;
    private float prevY;

    private void Awake()
    {
        mainCamera = Camera.main;
        jumpConfig = ProjectContext.Instance.Container.Resolve<JumpConfig>();
        gameModel = ProjectContext.Instance.Container.Resolve<GameModel>();
    }

    private void OnEnable()
    {
        prevY = transform.position.y;
        nextTimeJump = Time.time + jumpConfig.Frequency;
    }

    private void Update()
    {
        if (Mathf.Abs(prevY - transform.position.y) > epsilon)
        {
            nextTimeJump = Time.time + jumpConfig.Frequency;    
        }
        
        prevY = transform.position.y;
        
        if (Time.time < nextTimeJump)
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
}
