using UnityEditor;
using UnityEngine;

public static class FSMEditor
{
	private const int BTNWIDTH = 20;

	[InitializeOnLoadMethod]
	private static void Example()
	{
		EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
	}

	private static void OnGUI( int instanceID, Rect selectionRect )
	{
		GameObject go = EditorUtility.InstanceIDToObject( instanceID ) as GameObject;
		if (go == null) return;

		BaseController presenter = go.GetComponent<BaseController>();
		if (presenter == null) return;

		if (presenter.inState) GUI.backgroundColor = Color.red;
		
		var pos     = selectionRect;
		pos.x       = pos.xMax - BTNWIDTH;
		pos.width   = BTNWIDTH;

		if ( GUI.Button( pos, "  " ) )
		{
			
		}

		GUI.backgroundColor = Color.white;
	}
}