using UnityEngine;
using System.Collections.Generic;

public class ButtonScatter : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform gold1;
    [SerializeField] private RectTransform gold2;
    [SerializeField] private RectTransform rock1;
    [SerializeField] private RectTransform rock2;
    [SerializeField] private RectTransform rock3;
    [SerializeField] private RectTransform bomb;

    [SerializeField] private float leftOffset = 40f;
    [SerializeField] private float rightOffset = 40f;
    [SerializeField] private float topOffset = 40f;
    [SerializeField] private float bottomOffset = 40f;
    [SerializeField] private float overlapPadding = 15f;

    private readonly List<RectTransform> activeRects = new();

    public void ScatterButtons()
    {
        gold1.gameObject.SetActive(true);
        gold2.gameObject.SetActive(true);
        rock1.gameObject.SetActive(true);
        rock2.gameObject.SetActive(true);
        rock3.gameObject.SetActive(true);
        bomb.gameObject.SetActive(true);

        bool showFirstGold = Random.value > 0.5f;
        gold1.gameObject.SetActive(showFirstGold);
        gold2.gameObject.SetActive(!showFirstGold);

        activeRects.Clear();
        activeRects.Add(showFirstGold ? gold1 : gold2);
        activeRects.Add(rock1);
        activeRects.Add(rock2);
        activeRects.Add(rock3);
        activeRects.Add(bomb);

        List<Rect> occupiedRects = new();

        foreach (var rect in activeRects)
        {
            Vector2 size = rect.sizeDelta + new Vector2(overlapPadding, overlapPadding);
            Vector2 pos;
            Rect btnRect;
            int attempts = 0;
            do
            {
                pos = GetRandomPosition(size);
                rect.anchoredPosition = pos;
                btnRect = GetRectOnCanvas(rect);
                attempts++;
                if (attempts > 100) break;
            } while (Overlaps(btnRect, occupiedRects));

            occupiedRects.Add(btnRect);
        }
    }

    private Vector2 GetRandomPosition(Vector2 size)
    {
        Vector2 canvasSize = canvasRect.rect.size;
        float minX = -canvasSize.x * 0.5f + size.x * 0.5f + leftOffset;
        float maxX = canvasSize.x * 0.5f - size.x * 0.5f - rightOffset;
        float minY = -canvasSize.y * 0.5f + size.y * 0.5f + bottomOffset;
        float maxY = canvasSize.y * 0.5f - size.y * 0.5f - topOffset;
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2(x, y);
    }

    // RectTransform'un canvas üzerindeki köþelerini kullanarak gerçek alanýný döndürür
    private Rect GetRectOnCanvas(RectTransform rect)
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        // Canvas'ýn world-space'ine çevir
        Vector2 min = canvasRect.InverseTransformPoint(corners[0]);
        Vector2 max = canvasRect.InverseTransformPoint(corners[2]);
        return new Rect(min, max - min);
    }

    private bool Overlaps(Rect rect, List<Rect> others)
    {
        foreach (var other in others)
        {
            if (rect.Overlaps(other, true)) // true ile çapraz kenarlarý da kapsar
                return true;
        }
        return false;
    }
}