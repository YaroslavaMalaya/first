var operators = new char[] { '+', '-', '/', '*', '^', '(', ')' };
main.Stack<double> stack = new main.Stack<double>();
main.Stack<char?> stackOperators = new main.Stack<char?>();
Queue<string> queue = new Queue<string>();

bool check = false;
var buff = "";
char? oper = null;

while (true)
{
    Console.WriteLine("Enter an example for calculation:");
    var variable = Console.ReadLine();
    if (variable != null)
        foreach (var ch in variable)
        {
            if (char.IsDigit(ch))
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
            if (element != "+" && element != "-" && element != "/" && element != "*" && element != "^")
            {
                var db = Convert.ToDouble(element);
                stack.Push(db);
            }
            else
            {
                var num1 = stack.Pop();
                var num2 = stack.Pop();
                if (element == "+")
                {
                    var sum = num2 + num1;
                    stack.Push(sum);
                }

                if (element == "-")
                {
                    var minus = num2 - num1;
                    stack.Push(minus);
                }

                if (element == "*")
                {
                    var multiply = num2 * num1;
                    stack.Push(multiply);
                }

                if (element == "/")
                {
                    var divide = num2 / num1;
                    stack.Push(divide);
                }

                if (element == "^")
                {
                    var expo = Math.Pow(num2, num1);
                    stack.Push(expo);
                }
            }
        }
    }

    Console.WriteLine("RESULT:");
    Console.WriteLine(stack.Pop());
    buff = "";
    // stack.Clear();
    queue.Clear();
}