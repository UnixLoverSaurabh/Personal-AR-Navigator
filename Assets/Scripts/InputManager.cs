using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class InputManager : MonoBehaviour {

	public GameObject sphereGO;
	// Use this for initialization

	private Vector3[] hitArr;
	private int i;
	private int j;
	 GameObject[] myLines;
	 public float speed;
	 public GameObject followPrefab;
	 private int follow1;

	void Start () {

		hitArr = new Vector3[100];
		i=0;
		myLines = new GameObject[100];
		follow1 = 0;
		speed = 0.75f;
		j=0;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		

		// foreach(var t in Input.touches){

		// 	if(t.phase != TouchPhase.Began)
		// 		continue;

		// 	var ray = Camera.main.ScreenPointToRay(t.position);
			if(Input.GetMouseButtonDown(0)){	
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hitInfo;
			if(Physics.Raycast(ray , out hitInfo))
			{
				hitArr[i] = hitInfo.point;
				i++;
				var go  = GameObject.Instantiate(sphereGO , hitInfo.point , Quaternion.identity);
				go.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
				Debug.Log(hitArr[i-1]);

				if(i>1){
					myLines[i] = new GameObject();
					myLines[i].AddComponent<LineRenderer>();
           			var lr = myLines[i].GetComponent<LineRenderer>();
					lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            		lr.startColor = Color.green;
            		lr.useWorldSpace = false;
            		lr.endColor = Color.green;
            		lr.startWidth = 0.04f;//0.03f;
            		lr.endWidth = 0.04f;//0.03f;
            		lr.numCapVertices = 5;   
					lr.SetPosition(0, hitArr[i-1]);
					lr.SetPosition(1, hitArr[i-2]);
				}

			}
		}

		if(follow1 == 1){
			float step = speed * Time.deltaTime;
       		

			if(j>0){

				followPrefab.transform.position = Vector3.MoveTowards(followPrefab.transform.position, hitArr[j-1], step);
				

				Vector3 relativePos = hitArr[j-1] - followPrefab.transform.position;
        		Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        		followPrefab.transform.rotation = rotation;



				if(followPrefab.transform.position == hitArr[j-1]){
					j--;
				}



			}



		}


		
	}
	 
	public void follow(){
		follow1 = 1;

		followPrefab =  GameObject.Instantiate(followPrefab , hitArr[i-1] , Quaternion.identity);

		j = i-1;

     }

	


}

//AIzaSyDp902Irr3gsMCDcCKU9ymz1Pn5FOElRUo