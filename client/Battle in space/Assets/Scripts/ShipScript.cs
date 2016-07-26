using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

    // Максимальная сила тяги
    public int maxTraction;
    // Текущая тяга
    private Vector2 _traction;
    /// Крутящий момент
    public int torque;
    // Прочность корабля
    public int shipStrength;
    // Текущая прочность
    public int strength;
    // Количество мест для оборудования
    public int numberOfEquipments;
    // Количество мест для курсового вооружения
    public int numberOfАrms;
    // Количество мест под тяжелые снаряды
    public int numberOfMissiles;
    // Количество мест для турелей
    public int numberOfTurrets;

	private Rigidbody2D _rb;
	// момент инерции
	private float _inertia;
	// целевой угол поворота
	public float finalAngle;

    // Текущая тяга
    public float Traction
    {
        get { return _traction.y; }
        set 
		{
			float angle = transform.eulerAngles.z * Mathf.PI / 180;
			_traction.x = -value * maxTraction * Mathf.Sin(angle);
			_traction.y = value * maxTraction * Mathf.Cos(angle);
		}
    }

	// Возвращает разность между 2 углами в зависимости от направления
	// Пременима для углов от 0 до 360
	public float DifferenceAngleWithWay(float sourceAngle, float targetAngle, bool isАnticlockwise)
	{
		if (isАnticlockwise && targetAngle > sourceAngle || !isАnticlockwise && targetAngle < sourceAngle)
		{
			return Mathf.Abs(targetAngle - sourceAngle);
		}
		else
		{
			return 360 - Mathf.Abs(targetAngle - sourceAngle);
		}
	}

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody2D>();
		_inertia = _rb.inertia;
		finalAngle = _rb.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	 
	}

    void FixedUpdate()
    {
        _rb.AddForce(_traction); // Придал импульс

		float stopAngle = _inertia * _rb.angularVelocity * _rb.angularVelocity / (2 * torque);	// Угол через который вращение прекратится
		bool isАnticlockwise = _rb.angularVelocity > 0;
		//stopAngle *= isАnticlockwise ? 1 : -1;
		//stopAngle += transform.eulerAngles.z;
		Debug.Log("_rb.angularVelocity " + _rb.angularVelocity);
		Debug.Log("stopangle " + stopAngle);
		Debug.Log(finalAngle);
		Debug.Log("_rb.rotation " + transform.eulerAngles.z);
		Debug.Log("DifferenceAngleWithWay " + DifferenceAngleWithWay(transform.eulerAngles.z, finalAngle, isАnticlockwise));
		if (DifferenceAngleWithWay(transform.eulerAngles.z, finalAngle, isАnticlockwise) > stopAngle)
		{
			// ускоряем вращение
			if (isАnticlockwise) _rb.AddTorque(torque);
			else _rb.AddTorque(-torque);
		}
		else
		{
			if (stopAngle < 3)
			{
				_rb.rotation = finalAngle;
			}
			else
			{
				// тормозим
				if (isАnticlockwise) _rb.AddTorque(-torque);
				else _rb.AddTorque(torque);
			}
		}
		
    }
}
