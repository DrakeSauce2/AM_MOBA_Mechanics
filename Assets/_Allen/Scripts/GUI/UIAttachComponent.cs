using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttachComponent : MonoBehaviour
{
    private Camera cam;
    private Vector3 screenPoint;
    private float cullingDistance = 25f;

    public void Init(Transform attachTransform)
    {
        screenPoint = attachTransform.position;
        cam = Camera.main;
    }

    public void Init(Vector3 attachTransform)
    {
        screenPoint = attachTransform;
        cam = Camera.main;
    }

    void Update()
    {
        ScaleUIToSize();

        transform.position = cam.WorldToScreenPoint(screenPoint);
    }

    private void ScaleUIToSize()
    {
        float distance = Vector3.Distance(cam.transform.position, screenPoint);

        float scale = 1 - (distance / (cullingDistance * 2));
        if (distance > cullingDistance)
        {
            transform.localScale = Vector3.zero;
        }
        else
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public Vector3 GetRandomPosition(Vector3 start, float min, float max)
    {
        float x = Random.Range(start.x - min, start.x + max);
        float y = Random.Range(start.y - min, start.y + max);
        float z = Random.Range(start.z - min, start.z + max);

        return new Vector3(x, y, z);
    }

}
