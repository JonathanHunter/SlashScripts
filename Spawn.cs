﻿namespace Assets.Scripts
{
    class Spawn : UnityEngine.MonoBehaviour
    {
        public static Spawn spawn;

        void Start()
        {
            if (spawn == null)
                spawn = this;
        }
    }
}
