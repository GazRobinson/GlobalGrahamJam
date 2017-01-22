using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour {

public Transform Car;
public List<Transform> roadPieces = new List<Transform>();
public List<Transform> trees = new List<Transform>();
public float RoadPos = 0.0f;
float roadLength = 100.0f;
    // Use this for initialization
    void Start () {
        Init();
    }
	void Init(){
        roadLength = roadPieces[0].localScale.z;
        for(int i = 0; i < roadPieces.Count;i++){
			roadPieces[i].localPosition = new Vector3(0.0f, 0.0f, RoadPos);
			RoadPos += roadLength;
        }
    }
	// Update is called once per frame
	void Update () {
        Transform road = GetValidPiece(roadPieces);
		if(road!=null){
			road.localPosition = new Vector3(0.0f, 0.0f, RoadPos);
			RoadPos += roadLength;
		}
    }
	public Transform GetValidPiece(List<Transform> pieces){
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].position.z < Car.position.z - 75.0f){
                return pieces[i];
            }
		}
        return null;
    }
}
