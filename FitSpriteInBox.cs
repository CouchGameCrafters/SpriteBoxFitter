using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class FitSpriteInBox : MonoBehaviour
{
    public Vector2 boxSize = Vector2.one;
    private Vector2 lastSize = Vector2.zero;
    private SpriteRenderer spriteRenderer;
    public bool preserveAspect = true;
    private bool lastPreserveAspect = false;


    private void Start()
    {
        UpdateSpriteSize();        
    }

    private void Update()
    {
        UpdateSpriteSize();
    }

    private void UpdateSpriteSize ()
    {
        if(lastSize == boxSize && lastPreserveAspect == preserveAspect) return;

        if(spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer> ();
            if(spriteRenderer == null) return;
        }
        if(spriteRenderer.sprite == null) return;

        Vector2 size = spriteRenderer.sprite.rect.size / Mathf.Max(spriteRenderer.sprite.pixelsPerUnit, 0.001f);

        if(preserveAspect) {
            Vector2 difference = boxSize - size;
            if(difference.x < difference.y) {
                transform.localScale = Vector3.one * (boxSize.x / size.x);
            } else {
                transform.localScale = Vector3.one * (boxSize.y / size.y);
            }
        } else {
            transform.localScale = new Vector3((boxSize.x / size.x), (boxSize.y / size.y), 1f);
        }

        lastSize = boxSize;
        lastPreserveAspect = preserveAspect;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
    #endif
}
