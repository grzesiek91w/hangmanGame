using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame
{
    class Application
    {
        
        string capital="";
        string country="";
       
        int guessingTries=0;
        long elapsedMs=0L;  
         

        static void Main(string[] args)
        {
            

            Application app = new Application();

            readCounteriesAndCapitals(app);
            

                ConsoleKey response;
                 do
                 {
                    Console.Write("If you have continue new game? [y/n] ");
                    response = Console.ReadKey(false).Key;   
                    if (response != ConsoleKey.Enter)
                        Console.WriteLine();

                } while (response != ConsoleKey.Y && response != ConsoleKey.N);


                 if(response==ConsoleKey.Y){
                    Console.Clear();
                    readCounteriesAndCapitals(app);          
                }
                else{
                    Console.WriteLine("End game");
                }

        
        }


        private static void readCounteriesAndCapitals(Application app){
        
            var file=("countries_and_capitals.txt.txt");
            string[] lines=null;

            try
            {
            lines = File.ReadAllLines(file);
            }
            catch (Exception ex)
            {
             Console.WriteLine("Cannot open file" +ex);
            }

            
            char[] guess =null;
            char[] charArr = null;

            Random rand = new Random();
            String a = lines[rand.Next(lines.Length)];
            string[] textSplit = a.Split(" | ");

            //Console.WriteLine(a);
            //Console.WriteLine(textSplit[1]);

            app.capital = textSplit[1];  
            app.country = textSplit[0];

            charArr = app.capital.ToCharArray();  
            for(int i=0; i<charArr.Length;i++){
                Console.Write("_ ");
            }
            Console.WriteLine();

            /*
              foreach (char ch in charArr)  
                {  
                    Console.Write(ch);  
                }  
            */
            
            Console.WriteLine();

            Console.WriteLine("You have 5 score life");

            guess = new char[charArr.Length];

 
            for (int z = 0; z < charArr.Length; z++)
                guess[z] = '_';

            
            capitalFound(app,guess,charArr);

        }

        private static void capitalFound(Application app, char[] guess, char[] charArr){

            bool retry = true;
            int missed=6;
            int loseLife=0;

             
        
            int sumChar=0;
            List<char> charList = new List<char>(); 
            app.guessingTries=0;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            while (retry == true) {   
                
                bool isLife=true;

                if(sumChar!=charArr.Length){
            

                     ConsoleKey response;
                 do
                 {
                    Console.Write("If you have guess all word capital? [y/n] ");
                    response = Console.ReadKey(false).Key;   
                    if (response != ConsoleKey.Enter)
                        Console.WriteLine();

                } while (response != ConsoleKey.Y && response != ConsoleKey.N);

                

                while((loseLife==4&&isLife==true) || (response==ConsoleKey.Y&& loseLife<5)){
                    string sentence3="";
                    if(loseLife==4){
                        Console.WriteLine($"The capital of {app.country}");
                    }

                    Console.WriteLine("Write capital");
                    sentence3  = Console.ReadLine();
                    app.guessingTries++;
                    Console.WriteLine($"Hello! {sentence3}");

                    if(sentence3.Equals(app.capital)){
                        response=ConsoleKey.N;
                        isLife=false;
                        sumChar=charArr.Length;

                        Console.Write("Your guessed Capital is : ");
                        Console.WriteLine(sentence3);

                    }
                    else{
                        Console.WriteLine("Wrong answer");
                        loseLife+=2;
                        missed+=-2;
                        

                        if(loseLife<5){
                            Console.WriteLine("Number lose life! = "+loseLife);
                            

                            do
                            {
                            Console.Write("If you continue guess all word capital? [y/n] ");
                            response = Console.ReadKey(false).Key;   
                                if (response != ConsoleKey.Enter)
                                  Console.WriteLine();

                            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

                        }
                         
                    }
                    draw(missed);
                       
                }
            }
                

            if(loseLife>=5){
                Console.WriteLine("Your lose!!!");

                watch.Stop();
                app.elapsedMs =watch.ElapsedMilliseconds;
                Console.WriteLine("Execution time: " + app.elapsedMs);
                    
                charList.Clear();
                    
                retry=false;

                Console.WriteLine("##################");
                Console.WriteLine("Top rank 10: ");
                Console.WriteLine("##################"); 
                readTopWinnerGame(app);
                Console.WriteLine("##################");

            }


            else if(sumChar==charArr.Length){
                Console.WriteLine("You guessed passed! = "+app.guessingTries);
                watch.Stop();
                app.elapsedMs =watch.ElapsedMilliseconds;
                Console.WriteLine("Execution time: " + app.elapsedMs);
                Console.WriteLine($"Your guessed the capital after {app.guessingTries} letters. It took you {app.elapsedMs} Miliseconds");
                    
                charList.Clear();

                retry=false;

                appendWinnerGame(app);

                Console.WriteLine("##################");
                Console.WriteLine("Top rank 10: ");
                Console.WriteLine("##################");  
                readTopWinnerGame(app);
                Console.WriteLine("##################");

            Console.Write("Your guessed Capital is : ");
            Console.WriteLine(app.capital);
            }

            else{

                int numberSizeNotGuess=0;
                char playerGuess='0';
                    
                Console.Write("Wrong character list: ");

                foreach(char c in charList){
                    Console.Write(c +",");
                }

                Console.WriteLine("");
                Console.Write("Guess character Capital ");

                for (int z = 0; z < charArr.Length; z++)
                    Console.Write(guess[z]);

                Console.WriteLine("");
                Console.Write("Please enter your guess: ");

                numberSizeNotGuess=0;
                playerGuess = char.Parse(Console.ReadLine());
                app.guessingTries++;

                for (int j = 0; j < charArr.Length; j++){
                    numberSizeNotGuess++;

                    if (playerGuess == charArr[j]){
                        guess[j] = playerGuess;
                        numberSizeNotGuess--;
                        sumChar++;
                    }
                    
                    else if(numberSizeNotGuess==charArr.Length){
                        charList.Add(playerGuess);
                        loseLife++;
                        Console.WriteLine("You guessed wrong! = "+loseLife);
                    
                        missed--;
                                  
                    }
                    draw(missed);

                }
            Console.Write("Your guessed Capital is : ");
            Console.WriteLine(guess);

            }
 

            //Console.Clear();
            
            Console.WriteLine(); 
            }

        }

        private static void draw(int missed){

            Console.WriteLine();
                Console.WriteLine("=====HANGMAN GAME=====");
                Console.WriteLine();
                Console.WriteLine("======================");
                Console.WriteLine();

                switch (missed){
                    case 6:
                        Console.WriteLine("very good");
                        break;
                    case 5:
                        Console.WriteLine("   O");
                        break;
                    case 4:
                        Console.WriteLine("   O");
                        Console.WriteLine("   |");
                        Console.WriteLine("   |");
                        break;
                    case 3:
                        Console.WriteLine("   O");
                        Console.WriteLine("   |");
                        Console.WriteLine("   |");
                        Console.WriteLine("  /");
                        break;
                    case 2:
                        Console.WriteLine("   O");
                        Console.WriteLine("   |");
                        Console.WriteLine("   |");
                        Console.WriteLine("  / \\");
                        break;
                    case 1:
                        Console.WriteLine("   O");
                        Console.WriteLine("  \\|");
                        Console.WriteLine("   |");
                        Console.WriteLine("  / \\");
                        break;
                    default:
                        Console.WriteLine("   O");
                        Console.WriteLine("  \\|");
                        Console.WriteLine("   |");
                        Console.WriteLine("  / \\");
                        break;
                }
            
            Console.WriteLine();
            Console.WriteLine("======================");
            Console.WriteLine();

            
        }

        
        private static void appendWinnerGame(Application app)
        {
            Console.Write("What's your name?");
            string myName = Console.ReadLine();
            Console.WriteLine();

            File.AppendAllText("date.txt", 
                   myName +" | "+DateTime.Now.ToString() +" | "+app.guessingTries +" | "+app.elapsedMs + Environment.NewLine);

        }


        private static void readTopWinnerGame(Application app)
        {
            try{
            var recipients = File.ReadAllLines("rankScore.txt")
                                  .Select (record => record.Split('|'))
                                  .Select (tokens => new 
                                    {
                                    name = tokens[0],
                                    date = Convert.ToDateTime(tokens[1]),
                                    guessingTries = tokens[2],
                                    timeTriesMs = tokens[3]  
                                    }
                                        )
                                    .OrderBy (m => long.Parse(m.guessingTries))
                                    .ThenBy(m=>long.Parse(m.timeTriesMs))
                                    .Take(10);
            int i=0;
            foreach(var top in recipients){
                Console.WriteLine($"Top = {++i}: "+top);
                
            }
            }
            catch(Exception ex){
                Console.WriteLine("Cannot open file"+ ex);
            }
            
        }
    }
}
