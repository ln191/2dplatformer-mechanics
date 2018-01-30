using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Kunai : MonoBehaviour {
    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidbody;
    private Vector2 direction;
	// Use this for initialization
	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
    void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }
	public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    /// <summary>
    /// Unity standard method
    /// Runs when the gameobject is out the camera view 
    /// OBS!!! When in unity editor it will dectect both game screen and edit screen camera, when built it work correct after game screen.
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
