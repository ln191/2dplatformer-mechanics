using UnityEngine;
using System.Collections;

public class CamaraFollow : MonoBehaviour {
    [SerializeField]
    private float xMin, xMax, yMin, yMax;

    Transform target;
	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax),transform.position.z);
        
	}
}
