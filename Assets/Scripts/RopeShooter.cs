using UnityEngine;

public class RopeShooter : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 10f;
    [SerializeField] private float maxLength = 8f;
    [SerializeField] private float returnSpeed = 12f;

    private Vector3 initialPosition;
    private Vector3 shootDirection;
    private float currentLength = 0f;

    private bool isShooting = false;
    private bool isReturning = false;
    private Transform grabbedObject = null;

    [SerializeField] private GoldOreManager goldOreManager;

    public bool IsIdle => !isShooting && !isReturning;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isShooting)
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

    public void Shoot(Vector3 direction)
    {
        shootDirection = direction.normalized;
        isShooting = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isShooting) return;

        grabbedObject = other.transform;
        grabbedObject.SetParent(transform);

        switch (grabbedObject.tag)
        {
            case "gold1":
                grabbedObject.localPosition = new Vector3(0f, -1.75f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                goldOreManager.AddGoldOre(1);
                break;
            case "gold2":
                grabbedObject.localPosition = new Vector3(0f, -3.75f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                goldOreManager.AddGoldOre(2);
                break;
            case "gold3":
                grabbedObject.localPosition = new Vector3(0f, -5.25f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                goldOreManager.AddGoldOre(3);
                break;
            default:
                grabbedObject.localPosition = new Vector3(0f, 0f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                break;
        }

        isShooting = false;
        isReturning = true;
    }
}