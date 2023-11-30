using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemLine : MonoBehaviour
{
    
}

public class Line
{
    public Line(Vector2 startPosition, Vector2 endPosition, float lineThickness)
    {
        SpriteRenderer spriteRenderer = new SpriteRenderer();
        Sprite lineSprite = spriteRenderer.sprite;

        Vector2[] verts = new Vector2[3];
        ushort[] tris = new ushort[2];

        verts[0] = (new Vector2(startPosition.x - lineThickness, startPosition.y));
        verts[1] = (new Vector2(startPosition.x + lineThickness, startPosition.y));
        verts[2] = (new Vector2(endPosition.x - lineThickness, endPosition.y));
        verts[3] = (new Vector2(endPosition.x + lineThickness, endPosition.y));

        tris[0] = 0;
        tris[1] = 2;
        tris[2] = 1;

        lineSprite.OverrideGeometry(verts, tris);
    }

}