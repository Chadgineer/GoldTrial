using UnityEngine;

public class RopeShooter : MonoBehaviour
{
    private enum RopeState { Idle, Shooting, Returning }
    private RopeState state = RopeState.Idle;
    private RopeState prevState = RopeState.Idle;

    [SerializeField] private float shootSpeed = 10f;
    [SerializeField] private float maxLength = 8f;
    [SerializeField] private float returnSpeed = 12f;
    [SerializeField] private GoldOreManager goldOreManager;
    [SerializeField] private Spawner spawner;

    private Vector3 initialPosition;
    private Vector3 shootDirection;
    private float currentLength = 0f;
    private Transform grabbedObject = null;

    [SerializeField] private Animator bobAnimation;

    public bool IsIdle => state == RopeState.Idle;

    void Start()
    {
        initialPosition = transform.position;
        prevState = state;
    }

    void Update()
    {
        if (prevState != state)
        {
            bobAnimation.SetBool("isPulling", state == RopeState.Returning);
            prevState = state;
        }

        switch (state)
        {
            case RopeState.Shooting:
                MoveOut();
                break;
            case RopeState.Returning:
                ReturnBack();
                break;
        }
    }

    public void Shoot(Vector3 direction)
    {
        if (state != RopeState.Idle)
            return;
        shootDirection = direction.normalized;
        state = RopeState.Shooting;
    }

    private void MoveOut()
    {
        transform.position += shootDirection * shootSpeed * Time.deltaTime;
        currentLength += shootSpeed * Time.deltaTime;
        if (currentLength >= maxLength)
        {
            state = RopeState.Returning;
        }
    }

    private void ReturnBack()
    {
        Vector3 dir = (initialPosition - transform.position).normalized;
        float moveDelta = returnSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, initialPosition) <= moveDelta)
        {
            transform.position = initialPosition;
            currentLength = 0f;
            state = RopeState.Idle;

            if (grabbedObject != null)
            {
                grabbedObject.SetParent(null);
                var goldType = grabbedObject.tag;
                int goldValue = 0;
                switch (goldType)
                {
                    case "gold1": goldValue = 1; break;
                    case "gold2": goldValue = 2; break;
                    case "gold3": goldValue = 4; break;
                    case "obstacle1": break;
                    case "obstacle2": goldValue = -2; break;
                }
                goldOreManager.AddGoldOre(goldValue);

                Destroy(grabbedObject.gameObject);
                grabbedObject = null;
                spawner.CheckGolds();
            }
        }
        else
        {
            transform.position += dir * moveDelta;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (state != RopeState.Shooting) return;

        grabbedObject = other.transform;
        grabbedObject.SetParent(transform);

        switch (grabbedObject.tag)
        {
            case "gold1":
                grabbedObject.localPosition = new Vector3(0f, -1.75f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                break;
            case "gold2":
                grabbedObject.localPosition = new Vector3(0f, -3.75f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                break;
            case "gold3":
                grabbedObject.localPosition = new Vector3(0f, -5.25f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                break;
            case "obstacle1":
                grabbedObject.localPosition = new Vector3(0f, 0f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                break;
            case "obstacle2":
                goldOreManager.SpendGoldIngot(1);
                grabbedObject.localPosition = new Vector3(0f, 0f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                break;
            default:
                grabbedObject.localPosition = new Vector3(0f, 0f, 1f);
                grabbedObject.localEulerAngles = Vector3.zero;
                break;
        }
        state = RopeState.Returning;
    }
}
