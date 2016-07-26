using UnityEngine;
using CnControls;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private ShipScript _shipScript;

	// Use this for initialization
	void Start () {
		_shipScript = GetComponent<ShipScript>();
	}
	
	// Update is called once per frame
	void Update () {
		// Передвижение
        float inputX = CnInputManager.GetAxis("Horizontal");
        float inputY = CnInputManager.GetAxis("Vertical");
        
        _shipScript.Traction = Mathf.Sqrt(inputX * inputX + inputY * inputY);
		if (inputX != 0 && inputY != 0) _shipScript.finalAngle = Mathf.Atan2(inputX, -inputY) * 180 / Mathf.PI + 180;
	}

    void FixedUpdate()
    {
    
    }
}
