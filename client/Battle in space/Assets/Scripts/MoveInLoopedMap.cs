using UnityEngine;
using System.Collections;

public class MoveInLoopedMap : MonoBehaviour {
	private int _size;
	// Use this for initialization
	void Start () {
		_size = GetComponentInParent<LoopedMap>().size;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (Mathf.Abs(pos.x) > _size / 2) pos.x -= pos.x > 0 ? _size : -_size;
		if (Mathf.Abs(pos.y) > _size / 2) pos.y -= pos.y > 0 ? _size : -_size;
		transform.position = pos;
	}
}
