using UnityEngine;
using Zenject;

public class LinearController : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [Inject] private LinearConfig linearConfig;

    private Camera mainCamera;
    private float nextTimeRotate;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnValidate()
    {
        body ??= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        body.velocity = transform.forward * (linearConfig.LinearSpeed * Time.deltaTime);
     
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
