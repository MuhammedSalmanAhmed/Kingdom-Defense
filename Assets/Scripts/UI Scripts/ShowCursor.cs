using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D ReleasedState;
    [SerializeField]
    private Texture2D PressedState;

    private Vector2 hotspot = Vector2.zero;
    [SerializeField]
    private CursorMode _cursorMode = CursorMode.Auto;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(ReleasedState,hotspot,_cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(PressedState, hotspot, _cursorMode);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(ReleasedState,hotspot, _cursorMode);
        }
    }
}
