using UnityEngine;

public class StretchBetweenPoints : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        Vector2 dir = pointB.position - pointA.position;
        float distance = dir.magnitude;

        // Sprite merkezi A'da olacak þekilde objeyi kaydýr:
        transform.position = pointA.position + (Vector3)dir * 0.5f;

        // Açýyý ayarla
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // X skalasýný mesafe kadar ayarla
        Vector3 scale = transform.localScale;
        scale.x = distance / spriteRenderer.sprite.bounds.size.x;
        transform.localScale = scale;
    }
}
