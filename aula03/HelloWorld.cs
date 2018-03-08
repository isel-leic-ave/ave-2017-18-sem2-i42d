using System;


public class HelloWorld {
    public static void m() {
        Console.WriteLine("I'm @ HelloWorld.m()");
    }
    public static void Main(String[] args) {
        Console.WriteLine("Hello world");
        HelloWorld.m();
        Console.WriteLine(MathLibrary.add(2,2));
    }
}