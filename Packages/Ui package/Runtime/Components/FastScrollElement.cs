using UnityEngine;

namespace OneDay.Ui.Components
{
    public class InternalElement
    {
        public class CustomData
        {
            public int Id;
            public Transform ExternalElement;
        }

        public CustomData Data { get; }
        public FastScrollPanel.ElementVisibilityChanged VisibilityChanged { get; }
         
        public bool Visible;
        public bool NeedsPositionSync;
        public Rect Rect;

        public Vector2 Position
        {
            get => position;
            set
            {
                NeedsPositionSync = true;
                position = value;
            }
        }

        private Vector2 position;
        
        public InternalElement(int id, FastScrollPanel.ElementVisibilityChanged visibilityChanged)
        {
            Data = new CustomData
            {
                Id = id,
            };
            VisibilityChanged = visibilityChanged;
        }
    }
}