using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	void OnTransformChildrenChanged(){
		print(transform.childCount);
	}
}
