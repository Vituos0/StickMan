using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAnchor : MonoBehaviour
{
    [SerializeField] private Sprite Spritesticked;
    [SerializeField] private Sprite SpriteUnsticked;

    private SpriteRenderer spriteRender;
    private GameObject dashLine;

    private bool sticked=false;

    [SerializeField] private float animTime = 0.1f;
    [SerializeField] private AnimationCurve animationCurve;


    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        dashLine = gameObject.transform.GetChild(1).gameObject;
    }

    public void SetSticked()
    {
        spriteRender.sprite = Spritesticked;
        sticked = true;
    }

    public void SetUnsticked()
    {
        spriteRender.sprite = SpriteUnsticked;
        sticked = false;
        Unselected();
    }

    public void Selected()
    {
        if (!sticked)
        {
            // show dash line
            StartCoroutine(SelectingJoint());
        }
        else
        {
            dashLine.transform.localScale = Vector3.zero;
        }
    }

    public void Unselected()
    {
        StartCoroutine(SelectingJoint());
        dashLine.transform.localScale = Vector3.zero;
    }

    IEnumerator SelectingJoint()
    {
        float time = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale
            = new Vector3(1.15f, 1.15f, 1.15f);
       
        while(time <= animTime)
        {
            time += Time.deltaTime;
            dashLine.transform.localScale = Vector3.Lerp(startScale, endScale, animationCurve.Evaluate(time));
            yield return null;
        }
    }
}
