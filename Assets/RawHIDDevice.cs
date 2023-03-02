using UnityEngine;
using UnityEngine.InputSystem;
using Device.Net;
using Hid.Net.Windows;
using System.Reactive.Linq;
using System;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.XR.Interaction;

public class RawHIDDevice : BasePoseProvider
{
        [DllImport("AirAPI_Win", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartConnection();

        [DllImport("AirAPI_Win", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetQuaternion();
        [DllImport("AirAPI_Win", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetEuler();



    private void Start()
    {
        StartConnection();

    }

  
    public override bool TryGetPoseFromProvider(out Pose output)
    {
        if (true)
        {

            IntPtr ptr = GetEuler();
            float[] arr = new float[3];
            Marshal.Copy(ptr, arr, 0, 3);

            Quaternion target = Quaternion.Euler(arr[1]+90.0f, -arr[2], -arr[0]+180.0f);
            output = new Pose(new Vector3(0, 0, 0), target);

            return true;
        }
        else
        {
            output = new Pose(new Vector3(0, 0, 0), Quaternion.identity);
            return false;
        }
    }


}