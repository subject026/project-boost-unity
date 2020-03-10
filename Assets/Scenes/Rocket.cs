using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    // need to make this available so we can tweak it in the editor
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (state == State.Alive)
        {
            HandleThrustInput();
            HandleRotationInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadLevel2", 1f); // delay business
    }

    void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathExplosion);
        deathParticles.Play();
        Invoke("LoadLevel1", 1f);
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene(0);
    }

    void LoadLevel2()
    {
        SceneManager.LoadScene(1);
    }

    private void HandleRotationInput()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // give control back to game physics
    }

    void HandleThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float mainThrustThisFrame = mainThrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * mainThrustThisFrame);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                mainEngineParticles.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                mainEngineParticles.Stop();
            }
        }
    }
}
