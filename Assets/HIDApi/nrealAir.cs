using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;


public class nrealAir : MonoBehaviour
{
 IntPtr air;
 hid_device_info cur_dev;
 hid_device_info air3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Attempting to find Nreal Air");
        IntPtr nrealDevice = HIDapi.hid_enumerate(0x3318, 0x0424);
        hid_device_info devs = (hid_device_info)Marshal.PtrToStructure(nrealDevice, typeof(hid_device_info));

        if (nrealDevice == IntPtr.Zero)
        {
            Debug.Log("Nreal Air not found");
            return;
        }
        else
        {
            
            Debug.Log("Interface: " + devs.interface_number.ToString());
            




            Debug.Log("Nreal Air found");
            Debug.Log("Getting HID Info");
            hid_device_info info = (hid_device_info)Marshal.PtrToStructure(nrealDevice, typeof(hid_device_info));

            Debug.Log("Vendor: " + info.vendor_id.ToString("X"));
            Debug.Log("Product: " + info.product_id.ToString("X"));

            Debug.Log("Opening Nreal Air");
            air = HIDapi.hid_open_path(info.path);
            if(air == IntPtr.Zero)
            {
                Debug.Log("Failed to open Nreal Air");
                return;
            }
            else
            {
                Debug.Log("Nreal Air opened");
                byte[] magic_payload = new byte[] { 0xaa, 0xc5, 0xd1, 0x21, 0x42, 0x04, 0x00, 0x19, 0x01 };
                Debug.Log("Writing Magic payload");
                int magic = HIDapi.hid_write(air, magic_payload, (UIntPtr)magic_payload.Length);
                Debug.Log("Magic = " + magic.ToString());
                if(magic == -1)
                {
                    Debug.Log("Failed to write magic payload");
                }
                else {
                    Debug.Log("Magic payload written");
                }
            }

        }




    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct report {
        public UInt16 ukn1;
        public UInt16 ukn2;
        public sbyte ukn3;
        public UInt32 some_counter1;
        public sbyte ukn4;
        public UInt64 ukn5;
        public sbyte ukn6;
        public Int16 rate_pitch;
        public sbyte ukn7;
        public Int16 rate_roll;
        public sbyte ukn8;
        public Int16 rate_yaw;
        public UInt16 ukn9;
        public UInt32 ukn10;
        public sbyte ukn11;
        public Int16 rot_roll;
        public sbyte ukn12;
        public Int16 rot_pitch;
        public sbyte ukn13;
        public Int16 rot_yaw;
        public UInt16 ukn14;
        public UInt32 ukn15;
        public Int16 mag1;
        public Int16 mag2;
        public Int16 mag3;
        public UInt32 some_counter2;
        public UInt32 ukn16;
        public sbyte ukn17;
        public sbyte ukn18;
    }

    
    // Update is called once per frame
    void Update()
    {

        if(air == IntPtr.Zero)
        {
            return;
        }
        else {
            report test = new report();
            byte[] data = new byte[Marshal.SizeOf(test)];
            HIDapi.hid_set_nonblocking(air, 1);
            int res = HIDapi.hid_read(air, data, new UIntPtr(Convert.ToUInt32(Marshal.SizeOf(test))));

            


            //log data to human readable text
            if (res == -1) Debug.LogError("HidAPI reports error " + res + " on read: " + Marshal.PtrToStringUni(HIDapi.hid_error(air)));
            else
            {
                Debug.Log(BitConverter.ToString(data, 0));
            }
            
        }

    }
}
//(hid_device_info)Marshal.PtrToStructure(nrealDevice, typeof(hid_device_info));
