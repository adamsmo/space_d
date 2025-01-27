﻿using UnityEngine;
using System;

namespace AssemblyCSharp
{
	public class Utils
	{
		public static T fold<T>(Func<T, T, T> f, T[] coll){
			if (coll.Length == 1) {
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

