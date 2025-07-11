using System.Collections.Generic;
using UnityEngine;

public class ButtonScatter : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform gold1;
    [SerializeField] private RectTransform gold2;
    [SerializeField] private RectTransform rock1;
    [SerializeField] private RectTransform rock2;
    [SerializeField] private RectTransform rock3;
    [SerializeField] private RectTransform bomb;

    [Header("Parametreler")]
    [SerializeField] private float leftOffset = 40f;
    [SerializeField] private float rightOffset = 40f;
    [SerializeField] private float topOffset = 40f;
    [SerializeField] private float bottomOffset = 40f;
    [SerializeField] private float overlapPadding = 15f; 

    private const int MaxAttempts = 200;

    public void ScatterButtons()
    {
        // 1) Hangi butonlar aktif olacak?
        bool showFirstGold = Random.value > 0.5f;
        gold1.gameObject.SetActive(showFirstGold);
        gold2.gameObject.SetActive(!showFirstGold);
        rock1.gameObject.SetActive(true);
        rock2.gameObject.SetActive(true);
        rock3.gameObject.SetActive(true);
        bomb.gameObject.SetActive(true);

        // 2) Aktif RectTransform’larý listele
        var activeRects = new List<RectTransform>
        {
            showFirstGold ? gold1 : gold2,
            rock1, rock2, rock3,
            bomb
        };

        // 3) Yerleþmiþ dikdörtgenleri tutacak liste
        var occupied = new List<Rect>();

        // 4) Tek tek butonlarý yerleþtir
        foreach (var rt in activeRects)
        {
            Vector2 paddedSize = rt.sizeDelta + Vector2.one * overlapPadding;
            Rect candidateRect = new Rect();
            int attempt = 0;

            // Tekrar tekrar rasgele pozisyon dene
            do
            {
                Vector2 pos = GetRandomPosition(paddedSize);
                rt.anchoredPosition = pos;
                candidateRect = GetRectOnCanvas(rt);
                attempt++;
            }
            while (attempt < MaxAttempts && Overlaps(candidateRect, occupied));

            // Bulunan (ya da son denen) dikdörtgeni kilitle
            occupied.Add(candidateRect);
        }
    }

    private Vector2 GetRandomPosition(Vector2 size)
    {
        Vector2 canvasSize = canvasRect.rect.size;
        float minX = -canvasSize.x * 0.5f + size.x * 0.5f + leftOffset;
        float maxX = canvasSize.x * 0.5f - size.x * 0.5f - rightOffset;
        float minY = -canvasSize.y * 0.5f + size.y * 0.5f + bottomOffset;
        float maxY = canvasSize.y * 0.5f - size.y * 0.5f - topOffset;
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private Rect GetRectOnCanvas(RectTransform rt)
    {
        // Dünya-köþelerini al
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        // Canvas’ýn local alanýna dönüþtür
        Vector2 min = canvasRect.InverseTransformPoint(corners[0]);
        Vector2 max = canvasRect.InverseTransformPoint(corners[2]);
        return new Rect(min, max - min);
    }

    private bool Overlaps(Rect rect, List<Rect> others)
    {
        foreach (var o in others)
            if (rect.Overlaps(o, true))
                return true;
        return false;
    }
}
