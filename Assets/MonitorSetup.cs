using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonitorSetup : MonoBehaviour
{
	public GameObject monitors;

	public int GetCurrentDisplayNumber()
    {
        List<DisplayInfo> displayLayout = new List<DisplayInfo>();
        Screen.GetDisplayLayout(displayLayout);
        return displayLayout.IndexOf(Screen.mainWindowDisplayInfo);
    }

	IEnumerator Start()
	{
		yield return new WaitForSeconds(1);
		//todo, open on nreals window, define monitor id and test
		//for (int i = 1; i < Display.displays.Length; i++) {
		//	Debug.Log(i);
		    //Display.displays[i].Activate();
		//}
		
		//hide nreal monitor
		
		//get the current monitor id
		


		int x = 0;
		int nrealMonitorIdValue = GetCurrentDisplayNumber() + 1;
		
		//Debug.Log("MONITOR:" + nrealMonitorIdValue);
		//Debug.Log("\\\\.\\DISPLAY" + nrealMonitorIdValue.ToString());

		foreach (Transform t in monitors.transform) {
			if (string.Compare(("\\\\.\\DISPLAY2"),t.name) == 0) {
				//Debug.Log(t.name);
				t.gameObject.SetActive(false);
			}
			
			x += 1;
			// Debug.Log(x);
			// Debug.Log(t.name);
			// Debug.Log("Compare: "+string.Compare("\\\\.\\DISPLAY1",t.name));
		}
		
	}
}
