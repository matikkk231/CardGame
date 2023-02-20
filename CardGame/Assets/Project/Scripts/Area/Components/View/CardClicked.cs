using System;
using UnityEngine;

namespace Project.Scripts.Area.Components.Logic
{
    public class CardClicked : MonoBehaviour
    {
        public bool IsCardClicked;

        private void OnMouseDown()
        {
            IsCardClicked = true;
        }
    }
}