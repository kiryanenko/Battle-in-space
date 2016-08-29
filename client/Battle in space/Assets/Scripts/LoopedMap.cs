using UnityEngine;
using System.Collections;

public class LoopedMap : MonoBehaviour {

	public int size = 1000;
	public bool isClone = false;

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () {
		if (!isClone)
		{
			float z = transform.position.z;
			for (int i = -1; i <= 1; i++)
				for (int j = -1; j <= 1; j++)
				{
					if (i != 0 || j != 0)
					{
						var newMap = Instantiate(gameObject);
						newMap.transform.position = new Vector3(i * size, j * size, z);
						newMap.GetComponent<LoopedMap>().isClone = true;
					}
				}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
