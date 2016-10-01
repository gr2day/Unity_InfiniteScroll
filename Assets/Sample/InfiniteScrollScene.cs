using UnityEngine;
using System.Collections;

public class InfiniteScrollScene : MonoBehaviour
{
	private readonly string[] DATA_LIST = new string[]
	{
		"Item01", "Item02", "Item03", "Item04", "Item05",
		"Item06", "Item07", "Item08", "Item09", "Item10",
		"Item11", "Item12", "Item13", "Item14", "Item15",
		"Item16", "Item17", "Item18", "Item19", "Item20",
	};

	[SerializeField] private InfiniteScroll m_VerticalInfiniteScroll;
	[SerializeField] private InfiniteScroll m_HorizontalInfiniteScroll;

	// Use this for initialization
	void Start () 
	{
		m_VerticalInfiniteScroll.Init( 
			DATA_LIST.Length, 
			delegate( int poolIndex, int dataIndex )
			{
				Item item = m_VerticalInfiniteScroll.GetPoolItem( poolIndex ).GetComponent<Item>();
				item.Init( DATA_LIST[dataIndex] );
			}
		);

		m_HorizontalInfiniteScroll.Init( 
			DATA_LIST.Length, 
			delegate( int poolIndex, int dataIndex )
			{
				Item item = m_HorizontalInfiniteScroll.GetPoolItem( poolIndex ).GetComponent<Item>();
				item.Init( DATA_LIST[dataIndex] );
			}
		);
	}
}
