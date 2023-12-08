using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemLine : Graphic
{
    Vector2 startPosition;
    Vector2 endPosition;
    float lineThickness;

    public void Init(Vector3 startPosition, Vector3 endPosition, float lineThickness)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.lineThickness = lineThickness;

        color = Color.black;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        Vector3 cornerPosition = new Vector3(endPosition.x, startPosition.y);
        float angle = GetAngle(startPosition, endPosition) + 45f;

        vh.AddVert(CreateUIVertex(startPosition, -lineThickness / 2, angle));
        vh.AddVert(CreateUIVertex(startPosition, lineThickness / 2, angle));

        vh.AddVert(CreateUIVertex(endPosition, -lineThickness / 2, angle));
        vh.AddVert(CreateUIVertex(endPosition, lineThickness / 2, angle));

        vh.AddTriangle(0, 1, 3);
        vh.AddTriangle(3, 2, 0);
    }

    private float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180/Mathf.PI));
    }

    private UIVertex CreateUIVertex(Vector3 position, float lineThickness, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;

        Vector3 vertexPoint = new Vector3(lineThickness, 0);
        position += Quaternion.Euler(0, 0, angle) * vertexPoint;
        vertex.position = position;
        return vertex;
    }

}