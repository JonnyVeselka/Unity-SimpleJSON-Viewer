using UnityEditor;
using UnityEngine;
using UnityEngine.Internal;
using SimpleJSON;


public class JSONViewer : ScriptableWizard
{
	#region Properties
	private JSONNodeItem RootItem { get; set; }
	#endregion

	#region Static Methods
	[MenuItem("Tools/JSON Viewer %&j")]
	public static void Display ()
	{
		Display(EditorGUIUtility.systemCopyBuffer);
	}

	public static void Display ([DefaultValue("")] string json, string rootKey = default(string))
	{
		if (string.IsNullOrEmpty(json))
			return;

		string inputStr = json.Trim();
		if ((inputStr.StartsWith("[") && inputStr.EndsWith("]")) || 
			(inputStr.StartsWith("{") && inputStr.EndsWith("}")))
		{
			Display(JSON.Parse(json), rootKey);
		}
	}

	public static void Display ([DefaultValue("")] JSONNode node, string rootKey = default(string))
	{
		if (node.IsNullOrEmpty())
			return;
		
		JSONViewer wizard = ScriptableWizard.DisplayWizard<JSONViewer>("JSON Viewer", "Close", "Preview");
		wizard.RootItem = new JSONNodeItem(rootKey, node);
		wizard.RootItem.Expand();
	}
	#endregion

	#region Methods
	protected override bool DrawWizardGUI ()
	{
		bool isDrawWizardGUI = base.DrawWizardGUI();
		RootItem.DrawGUI();
		return isDrawWizardGUI;
	}

	protected void OnWizardCreate ()
	{
		Close();
	}

	protected void OnWizardUpdate () { }

	protected void OnWizardOtherButton ()
	{
		helpString = RootItem.Value.ToString();
	}
	#endregion

	#region Nested Types
	public class JSONNodeItem
	{
		#region Fields
		public int Depth = default(int);
		public bool IsExpanded = default(bool);
		public GUIContent Content = default(GUIContent);
		public JSONNode Value = default(JSONNode);
		public JSONNodeItem[] Childs = default(JSONNodeItem[]);
		#endregion

		#region Constructors
		public JSONNodeItem (string key, JSONNode value, int depth = default(int))
		{
			string label = string.Format("  {0}", key ?? "<root data>");
			this.Content = new GUIContent(label, GetNodeIcon(value));
			this.Depth = depth;
			this.Value = value;
		}
		#endregion

		#region Methods
		public void Expand ()
		{
			IsExpanded = !IsExpanded;
			if (IsExpanded && Childs == null && !(Value is JSONData))
			{
				int index = default(int);
				Childs = new JSONNodeItem[Value.Count];
				if (Value is JSONClass)
				{
					foreach (var child in Value.AsObject.KeyChilds)
						Childs[index++] = new JSONNodeItem(child.Key, child.Value, Depth + 1);
				}
				else if (Value is JSONArray)
				{
					foreach (var child in Value.Childs)
						Childs[index] = new JSONNodeItem(string.Format("[{0}]", index++), child, Depth + 1);
				}
			}
		}

		public void DrawGUI ()
		{
			using (new EditorGUILayout.HorizontalScope())
			{
				GUILayout.Space(Depth * 25.0f);
				if (Value is JSONData)
				{
					GUILayout.Label(Content, GUIHelper.ItemTitle, GUILayout.Width(200.0f));
					GUILayout.Label(new GUIContent(Value.Value), GUIHelper.HelpBox, GUILayout.ExpandWidth(true));
				}
				else
				{
					GUI.enabled = !Value.IsNullOrEmpty();
					if (GUILayout.Button(Content, GUIHelper.ItemTitle))
						Expand();

					Rect lastRect = GUILayoutUtility.GetLastRect();
					Rect progressRect = new Rect(lastRect.x + GUIHelper.ItemTitle.CalcSize(Content).x, lastRect.y, 72, lastRect.height);
					EditorGUI.LabelField(progressRect, string.Format("[items: {0}]", Value.Count), EditorStyles.centeredGreyMiniLabel);
					GUI.enabled = true;
				}
			}

			if (IsExpanded)
			{
				foreach (JSONNodeItem child in Childs)
					child.DrawGUI();
			}
		}

		public Texture GetNodeIcon (JSONNode node)
		{
			if (node is JSONClass) return (Texture)EditorGUIUtility.Load("Object.png");
			if (node is JSONArray) return (Texture)EditorGUIUtility.Load("Array.png");
			if (node is JSONData) return (Texture)EditorGUIUtility.Load("Data.png");

			return EditorGUIUtility.whiteTexture;
		}
		#endregion
	}
	#endregion
}