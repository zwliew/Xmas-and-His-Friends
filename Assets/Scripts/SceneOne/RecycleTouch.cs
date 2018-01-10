using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecycle{
	void Restart ();
	void Shutdown();
}

public class RecycleTouch : MonoBehaviour {

	private List<IRecycle> recycleComponents;

	void Awake(){
		var components = GetComponents<MonoBehaviour> ();
		recycleComponents = new List<IRecycle>();
		foreach(var component in components){
			if (component is IRecycle){
				//does it implement IRecycle?
				recycleComponents.Add(component as IRecycle);
			}
		}
	}

	public void Restart(){
		gameObject.SetActive (true);
		foreach (var component in recycleComponents) {
			component.Restart ();
		}
	}

	public void Shutdown(){
		gameObject.SetActive (false);
		foreach (var component in recycleComponents) {
			component.Shutdown();
		}
	}
}
