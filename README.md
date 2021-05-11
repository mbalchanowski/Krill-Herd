# Krill herd
Simple console implementation of the Krill Herd (KH) algorithm as described in: *Amir Hossein Gandomi and Amir Hossein Alavi, Krill herd: A new bio-inspired optimization algorithm, Communications in Nonlinear Science and Numerical Simulation, 2012*

Please note:
* All equation numbers in source code have been taken from above article. 
* This implementation doesn't include genetic operators.
* By default this algorithm tries to maximize fitness value.

It is written in C# (build with .NET Core 3.1).

# Running
By default it tries to solve `RASTRIGIN FUNCTION` for only 2 dimensions but of course you can increase dimensions or add more functions in `TestFunction.cs`

# Parameters
Default parameters (file `Parameters.cs`):
* Dimensions = 2;
* Iterations = 250;
* Population = 50;
* C_t = 0.1;
* N_Max = 0.01;
* V_f = 0.02;
* D_max = 0.02;