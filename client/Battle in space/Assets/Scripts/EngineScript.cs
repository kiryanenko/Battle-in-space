using UnityEngine;
using System.Collections;

public class EngineScript : MonoBehaviour {
    ShipScript _ship;
    ParticleSystem _engine;
    public float maxSize;         // Максимальный размер частиц

	// Use this for initialization
	void Start () {
        _ship = GetComponentInParent<ShipScript>();
        _engine = transform.Find("engine").gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        _engine.startSize = maxSize * _ship.Traction;
	}
}
