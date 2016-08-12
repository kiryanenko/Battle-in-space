using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject Ship;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 pos = Ship.transform.position;
		pos.z = transform.position.z;
		transform.position = pos;
	}
}
