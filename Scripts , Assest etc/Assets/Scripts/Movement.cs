using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myrigidBody;
    AudioSource myaudioSource;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 80f;
    [SerializeField] AudioClip mainEngine; 
    [SerializeField] ParticleSystem thrust;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;

    // Start is called before the first frame update
    void Start()
    {
        myrigidBody = GetComponent<Rigidbody>(); 
        myaudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }



    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            // Debug.Log("Pressed space : Thrusting");
            myrigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if(!myaudioSource.isPlaying){
                myaudioSource.PlayOneShot(mainEngine);
            }
            if(!thrust.isPlaying){
                thrust.Play();
            }
        }
        else{
            myaudioSource.Stop();
            thrust.Stop();
        }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A)){
            // Debug.Log("Pressed A : rotate to the left");
            rotation(rotateThrust);
            if(!rightThrust.isPlaying){
                rightThrust.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D)){
            // Debug.Log("Pressed D : rotate to the Right");
            rotation(-rotateThrust);
            if(!leftThrust.isPlaying){
                leftThrust.Play();
            }
        }
        else{
            rightThrust.Stop();
            leftThrust.Stop();
        }
    }

    void rotation(float rotationspeed){
        myrigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationspeed);
        myrigidBody.freezeRotation = false;
    }
}
