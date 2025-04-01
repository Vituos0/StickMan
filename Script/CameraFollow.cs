using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    private Rigidbody2D playerRb;

    [HideInInspector] public float OffSet;
    public float maxOffset;
    public float minX, maxX;
    public float orthographicSizeAfterWin = 2.5f;



    void Start()
    {
        OffSet = 0;
        playerRb = player.GetComponent<Rigidbody2D>();

        gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

    }

    private void Update()
    {
       
        if(playerRb.velocity.x > 0)
        {
            OffSet += Time.deltaTime * speed;
            if (OffSet > maxOffset)
            {
                OffSet = maxOffset;
            }
        }
        else if(playerRb.velocity.x < 0)
        {
            OffSet -= Time.deltaTime * speed;
            if (OffSet < maxOffset)
            {
                OffSet = -maxOffset;
            }
        }

        float nextX = player.transform.position.x + OffSet;
        if (nextX < minX) nextX = minX;
        if (nextX > maxX) nextX = maxX;

        gameObject.transform.position = new Vector3(nextX, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    //  called in GM
    public void Win()
    {
        maxOffset = 0;
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize /= orthographicSizeAfterWin;


    }
}
