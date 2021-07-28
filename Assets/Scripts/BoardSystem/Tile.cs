using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public class Tile   //is made public so its accessible outside the boardsystem assembly
    {
        public Position Position { get; }
        public Tile(Position position) => Position = position;

        public event EventHandler HighlightStatusChanged;
        private bool _isHighlighted = false;

        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                _isHighlighted = value;
                OnHighlightStatusChanged(EventArgs.Empty);
            }
        }

        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HighlightStatusChanged;
            handler?.Invoke(this, args);    //? means if its not null then invoke
        }

    }
}
