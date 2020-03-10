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
        ProcessInput();
    }

    private void ProcessInput() {
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
        if (Input.GetKey(KeyCode.LeftArrow)) {
            print("left key pressed");
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            print("right key pressed");
            transform.Rotate(Vector3.back);
        }
    }
}
