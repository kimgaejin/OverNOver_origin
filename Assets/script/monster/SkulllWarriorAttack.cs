using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulllWarriorAttack : MonoBehaviour {

    public float deleteTime = 0.25f;

    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

}
