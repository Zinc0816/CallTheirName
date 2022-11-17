using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitleScene : MonoBehaviour
{
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip command;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            audioS.PlayOneShot(command);
            FadeManager.Instance.LoadScene ("NameScene", 1.5f);
            //audioS.PlayOneShot(command);
        }
    }
}
