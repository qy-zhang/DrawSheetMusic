namespace util
{
    public class NoteGetter
    {
        private int[] _ints;
        private int _length;
        private string[] _steps;
        private string[] _octaves;

        public NoteGetter(int[] ints) {
            _ints = ints;
            _length = ints.Length;
            _steps = new string[_length];
            _octaves = new string[_length];
            Init();
        }

        private void Init() {
            for (int i = 0; i < _length; i ++) {
//            temp = _ints[i] - 21; // midipitch
                int temp = _ints[i];
                switch (temp % 12) {
                    case 0: _steps[i] = "A"; break;
                    case 1: _steps[i] = "A"; break;
                    case 2: _steps[i] = "B"; break;
                    case 3: _steps[i] = "C"; break;
                    case 4: _steps[i] = "C"; break;
                    case 5: _steps[i] = "D"; break;
                    case 6: _steps[i] = "D"; break;
                    case 7: _steps[i] = "E"; break;
                    case 8: _steps[i] = "F"; break;
                    case 9: _steps[i] = "F"; break;
                    case 10: _steps[i] = "G"; break;
                    case 11: _steps[i] = "G"; break;
//                    default: break;
                }

                if ((temp - 3) < 0) {
                    _octaves[i] = "0";
                } else
                {
                    int tmp = (temp - 3) / 12 + 1;
                    _octaves[i] = tmp.ToString();
                }
            }
        }

        public string[] GetSteps() {
            return _steps;
        }

        public string[] GetOctaves() {
            return _octaves;
        }
    }
}