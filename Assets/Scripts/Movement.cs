using UnityEngine;

public class Movement : MonoBehaviour
{
  // SECTION PARAMETERS
  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float rotationThrust = 1f;
  [SerializeField] AudioClip mainEngine;

  // SECTION CACHE
  Rigidbody rb;
  AudioSource audioSource;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    ProcessThrust();
    ProcessRotation();
  }

  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(mainEngine);
      }
      rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }
    else
    {
      audioSource.Stop();
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(rotationThrust);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      ApplyRotation(-rotationThrust);
    }
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; // freezes rotation so we can manually rotate
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; //unfreezing rotation so physics system can take over again
  }
}
