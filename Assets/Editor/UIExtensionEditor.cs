using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class UIExtensionEditor
{
	[MenuItem("GameObject/UI/Text",false,1)]
	private static void AddUITextComponent()
	{
		GameObject newGameObject = new GameObject();
		newGameObject.name = "Text";

		newGameObject.transform.SetParent(Selection.activeGameObject == null ? null : Selection.activeGameObject.transform);
		RectTransform rt = newGameObject.AddComponent<RectTransform>();
		rt.anchoredPosition = Vector2.zero;
		rt.sizeDelta = new Vector2(160, 30);

		UIText text = newGameObject.AddComponent<UIText>();
		text.color = new Color (0.2f, 0.2f, 0.2f, 1f);
		text.text = "New Text";
		text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		text.alignment = TextAnchor.MiddleCenter;
	}

	[MenuItem("GameObject/UI/Button",false,1)]
	private static void AddUIButtonComponent()
	{
		GameObject newGameObject = new GameObject();
		newGameObject.name = "Button";

		newGameObject.transform.SetParent(Selection.activeGameObject == null ? null : Selection.activeGameObject.transform);
		RectTransform rt = newGameObject.AddComponent<RectTransform>();
		rt.anchoredPosition = Vector2.zero;
		rt.sizeDelta = new Vector2(160, 30);

		Image image = newGameObject.AddComponent<Image>();
		UIButton button = newGameObject.AddComponent<UIButton>();
		button.targetGraphic = image;

		Selection.activeObject = newGameObject;
		AddUITextComponent();
	}
}

[CustomEditor(typeof(UIText), true)]
public class UITextEditor : UnityEditor.UI.TextEditor
{
	E.Languages languages = E.Languages.English;
	bool check = false;

	public override void OnInspectorGUI ()
	{
		SerializedProperty sp = serializedObject.FindProperty("localizeKey");
		if (Application.isPlaying){
			EditorGUILayout.TextField("Localize Key", sp.stringValue);
		}else{
			sp.stringValue = EditorGUILayout.TextField("Localize Key", sp.stringValue);
			languages = (E.Languages) EditorGUILayout.EnumPopup("Language",languages);
			check = EditorGUILayout.Toggle("without Localize", check);

			serializedObject.ApplyModifiedProperties();

			if (!string.IsNullOrEmpty(sp.stringValue)){
				List<string> data = Get(sp.stringValue);
				if (data == null) {
					GUI.color= Color.red;
					EditorGUILayout.HelpBox("Cannot Find Localize Data", MessageType.Warning);
					if (GUILayout.Button("Reload"))
					{
						master = null;
					}
				}else{
					if (!check) {
						UIText text = (UIText) target;
						text.text = data[Mathf.Min(data.Count-1, (int)languages + 1)];
						GUI.color = Color.white;
					}else {
						GUI.color = Color.red;
					}
					EditorUtility.SetDirty(target);
				}
			}
		}
		base.OnInspectorGUI();
		GUI.color = Color.white;
	}

	static Dictionary<string, List<string>> master;

	static void Load () 
	{
		master = new Dictionary<string, List<string>>();
		List<List<string>> csv = Master.GetCSV("m_localize");
		for (int i = 0; i < csv.Count; i++) {
			master.Add(csv[i][0], csv[i]);
		}
	}

	List<string> Get (string key)
	{	
		if (master == null){
			Load();	
		}

		if (master.ContainsKey(key)){
			return master[key];
		}else {
			return null;
		}
	}
}
	