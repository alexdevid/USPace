using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    public class NumberField : MonoBehaviour
    {
        private InputField _inputField;
        
        private void Start()
        {
            _inputField = gameObject.GetComponent<InputField>();
            _inputField.characterValidation = InputField.CharacterValidation.Integer;
        }
    }
}
