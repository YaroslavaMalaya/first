Console.WriteLine("Enter an example for calculation:");
var variable = Console.ReadLine();
var operators = new char[] { '+', '-', '/', '*' };
List<string> operations = new List<string>();
Stack<double> stack = new Stack<double>();
Queue<char?> queue = new Queue<char?>();

var buff = "";
char? oper = null;

if (variable != null)
    foreach (var ch in variable)
    {
        if (char.IsDigit(ch))
        {
            buff += ch;
            if ((queue.Contains('-') || queue.Contains('+')) && (oper is '+' or '-'))
            {
                operations.Add(queue.Dequeue().ToString());
            }
        }
        else if (operators.Contains(ch))
        {
            operations.Add(buff);
            buff = "";
            if (oper is not null)
            {
                if (oper is '/' or '*')
                {
                    operations.Add(oper.ToString());
                }
                else
                {
                    queue.Enqueue(oper);
                }
            }
            oper = ch;
        }
    }

if (buff != "")
{
    operations.Add(buff);
    operations.Add(oper.ToString());
}

if (queue.Count == 1)
{
    operations.Add(queue.Dequeue().ToString());
}

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
    }
}

Console.WriteLine(stack.Pop());