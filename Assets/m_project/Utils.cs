using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Utils
	{
		public static T fold<T>(Func<T, T, T> f, T[] coll){
			if (coll.Length == 0) {
				return default(T);
			} else if (coll.Length == 1) {
				return coll [0];
			} 
			T result = coll[0];
			for(int i = 1; i < coll.Length; i++){
				result = f (result, coll [i]);
			}
			return result;
		}

		public static T[] filter<T>(Func<T, Boolean> f, T[] coll){
			if (coll.Length == 0) {
				return default(T[]);
			}

			List<T> result = new List<T>();

			for(int i = 1; i < coll.Length; i++){
				if(f(coll[i])){
					result.Add (coll [i]);
				}
			}
			return result.ToArray();
		}

		public static GameObject getNearestGameObject(GameObject[] objects, Transform transform){
			return Utils.fold<GameObject>(
				(first, second) => {
					float distanceFirst = Vector3.Distance(first.transform.position, transform.position);
					float distanceSecond = Vector3.Distance(second.transform.position, transform.position);

					if(distanceFirst>distanceSecond){
						return second;
					}else{
						return first;
					}
				}, objects);
		}

	}
}

