using UnityEngine;
using System.Collections;

public class mainCamera_Action : MonoBehaviour {

	GameObject player;

	public float offsetX = 0f;
	public float offsetY = 0f;
	public float offsetZ = -10f;

	Vector3 cameraPosition;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		StartCoroutine ("CheckPlayer");
	}

	void LateUpdate ()
	{
		cameraPosition.x = player.transform.position.x + offsetX;
		cameraPosition.y = player.transform.position.y + offsetY;
		cameraPosition.z = player.transform.position.z + offsetZ;

		transform.position = cameraPosition;
	}

	IEnumerator CheckPlayer()
	{
		while (true) {
			if (player) {
				yield return new WaitForSeconds (1.0f);
			}else {
				player = GameObject.FindGameObjectWithTag ("Player");
				yield return new WaitForSeconds (1.0f);
			}
		}
	}
}