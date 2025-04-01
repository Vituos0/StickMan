using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] Material shadowMaterial;
    [SerializeField] private Vector2 shadowOffset;

    protected SpriteRenderer spriteRenderer;
    protected GameObject ShadowGameObject;

    protected SpriteRenderer ShadowSpriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ShadowGameObject = new GameObject("Shadow2D");

        ShadowSpriteRenderer = ShadowGameObject.AddComponent<SpriteRenderer>();
        ShadowSpriteRenderer.sprite = spriteRenderer.sprite;
        ShadowSpriteRenderer.material = shadowMaterial;
        ShadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        ShadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;

        ShadowGameObject.transform.parent = gameObject.transform;
        ShadowGameObject.transform.localScale = Vector3.one;
        ShadowGameObject.transform.rotation = gameObject.transform.rotation;
        ShadowGameObject.transform.position = gameObject.transform.position + (Vector3)shadowOffset;


    }
}
