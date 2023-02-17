Console.WriteLine("Enter an example for calculation:");
var variable = Console.ReadLine();
var operators = new char[] { '+', '-', '/', '*', '^', '(', ')' };
List<string> operations = new List<string>();
Stack<double> stack = new Stack<double>();
Stack<char?> stackOperators = new Stack<char?>();
bool check = false;

var buff = "";
char? oper = null;

if (variable != null)
    foreach (var ch in variable)
    {
        if (char.IsDigit(ch))
        {
            buff += ch;
        }
        else if (operators.Contains(ch))
        {
            operations.Add(buff);
            buff = "";
            if (ch is '^')
            {
                stackOperators.Push(ch);
            }
            else if ((oper is '*' or '/' or '^') && stackOperators.Count > 0 && (ch != '(') && (check == false))
            {
                operations.Add(stackOperators.Pop().ToString());
                if ((stackOperators.Contains('+') || stackOperators.Contains('-')) && (ch is '+' or '-'))
                {
                    operations.Add(stackOperators.Pop().ToString());
                    stackOperators.Push(ch); 
                }
                else
                {
                    stackOperators.Push(ch);
                }
            }
            
            else if ((stackOperators.Contains('+') || stackOperators.Contains('-')) && (ch is '+' or '-') && (check == false))
            {
                operations.Add(stackOperators.Pop().ToString());
                stackOperators.Push(ch);
            }
            else if (ch == '(')
            {
                check = true;
            }
            else if ((ch == ')')  && stackOperators.Count > 0)
            {
                check = false;
                while (stackOperators.Count > 0)
                {
                    operations.Add(stackOperators.Pop().ToString());
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
    operations.Add(buff);
    if (stackOperators.Count == 0)
    {
        operations.Add(oper.ToString());
    }
}

while (stackOperators.Count > 0)
{
    operations.Add(stackOperators.Pop().ToString());
}

string itemToRemove = "";
while (operations.Contains(""))
{
    operations.Remove(itemToRemove);
}

Console.WriteLine("RPL:");
Console.WriteLine(string.Join(" ", operations));



foreach (var element in operations)
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

Console.WriteLine("RESULT:");
Console.WriteLine(stack.Pop());