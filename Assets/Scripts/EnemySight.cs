using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{

    private Enemy owner;
    // Use this for initialization
    void Start()
    {
        owner = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            owner.Target = other.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            owner.Target = null;
        }

    }
}
