Console.WriteLine("Enter an example for calculation:");
var variable = Console.ReadLine();
var operators = new char[] { '+', '-', '/', '*', '^' };
List<string> operations = new List<string>();
Stack<char?> stack = new Stack<char?>();
Queue<double> queue = new Queue<double>();
Stack<char?> stack1 = new Stack<char?>();


var buff = "";
char? oper = null;

if (variable != null)
    foreach (var ch in variable)
    {
        if (stack1.Count == 0)
        {
            if (char.IsDigit(ch))
            {
                buff += ch;
                if ((stack.Contains('-') || stack.Contains('+')) && (oper is '+' or '-'))
                {
                    operations.Add(stack.Pop().ToString());
                }
            }
            else if (operators.Contains(ch))
            {
                operations.Add(buff);
                buff = "";
                if (oper is not null)
                {
                    if (oper is '/' or '*' or '^')
                    {
                        if (ch != '(')
                        {
                            operations.Add(oper.ToString());
                        }
                        else
                        {
                            stack.Push(oper);
                        }
                    }
                    else
                    {
                        stack.Push(oper);
                    }
                } 
                oper = ch;
            }
            else if (ch == '(')
            {
                stack.Push(oper);
                stack1.Push(ch);
                oper = ch;
            }
        }
        else
        {
            if (char.IsDigit(ch))
            {
                buff += ch;
                if ((stack1.Contains('-') || stack1.Contains('+')) && (oper is '+' or '-'))
                {
                    operations.Add(stack1.Pop().ToString());
                }
            }
            else if (operators.Contains(ch))
            {
                operations.Add(buff);
                buff = "";
                if (oper is not null && oper is not '(')
                { 
                    if (oper is '/' or '*' or '^')
                    {
                        if (ch != '(')
                        {
                            operations.Add(oper.ToString());
                        }
                        else
                        {
                            stack1.Push(oper);
                        }
                    }
                    else
                    {
                        stack1.Push(oper);
                    }
                } 
                oper = ch;
            }
            else
            {
                stack1.Push(oper);
                oper = ch;
            }
            if (oper == ')')
            {
                operations.Add(buff);
                buff = "";
                if (ch != '/' && ch != '*')
                {
                    while (stack1.Count != 0)
                    {
                        operations.Add(stack1.Pop().ToString());
                    }
                    while (stack.Count != 0)
                    {
                        operations.Add(stack.Pop().ToString());
                    }
                    oper = ch;
                }
                else
                {
                    oper = ch;
                    stack1.Push(oper);
                }
            }
        }
    }

if (buff != "")
{
    operations.Add(buff);
    operations.Add(oper.ToString());
}

if (stack.Count == 1)
{
    operations.Add(stack.Pop().ToString());
}

if (operations.Contains("(") || operations.Contains(")") )
{
    operations.Remove("(");
    operations.Remove(")");
}

Console.WriteLine(string.Join(" ", operations));

foreach (var element in operations)
{
    if (element == "")
    {
        continue;
    }
    if (element != "+" && element != "-" && element != "/" && element != "*" && element != "^")
    {
        var db = Convert.ToDouble(element);
        queue.Enqueue(db);
    }
    else
    {
        var num1 = queue.Dequeue();
        var num2 = queue.Dequeue();
        if (element == "+")
        {
            var sum = num2 + num1;
            queue.Enqueue(sum);
        }

        if (element == "-")
        { 
            var minus = num2 - num1;
            queue.Enqueue(minus);
        }

        if (element == "*")
        {
            var multiply = num2 * num1;
            queue.Enqueue(multiply);
        }

        if (element == "/")
        {
            var divide = num2 / num1;
            queue.Enqueue(divide);
        }
        if (element == "^")
        {
            var expo = Math.Pow(num2, num1);
            queue.Enqueue(expo);
        }
    }
}

Console.WriteLine(queue.Dequeue());