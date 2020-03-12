using UnityEngine;

[DisallowMultipleComponent] // stops you assigning two oscillators
public class SlidingObsatcle : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    // [Range(0, 1)] [SerializeField] float movementFactor; // 0 to 1
    [SerializeField] float period = 0.3f;

    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time * period;
        const float tau = Mathf.PI * 2; // about 6.28
        float rawSineWave = Mathf.Sin(cycles * tau);
        float movementFactor = rawSineWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
