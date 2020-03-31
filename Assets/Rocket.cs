using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rocketThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidBody;
    AudioSource thrustSound;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        Thrust();
        MotionDirection();
    }

    void OnCollisionEnter(Collision collision) {
        switch(collision.gameObject.tag) { //reads gameObject's tag
            case "Friendly":
                print("Ok");
                break;
            case "Enemy":
                print("Dead");
                Start();
                break;
        }
    }
        
    void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!thrustSound.isPlaying) { //prevents sound from layering
                thrustSound.Play();
            }
        }
        else {
            thrustSound.Stop();
        }
    }
    void MotionDirection() {

        rigidBody.freezeRotation = true; //take manual control of the rotation

        float rotationThisFrame = rocketThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
           
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //resume physics control of rotation
    }
}
