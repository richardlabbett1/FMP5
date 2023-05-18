using UnityEngine;

public class Kill : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {

        if (collisionInfo.collider.CompareTag("Bullet"))
        {
            Debug.Log("hello");
            Destroy(gameObject); // this destroys the bullet
        }
    }
}
