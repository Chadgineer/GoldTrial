using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFloatFade : MonoBehaviour
{
    [SerializeField] private float floatDistance = 100f; // UI için pixel cinsinden mesafe
    [SerializeField] private float duration = 1f;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        Activate(duration, floatDistance);
    }

    public void Activate(float durationOverride, float distanceOverride)
    {
        duration = durationOverride;
        floatDistance = distanceOverride;

        Vector3 targetPos = rectTransform.anchoredPosition + Vector2.up * floatDistance;

        rectTransform.DOAnchorPos(targetPos, duration).SetEase(Ease.Linear);
        canvasGroup.DOFade(0f, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
