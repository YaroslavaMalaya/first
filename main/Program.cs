Console.WriteLine("Enter an example for calculation:");
var variable = Console.ReadLine();
var operators = new char[] { '+', '-', '/', '*' };
List<string> operations = new List<string>();
Stack<int> stack = new Stack<int>();
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
                    oper = ch;
                    if (oper != '/' && oper != '*')
                    {
                        if (queue.Count > 0)
                        {
                            operations.Add(queue.Dequeue().ToString());
                        }
                    }
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

foreach (var operation in operations)
{
    // Console.WriteLine(operation);
}
Console.WriteLine(string.Join(" ", operations));
//      10-1+2*8-4 -> 10|1|-|2|8|*|+|4|-
