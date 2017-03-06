using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlotTiler : MonoBehaviour {
	public Vector2 playSpaceSize = new Vector2();
	public float tileSize;
	private Dictionary<float, float> tiler = new Dictionary<float, float>();
	public int GridSpacing = 1;

	private Vector2 numTiles = new Vector2 ();
	public GameObject tilePref;
	private GameObject tile;
	// Use this for initialization
	void Start () 
	{
		if (playSpaceSize.x == 0) 
		{
			playSpaceSize.x = 400;
			playSpaceSize.y = 300;
		}
		GameObject.Find ("Plot").transform.localScale = new Vector3 (playSpaceSize.x, 0.001f, playSpaceSize.y);
		numTiles.x =  Mathf.CeilToInt(playSpaceSize.x / tileSize);
		numTiles.y = Mathf.CeilToInt(playSpaceSize.y / tileSize);

		//tile = (GameObject) Instantiate(tilePref, 

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = SnapToGrid (transform, GridSpacing);
	}

	Vector3 SnapToGrid (Transform obj, int gridSnap)
	{
		Vector3 pos = obj.transform.position;
		Vector3 snapHits = new Vector3 (Mathf.Round (pos.x / gridSnap) * gridSnap, pos.y, Mathf.Round (pos.z / gridSnap) * gridSnap);
		Vector3 snapTransform = new Vector3 (snapHits.x - (obj.transform.localScale.x / 2.0F), pos.y, snapHits.z - (obj.transform.localScale.z / 2.0F));
		pos = snapTransform;
		return pos;
	}
}
