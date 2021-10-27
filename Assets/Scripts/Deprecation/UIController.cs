using System.Collections.Generic;
using UnityEngine;

namespace GameEditor.UI
{
    public class UIController : MonoBehaviour
    {
        /*
    UI 의 Active, Inactive 설정을 제어한다.
    판넬의 이름으로 On Off 이벤트가 일어나므로 각 판넬의 이름은 Unique 하게 설정해야 한다.
    TargetPanels 의 Child를 대상으로 검색, inactive, active를 수행한다.
    */
        private Dictionary<string, GameObject> panels;
        public GameObject targetPanels;

        void Start(){
            panels = new Dictionary<string, GameObject>();
            for(int i=0; i < targetPanels.transform.childCount; ++i){
                GameObject pan = targetPanels.transform.GetChild(i).gameObject;
                panels.Add(pan.name, pan);
                // Debug.Log("name : " + pan.name + "object" + pan.ToString());
            }  
        }


        public void InactiveAll(){
            foreach(KeyValuePair<string, GameObject> pair in panels){
                pair.Value.SetActive(false);
            }
        }
        public void LeftOnePanel(string except){
            foreach(KeyValuePair<string, GameObject> pair in panels){
                if(pair.Key == except){
                    pair.Value.SetActive(true);
                    continue;
                }
                pair.Value.SetActive(false);
            }
        }

        public void ActivatePanel(string panelName){
            panels[panelName].SetActive(true);
        }

        public void InactivatePanel(string panelName){
            panels[panelName].SetActive(false);
        }
    }
}
