using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float MovementDelay = 2f;
    bool isCrashed;
    [SerializeField] AudioClip DeathSFX;
    [SerializeField] AudioClip SucessSFX;
    AudioSource audioSource;
    bool isTransisioning = false;
   void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision other)
    {
        if (isTransisioning == false)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This Thing is Friendly");
                    break;
                case "Finish":
                    Debug.Log("You reached the finish point");
                    //NextLevel();
                    StartCoroutine(StartSuccesSequence());
                    break;

                default:
                    if (!isCrashed)
                    {
                        Debug.Log("You Blew Up");


                        // ReloadLevel1();
                        StartCoroutine(StartCrashSequence());
                        isCrashed = true;
                    }

                    break;
            }
        }
       
        
        
        IEnumerator StartCrashSequence()
        {
            isTransisioning=true;
            audioSource.PlayOneShot(DeathSFX);
            GetComponent<Movement>().enabled = false;
            yield return new WaitForSeconds(2f);
            ReloadLevel1();
        }

        IEnumerator StartSuccesSequence()
        {
            isTransisioning=true;
            audioSource.PlayOneShot(SucessSFX);
            GetComponent<Movement>().enabled = false;
            yield return new WaitForSeconds(2f);
            NextLevel();
        }

        void NextLevel()
        {
            
            Debug.Log("NextLevel");
            int currentActiveIndex = SceneManager.GetActiveScene().buildIndex;
            int NextSceneIndex = currentActiveIndex + 1;
            if(NextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                NextSceneIndex = 0;
            }
            SceneManager.LoadScene(NextSceneIndex);
        }

        void ReloadLevel1()
        {
            
            int currentActiveIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentActiveIndex);
        }
    }
}
