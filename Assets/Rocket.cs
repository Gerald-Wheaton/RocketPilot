using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

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
    void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!thrustSound.isPlaying) { //prevents sound from layering
                thrustSound.Play();
            }
            print("Thrust");
        }
        else {
            thrustSound.Stop();
        }
    }
    void MotionDirection() {
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
            print("Left");
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward);
            print("Right");
        }
    }
}
