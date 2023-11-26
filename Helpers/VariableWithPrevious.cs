namespace Helpers
{
    public class VariableWithPrevious<T>
    {
        public VariableWithPrevious(T variable)
        {
            _value = variable;
            _previous = null;
        }

        private T _value;
        private VariableWithPrevious<T>? _previous;

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_previous == null)
                {
                    _previous = new(_value);
                }
                else
                {
                    _previous.Value = _value;
                }
                _value = value;
            }
        }

        public VariableWithPrevious<T> Previous
        {
            get
            {
                if (_previous == null)
                {
                    return new(_value);
                }
                return _previous;
            }
        }

        public void SetManual(T value, VariableWithPrevious<T>? previous)
        {
            _value = value;
            _previous = previous;
        }

        public List<T> Revert(int times = 1)
        {
            List<T> backup = new();
            if (times < 1) return backup;
            VariableWithPrevious<T>? previous = null;
            T value = _value;
            while (times >= 1)
            {
                backup.Add(value);
                if (_previous == null) break;
                previous = _previous._previous;
                value = _previous.Value;
                times--;
            }
            _previous = previous;
            _value = value;
            return backup;
        }

        public List<T> GetHistory(bool includeCurrent = true)
        {
            List<T> history = new();
            if (includeCurrent) history.Add(_value);
            VariableWithPrevious<T>? previous = _previous;
            while (previous != null)
            {
                history.Add(previous.Value);
                previous = previous._previous;
            }
            history.Reverse();
            return history;
        }

        public int GetDepth()
        {
            int i = 0;
            VariableWithPrevious<T>? previous = _previous;
            while (previous != null)
            {
                previous = previous._previous;
                i++;
            }
            return i;
        }

        public VariableWithPrevious<T> GetPrevious(int depth = 0)
        {
            VariableWithPrevious<T>? previous = _previous;
            if (previous is null) return this;
            for (int i = 0; i < depth; i++)
            {
                VariableWithPrevious<T>? previousT = previous._previous;
                if (previousT is null) return previous;
                previous = previousT;
            }
            return previous;
        }

        public static implicit operator T(VariableWithPrevious<T> t) => t.Value;
        public static implicit operator VariableWithPrevious<T>(T t) => new(t);

        public override string ToString()
        {
            if (Value is not null)
            {
                string? retString = Value.ToString();
                if (retString is not null)
                {
                    return retString;
                }

            }
            return "null";
        }
    }
}