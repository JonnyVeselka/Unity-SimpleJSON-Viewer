using UnityEngine;
using UnityEditor;


public class GUIHelper
{
	#region Static Fields
	private static GUIStyle m_itemTitle = default(GUIStyle);
	private static GUIStyle m_helpBox = default(GUIStyle);
	private static GUIStyle m_foldoutPlus = default(GUIStyle);
	private static GUIStyle m_foldoutMinus = default(GUIStyle);
	#endregion

	#region Static Properties
    public static GUIStyle ItemTitle
    {
        get
        {
			if (m_itemTitle == null)
			{
				m_itemTitle = new GUIStyle("ShurikenEmitterTitle");
				m_itemTitle.alignment = TextAnchor.MiddleLeft;
				m_itemTitle.padding = new RectOffset(5, 5, 5, 5);
				m_itemTitle.contentOffset = new Vector2(0.5f, -0.5f);
				m_itemTitle.fontSize = 12;
			}

			return m_itemTitle;
        }
    }
	public static GUIStyle HelpBox
	{
		get
		{
			if (m_helpBox == null)
			{
				m_helpBox = new GUIStyle(EditorStyles.helpBox);
				m_helpBox.alignment = TextAnchor.MiddleLeft;
				m_helpBox.margin = new RectOffset(2, 2, 1, 2);
				m_helpBox.fixedHeight = default(float);
				m_helpBox.stretchHeight = true;
				m_helpBox.fontSize = 12;
			}

			return m_helpBox;
		}
	}
    public static GUIStyle FoldoutPlus
    {
        get
        {
			if (m_foldoutPlus == null)
			{
				m_foldoutPlus = new GUIStyle("OL Plus");
				m_foldoutPlus.stretchWidth = false;
				m_foldoutPlus.overflow = new RectOffset(-7, 7, -7, 7);
				m_foldoutPlus.margin = new RectOffset(-7, 7, 0, 0);
				m_foldoutPlus.contentOffset = new Vector2(14, -7);
			}

			return m_foldoutPlus;
        }
    }
    public static GUIStyle FoldoutMinus
    {
        get
        {
			if (m_foldoutMinus == null)
			{
				m_foldoutMinus = new GUIStyle("OL Minus");
				m_foldoutMinus.stretchWidth = false;
				m_foldoutMinus.overflow = new RectOffset(-7, 7, -7, 7);
				m_foldoutMinus.margin = new RectOffset(-7, 7, 0, 0);
				m_foldoutMinus.contentOffset = new Vector2(14, -7);
			}

			return m_foldoutMinus;
        }
    }
	#endregion
}
