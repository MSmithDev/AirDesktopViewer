using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonitorSetup : MonoBehaviour
{
	public GameObject monitors;

	IEnumerator Start()
	{
		//wait 1 second to allow monitors to load and then hide the glasses display
		//todo change this to be adaptable
		yield return new WaitForSeconds(1);
		foreach (Transform t in monitors.transform) {
			if (string.Compare(("\\\\.\\DISPLAY2"),t.name) == 0) {
				t.gameObject.SetActive(false);
			}
		}
		
	}
}
