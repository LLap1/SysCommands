/*
using:
    The using directive creates an alias for a namespace
    or imports types defined in other namespaces.
*/

using System;
using System.Collections.Generic;
using System.Linq;


/*
Static: 
    In C#, static means something which cann0ot be instantiated. 
    You cannot create an object of a static class and cannot access 
    static members using an object. An attribute that belongs to the class.
    
*/

namespace MainScope{ // declare namespace. namespace = scope.


    class Test{
        public static int _int_val = 1;

        public static int int_val { 
            get {return _int_val; }
            set {_int_val = value;}
        }
    }

    class Program{ // declare class
        static void Main(){ // declare static function. A function that belongs to the class

            //Test.int_val = 3;
            Console.WriteLine("The value is: " + Test.int_val); 
        }
    }
}

