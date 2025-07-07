using UnityEngine;

public class RopeScaler : MonoBehaviour
{
    [SerializeField] private Transform basePoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private bool updateScaleX = false;
    [SerializeField] private bool updateScaleY = true;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (basePoint == null || targetPoint == null) return;

        float distance = Vector3.Distance(basePoint.position, targetPoint.position);
        Vector3 newScale = originalScale;

        if (updateScaleY)
            newScale.y = distance;

        if (updateScaleX)
            newScale.x = distance;

        transform.localScale = newScale;

        Vector3 midPoint = (basePoint.position + targetPoint.position) / 2f;
        transform.position = midPoint;

        Vector3 direction = targetPoint.position - basePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
