using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour {

public Transform Car;
public List<Transform> roadPieces = new List<Transform>();
public List<Transform> trees = new List<Transform>();
public List<Transform> pillars = new List<Transform>();
public List<Transform> uniques = new List<Transform>();
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
    public float treeDrop = 0.25f;
    // Update is called once per frame
    void Update () {
        Transform road = GetValidPiece(roadPieces);
		if(road!=null){
			road.localPosition = new Vector3(0.0f, 0.0f, RoadPos);
			RoadPos += roadLength;
		}
        Vector3 pos;

        for (int i = 0; i < trees.Count; i++){
            pos = trees[i].localPosition;
			
            float dist = pos.z - Car.position.z;
			if(dist < -75f){
                trees[i].localPosition += Vector3.forward * 550f;
            pos = trees[i].localPosition;
			dist = pos.z - Car.position.z;
            }
			pos.y = 0 - treeDrop * (dist* dist);
            trees[i].localPosition = pos;
        }
        for (int i = 0; i < pillars.Count; i++){
            pos = pillars[i].localPosition;
			
            float dist = pos.z - Car.position.z;
			if(dist < -75f){
                pillars[i].localPosition += Vector3.forward * 550f;
            pos = pillars[i].localPosition;
			dist = pos.z - Car.position.z;
            }
			pos.y = 0 - treeDrop * (dist* dist);
            pillars[i].localPosition = pos;
        }
		for (int i = 0; i < uniques.Count; i++){
            pos = uniques[i].localPosition;
			
            float dist = pos.z - Car.position.z;
			if(dist < -75f){
                uniques[i].localPosition += Vector3.forward * 600f;
            pos = uniques[i].localPosition;
			dist = pos.z - Car.position.z;
            }
			pos.y = 0 - treeDrop * (dist* dist);
            uniques[i].localPosition = pos;
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
