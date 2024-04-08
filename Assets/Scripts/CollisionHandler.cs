using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
  void OnCollisionEnter(Collision other)
  {
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("This is friendly");
        break;
      case "Finish":
        Debug.Log("You reached the landing pad");
        break;
      default:
        Debug.Log("You collided with something");
        break;
    }
  }
}
