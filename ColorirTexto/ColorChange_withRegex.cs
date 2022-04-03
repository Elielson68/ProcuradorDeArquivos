using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
namespace Coloracao{
    public class TextoColorido{
        private static string colors = "Black|Blue|Cyan|DarkBlue|DarkCyan|DarkGray|DarkGreen|DarkMagenta|DarkRed|DarkYellow|Gray|Green|Magenta|Red|White|Yellow";
        private static string pattern_init = $@"(\(B=({colors})\|L=({colors})\))";
        private static string pattern_end = $@"(\(\.\))";
        private static void ChoiceBackGroundColor(string color)
        {
            switch (color)
            {
                case "Black":
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "Blue":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "Cyan":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case "DarkBlue":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case "DarkCyan":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case "DarkGray":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case "DarkGreen":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case "DarkMagenta":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "DarkRed":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case "DarkYellow":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case "Gray":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case "Green":
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case "Magenta":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case "Red":
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "White":
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case "Yellow":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.WriteLine($"Valor da cor de background é inválido: {color}");
                    break;
            }
        }
        private static void ChoiceLetterColor(string color)
        {
            switch (color)
            {
                case "Black":
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "Cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "DarkBlue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "DarkCyan":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "DarkGray":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "DarkGreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "DarkMagenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "DarkRed":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "DarkYellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "Gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "White":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.WriteLine($"Valor da cor de letra é inválido: {color}");
                    break;
            }
        }
        
        private static Boolean ValidatorSintaxeWithProblem(MatchCollection l1, MatchCollection l2){
            if(l1.Count != l2.Count){
                var lista = l1.Count > l2.Count ? l1 : l2;
                Boolean lista_vazia = (l1.Count==0 || l2.Count == 0) ? true:false;
                if(lista_vazia){
                    foreach(Match match in lista){
                        ChoiceBackGroundColor("DarkRed");
                        Console.Write($"ERROR SYNTAX: O valor {match.Value} no index {match.Index} não foi {(l1.Count==0?"aberto":"fechado")} corretamente!");
                        ChoiceBackGroundColor("Black");
                    }
                }
                else{
                    List<Tuple<Match,Match>> aberturas_fechamentos = new List<Tuple<Match, Match>>();
                    int aux = lista.Count-1;
                    for(int i=0; i<lista.Count; i++){
                        if(i <= l1.Count-1){
                            Match match_vazio = null;
                            bool opencode_range = i <= l1.Count-1;
                            bool  closecode_range = aux <= l2.Count-1;
                            aberturas_fechamentos.Add(new Tuple<Match, Match>(opencode_range?l1[i]:match_vazio, closecode_range?l2[aux]:match_vazio));
                            aux--;
                        }
                    }
                    foreach(Tuple<Match, Match> t in aberturas_fechamentos){
                        if(t.Item2 ==null){
                            ChoiceBackGroundColor("DarkRed");
                            Console.Write($"ERROR SYNTAX: O valor {t.Item1.Value} no index {t.Item1.Index} não foi fechado corretamente!");
                            ChoiceBackGroundColor("Black");
                        }
                        if(t.Item1 == null){
                            ChoiceBackGroundColor("DarkRed");
                            Console.Write($"ERROR SYNTAX: O valor {t.Item2.Value} no index {t.Item2.Index} não foi aberto corretamente!");
                            ChoiceBackGroundColor("Black");
                        }
                    }
                }
                return true;
            }
            else{
                bool problem = false;
                for(int i=0; i<l1.Count; i++){
                    if(i+1 <= l1.Count-1){
                        if((l2[i].Index > l1[i].Index) && (l2[i].Index > l1[i+1].Index)){
                            ChoiceBackGroundColor("DarkRed");
                            Console.Write($"ERROR SYNTAX: O valor {l1[i].Value} no index {l1[i].Index} não foi fechado corretamente!");
                            ChoiceBackGroundColor("Black");
                            problem = true;
                        }
                    }
                    if(l2[i].Index < l1[i].Index){
                        ChoiceBackGroundColor("DarkRed");
                        Console.Write($"ERROR SYNTAX: O valor {l1[i].Value} deve vir antes de {l2[i].Value}!");
                        ChoiceBackGroundColor("Black");
                        problem = true;
                    }
                
                }
                return problem;
            }  
        }
        public static void Test(string texto){
            Match t = Regex.Match(texto, pattern_init);
            Console.WriteLine($"{t.Value} {t.Index} {t.Success}");
            t = t.NextMatch();
        }
        public static void Imprimir(string texto, Boolean Logger=false)
        {
            MatchCollection OpenCode_List = Regex.Matches(texto, pattern_init);
            MatchCollection CloseCode_List = Regex.Matches(texto, pattern_end);
            int index_last_stop = 0;
            int index_aux = 0;
            
            if(ValidatorSintaxeWithProblem(OpenCode_List, CloseCode_List))
            {
                return;
            }
            for (int i=0;i<OpenCode_List.Count;i++)
            {
                
                if (Logger) {
                    string t = $"Abertura\n|Código:\t{OpenCode_List[i].Value} \n|ColorBackground:\t{OpenCode_List[i].Groups[2]} \n|ColorLetter:\t{OpenCode_List[i].Groups[3]} \n|Index:\t{OpenCode_List[i].Index} \n|Length:\t{OpenCode_List[i].Length} \n|Name:\t{OpenCode_List[i].Name} \n";
                    string f = $"Fechamento\n|Código:\t{CloseCode_List[i].Value} \n|Index:\t{CloseCode_List[i].Index} \n|Length:\t{CloseCode_List[i].Length} \n|Name:\t{CloseCode_List[i].Name} \n\n_______________\n";
                    Console.WriteLine(t);
                    Console.WriteLine(f);
                }
                
                Console.Write(texto[index_last_stop..OpenCode_List[i].Index]);
                index_aux = OpenCode_List[i].Index + OpenCode_List[i].Length;
                ChoiceBackGroundColor($"{OpenCode_List[i].Groups[2]}");
                ChoiceLetterColor($"{OpenCode_List[i].Groups[3]}");
                Console.Write(texto[index_aux..CloseCode_List[i].Index]);
                ChoiceBackGroundColor("Black");
                ChoiceLetterColor("White");
                index_last_stop = CloseCode_List[i].Index + CloseCode_List[i].Length;
            }
            Console.Write(texto[index_last_stop..texto.Length]);
            
        }
    
        public static void BarraCarregamento(string texto="SUCCESS", string bar_color="Green", string empty_color="White", string lt = "White"){
            string space_add = "";
            string space_sub = "                    ";
            string progressbar = $"[(B=Green|L=White){space_add}(.){space_sub}]";
            int y = space_sub.Length;
            int total_spaces = space_sub.Length+2;
            string success = texto;
            int index_suc = 0;
            Console.WriteLine(y);
            for (int x=0; x<total_spaces; x++){
                System.Console.Clear();
                Imprimir(progressbar+'\n');
                if (x > (total_spaces/4) && x < (total_spaces/4)+1+success.Length){
                    space_add += success[index_suc];
                    index_suc++;
                }
                else{
                    space_add += " ";
                }
                
                if (y >= 0)
                    space_sub = space_sub[0..y];
                y--;
                
                progressbar = $"[(B={bar_color}|L={lt}){space_add}(.)(B={empty_color}|L=Black){space_sub}(.)]";
                Thread.Sleep(500);
            }
            
        }
        public static void Mario(){
            /*
                DarkBlue"
                "DarkCyan"
                "DarkGray"
                "DarkGreen"
                "DarkMagenta"
                "DarkRed"
                "DarkYellow"
            */
            //Console.BackgroundColor = ConsoleColor.White;
            string andar = "";
            ConsoleKeyInfo cki;
            while(true){
                Console.Clear();
                string mario = $"{andar}       (B=DarkRed|L=Black)        (.)\n";
                  mario += $"{andar}     (B=DarkRed|L=Black)               (.)\n";
                  mario += $"{andar}     (B=DarkGray|L=DarkGray)    (.)(B=DarkYellow|L=DarkGray)   (.)(B=DarkGray|L=DarkGray) (.)(B=DarkYellow|L=DarkGray)   (.)\n";
                  mario += $"{andar}    (B=DarkGray|L=DarkGray) (.)(B=DarkYellow|L=DarkGray) (.)(B=DarkGray|L=DarkGray) (.)(B=DarkYellow|L=DarkGray)     (.)(B=DarkGray|L=DarkGray) (.)(B=DarkYellow|L=DarkGray)     (.)\n";
                  mario += $"{andar}    (B=DarkGray|L=DarkGray) (.)(B=DarkYellow|L=DarkGray) (.)(B=DarkGray|L=DarkGray)  (.)(B=DarkYellow|L=DarkGray)     (.)(B=DarkGray|L=DarkGray) (.)(B=DarkYellow|L=DarkGray)      (.)\n";
                  mario += $"{andar}    (B=DarkGray|L=DarkGray)  (.)(B=DarkYellow|L=DarkGray)     (.)(B=DarkGray|L=DarkGray)     (.)\n";
                  mario += $"{andar}      (B=DarkYellow|L=DarkGray)         (.)\n";//Linha do pescoço
                  mario += $"{andar}     (B=DarkGray|L=DarkGray)   (.)(B=DarkRed|L=DarkGray)  (.)(B=DarkGray|L=DarkGray)    (.)\n";
                  mario += $"{andar}    (B=DarkGray|L=DarkGray)    (.)(B=DarkRed|L=DarkGray)  (.)(B=DarkGray|L=DarkGray)   (.)(B=DarkRed|L=DarkGray) (.)(B=DarkGray|L=DarkGray)     (.)\n";
                  mario += $"{andar}   (B=DarkGray|L=DarkGray)     (.)(B=DarkRed|L=DarkGray)      (.)(B=DarkGray|L=DarkGray)      (.)\n";
                  mario += $"{andar}   (B=DarkYellow|L=DarkGray)    (.)(B=DarkGray|L=DarkGray) (.)(B=DarkRed|L=DarkGray) (.)(B=Yellow|L=DarkGray) (.)(B=DarkRed|L=DarkGray)   (.)(B=Yellow|L=DarkGray) (.)(B=DarkRed|L=DarkGray) (.)(B=DarkGray|L=DarkGray) (.)(B=DarkYellow|L=DarkGray)    (.)\n";
                  mario += $"{andar}   (B=DarkYellow|L=DarkGray)     (.)(B=DarkRed|L=DarkGray)       (.)(B=DarkYellow|L=DarkGray)     (.)\n";
                  mario += $"{andar}   (B=DarkYellow|L=DarkGray)   (.)(B=DarkRed|L=DarkGray)           (.)(B=DarkYellow|L=DarkGray)   (.)\n";
                  mario += $"{andar}      (B=DarkRed|L=DarkGray)     (.) (B=DarkRed|L=DarkGray)     (.)\n";
                  mario += $"{andar}    (B=DarkGray|L=DarkGray)      (.)   (B=DarkGray|L=DarkGray)      (.)\n";
                  mario += $"{andar}  (B=DarkGray|L=DarkGray)        (.)   (B=DarkGray|L=DarkGray)        (.)\n";
                Imprimir(mario);
                if(Console.KeyAvailable == true){
                    cki = Console.ReadKey(true);
                    if(cki.Key == ConsoleKey.RightArrow){
                        andar += " ";
                    }
                    if(cki.Key == ConsoleKey.LeftArrow){
                        andar = andar[0..(andar.Length-2)];
                    }
                    
                }
                Thread.Sleep(300);
            }
            
        }
    
        public static void ArcoIris(string texto){
            Random r = new Random();
            int rInt = r.Next(0, 100); //for ints
            int range = 15;
            int core = 0;
            int posicao = core;
            int velocidade = 500;
            ConsoleKeyInfo cki;
            while(true){
                string imprimir = "";
                for(int x=0;x<texto.Length;x++){
                    int index =(int)(r.NextDouble()* range);
                    string color = colors.Split("|")[core];
                    imprimir += x==posicao ? $"(B={color}|L=White){texto[x]}(.)":texto[x];
                }
                posicao = (posicao+1)%texto.Length;
                if(posicao == 0){
                   core =(int)(r.NextDouble()* range);
                }
                Imprimir(imprimir);
                Thread.Sleep(velocidade);
                Console.Clear();
                Console.WriteLine("Velocidade: {0}", velocidade);
                if(Console.KeyAvailable == true){
                    cki = Console.ReadKey(true);
                    if(cki.Key == ConsoleKey.UpArrow){
                        velocidade -= 10;
                    }
                    if(cki.Key == ConsoleKey.DownArrow){
                        velocidade += 10;
                    }
                    
                }
                
                
                
                
            }
        }
    }
}