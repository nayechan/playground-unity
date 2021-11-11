using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Examples
{
    public class Test : MonoBehaviour
    {
        public int abc = 1;
        public static int ABC = 1;

        public string toGo;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"Current ABC : {ABC}");
        }


        private void OnDisable()
        {
            Debug.Log("TestScene Call Disable");
            abc += 1;
            ABC += 1;
        }

        IEnumerator ComeBack()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadSceneAsync("TestScene");
        }

        public void ChangeScene()
        {
            SceneManager.LoadSceneAsync(toGo);
        }

        // Update is called once per frame
        void Update()
        {
        
        
        }

    }
}
