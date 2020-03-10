using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    // need to make this available so we can tweak it in the editor
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 200f;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        HandleThrust();
        HandleRotation();
    }

    void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Friendly":
                // do nothing
                print("Friendly");
                break;
            case "Fuel":
                print("Fuel");
                break; 
            default:
                print("Dead!");
                break;
        }
    }

    private void HandleRotation() {
        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // give control back to game physics
    }

    void HandleThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            float mainThrustThisFrame = mainThrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * mainThrustThisFrame);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } else {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }
        }
    }
}
