using System;
using System.Collections.Generic;
using System.Threading;

[Serializable]
public class UserData
{
	public string nickName;
	public int level;
	public CharacterType type;
	public InventoryData inventory;

	public UserData()
	{
		this.nickName = "nickName";
		this.level = 1;
		this.type = CharacterType.Warrior;
		this.inventory = new InventoryData();

		inventory.items.Add(new ItemData("器记"));
		inventory.items.Add(new ItemData("公扁"));
	}

	public UserData(string nickName, int level, CharacterType type)
	{
		this.nickName = nickName;
		this.level = level;
		this.type = type;
		this.inventory = new InventoryData();

		inventory.items.Add(new ItemData("器记"));
		inventory.items.Add(new ItemData("公扁"));
	}
}

[Serializable]
public class InventoryData
{
	public List<ItemData> items = new List<ItemData>();
}

[Serializable]
public class ItemData
{
	public string name;

	public ItemData(string name)
	{
		this.name = name;
	}
}

public enum CharacterType
{
	Warrior, Wizard, Archer
}
