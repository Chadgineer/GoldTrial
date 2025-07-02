using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 2f;
    [SerializeField] private float swingAngle = 70f;
    [SerializeField] private float shootSpeed = 10f;
    [SerializeField] private float maxLength = 8f;
    [SerializeField] private float returnSpeed = 12f;

    private float currentAngle;
    private int swingDirection = 1;

    private Vector3 initialPosition;
    private bool isShooting = false;
    private bool isReturning = false;
    private Vector3 shootDirection;
    private float currentLength = 0f;

    private Transform grabbedObject = null;

    void Start()
    {
        initialPosition = transform.position;
        currentAngle = -swingAngle / 2f;
    }

    void Update()
    {
        if (!isShooting && !isReturning)
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isShooting = true;
                shootDirection = transform.right;
            }
        }
        else if (isShooting)
        {
            transform.position += shootDirection * shootSpeed * Time.deltaTime;
            currentLength += shootSpeed * Time.deltaTime;

            if (currentLength >= maxLength)
            {
                isShooting = false;
                isReturning = true;
            }
        }
        else if (isReturning)
        {
            Vector3 dir = (initialPosition - transform.position).normalized;
            transform.position += dir * returnSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
                transform.position = initialPosition;
                currentLength = 0f;
                isReturning = false;

                if (grabbedObject != null)
                {
                    grabbedObject.SetParent(null);
                    Destroy(grabbedObject.gameObject);
                    grabbedObject = null;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isShooting) return;

        grabbedObject = other.transform;
        grabbedObject.SetParent(transform);
        isShooting = false;
        isReturning = true;
    }
}
