using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 2f;
    [SerializeField] private float swingAngle = 70f;
    [SerializeField] private RopeShooter ropeShooter;

    private float currentAngle;
    private int swingDirection = 1;
    private Vector3 initialPosition;

    private void Awake()
    {
        if (ropeShooter == null)
            ropeShooter = GetComponentInChildren<RopeShooter>();
        if (ropeShooter == null)
        {
            Debug.LogError("RopeShooter not found!");
            enabled = false;
        }
    }

    void Start()
    {
        initialPosition = transform.position;
        currentAngle = -swingAngle / 2f;
    }

    void Update()
    {
        if (!ropeShooter.IsIdle)
            return;

        // Swing hareketi
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

        // Input yönetimi (tek tetiklenmeye garanti)
        if (CheckInputOnce() && ropeShooter.IsIdle)
        {
            ropeShooter.Shoot(transform.right);
        }
    }

    private bool _inputConsumed = false;

    private bool CheckInputOnce()
    {
        bool input = false;

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            input = true;
#else
        if (Input.GetMouseButtonDown(0))
            input = true;
#endif

        if (input && !_inputConsumed)
        {
            _inputConsumed = true;
            return true;
        }
        else if (!input)
        {
            _inputConsumed = false;
        }
        return false;
    }
}
