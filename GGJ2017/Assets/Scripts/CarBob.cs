using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBob : MonoBehaviour {
    public Transform carBody;
    public float Amplitude = 1.0f;
    public float Frequency = 1.0f;
    private Rigidbody m_Rigidbody;
    private Vector3 localPos = Vector3.zero;
    private float offset = 0.0f;
    // Use this for initialization
    void Start () {
        m_Rigidbody = GetComponent<Rigidbody>();
        localPos = carBody.localPosition;

    }
	
	// Update is called once per frame
	void Update () {
        offset = Mathf.Sin(Time.time * Frequency) * Amplitude;
        carBody.transform.localPosition = localPos + transform.up * offset;
    }
}
