using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExternalScript.keyboardMobileWebGLUnity
{
	public class keyboardClass : MonoBehaviour, ISelectHandler {

		[DllImport("__Internal")]
		private static extern void focusHandleAction (string _name, string _str);

		public void ReceiveInputData(string value) {
			gameObject.GetComponent<InputField> ().text = value;
		}

		public void OnSelect(BaseEventData data) {
#if UNITY_WEBGL
		try{
			focusHandleAction (gameObject.name, gameObject.GetComponent<InputField> ().text);
		}
		catch(Exception error){}
#endif
		}
	}
}
