using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

using UnityEngine.SceneManagement;


public class FinishLine : MonoBehaviour
{

    public int lvlToload;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(lvlToload);
    }
    

}
