using UnityEngine;
using System.Collections;

public class Utility 
{
	public static GameObject Instantiate (GameObject prefab, Transform parent)
	{
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		return SetParent(go, parent);
	}

	public static GameObject SetParent (GameObject go, Transform parent)
	{
		go.transform.SetParent(parent);
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
		if (parent != null) go.layer = parent.gameObject.layer;

		RectTransform rt = go.GetComponent<RectTransform>();
		if (rt != null) {
			rt.anchorMin = Vector2.zero;
			rt.anchorMax = Vector2.one;
			rt.offsetMin = Vector2.zero;
			rt.offsetMax = Vector2.one;
			rt.anchoredPosition = Vector2.zero;
		}
		return go;
	}
}
