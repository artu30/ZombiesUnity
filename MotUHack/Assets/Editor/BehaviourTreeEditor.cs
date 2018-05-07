using System;
using UnityEditor;
using UnityEngine;

public class BehaviourTreeEditor : EditorWindow {

    private static Texture2D tex;

    [MenuItem("Window/Behaviour Tree")]
    public static void ShowWindow()
    {
        tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        tex.SetPixel(0, 0, new Color(0.007f, 0.066f, 0.156f));
        tex.Apply();
        GetWindow<BehaviourTreeEditor>("Behaviour Tree");
    }

    void OnGUI()
    {
        //EditorGUI.DrawPreviewTexture(new Rect(0, 0, maxSize.x, maxSize.y), tex);
        /*Event current = Event.current;
        if (current.type == EventType.ContextClick)
        {
            
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Prueba"), false, OnCreateSelected, current.mousePosition);
            menu.DropDown(new Rect(current.mousePosition, new Vector2(100f, 100f)));
            Texture2D tex2 = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            tex2.SetPixel(0, 0, new Color(1f, 1f, 1f));
            tex2.Apply();
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a box");
            //EditorGUI.DrawPreviewTexture(new Rect(199f, 190f, 200f, 200f), tex2);
            Repaint();
            menu.ShowAsContext();
        }*/
    }

    void Update()
    {
        /*Event current = Event.current;
        if (current.type == EventType.ContextClick)
        {
            Debug.Log("Right button clicked at position : " + current.mousePosition.ToString());
        }*/
    }


    private void OnCreateSelected(object mousePosition)
    {
        Debug.Log("Right button clicked at position : " + mousePosition.ToString());
        Rect r = new Rect((Vector2)mousePosition, new Vector2(200, 200));
        
        
    }

}
