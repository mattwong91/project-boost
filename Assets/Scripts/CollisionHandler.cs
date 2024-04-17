using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelLoadDelay = 1f;
  [SerializeField] AudioClip crashAudio;
  [SerializeField] AudioClip successAudio;

  AudioSource audioSource;

  bool isTransitioning = false;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision other)
  {
    if (isTransitioning) { return; }
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
    // TODO add particle effect upon success
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(successAudio);
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }
  void StartCrashSequence()
  {
    // TODO add particle effect upon crash
    isTransitioning = true;
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
