using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using System.IO.Ports;
using System;

public class testpose : BasePoseProvider
{

    private SerialPort serial;
       private float yaw, pitch, roll;
       	private float x;
	private float y;
	private float z;

    // Start is called before the first frame update
    void Start()
    {
        serial = new SerialPort("COM3", 115200);
		serial.Open();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool TryGetPoseFromProvider(out Pose output)
    {
        try{

        //Debug.Log("Start readLine");
        // Read data from the UM7
        string data = serial.ReadLine();
       // Debug.Log("End ReadLine: "+ data.ToString());
        // Check if the data is a valid orientation packet
        if (data.StartsWith("$PCHRA"))
        {
            //Debug.Log(data);
            // Split the data into values
            string[] values = data.Split(',');

            // Parse the yaw, pitch, and roll values
            x = float.Parse(values[2]);
            y = float.Parse(values[3]);
            z = float.Parse(values[4]);

            // Use the yaw, pitch, and roll values to rotate the object
            //transform.rotation = Quaternion.Euler(pitch, yaw, roll);
        }
        } catch (Exception ex){
            Debug.Log(ex.StackTrace);
        }


		Quaternion target = Quaternion.Euler(y, z, x);


        output = new Pose(new Vector3(0, 0, 0), target);
        return true;
    }
}
