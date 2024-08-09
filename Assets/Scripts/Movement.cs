using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float MainThrust = 100f;
    [SerializeField] float MainRotation = 100f;
    [SerializeField] AudioClip MainEngine;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessThrust();
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * MainThrust);
            if(!audioSource.isPlaying )
            {
                audioSource.PlayOneShot(MainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(MainRotation);
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-MainRotation);
        }

    void ApplyRotation(float rotateThisFeild)
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * Time.deltaTime * rotateThisFeild);
            rb.freezeRotation = false;
        }
    }
}
