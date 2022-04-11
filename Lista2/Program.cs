﻿// See https://aka.ms/new-console-template for more information
using Lista2.Model;

var variables = new List<Field>();
int size = 6;

for (int i = 0; i < size; i++)
{
    for (int j = 0; j < size; j++)
    {
        variables.Add(new Field() { Row = i, Column = j });
    }
}

var domains = new Dictionary<Field, List<int>>();
foreach (var variable in variables)
{
    domains.Add(variable, new List<int> { 0, 1});
}

var constant_0_0 = variables.First(v => v.Row == 0 && v.Column == 0);
domains[constant_0_0] = new List<int> { 1 };

var constant_0_3 = variables.First(v => v.Row == 0 && v.Column == 3);
domains[constant_0_3] = new List<int> { 0 };

var constant_1_2 = variables.First(v => v.Row == 1 && v.Column == 2);
domains[constant_1_2] = new List<int> { 0 };

var constant_1_3 = variables.First(v => v.Row == 1 && v.Column == 3);
domains[constant_1_3] = new List<int> { 0 };

var constant_1_5 = variables.First(v => v.Row == 1 && v.Column == 5);
domains[constant_1_5] = new List<int> { 1 };

var constant_2_1 = variables.First(v => v.Row == 2 && v.Column == 1);
domains[constant_2_1] = new List<int> { 0 };

var constant_2_2 = variables.First(v => v.Row == 2 && v.Column == 2);
domains[constant_2_2] = new List<int> { 0 };

var constant_2_5 = variables.First(v => v.Row == 2 && v.Column == 5);
domains[constant_2_5] = new List<int> { 1 };

var constant_4_0 = variables.First(v => v.Row == 4 && v.Column == 0);
domains[constant_4_0] = new List<int> { 0 };

var constant_4_1 = variables.First(v => v.Row == 4 && v.Column == 1);
domains[constant_4_1] = new List<int> { 0 };

var constant_4_3 = variables.First(v => v.Row == 4 && v.Column == 3);
domains[constant_4_3] = new List<int> { 1 };

var constant_5_1 = variables.First(v => v.Row == 5 && v.Column == 1);
domains[constant_5_1] = new List<int> { 1 };

var constant_5_4 = variables.First(v => v.Row == 5 && v.Column == 4);
domains[constant_5_4] = new List<int> { 0 };

var constant_5_5 = variables.First(v => v.Row == 5 && v.Column == 5);
domains[constant_5_5] = new List<int> { 0 };

var csp = new CSP<Field, int>(variables, domains);

// equal row count constraint
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 0).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 1).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 2).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 3).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 4).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Row == 5).ToList()));

// equal column count constraint
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 0).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 1).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 2).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 3).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 4).ToList()));
csp.AddConstraint(new EqualCountConstraint(variables.Where(v => v.Column == 5).ToList()));

// rows
for (int i = 0; i < size; i++)
{
    for (int j = 0; j < size - 3; j++)
    {
        var variable1 = variables[i * size + j];
        var variable2 = variables[i * size + j + 1];
        var variable3 = variables[i * size + j + 2];
        csp.AddConstraint(new NotSameInRowConstraint(new List<Field> { variable1, variable2, variable3}));
    }
}

// columns
for (int column = 0; column < size; column++)
{
    for (int row = 0; row < size - 3; row++)
    {
        var variable1 = variables[row * size + column];
        var variable2 = variables[(row + 1) * size + column];
        var variable3 = variables[(row + 2) * size + column];
        csp.AddConstraint(new NotSameInRowConstraint(new List<Field> { variable1, variable2, variable3 }));
    }
}

//unique
csp.AddConstraint(new UniqueConstraint(variables, size));

var solution = csp.Backtracking(new Dictionary<Field, int>());
if (solution is null)
{
    Console.WriteLine("No solution");
}
else
{
    Console.WriteLine("Solution found!");
    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            var variable = variables[i * size + j];
            int value = solution[variable];
            Console.Write($" {value} |");
        }
        Console.WriteLine();
    }
}