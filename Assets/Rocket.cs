using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rocketThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State{Alive, Transcending, Dying};
    State currentState;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (currentState != State.Dying) 
        {
            RespondToThrustInput();
            RespondToRotationInput();
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (currentState != State.Alive) { //ignore collisions if dead
            return;
        }

        switch(collision.gameObject.tag) { //reads gameObject's tag
            case "Friendly":
                print("Ok");
                currentState = State.Alive;
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Enemy":
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence() 
    {
        currentState = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextScene", 3f); //waits 1 second before calling function. Invoke requires a string
    }

    private void StartDeathSequence()
    {
        currentState = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstLevel", 3f);
    }
        
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0); //level one
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); //level two
    }

    void RespondToThrustInput() 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else 
        {
            print("Stopped Sound");
            audioSource.Stop();
        }
    }

    void ApplyThrust() 
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying) //prevents sound from layering
        { 
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void RespondToRotationInput()
    {
        rigidBody.freezeRotation = true; //take manual control of the rotation

        float rotationThisFrame = rocketThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //resume physics control of rotation
    }
}
