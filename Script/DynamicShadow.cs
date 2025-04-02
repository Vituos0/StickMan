using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShadow : Shadow
{
    [SerializeField] private bool isDynamic;
    [SerializeField] private float maxOffset;
    [SerializeField] private CameraFollow cameraFollow;

    private void LateUpdate()
    {
        if (isDynamic)
        {
            ShadowSpriteRenderer.sprite = spriteRenderer.sprite;
            ShadowSpriteRenderer.flipX = spriteRenderer.flipX;
            float offset = 0.1f;
            if(cameraFollow.maxOffset != 0)
            {
                offset = (cameraFollow.OffSet / cameraFollow.maxOffset) * (maxOffset * 100);
                offset /= 100;

            }

            ShadowGameObject.transform.position = gameObject.transform.position + new Vector3(shadowOffset.x, shadowOffset.y) + new Vector3(-offset, 0f, 0f);
        }
    }
}
