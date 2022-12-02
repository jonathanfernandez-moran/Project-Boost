using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem Thrust;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;
    Rigidbody m_Rigidbody;
    AudioSource m_Audiosource;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void StartThrusting()
    {
        m_Rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!m_Audiosource.isPlaying)
        {
            m_Audiosource.PlayOneShot(mainEngine);
        }
        if (!Thrust.isPlaying)
        {
            Thrust.Play();
            Debug.Log("Playing");
        }
    }
    void StopThrusting()
    {
        m_Audiosource.Stop();
        Thrust.Stop();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else
        {
            StopRotationing();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotateThrust);
        if (!rightThrust.isPlaying)
        {
            rightThrust.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotateThrust);
        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();
        }
    }

    private void StopRotationing()
    {
        rightThrust.Stop();
        leftThrust.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        m_Rigidbody.freezeRotation = true; // Freezing roation so we can manually roate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        m_Rigidbody.freezeRotation = false; // Unfreezing rotation so we can manually rotate
    }


}
