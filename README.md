# Nonlinear-FrankWolfeMethod
solving a nonlinear problem with frank-wolfe algorithm

FRANK–WOLFE ALGORITHM

The Frank–Wolfe algorithm is an iterative first-order optimization algorithm for constrained convex optimization. Also known as the conditional gradient method,[1] reduced gradient algorithm and the convex combination algorithm, the method was originally proposed by Marguerite Frank and Philip Wolfe in 1956.[2] In each iteration, the Frank–Wolfe algorithm considers a linear approximation of the objective function, and moves towards a minimizer of this linear function (taken over the same domain).

Initialization: Let k <- 0, and let {\displaystyle \mathbf {x} _{0}\!} be any point in {\displaystyle {\mathcal {D}}}.

Step 1. Direction-finding subproblem: Find {\displaystyle \mathbf {s} _{k}} solving

Minimize {\displaystyle \mathbf {s} ^{T}\nabla f(\mathbf {x} _{k})} 

Subject to {\displaystyle \mathbf {s} \in {\mathcal {D}}}

(Interpretation: Minimize the linear approximation of the problem given by the first-order Taylor approximation of {\displaystyle f} around {\displaystyle \mathbf {x} _{k}\!} .)

Step 2. Step size determination: Set {\displaystyle \gamma \leftarrow {\frac {2}{k+2}}}, or alternatively find {\displaystyle \gamma } that minimizes {\displaystyle f(\mathbf {x} _{k}+\gamma (\mathbf {s} _{k}-\mathbf {x} _{k}))}   subject to {\displaystyle 0\leq \gamma \leq 1}.

Step 3. Update: Let {x} _{k+1} <- {x} _{k}+ \gamma ({s} _{k}-\mathbf {x} _{k}), let k <- k+1 and go to Step 1.




GAMS CODE:

variable y;

positive variable x1, x2;

equations obj, c1;

obj..y=e=5*x1-(x1**2)+8*x2-2*(x2**2);

c1..3*x1+2*x2=l=6;

model ex1 /all/;

solve ex1 using nlp maximazing y;
