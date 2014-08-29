//Taken from: http://wiki.unity3d.com/index.php?title=PopupList#C.23_-_Popup.cs_-_Updated
// Popup list created by Eric Haines
// Popup list Extended by John Hamilton. john@nutypeinc.com
// Edited to work with UnityEngine.Resolution by: Jonathan Hunter

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Popup
{
    static int popupListHash = "PopupList".GetHashCode();

    public static bool List(Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, object[] list,GUIStyle listStyle)
    {
        return List(position, ref showList, ref listEntry, buttonContent, list, "button", "box", listStyle);
    }

    public static bool List(Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, object[] list, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
    {
        int controlID = GUIUtility.GetControlID(popupListHash, FocusType.Passive);
        bool done = false;
        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.mouseDown:
                if (position.Contains(Event.current.mousePosition))
                {
                    GUIUtility.hotControl = controlID;
                    showList = true;
                }
                break;
            case EventType.mouseUp:
                if (showList)
                {
                    done = true;
                }
                break;
        }

        GUI.Label(position, buttonContent, buttonStyle);
        if (showList)
        {
            // Get our list of strings
            string[] text = new string[list.Length];
            // convert to string
            for (int i = 0; i < list.Length; i++)
            {
                text[i] = list[i].ToString();
            }

            Rect listRect = new Rect(position.x, position.y, position.width, list.Length * 20);
            GUI.Box(listRect, "", boxStyle);
            listEntry = GUI.SelectionGrid(listRect, listEntry, text, 1, listStyle);
        }
        if (done)
        {
            showList = false;
        }
        return done;
    }

    public static bool List(Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, Resolution[] list, GUIStyle listStyle)
    {
        return List(position, ref showList, ref listEntry, buttonContent, list, "button", "box", listStyle);
    }

    public static bool List(Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, Resolution[] list, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
    {
        int controlID = GUIUtility.GetControlID(popupListHash, FocusType.Passive);
        bool done = false;
        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.mouseDown:
                if (position.Contains(Event.current.mousePosition))
                {
                    GUIUtility.hotControl = controlID;
                    showList = true;
                }
                break;
            case EventType.mouseUp:
                if (showList)
                {
                    done = true;
                }
                break;
        }

        GUI.Label(position, buttonContent, buttonStyle);
        if (showList)
        {
            // Get our list of strings
            string[] text = new string[list.Length];
            // convert to string
            for (int i = 0; i < list.Length; i++)
            {
                text[i] = list[i].width+"x"+list[i].height;
            }

            Rect listRect = new Rect(position.x, position.y, position.width, list.Length * 20);
            GUI.Box(listRect, "", boxStyle);
            listEntry = GUI.SelectionGrid(listRect, listEntry, text, 1, listStyle);
        }
        if (done)
        {
            showList = false;
        }
        return done;
    }
}