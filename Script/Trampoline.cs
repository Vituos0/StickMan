using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("JumpIn");
    }

}
