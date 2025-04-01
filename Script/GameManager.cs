using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform finishLine;
    [SerializeField] private Player player;
    [SerializeField] private float WinningSpeed;

    [SerializeField] private GameObject Stickman;
    [SerializeField] private GameObject particleEffect;

    public Vector3 initPos;
    private bool won;
    private void Start()
    {
        player = Stickman.GetComponent<Player>();
        initPos = Stickman.transform.position;

    }

    private void Update()
    {   //lose condition
        if (player.getSticked() == false)
        {
            if(Stickman.transform.position.x < -5)
            {
                //reset
                ResetGame();
            }

            if(player.transform.position.y < -5)
            {
                ResetGame();
            }
        }
        //win condition
        if(finishLine.position.x < Stickman.transform.position.x && !won)
        {
            won = true;
            Win();
        }
    }

    public void ResetGame()
    {
        player.ResetPlayerObject(initPos);
    }

    private void Win()
    {
        //slow player moment
        player.Win(WinningSpeed);
        //play particle eff
        particleEffect.SetActive(true);
        particleEffect.transform.parent = null;

        //slow camera
        cameraFollow.Win();
        //finish lv
        StartCoroutine(FinishLevel());
    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
