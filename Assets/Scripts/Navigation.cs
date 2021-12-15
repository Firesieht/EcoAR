using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update
    Text score;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadMenu()
    {
        score = GameObject.Find("Counter").GetComponent<Text>();

        rotation.MemorizeClass.Memorize(score.text, "score.ini");
        SceneManager.LoadScene("menu");

    }
    
    public void loadMain()
    {
        SceneManager.LoadScene("main");
    }

    public void loadAbout()
    {
        Application.OpenURL("https://firesieht.github.io/");
    }
}
