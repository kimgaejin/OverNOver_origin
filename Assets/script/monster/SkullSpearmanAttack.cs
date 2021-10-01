using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpearmanAttack : MonoBehaviour {

    GameObject player;
    Vector3 target;

    Rigidbody2D rigid;

    float throwingPower = 0.0f;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform.position;

        rigid = GetComponent<Rigidbody2D>();

        throwingPower = 5.0f;

        rigid.AddForce (new Vector2(throwingPower * (target.x - transform.position.x)/Mathf.Abs(target.x - transform.position.x), 0), ForceMode2D.Impulse);
        Destroy(gameObject, 2.0f);
	}

    void Update()
    {
        rigid.position += rigid.velocity * Time.deltaTime ;
    }
}
