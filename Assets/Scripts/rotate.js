#pragma strict

var target : Transform;

var orbitSpeed : float = 10.0;

function Start () {
	
}

function Update () {
	transform.RotateAround(target.position, Vector3.up,orbitSpeed * Time.deltaTime);
}
