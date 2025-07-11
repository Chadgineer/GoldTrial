using System.Collections.Generic;
using UnityEngine;

public class RectElement : MonoBehaviour
{
    public List<RectTransform> rectTransforms;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter");
        if (!rectTransforms.Contains(collision.transform.GetComponent<RectTransform>()))
        {
           rectTransforms.Add(collision.transform.GetComponent<RectTransform>());
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (rectTransforms.Contains(collision.transform.GetComponent<RectTransform>()))
        {
            rectTransforms.Remove(collision.transform.GetComponent<RectTransform>());
        }
    }
}
