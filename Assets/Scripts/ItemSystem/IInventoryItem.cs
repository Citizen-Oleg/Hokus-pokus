using ItemSystem;
using UnityEngine;

namespace ResourceSystem
{
	public interface IInventoryItem
	{
		public Transform TopItem { get; }
		public Transform Transform { get; }
		public ItemType ItemType { get; }
		public int Weight { get; }
		public void Release();
	}
}