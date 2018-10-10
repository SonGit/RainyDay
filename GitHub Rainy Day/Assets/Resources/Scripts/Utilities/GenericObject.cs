using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObject <T> where T : Cacheable
{
	private List<T> _objs;

	private ObjectFactory.PrefabType _type;

	public GenericObject(ObjectFactory.PrefabType type,int num)
	{
		_objs = new List<T>();
		_type = type;

		for (int i = 0; i < num; i++) {
			MakeObj ();
		}
	}

	private T MakeObj()
	{
		GameObject go = ObjectFactory.instance.MakeObject(_type);
		T hs = go.GetComponent<T> ();

		if (hs != null) {
			_objs.Add (hs);
			hs.Destroy ();
			return hs;
		}
		return default(T);
	}

	public T GetObj()
	{
		//Check in cache to see if there is any free spark
		foreach (T t in _objs) 
		{
			if (!t._living)
			{
			// Spawn Position ???
				//t.Live();
			// Spawn Position ???
				return t;
			}
		}

		T newObj = MakeObj();
		newObj.Live();

		return newObj;
	}

}