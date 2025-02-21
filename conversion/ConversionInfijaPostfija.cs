namespace Actividad1.conversion
{
    public class ConversionInfijaPostfija
    {
        public event Action OnOutputChanged;
        private string _resultado = "";

        public string Output
        {
            get => _resultado;
            private set
            {
                _resultado = value;
                OnOutputChanged?.Invoke();
            }
        }

        private int Precedencia(char operador)
        {
            return operador switch
            {
                '+' or '-' => 1,
                '*' or '/' => 2,
                _ => 0
            };
        }

        public void ConvertirInfijaAPostfija(string expresion)
        {
            Stack<char> pila = new Stack<char>();
            List<string> postfija = new List<string>();
            string numero = "";

            foreach (char c in expresion)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    numero += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(numero))
                    {
                        postfija.Add(numero);
                        numero = "";
                    }

                    if (c == '(')
                    {
                        pila.Push(c);
                    }
                    else if (c == ')')
                    {
                        while (pila.Count > 0 && pila.Peek() != '(')
                        {
                            string op = pila.Pop().ToString();
                            postfija.Add(op);
                        }
                        pila.Pop();
                    }
                    else if ("+-*/".Contains(c))
                    {
                        while (pila.Count > 0 && Precedencia(pila.Peek()) >= Precedencia(c))
                        {
                            string op = pila.Pop().ToString();
                            postfija.Add(op);
                        }
                        pila.Push(c);
                    }
                }
            }

            if (!string.IsNullOrEmpty(numero))
            {
                postfija.Add(numero);
            }

            while (pila.Count > 0)
            {
                string op = pila.Pop().ToString();
                postfija.Add(op);
            }

            Output = string.Join(" ", postfija);
        }
    }
}