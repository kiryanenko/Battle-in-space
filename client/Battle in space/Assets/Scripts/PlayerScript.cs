using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Vector2 speed = new Vector2(10, 10);
    private Vector2 movement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);

        // 5 - Стрельба
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        // Замечание: Для пользователей Mac, Ctrl + стрелка - это плохая идея

        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                // ложь, так как игрок не враг
                weapon.Attack(false);
            }
        }
	}

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
