using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : Singleton<DatabaseManager>
{
    public List<Item> ItemsDatabase;

    public Item FindItemByGUID(string GUID)
	{
		return ItemsDatabase.Find(x => x.GUID == GUID);
	}
}
