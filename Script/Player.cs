using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Sprite Stickman")]
    [SerializeField] Sprite ballSprite;
    [SerializeField] Sprite stopSprite;
    [SerializeField] Sprite goSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite winSprite;

    [Header("Component")]
    private HingeJoint2D hJoint;
    private Rigidbody2D rb;
    private LineRenderer lineRender;
    private SpriteRenderer spriteRenderer;

    [Header("Anchor")]
    [SerializeField] private GameObject anchor;

    [Header("Private Variables")]
    private int lastBestPosJoint;
    private int lastBestPosSelected;
    private int touches;
    private int bestPos;
    private float bestDistance;
    private Vector3 actualJointPos;

    [Header("Public Variables")] 
    [SerializeField] private float gravityRope = 2f; // luc keo len cua day -> trong luc am
    [SerializeField] private float gravityAir = 0.5f;
    [SerializeField] private float factorX = 1.2f; 
    [SerializeField] private float factorY = 1f; 


    [Header("Bool")]
    private bool sticked = false;
    private bool won = false;

    
    
    private void Start()
    {
        hJoint = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        lineRender = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //initialize
        lastBestPosJoint = 0;
        lastBestPosSelected = 0;
        touches = 0;

        anchor.transform.GetChild(lastBestPosJoint).gameObject.GetComponent<JointAnchor>().Selected();
        //this is  condition involve to GM, ignore it when debuging
        won = false;
    }
    private void Update()
    {

        bestPos = 0;
        bestDistance = float.MaxValue;
        for(int i = 0; i< anchor.transform.childCount; i++)
        {
            float actualDistance = Vector2.Distance(gameObject.transform.position, anchor.transform.GetChild(i).transform.position);
            if( actualDistance < bestDistance)
            {
                bestPos = i;
                bestDistance = actualDistance;

            }
        }
        if(!won)
            CheckInput();

        if (sticked)
        {
            lineRender.SetPosition(0, gameObject.transform.position);
            lineRender.SetPosition(1, actualJointPos);
            //
            changeSprite();
        }

        if(lastBestPosSelected != bestPos)
        {
            anchor.transform.GetChild(lastBestPosSelected).gameObject.GetComponent<JointAnchor>().Unselected();
            anchor.transform.GetChild(bestPos).gameObject.GetComponent<JointAnchor>().Selected();
           
        }
        lastBestPosSelected = bestPos;
    }

    private void CheckInput()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || ((Input.touchCount>0) && (touches == 0)))
        {
            lineRender.enabled = true;
            hJoint.enabled = true;
            rb.gravityScale = gravityRope;

            // connect the joint rb to hinge
            hJoint.connectedBody = anchor.transform.GetChild(bestPos).transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
            actualJointPos = anchor.transform.GetChild(bestPos).transform.gameObject.transform.position;
            anchor.transform.GetChild(bestPos).gameObject.GetComponent<JointAnchor>().SetSticked();
            anchor.transform.GetChild(bestPos).gameObject.GetComponent<JointAnchor>().Unselected();

            lastBestPosJoint = bestPos;
            rb.angularVelocity = 0.5f;
            sticked = !sticked;
        }

        if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || ((Input.touchCount==0) && (touches > 0)))
        {
            lineRender.enabled = false;
            hJoint.enabled = false;
            rb.velocity = new Vector2(rb.velocity.x * factorX, rb.velocity.y + factorY);
            rb.gravityScale = gravityAir;

            anchor.transform.GetChild(lastBestPosJoint).gameObject.GetComponent<JointAnchor>().SetUnsticked();

            if (bestPos == lastBestPosJoint)
            {
                anchor.transform.GetChild(bestPos).gameObject.GetComponent<JointAnchor>().Selected();
                anchor.transform.GetChild(lastBestPosSelected).gameObject.GetComponent<JointAnchor>().Unselected();
            }

            spriteRenderer.sprite = ballSprite;
            rb.AddTorque(-rb.velocity.magnitude);
            sticked = !sticked;

        }
        touches = Input.touchCount;
    }

    private void changeSprite()
    {
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        if(rb.velocity.x < 0.7f && rb.velocity.x > -0.7f && gameObject.transform.position.y  < actualJointPos.y)
        {
            spriteRenderer.sprite = stopSprite;
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                spriteRenderer.sprite = goSprite;
            }
            else
            {
                spriteRenderer.sprite = backSprite;
            }
        }
        gameObject.transform.eulerAngles = LookAt2d(actualJointPos - gameObject.transform.position);
    }

    public Vector3 LookAt2d (Vector3 vec)
    {
        return new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, Vector2.SignedAngle(Vector2.up, vec));
    }

    public bool getSticked()
    {
        return sticked;
    }

    public void ResetPlayerObject(Vector3 iniPos) 
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        gameObject.transform.position = iniPos;
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

    }

    //called this in GM
    public void Win(float speedWhenWining)
    {
        won = true;
        spriteRenderer.flipX = false;
        rb.gravityScale = 0;
        gameObject.transform.eulerAngles = LookAt2d(rb.velocity);
        rb.velocity = rb.velocity.normalized * speedWhenWining;
        rb.angularVelocity = 0f;
        spriteRenderer.sprite = winSprite;
    }

}
