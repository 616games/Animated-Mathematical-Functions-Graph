# Animated-Mathematical-Functions-Graph
An animated graph that can be used to display different types of mathematical functions.

Functions are in the form of f(x) = x using a Vector3 to hold the input and output values (x, f(x), 0).  This means the graph function will always return a value on the y-axis.  

The domain of each function is limited to -1 and 1 and the points used to display the graph are scaled accordingly based on the chosen resolution (number of points).

To animate the graph, Time.time (t) is added to the input of each function, making it f(x, t) = (x, f(x + t), 0).  In order to prevent the graph simply rising on the y-axis eventually out of view, we stick to animating functions that rely on simple harmonic motion using the sine function.

To properly display the full period of our sine functions (2PI) to match our domain of -1 to 1, we further scale the input by PI, making our final function form f(PI(x + t)) = (x, PI * (x + t), 0).

Created in Unity 2020.3.11f1
