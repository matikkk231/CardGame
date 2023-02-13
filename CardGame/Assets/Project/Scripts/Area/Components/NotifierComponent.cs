using Project.Scripts.Core.ECS.Component;
using TMPro;

namespace Project.Scripts.Area.Components
{
    public class NotifierComponent : IComponent
    {
        public TextMeshProUGUI TextMeshProUGUI;
        public string TextMessage { get; set; }
        public bool IsNotified { get; set; }

        public NotifierComponent(string textMessage, TextMeshProUGUI textMeshProUGUI)
        {
            TextMessage = textMessage;
            TextMeshProUGUI = textMeshProUGUI;
        }
    }
}