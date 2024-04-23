using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  // SECTION VARIABLES
  // PARAMETERS
  [SerializeField] float levelLoadDelay = 1f;
  [SerializeField] AudioClip successAudio;
  [SerializeField] AudioClip crashAudio;

  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;

  // CACHE
  AudioSource audioSource;

  // STATE
  bool isTransitioning = false;
  bool collisionDisabled = false;
  // !SECTION VARIABLES

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    // RespondToDebugKeys();
  }

  void RespondToDebugKeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      collisionDisabled = !collisionDisabled; // toggle collision
    }
  }

  void OnCollisionEnter(Collision other)
  {
    if (isTransitioning || collisionDisabled) { return; }
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("This is friendly");
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      default:
        StartCrashSequence();
        break;
    }
  }

  void StartSuccessSequence()
  {
    isTransitioning = true;
    successParticles.Play();
    audioSource.Stop();
    audioSource.PlayOneShot(successAudio);
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }
  void StartCrashSequence()
  {
    isTransitioning = true;
    crashParticles.Play();
    audioSource.Stop();
    audioSource.PlayOneShot(crashAudio);
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelay);
  }

  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  void LoadNextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }
}
