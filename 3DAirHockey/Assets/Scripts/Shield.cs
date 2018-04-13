using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public Collider PlayerCol;
    private Collider ShieldCol;

    // Use this for initialization
	void Start () {
        ShieldCol = GetComponent<Collider>();
        Physics.IgnoreCollision(ShieldCol, PlayerCol);
    }
}
