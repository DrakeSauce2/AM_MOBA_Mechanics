using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttachComponent : MonoBehaviour
{
    private Camera cam;
    private Transform attachPoint;
    private float cullingDistance = 50f;

    public void Init(Transform attachTransform)
    {
        attachPoint = attachTransform;
        cam = Camera.main;
    }

    void Update()
    {
        if (attachPoint == null) return;

        float distance = Vector3.Distance(cam.transform.position, attachPoint.position);

        float scale = 1 - (distance / cullingDistance);
        transform.localScale = new Vector3(scale, scale, scale);

        transform.position = cam.WorldToScreenPoint(attachPoint.position);
    }
}
