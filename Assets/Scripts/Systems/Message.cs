using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class Message : SingletonMonoBehaviour<Message>
    {
        private static List<string> messages;

        public static IEnumerable<string> Log => messages;

        public static void Write(string message)
        {
            messages.Insert(0, message);
            ShowLog();
        }

        public static void ShowLog()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
