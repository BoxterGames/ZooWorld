using System.Linq;
using UnityEngine;
using Zenject;

public class LinearController : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private BoxCollider boxCollider;

    private LinearConfig linearConfig;
    private GameModel gameModel;
    private Camera mainCamera;
    private float nextTimeRotate;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        linearConfig = ProjectContext.Instance.Container.Resolve<LinearConfig>();
        gameModel = ProjectContext.Instance.Container.Resolve<GameModel>();
    }

    private void Update()
    {
        var velocity = transform.forward * (linearConfig.LinearSpeed * Time.deltaTime);
        velocity.y = body.velocity.y;
        body.velocity = velocity;
     
        if (mainCamera.IsOutsideView(transform))
        {
            transform.LookAt(mainCamera.transform);
            transform.eulerAngles = Vector3.up * transform.eulerAngles.y;
            nextTimeRotate = Time.time;
        }

        if (Time.time < nextTimeRotate)
        {
            return;
        }
        
        nextTimeRotate = Time.time + linearConfig.Frequency;
        var angle = Random.Range(-linearConfig.RotationAngle, linearConfig.RotationAngle);
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
    }
}
