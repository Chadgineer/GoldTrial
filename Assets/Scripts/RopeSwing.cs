using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 2f;
    [SerializeField] private float swingAngle = 70f;

    private float currentAngle;
    private int swingDirection = 1;
    private Vector3 initialPosition;
    private RopeShooter ropeShooter;

    void Start()
    {
        initialPosition = transform.position;
        currentAngle = -swingAngle / 2f;
        ropeShooter = GetComponentInChildren<RopeShooter>();
    }

    void Update()
    {
        if (ropeShooter.IsIdle)
        {
            transform.position = initialPosition;

            currentAngle += swingDirection * swingSpeed * Time.deltaTime;
            if (currentAngle >= swingAngle / 2f)
            {
                swingDirection = -1;
                currentAngle = swingAngle / 2f;
            }
            else if (currentAngle <= -swingAngle / 2f)
            {
                swingDirection = 1;
                currentAngle = -swingAngle / 2f;
            }

            transform.rotation = Quaternion.Euler(0, 0, 270 + currentAngle);

            if (Input.GetMouseButtonDown(0))
            {
                ropeShooter.Shoot(transform.right);
            }
        }
    }
}