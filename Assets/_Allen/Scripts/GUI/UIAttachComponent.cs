using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttachComponent : MonoBehaviour
{
    private Transform attachPoint;

    public void Init(Transform attachTransform)
    {
        attachPoint = attachTransform;
    }

    void Update()
    {
        if (attachPoint == null) return;

        transform.position = Camera.main.WorldToScreenPoint(attachPoint.position);
    }
}
