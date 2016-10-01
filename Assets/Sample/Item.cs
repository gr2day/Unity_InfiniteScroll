using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour 
{
	[SerializeField] private Text m_Text;

	public void Init( string name )
	{
		m_Text.text = name;
	}
}
