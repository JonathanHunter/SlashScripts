using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class MidLevelTransition : MonoBehaviour
    {
        public GameObject[] sections;
        public string nextLevel;

        private GameObject section;

        void Start()
        {
            Transition(0, null);
        }

        public void Transition(int index, Transform entry)
        {
            Debug.Log(index);
            if (index >= sections.Length)
                Application.LoadLevel(nextLevel);
            else
            {
                if (section != null)
                    Destroy(section);
                GameObject temp = FindObjectOfType<Player.Player>().gameObject;
                if(entry!=null)
                    temp.transform.position = new Vector3(entry.position.x, entry.transform.position.y, temp.transform.position.z);
                section = (GameObject)Instantiate(sections[index]);
            }
        }

    }
}
