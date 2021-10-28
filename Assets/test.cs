using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Awake() {
        IEnumerator enumerator = TestFunction();
        int result = (int)enumerator.Current;
        Debug.Log(result);
        enumerator.MoveNext();
        result = (int)enumerator.Current;
        Debug.Log(result);
        enumerator.MoveNext();
        result = (int)enumerator.Current;
        Debug.Log(result);
        enumerator.MoveNext();
        result = (int)enumerator.Current;
        Debug.Log(result);
    }

    IEnumerator TestFunction()
    {
        yield return 1;
        yield return 2;
    }
}
