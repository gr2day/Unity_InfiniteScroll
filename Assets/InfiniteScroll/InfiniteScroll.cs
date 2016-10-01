using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InfiniteScroll : MonoBehaviour
{
	[SerializeField] private RectTransform		m_Content;
	[SerializeField] private RectTransform[] 	m_PoolItems;
	[SerializeField] private bool				m_IsVertical;

	private Action<int, int> 					m_CallbackChanged;
	private Vector2 							m_ItemSize;

	private int									m_DataCount;
	private int									m_PoolItemCount;
	private int 								m_MoveIndex;

	public void Init( int dataCount, Action<int, int> callbackChanged )
	{
		m_PoolItemCount = m_PoolItems.Length;

		if( m_PoolItemCount == 0 ) return;

		m_CallbackChanged = callbackChanged;

		m_MoveIndex = 0;
		m_DataCount = dataCount;
		m_ItemSize = m_PoolItems[0].sizeDelta;

		for( int i = 0; i < m_PoolItems.Length; i++ ) 
		{
			SetPositionPoolItem( i, i );
			if( m_CallbackChanged != null ) m_CallbackChanged( i, i );
		}

		SetContentSize();
	}

	void Update()
	{
		if( m_PoolItemCount == 0 ) return;

		float itemSize 			= GetItemSize();
		float contentPosition 	= GetContentPosition();

		while( itemSize * ( m_MoveIndex + 2 ) < contentPosition )
		{
			if( m_MoveIndex >= m_DataCount - m_PoolItemCount ) return;

			int poolIndex = m_MoveIndex % m_PoolItemCount;
			int dataIndex = m_PoolItemCount + m_MoveIndex;

			SetPositionPoolItem( poolIndex, dataIndex );
			m_MoveIndex++;

			if( m_CallbackChanged != null )
			{
				m_CallbackChanged( poolIndex, dataIndex );
			}
		}

		while( itemSize * ( m_MoveIndex + 1 ) > contentPosition )
		{
			if( m_MoveIndex == 0 ) return;
			m_MoveIndex--;

			int poolIndex = m_MoveIndex % m_PoolItemCount;
			int dataIndex =	m_MoveIndex;

			SetPositionPoolItem( poolIndex, dataIndex );

			if( m_CallbackChanged != null ) 
			{
				m_CallbackChanged( poolIndex, dataIndex );
			}
		}
	}

	private void SetContentSize()
	{
		Vector2 contentSize = m_Content.sizeDelta;

		if( m_IsVertical )
		{
			contentSize.y = m_ItemSize.y * (float)m_DataCount;
		}
		else
		{
			contentSize.x = m_ItemSize.x * (float)m_DataCount;
		}

		m_Content.sizeDelta = contentSize;
	}

	private float GetItemSize()
	{
		if( m_IsVertical )
		{
			return m_ItemSize.y;
		}

		return m_ItemSize.x;
	}

	private float GetContentPosition()
	{
		if( m_IsVertical )
		{
			return m_Content.anchoredPosition.y;
		}

		return -m_Content.anchoredPosition.x;
	}

	private void SetPositionPoolItem( int poolIndex, int dataIndex )
	{
		if( m_IsVertical ) 
		{
			m_PoolItems[ poolIndex ].anchoredPosition = new Vector2( 0.0f, -GetItemSize() * dataIndex );
		}
		else
		{
			m_PoolItems[ poolIndex ].anchoredPosition = new Vector2( GetItemSize() * dataIndex, 0.0f );
		}
	}

	public GameObject GetPoolItem( int index )
	{
		return m_PoolItems[ index ].gameObject;
	}
}
