

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using Commands;

//using System.CommandLine;


namespace Commands{ 

    class Grep: ICommand{ // declare class
    
        private bool _recurse;
        private string _match;
        private Stack<string> _files = new Stack<string>();
        private bool _silent;

        public Grep(string[] args){ // constructor 

            _recurse = args.Contains("-r"); // recurse search from current directory.
            _silent = args.Contains("--silent"); // disables error messages.
            _match = args[args.Length - 1]; // string to search.

            if(!_recurse){
                int i = 1;
                string current_file;
                try{
                    for (; i < args.Length - 1; i++) 
                    {
                        current_file = args[i];
                        _DoesFileExists(current_file);
                        _files.Push(current_file);
                    }
                }catch(FileNotFoundException){
                    if(!_silent){
                        Console.WriteLine(args[i] + " Does not exist!");
                    }
                }
            }else{ 
                _GetFilesInDir(".");
            }
        }   

           private void _GetFilesInDir(string current){  // adds all the files from the current dir with recursion.
            
            string [] fileEntries = Directory.GetFiles(current);
            string [] DirEntries = Directory.GetDirectories(current);

            foreach(string file in fileEntries){
                _files.Push(file);
            }
            
            foreach(string dir in DirEntries){
                _GetFilesInDir(dir);
            }
        }

        private void _DoesFileExists(string file){
            bool is_exists = File.Exists(file);
            if(!is_exists){
                throw new FileNotFoundException(file + " Does Not Exist");
            }
        }

        public new void Run() // running the grep command.
        {
            string file;
            while(_files.Count != 0){
                file = _files.Pop();
                _SearchMatch(file, _match);
            } 
        }

        
        private void _SearchMatch(string file, string match){
            try{
                using(var sr = new StreamReader(file)){

                    string output = sr.ReadToEnd(); // reading output from the stream.
                    string[] iter_output = output.Split("\n"); // splitting the output into lines.
                    string line;
                    string found_message = file + ":\n"; // Found message
                    for (var row = 0; row < iter_output.Length; row++)
                    {
                        line = iter_output[row];
                        if(line.Contains(match)){
                            found_message += "Line - " + row + ": " + line + "\n";
                        }
                        row++;
                    }

                    if(found_message != file + ":\n"){ // if there was a match, found_message is being altered, and will not equal to the original value.
                        Console.WriteLine(found_message);
                    }
                }
            }catch(IOException e){
                Console.WriteLine(
                    String.Format(
                        "An error has occurred while reading {}:\n{}", file, e.Message
                ));
            }  
        }

        private void _PrintMenu(){
            Console.WriteLine("Menu");
        }
    }



    class Program {
        static void Main(string[] args){ // declare static function. A function that belongs to the class
            Grep grep = new Grep(args);
            grep.Run();
        }
    }
}


