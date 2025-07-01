using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 2f;
    [SerializeField] private float swingAngle = 70f;
    [SerializeField] private float shootSpeed = 10f;
    [SerializeField] private float maxLength = 8f;
    [SerializeField] private float returnSpeed = 12f;
    [SerializeField] private LayerMask grabbableLayer;

    private float currentAngle;
    private int swingDirection = 1;

    private Vector3 initialPosition;
    private bool isShooting = false;
    private bool isReturning = false;
    private Vector3 shootDirection;
    private float currentLength = 0f;
    private int grabbedCount = 0;

    private void Start()
    {
        initialPosition = transform.position;
        currentAngle = -swingAngle / 2f;
    }

    private void Update()
    {
        if (!isShooting && !isReturning)
        {
            SwingRope();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ShootRopeCoroutine());
            }
        }
    }

    private void SwingRope()
    {
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
    }

    private IEnumerator ShootRopeCoroutine()
    {
        isShooting = true;
        shootDirection = transform.right;
        currentLength = 0f;

        while (currentLength < maxLength)
        {
            yield return new WaitForFixedUpdate();
            transform.position += shootDirection * shootSpeed * Time.fixedDeltaTime;
            currentLength += shootSpeed * Time.fixedDeltaTime;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, shootDirection, 0.1f, grabbableLayer);
            if (hit.collider != null && hit.collider.attachedRigidbody != null)
            {
                StartCoroutine(HandleObjectGrab(hit.collider.attachedRigidbody));
                isShooting = false;
                yield break;
            }
        }

        isShooting = false;
        StartCoroutine(ReturnRopeCoroutine());
    }

    private IEnumerator ReturnRopeCoroutine()
    {
        isReturning = true;
        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            yield return new WaitForFixedUpdate();
            Vector3 dir = (initialPosition - transform.position).normalized;
            transform.position += dir * returnSpeed * Time.fixedDeltaTime;
        }
        transform.position = initialPosition;
        currentLength = 0f;
        isReturning = false;
    }

    private IEnumerator HandleObjectGrab(Rigidbody2D grabbedBody)
    {
        isReturning = true;

        if (grabbedBody != null)
        {
            grabbedBody.bodyType = RigidbodyType2D.Kinematic;
            grabbedBody.transform.SetParent(transform);
        }

        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            yield return new WaitForFixedUpdate();
            Vector3 dir = (initialPosition - transform.position).normalized;
            transform.position += dir * returnSpeed * Time.fixedDeltaTime;
        }

        if (grabbedBody != null)
        {
            grabbedCount++;
            grabbedBody.transform.SetParent(null);
            Destroy(grabbedBody.gameObject);
        }
        transform.position = initialPosition;
        currentLength = 0f;
        isReturning = false;
    }
}
