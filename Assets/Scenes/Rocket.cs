using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        HandleThrust();
        HandleRotation();
    }

    private void HandleRotation() {
        rigidBody.freezeRotation = true; // take manual control of rotation (spin caused by collisions)
        if (Input.GetKey(KeyCode.LeftArrow)) {
            print("left key pressed");
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            print("right key pressed");
            transform.Rotate(Vector3.back);
        }

        rigidBody.freezeRotation = false; // give control back to game physics
    }

    void HandleThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            print("spacebar pressed");
            rigidBody.AddRelativeForce(Vector3.up);
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
