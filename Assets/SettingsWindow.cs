using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SettingsWindow : MonoBehaviour
{
  private string playerName = "Player";
    private Rect windowRect;
    
    void Start()
    {
        // Get the size of monitor 1
        var display1 = Display.displays[0];
        var screen1Size = new Vector2(display1.systemWidth, display1.systemHeight);
        
        // Set the position and size of the window to be on monitor 1
        windowRect = new Rect(screen1Size.x / 2f, 0f, screen1Size.x / 2f, screen1Size.y);
    }
    
    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, DrawWindow, "Player Settings");
    }
    
    void DrawWindow(int windowId)
    {
        playerName = GUILayout.TextField(playerName);
    }
}
