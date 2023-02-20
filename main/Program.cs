var operators = new char[] { '+', '-', '/', '*', '^', '(', ')' };
var stack = new main.Stack<double>();
var stackOperators = new main.Stack<char?>();
var queue = new main.Queue<string>();

var check = false;
var buff = "";
char? oper = null;

while (true)
{
    Console.WriteLine("Enter an example for calculation:");
    var variable = Console.ReadLine();
    if (variable != null)
        foreach (var ch in variable)
        {
            if ((char.IsDigit(ch)) || char.IsLetter(ch))
            {
                buff += ch;
            }
            else if (operators.Contains(ch))
            {
                queue.Enqueue(buff);
                buff = "";
                if (ch is '^')
                {
                    stackOperators.Push(ch);
                }
                else if ((oper is '^') && (check == true) && (ch is not ')'))
                {
                    queue.Enqueue(stackOperators.Pop().ToString());
                    stackOperators.Push(ch);
                }
                else if ((oper is '*' or '/' or '^') && stackOperators.Count > 0 && (ch != '(') && (check == false))
                {
                    queue.Enqueue(stackOperators.Pop().ToString());
                    if ((stackOperators.Contains('+') || stackOperators.Contains('-')) && (ch is '+' or '-'))
                    {
                        queue.Enqueue(stackOperators.Pop().ToString());
                        stackOperators.Push(ch);
                    }
                    else
                    {
                        stackOperators.Push(ch);
                    }
                }
                else if ((oper is '*' or '/' or '^') && stackOperators.Count > 0 && (ch != '(') && (check == true) && (ch is not ')'))
                {
                    queue.Enqueue(stackOperators.Pop().ToString());
                    stackOperators.Push(ch);
                    
                }
                else if ((stackOperators.Contains('+') || stackOperators.Contains('-')) && (ch is '+' or '-') &&
                         (check == false))
                {
                    queue.Enqueue(stackOperators.Pop().ToString());
                    stackOperators.Push(ch);
                }
                else if ((stackOperators.Contains('+') || stackOperators.Contains('-')) && (oper is '+' or '-') &&
                         (check == true) && (ch is (not '*' or '/' or '^') and not ')'))
                {
                    queue.Enqueue(stackOperators.Pop().ToString());
                    if (ch is not '(' or ')')
                    {
                        stackOperators.Push(ch);
                    }
                }   
                else if (ch == '(')
                {
                    check = true;
                }
                else if ((ch == ')') && stackOperators.Count > 0)
                {
                    check = false;
                    while (stackOperators.Count > 0)
                    {
                        queue.Enqueue(stackOperators.Pop().ToString());
                    }
                }
                else if (ch is not '(' or ')')
                {
                    stackOperators.Push(ch);
                }

                oper = ch;
            }
        }

    if (buff != "")
    {
        queue.Enqueue(buff);
        if (stackOperators.Count == 0)
        {
            queue.Enqueue(oper.ToString());
        }
    }

    while (stackOperators.Count > 0)
    {
        queue.Enqueue(stackOperators.Pop().ToString());
    }

    Console.WriteLine("RPL:");
    Console.WriteLine(string.Join(" ", queue));

    foreach (var element in queue)
    {
        if (element != "")
        {
            if (element.Contains("sin"))
            {
                string[] parts = element.Split("n");
                var degrees = Convert.ToDouble(parts[1]);
                var radians = (degrees * Math.PI) / 180; 
                stack.Push(Math.Sin(radians));
                
            }
            else if (element.Contains("cos"))
            {
                string[] parts = element.Split("s");
                var degrees = Convert.ToDouble(parts[1]);
                var radians = (degrees * Math.PI) / 180;
                stack.Push(Math.Cos(radians));
            }
            else if (element.Contains("tan"))
            {
                string[] parts = element.Split("n");
                var degrees = Convert.ToDouble(parts[1]);
                var radians = (degrees * Math.PI) / 180;
                stack.Push(Math.Tan(radians));
            }
            else if (element.Contains("ctg"))
            {
                string[] parts = element.Split("g");
                var degrees = Convert.ToDouble(parts[1]);
                var radians = (degrees * Math.PI) / 180;
                stack.Push(1/Math.Tan(radians));
            }
            else if (element != "+" && element != "-" && element != "/" && element != "*" && element != "^")
            {
                var db = Convert.ToDouble(element);
                stack.Push(db);
            }
            else
            {
                var num1 = stack.Pop();
                var num2 = stack.Pop();
                switch (element)
                {
                    case "+":
                        stack.Push(num2 + num1);
                        break;
                    case "-":
                        stack.Push(num2 - num1);
                        break;
                    case "*":
                        stack.Push(num2 * num1);
                        break;
                    case "/":
                        stack.Push(num2 / num1);
                        break;
                    case "^":
                        stack.Push(Math.Pow(num2, num1));
                        break;
                }
            }
        }
    }

    Console.WriteLine("RESULT:");
    Console.WriteLine(stack.Pop());
    
    buff = "";
    stack.Clear();
    queue.Clear();
}