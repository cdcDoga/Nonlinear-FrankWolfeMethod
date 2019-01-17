# Nonlinear-FrankWolfeMethod1
solving a nonlinear problem with frank-wolfe algorithm

GAMS:
variable y;
positive variable x1, x2;
equations obj, c1;
obj..y=e=5*x1-(x1**2)+8*x2-2*(x2**2);
c1..3*x1+2*x2=l=6;
model ex1 /all/;
solve ex1 using nlp maximazing y;
