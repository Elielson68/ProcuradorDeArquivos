using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Coloracao;
using System.Text.RegularExpressions;

namespace Estudo.Threads
{
    /// <summary>
    /// A classe abaixo é responsável por fazer a busca de um arquivo específico, criando 1 thread para cada subdiretório
    /// encontrado a partir do diretório especificado.
    /// As booleanas abaixo representam se determinada opção inserida pelo usuário foi encontrada com êxito ou não.
    /// Por exemplo:
    /// É dado o diretório C:/Users/Usuario/Documents/PastaQualquer
    /// Onde dentro dessa pasta contém o arquivo text.txt
    /// Caso seja passado para a busca ele procurar pela extensão .html com o nome do arquivo sendo text
    /// O programa irá retornar que não encontrou o arquivo com o motivo sendo que ele não encontrou a extensão especificada.
    /// DicOptions é referente a opção em sequência do tipo de procura.
    /// sendo: path -> nome do Arquivo -> Extensão do Arquivo -> Data de criação -> Tamanho em bytes -> Palavra ou frase 
    /// Agora entendendo cada booleana e o que ela deve retornar para o usuário:
    /// </summary>
    /// <remarks>  
    /// fileIsFinded: retorna se o arquivo foi encontrado ou não.
    /// typeFileIsFinded: retorna se o tipo especificado para se buscar foi encontrado ou não.
    /// dateIsFinded: retonra se a data de criação especificada foi encontrada ou não.
    /// lengthIsFinded: retorna se o tamanho em bytes especificado foi encontrado ou não.
    /// wordOrPhraseIsFinded: retorna se o conteúdo de dentro do arquivo possui a palavra ou frase especificada ou não.       
    /// </remarks>
    
    class FindFiller
    {
        static bool fileIsFinded = false;
        static bool typeFileIsFinded = false;
        static bool dateIsFinded = false;
        static bool lengthIsFinded = false;
        static bool wordOrPhraseIsFinded = false;
        /// <summary>
        /// path: path para iniciar a busca
        /// file: Arquivo para ser encontrado
        /// extension: Extensão do arquivo para ser encontrada
        /// date: Data de criação do arquivo para ser encontrada
        /// bytes: Tamanho do arquivo em bytes para ser encontrado
        /// content: Palavra ou frase contida no arquivo para ser encontrada
        /// </summary>
        static Dictionary<string, string> DicOptions = new Dictionary<string, string>(){
            {"path", ""},
            {"file", ""},
            {"extension", ""},
            {"date", ""},
            {"bytes", ""},
            {"content", ""}
        };
        /// <summary>
        /// Primeiro o programa verifica os argumentos passados para ele na hora da execução
        /// definindo o tipo de busca que será realizado.
        /// Logo em seguida é criado um objeto do tipo Stopwatch que é utilizado para verificar
        /// o tempo de execução da nossa busca.
        /// Então é utilizado o Parallel.Invoke, para criarmos a nossa thread da nossa primeira chamada do método IniSearch.
        /// A busca sendo finalizada o tempo é parado e logo em seguida é printado o Log da procura e o tempo que foi gasto.
        /// </summary>
        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                TextoColorido.Imprimir("(B=Red|L=Yellow)Argumentos path e file são necessários!(.)\n");
                return;
            }
            string pattern = @"(.+)=(.+)";
            for(int i=0; i<args.Length; i++)
            {
                Match arg = Regex.Match(args[i], pattern);
                if(arg.Groups[1].Value is "" || arg.Groups[2].Value is "")
                {
                    TextoColorido.Imprimir($"(B=Red|L=Yellow)O argumento {i+1} está vazio ou incorreto!(.)\n");
                }
                else if (!DicOptions.Keys.Contains(arg.Groups[1].Value))
                {
                    TextoColorido.Imprimir($"(B=Red|L=Yellow)A chave \"{arg.Groups[1].Value}\" não é válida!(.)\n");
                }
                else
                {
                    DicOptions[arg.Groups[1].Value] = arg.Groups[2].Value;
                }
            }
            Console.WriteLine("Iniciando busca...");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Parallel.Invoke(() => InitSearch(path:DicOptions["path"], fileName:DicOptions["file"],
                                             typeFile: DicOptions["extension"], dateFile: DicOptions["date"],
                                             lengthFile: DicOptions["bytes"], wordOrPhraseIncluse: DicOptions["content"]));
            // InitSearch(path:DicOptions[0], fileName:DicOptions[1],
            //                                  typeFile: DicOptions[2], dateFile: DicOptions[3],
            //                                  lengthFile: DicOptions[4], wordOrPhraseIncluse: DicOptions[5]);
            stopwatch.Stop();
            PrintLogSearch(DicOptions["file"]);
            Console.WriteLine($"Tempo de procura: {stopwatch.Elapsed}"); 
        }
        /// <summary>
        /// Printa o Log da busca. 
        /// A primeira booleana fileIsFinded verifica se o arquivo foi encontrado ou não.
        /// A segunda dentro verificam se o tipo de arquivo especificado foi encontrado
        /// A terceira verifica se a data foi encontrada
        /// A quarta verifica se o tamanho em bytes foi encontrado
        /// a quinta verifica se a palavra ou frase foi encontrada
        /// </summary>
        static void PrintLogSearch(string fileToFind)
        {
            if(!fileIsFinded)
            {
                TextoColorido.Imprimir($"O arquivo (B=White|L=Black){fileToFind}(.) (B=Red|L=Yellow)não foi encontrado!(.)\n");
                if(!typeFileIsFinded)
                {
                    TextoColorido.Imprimir("Não foi encontrado o (B=White|L=Black)tipo(.) de arquivo especificado\n");
                }
                if(!dateIsFinded)
                {
                    TextoColorido.Imprimir("Não foi encontrado a (B=White|L=Black)data(.) do arquivo especificado\n");
                }
                if(!lengthIsFinded)
                {
                    TextoColorido.Imprimir("Não foi encontrado o (B=White|L=Black)tamanho(.) do arquivo especificado\n");
                }
                if(!wordOrPhraseIsFinded)
                {
                    TextoColorido.Imprimir("Não foi encontrada a (B=White|L=Black)palavra(.) ou frase especificada\n");
                }
            }
        }
        /// <summary>
        /// Função para encontrar um arquivo específico dado as opções.
        /// <param name="path">Path de onde deve ser feita a busca do arquivo</param>
        /// <param name="nameFile">Nome do arquivo que deve ser encontrado</param>
        /// <param name="typeFile">Parametro opcional: verifica o tipo do arquivo</param>
        /// <param name="dateFile">Parametro opcional: verifica a data de criação do arquivo</param>
        /// <param name="lengthFile">Parametro opcional: verifica o tamanho em bytes do arquivo</param>
        /// <param name="wordOrPhraseIncluse">Parametro opcional: verifica uma palavra ou frase existe dentro do arquivo</param>
        /// </summary>
        static bool FindSomething(string path, string nameFile, string typeFile="", string dateFile="", string lengthFile="", string wordOrPhraseIncluse="")
        {
            
            var dirInfo = new DirectoryInfo(path);
            List<FileInfo> findedFiles = dirInfo.GetFiles().ToList().FindAll(f => {
                bool finded = f.Name.Contains(nameFile);
                if(typeFile is not "" && !typeFileIsFinded)
                {
                    typeFileIsFinded = f.Name.Contains(typeFile) && f.Name.EndsWith(typeFile);
                    finded  &= typeFileIsFinded;
                }
                if(finded)
                {
                    if(dateFile is not "" && !dateIsFinded)
                    {
                        dateIsFinded = IsSameDate(f.CreationTime.ToString(), dateFile);
                        finded  &= dateIsFinded;
                    }
                    if(lengthFile is not "" && !lengthIsFinded)
                    {
                        lengthIsFinded = f.Length.ToString() == lengthFile;
                        finded &= lengthIsFinded;
                    }
                    if(wordOrPhraseIncluse is not "" && !wordOrPhraseIsFinded)
                    {
                        try
                        {
                            var fileOpened = f.OpenText();
                            string conteudo = fileOpened.ReadToEnd();
                            wordOrPhraseIsFinded = conteudo.Contains(wordOrPhraseIncluse);
                            fileOpened.Close();
                            finded &= wordOrPhraseIsFinded;
                        }
                        catch
                        {
                            TextoColorido.Imprimir($"(B=Red|L=Yellow)O arquivo {f.Name} foi tentado ser aberto porém algo de errado ocorreu!(.)\n");
                        }
                        
                    }
                    
                }
                if(finded){
                    string texto = $"INFO Arquivo:\n\tNome: {f.Name} |\n\tTipo: {f.Extension} |\n\tData Criação: {f.CreationTime} |\n\t"+
                                   $"Atributos: {f.Attributes} |\n\tTamanho: {f.Length} bytes |\n\tÚltima modificação: {f.LastWriteTime} |\n\t"+
                                   $"Última vez aberto: {f.LastAccessTime}";
                    Console.WriteLine(texto);
                }
                return finded;
                });
            if (findedFiles.Count > 0)
                return true;
            return false;
        }
        
        /// <summary>
        /// Função para iniciar a busca por um arquivo.
        /// <param name="path">Path de onde deve ser feita a busca do arquivo</param>
        /// <param name="nameFile">Nome do arquivo que deve ser encontrado</param>
        /// <param name="typeFile">Parametro opcional: verifica o tipo do arquivo</param>
        /// <param name="dateFile">Parametro opcional: verifica a data de criação do arquivo</param>
        /// <param name="lengthFile">Parametro opcional: verifica o tamanho em bytes do arquivo</param>
        /// <param name="wordOrPhraseIncluse">Parametro opcional: verifica uma palavra ou frase existe dentro do arquivo</param>
        /// </summary>
        static void InitSearch(string path, string fileName, string typeFile="", string dateFile="", string lengthFile="", string wordOrPhraseIncluse="", bool enableLog=false)
        {
            if(fileIsFinded) return;
            bool isHere = false;
            try
            {
                isHere = FindSomething(path, fileName, typeFile, dateFile, lengthFile, wordOrPhraseIncluse);
            }
            catch
            {
                if(enableLog)
                {
                    TextoColorido.Imprimir("(B=Red|L=Yellow)Sem permissão para acesso o diretório(.)\n");
                }
            }
            if(!isHere)
            {
                try
                {
                    var directories = Directory.EnumerateDirectories(path);
                    Parallel.ForEach(directories,  item =>  InitSearch(item, fileName, typeFile, dateFile, lengthFile, wordOrPhraseIncluse, enableLog));
                    // foreach(string item in directories)
                    //     InitSearch(item, fileName, typeFile, dateFile, lengthFile, wordOrPhraseIncluse, enableLog);
                }
                catch
                {
                    if(enableLog)
                    {
                        TextoColorido.Imprimir($"(B=Red|L=Yellow)Sem permissão para acessar os diretórios de: {path}(.)\n");
                    }
                    
                }
                
            }
            else
            {
                TextoColorido.Imprimir($"(B=Green|L=Black)O arquivo está no PATH:(.) (B=Green|L=DarkRed){path}(.)\n"); 
                fileIsFinded = true;
            }
                
        }

        /// <summary>
        /// Função para validar se uma data é a mesma da outra independente do tipo de separador da mesma.
        /// <example>
        /// A data 02/04/2022 é a mesma que 02.04.2022 mudando apenas o separador.
        /// </example>
        /// </summary>
        static bool IsSameDate(string dateOne, string dateTwo)
        {
            Match d1 = Regex.Match(dateOne, @"(\d\d).(\d\d).(\d\d\d\d)");
            Match d2 = Regex.Match(dateTwo, @"(\d\d).(\d\d).(\d\d\d\d)");
            string d1Text = $"{d1.Groups[1]}{d1.Groups[2]}{d1.Groups[3]}";
            string d2Text = $"{d2.Groups[1]}{d2.Groups[2]}{d2.Groups[3]}";
            return d1Text == d2Text;
        }
    }
}
