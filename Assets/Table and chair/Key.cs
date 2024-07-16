
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeMonkey.KeyDoorSystemCM
{

    [CreateAssetMenu(fileName = "Key", menuName = "Add-Ons/Code Monkey/Key Door System/Create Key", order = 1)]
    public class Key : ScriptableObject
    {
        [Tooltip("Color of the Key")]
        public Color keyColor;
    }

}
