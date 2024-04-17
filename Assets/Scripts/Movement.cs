using UnityEngine;

public class Movement : MonoBehaviour
{
  // SECTION VARAIBLES
  // PARAMETERS
  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float rotationThrust = 1f;
  [SerializeField] AudioClip mainEngine;

  [SerializeField] ParticleSystem mainEngineParticles;
  [SerializeField] ParticleSystem leftThrusterParticles;
  [SerializeField] ParticleSystem rightThrusterParticles;

  // CACHE
  Rigidbody rb;
  AudioSource audioSource;
  // !SECTION VARIABLES

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
      if (!mainEngineParticles.isPlaying)
      {
        mainEngineParticles.Play();
      }
    }
    else
    {
      audioSource.Stop();
      mainEngineParticles.Stop();
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(rotationThrust);
      if (!rightThrusterParticles.isPlaying)
      {
        rightThrusterParticles.Play();
      }
    }
    else if (Input.GetKey(KeyCode.D))
    {
      ApplyRotation(-rotationThrust);
      if (!leftThrusterParticles.isPlaying)
      {
        leftThrusterParticles.Play();
      }
    }
    else
    {
      rightThrusterParticles.Stop();
      leftThrusterParticles.Stop();
    }
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; // freezes rotation so we can manually rotate
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; //unfreezing rotation so physics system can take over again
  }
}
