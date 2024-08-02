using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Graphs
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Console.WriteLine("Здравствуйте, дорогой гость! Вам пердставлен интерфейс, с помощью которого Вы сможете работь с графами." +
                " Пожалуйста, внимательно читайте инструкции по работе с данным интерфейсом. Желаю Вам приятной работы!\n");


            Graph gr1 = new Graph(false, false);
            string path = "";
            Console.WriteLine("\tВы хотите, чтобы программа прочитала Ваш граф из файла " +
                "или создала граф самостоятельно?");
            Console.WriteLine("\tВведите f, если хотите прочитать граф из файла, или " +
                "s, если создать по шаблону, или m, " +
                "если хотите пустой граф со своими параметрами");
            string graph = Console.ReadLine().ToLower();
            if(graph == "f")
            {
                Console.WriteLine("\tИз какого файла вы хотите прочитать граф?\n");
                Console.WriteLine("\t[1] - из первого");
                Console.WriteLine("\t[2] - из второго");
                Console.WriteLine("\t[3] - из третьего");
                Console.WriteLine("\t[4] - из четвертого");
                int n = int.Parse(Console.ReadLine());
                //string path = "";
                if (n == 1)
                {
                    path = "C:/Users/User/source/repos/Graphs/Graphs/Input1.txt";
                }
                else if (n == 2)
                {
                    path = "C:/Users/User/source/repos/Graphs/Graphs/Input2.txt";
                }
                else if (n == 3)
                {
                    path = "C:/Users/User/source/repos/Graphs/Graphs/Input3.txt";
                }
                else if (n == 4)
                {
                    path = "C:/Users/User/source/repos/Graphs/Graphs/Input4.txt";
                }
                else
                {
                    Console.WriteLine("\tКак мне это понимать?\n");
                    Console.WriteLine("\tУвы и Ах, граф будет прочитан из первого файла");
                    path = "C:/Users/User/source/repos/Graphs/Graphs/Input1.txt";
                }
                gr1 = new Graph(path);
            }
            else if(graph == "s")
            {
                Console.WriteLine("\tВведите число городов от 1 до 30");
                int count = int.Parse(Console.ReadLine());
                gr1 = new Graph(count);
            }
            else if(graph == "m")
            {
                Console.WriteLine("Ваш граф будет ориентированным? [y/n]");
                string ans = Console.ReadLine().ToLower();
                bool dir = false;
                bool wght = false;
                if(ans == "y")
                {
                    dir = true;
                }
                Console.WriteLine("Ваш граф будет взвешенным? [y/n]");
                ans = Console.ReadLine().ToLower();
                if(ans == "y")
                {
                    wght = true;
                }
                gr1 = new Graph(dir, wght);
            }
            else
            {
                Console.WriteLine("\tПрограмма не поняла вашу команда и создает пустой граф невзвешенный " +
                    "и неориентированный граф");
            }
            //gr1 = new Graph(path);
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить:)\n");
            var key = Console.ReadKey();

            while(key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("\tДоступны следующие операции:");
                Console.WriteLine("\t[1] - Копирование Вашего графа в другой");
                Console.WriteLine("\t[2] - Добавление вершины в граф");
                Console.WriteLine("\t[3] - Удаление вершины графа");
                Console.WriteLine("\t[4] - Добавление ребра между двумя вершинами");
                Console.WriteLine("\t[5] - Удаление ребра между вершинами");
                Console.WriteLine("\t[6] - Запись Вашего графа в файл");
                Console.WriteLine("\t[7] - Поиск вершины, достижимой из заданных");
                Console.WriteLine("\t[8] - Вывод графа на консоль");
                Console.WriteLine("\t[9] - Поиск вершины, достижимой из заданных");
                Console.WriteLine("\t[0] - Построение дополнения к Вашему графу");
                Console.WriteLine("\t[P} - Нахождение пути между заданными вершинами, не проходящий через заданное множество вершин");
                Console.WriteLine("\t[Q} - Нахождение путей от заданной вершины до всех");
                Console.WriteLine("\t[K] - Построение Каркаса по алгоритму Прима");
                Console.WriteLine("\t[B] - Нахождение длин кратчайших путей от U до V");
                Console.WriteLine("\t[D] - Поиск вершины, сумма длин кратчайших путей от которой до остальных вершин минимальна");
                Console.WriteLine("\t[F] - Нахождение длин кратчайших путей от V до U1 и U2");
                Console.WriteLine("\t[C] - Нахождение максимального потока");
                Console.WriteLine("\tНажмите Escape, чтобы завершить работу программы");
                Console.WriteLine();
                key = Console.ReadKey();

                switch(key.Key)
                {
                    case ConsoleKey.D1:

                        Console.WriteLine("\tКопирование графа");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an1 = Console.ReadLine().ToLower();
                        if (an1 == "y")
                        {
                            Graph gr2 = new Graph(gr1);
                            Console.WriteLine("\tВаш граф скопирован\n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                    case ConsoleKey.D2:
                        Console.WriteLine("\tДобавление вершины");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an2 = Console.ReadLine().ToLower();
                        if (an2 == "y")
                        {
                            Console.WriteLine("Вы можете добавить вершину несколькими способами");
                            Console.WriteLine("\t[1] - добавить вершину со списком смежных с ней вершин");
                            Console.WriteLine("\t[2] - добавить изолированную вершину");
                            Console.WriteLine("\t[3] - добавить вершину со списком смежности, и списком вершин, смежных с ней");
                            Console.WriteLine("\tВведите номер команды:");
                            int comand = int.Parse(Console.ReadLine());
                            if (comand == 1)
                            {
                                try
                                {
                                    Console.WriteLine("\tВведите название вершины");
                                    string vertex = Console.ReadLine();
                                    if (gr1.is_graph_weighted)
                                    {
                                        Dictionary<string, int> d = new Dictionary<string, int>();
                                        Console.WriteLine("\tВаш граф взвешенные, " +
                                            "поэтому вам необходимо вводить вершины и длины путей");
                                        string line = Console.ReadLine();
                                        char[] seps = { ' ', '\t', '\n', '\r', '\v' };
                                        string[] l;
                                        l = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 1; i < l.Length; i += 2)
                                        {
                                            d.Add(l[i], int.Parse(l[i + 1]));
                                        }

                                        gr1.AddVertex(vertex, d);
                                        Console.WriteLine("\tВершина добавлена\n");
                                    }

                                    else
                                    {
                                        Dictionary<string, string> dict1 = new Dictionary<string, string>();
                                        Console.WriteLine("\tВаш граф невзвешенный, " +
                                            "поэтому вам необходимо ввести только названия вершин");
                                        string line = Console.ReadLine();
                                        char[] seps = { ' ', '\t', '\n', '\r', '\v' };
                                        string[] l;
                                        l = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 1; i < l.Length; i++)
                                        {
                                            dict1.Add(l[i], null);
                                        }
                                        gr1.AddVertex(vertex, dict1);
                                        Console.WriteLine("\tВершина добавлена\n");
                                    }
                                }
                                catch (Exception e)
                                {

                                    Console.WriteLine(e.Message);
                                }
                            }
                            else if (comand == 2)
                            {
                                try
                                {
                                    Console.WriteLine("Введите название изолированной вершины");
                                    string vertex1 = Console.ReadLine();
                                    gr1.AddVertex(vertex1);
                                    Console.WriteLine("\tВершина добавлена\n");
                                }
                                catch (Exception e)
                                {

                                    Console.WriteLine(e.Message);
                                }
                            }
                            else if (comand == 3)
                            {
                                try
                                {
                                    if (gr1.direction_of_graph)
                                    {
                                        Console.WriteLine("\tВведите название вершины");
                                        string vertex2 = Console.ReadLine();
                                        if (gr1.is_graph_weighted)
                                        {
                                            Console.WriteLine("\tВаш граф взвешенный," +
                                                "поэтому вам необходимо ввести названия вершин и длины путей");
                                            Dictionary<string, int> d = new Dictionary<string, int>();
                                            string line = Console.ReadLine();
                                            char[] seps = { ' ', '\t', '\n', '\r', '\v' };
                                            string[] l;
                                            l = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i = 1; i < l.Length; i += 2)
                                            {
                                                d.Add(l[i], int.Parse(l[i + 1]));
                                            }
                                            Console.WriteLine("\tВведите список вершин, " +
                                                "смежных с новой");
                                            line = "";
                                            line = Console.ReadLine();
                                            List<string> ls = new List<string>();
                                            string[] l1;
                                            l1 = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i = 1; i < l1.Length; i++)
                                            {
                                                ls.Add(l[i]);
                                            }
                                            gr1.AddVertex(vertex2, d, ls);
                                            Console.WriteLine("\tВершина добавлена\n");
                                        }
                                        else
                                        {
                                            Dictionary<string, string> dict1 = new Dictionary<string, string>();
                                            Console.WriteLine("\tВаш граф невзвешенный, " +
                                                "поэтому вам необходимо ввести только названия вершин");
                                            string line = Console.ReadLine();
                                            char[] seps = { ' ', '\t', '\n', '\r', '\v' };
                                            string[] l;
                                            l = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i = 1; i < l.Length; i++)
                                            {
                                                dict1.Add(l[i], null);
                                            }
                                            List<string> ls = new List<string>();
                                            string[] l1;
                                            l1 = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i = 1; i < l1.Length; i++)
                                            {
                                                ls.Add(l[i]);
                                            }
                                            gr1.AddVertex(vertex2, dict1, ls);
                                            Console.WriteLine("\tВершина добавлена\n");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\tВаш граф неориентированный, это действие не может быть выполнено\n");
                                    }
                                }
                                catch (Exception e)
                                {

                                    Console.WriteLine(e.Message);
                                }
                            }

                            else
                            {
                                Console.WriteLine("\tНет такой команды\n");
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }


                    case ConsoleKey.D3:
                        Console.WriteLine("\tУдаление вершины");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an3 = Console.ReadLine().ToLower();
                        if (an3 == "y")
                        {
                            Console.WriteLine("\tВведите назвние вершины, которую хотите удалить");
                            string vertex3 = Console.ReadLine();
                            try
                            {
                                gr1.DeleteVertex(vertex3);
                                Console.WriteLine("\tВершина удалена\n");
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }

                    case ConsoleKey.D4:
                        Console.WriteLine("\tДобавление ребра");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an4 = Console.ReadLine().ToLower();
                        if (an4 == "y")
                        {
                            Console.WriteLine("\tПоочередно введите вершины, после команд ввода\n");
                            Console.WriteLine("\tВведите первую вершину");
                            string Vertex1 = Console.ReadLine();
                            Console.WriteLine("\tВведите вторую вершину");
                            string Vertex2 = Console.ReadLine();
                            if (gr1.is_graph_weighted)
                            {
                                Console.WriteLine("\tВаш граф взвешенный. " +
                                    "Введите вес ребра");
                                int edge = int.Parse(Console.ReadLine());
                                try
                                {
                                    gr1.AddEdge(Vertex1, Vertex2, edge);
                                    Console.WriteLine("\tРебро добавлено\n");
                                }
                                catch (Exception e)
                                {

                                    Console.WriteLine(e.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    gr1.AddEdge(Vertex1, Vertex2);
                                    Console.WriteLine("\tРебро добавлено\n");
                                }
                                catch (Exception e)
                                {

                                    Console.WriteLine(e.Message);
                                }
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }

                    case ConsoleKey.D5:
                        Console.WriteLine("\tУдаление ребра");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an5 = Console.ReadLine().ToLower();
                        if (an5 == "y")
                        {
                            Console.WriteLine("\tПоочередно введите вершины, после команд ввода\n");
                            Console.WriteLine("\tВведите первую вершину");
                            string Vertex_1 = Console.ReadLine();
                            Console.WriteLine("\tВведите вторую вершину");
                            string Vertex_2 = Console.ReadLine();
                            try
                            {
                                gr1.DeleteEdge(Vertex_1, Vertex_2);
                                Console.WriteLine("\tРебро удалено\n");
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }

                    case ConsoleKey.D6:
                        Console.WriteLine("\tЗапись в файл\n");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an6 = Console.ReadLine().ToLower();
                        if (an6 == "y")
                        {
                            Console.WriteLine("\tВ какой файл вы хотите записать граф?\n");
                            Console.WriteLine("\t[1] - в первый");
                            Console.WriteLine("\t[2] - во второй");
                            Console.WriteLine("\t[3] - в третий");
                            Console.WriteLine("\t[4] - в четвертый");
                            int p = int.Parse(Console.ReadLine());
                            if (p == 1)
                            {
                                gr1.PrintToFile("C:/Users/User/source/repos/Graphs/Graphs/Output1.txt");
                                Console.WriteLine("\tГраф записан в файл\n");
                            }
                            else if (p == 2)
                            {
                                gr1.PrintToFile("C:/Users/User/source/repos/Graphs/Graphs/Output2.txt");
                                Console.WriteLine("\tГраф записан в файл\n");
                            }
                            else if (p == 3)
                            {
                                gr1.PrintToFile("C:/Users/User/source/repos/Graphs/Graphs/Output3.txt");
                                Console.WriteLine("\tГраф записан в файл\n");
                            }
                            else if (p == 4)
                            {
                                gr1.PrintToFile("C:/Users/User/source/repos/Graphs/Graphs/Output4.txt");
                                Console.WriteLine("\tГраф записан в файл\n");
                            }
                            else
                            {
                                Console.WriteLine("\tТакой команды нет\n");
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }

                    case ConsoleKey.D7:

                        Console.WriteLine("\tПоиск по полустепеням\n");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an7 = Console.ReadLine().ToLower();
                        if(an7 == "y")
                        {
                            Console.WriteLine("\tВведите название вершины\n");
                            string ver = Console.ReadLine();
                            try
                            {
                                List<string> lst = gr1.HalfDegree(ver);
                                foreach (var it in lst)
                                {
                                    Console.Write("{0} ", it);
                                }
                                Console.WriteLine();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }

                    case ConsoleKey.D8:
                        
                        Console.WriteLine("\tВывод графа на консоль\n");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an8 = Console.ReadLine().ToLower();
                        if(an8 == "y")
                        {
                            gr1.PrintToConsole();
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }

                    case ConsoleKey.D9:
                        Console.WriteLine("\tПоиск вершины, достижимой из вершины u и вершины v");
                        Console.WriteLine("\tВы правильно выбрали действие? [y/n]");
                        string an = Console.ReadLine().ToLower();
                        if(an == "y")
                        {
                            Console.WriteLine("\tВведите вершину u");
                            string u = Console.ReadLine();
                            Console.WriteLine("\tВведите вершину v");
                            string v = Console.ReadLine();
                            try
                            {
                                List<string> foundVertexes = gr1.FindVertex(u, v);
                                if (foundVertexes.Count == 0)
                                {
                                    Console.WriteLine("\tТаких вершин нет");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine("\tНашлись такие вершины\n");
                                    foreach (var it in foundVertexes)
                                    {
                                        Console.Write("{0} ", it);
                                    }
                                    Console.WriteLine();
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                        
                        
                    case ConsoleKey.D0:
                        Console.WriteLine("\tПостроение дополнения к Вашему графу");
                        
                        if(gr1.is_graph_weighted)
                        {
                            Console.WriteLine("\tВаш граф взвешенный, дополнение к нему построиться с нулевыми весами, " +
                                "так как программа не может придумать новые веса\n");
                            Console.WriteLine("\tОбратите внимание, что мы не рекомендуем строить дополнение к взвешенным графам, " +
                                "ввиду описанной выше причины\n");
                            Console.WriteLine("\tПоложение, конечно, исправимо, " +
                                "но Вам в таком случае придется механически удалять нулевые ребра и заполнять их с новыми весами");
                            Console.WriteLine("\tВы точно хотите построить дополнение? [y/n]");
                            string answ = Console.ReadLine().ToLower();
                            if(answ == "y")
                            {
                                
                                gr1.AdditionToGraphWeighted();
                                Graph AddGr = new Graph("C:/Users/User/source/repos/Graphs/Graphs/Addition.txt");
                                AddGr.PrintToConsole();
                            }
                            else
                            {
                                Console.WriteLine("\tВозврат на панель действий\n");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tВы точно хотите построить дополнение? [y/n]");
                            string answ = Console.ReadLine().ToLower();
                            if (answ == "y")
                            {

                                Console.WriteLine("\tВаш граф невзвешенный, строим дополнение");
                                gr1.AdditionToGraph();
                                Graph AddGr = new Graph("C:/Users/User/source/repos/Graphs/Graphs/Addition.txt");
                                AddGr.PrintToConsole();
                            }
                            else
                            {
                                Console.WriteLine("\tВозврат на панель действий\n");
                                break;
                            }
                        }
                        break;
                    case ConsoleKey.P:
                        Console.WriteLine("\n\tПоиск пути  между вершинами");
                        Console.WriteLine("\tВы точно хотите найти путь? [y/n]");
                        string aP = Console.ReadLine().ToLower();
                        if (aP == "y")
                        {
                            Console.WriteLine("Вы хотите задать множество вершин, " +
                                "через которое ваш путь не пройдет? [y/n]");
                            string ayn = Console.ReadLine().ToLower();
                            if(ayn == "y")
                            {
                                
                                Console.WriteLine("Введите название стратовой вершины");
                                string startVertex = Console.ReadLine();
                                Console.WriteLine("Введите название конечной вершины");
                                string endVertex = Console.ReadLine();
                                Console.WriteLine("\tВведите список избегаемых вершин\t");
                                char[] pr = { ' ', '\n', '\r', '\v', '\t' };
                                string l = Console.ReadLine();
                                string[] a = l.Split(pr, StringSplitOptions.RemoveEmptyEntries);
                                HashSet<string> avoid = new HashSet<string>();
                                foreach(var item in a)
                                {
                                    avoid.Add(item);
                                }
                                try
                                {
                                    List<string> pathWithAv = gr1.PathWithAvoidence(startVertex, endVertex, avoid);
                                    if (pathWithAv.Count() == 0)
                                    {
                                        Console.WriteLine("Между заданными вершинами нет пути");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Найден такой путь");
                                        foreach (var item in pathWithAv)
                                        {
                                            Console.Write("{0} ", item);
                                        }
                                        Console.WriteLine();
                                    }
                                }
                                catch (Exception eP)
                                {
                                    Console.WriteLine(eP.Message);
                                }
                            }
                            else
                            {
                                
                                Console.WriteLine("Введите название стратовой вершины");
                                string startVertex = Console.ReadLine();
                                Console.WriteLine("Введите название конечной вершины");
                                string endVertex = Console.ReadLine();
                                HashSet<string> passedVertexes = new HashSet<string>();
                                try
                                {
                                    
                                    List<string> pathP = new List<string>();
                                    pathP = gr1.PathWithAvoidence(startVertex, endVertex, passedVertexes);
                                    if(pathP.Count() == 0)
                                    {
                                        Console.WriteLine("Между заданными вершинами нет пути");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Найден такой путь");
                                        foreach(var item in pathP)
                                        {
                                            Console.Write("{0} ", item);
                                        }
                                        Console.WriteLine();
                                    }
                                }
                                catch(Exception eP)
                                {
                                    Console.WriteLine(eP.Message);
                                }

                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                    case ConsoleKey.Q:
                        Console.WriteLine("\n\tПоиск кратчайших путей");
                        Console.WriteLine("\tВы точно хотите найти путь? [y/n]");
                        string aQ = Console.ReadLine().ToLower();
                        if (aQ == "y")
                        {
                            try
                            {
                                Console.WriteLine("/tВведите название вершины, с которой будем работать\n");
                                string v = Console.ReadLine();
                                if (gr1.direction_of_graph)
                                {
                                    gr1.ReverseGr(v);
                                    Graph gr1_copy = new Graph("C:/Users/User/source/repos/Graphs/Graphs/reverse.txt");
                                    Dictionary<string, List<string>> rev_path = gr1_copy.BFS(v);
                                    Console.WriteLine("Кратчайшие пути до вершины {0} ", v);
                                    
                                    foreach (var item in rev_path)
                                    {
                                        Console.Write("Путь до  {0}: ", item.Key);
                                        if (item.Value == null)
                                        {
                                            Console.Write("Нет пути");
                                        }
                                        else
                                        {
                                            foreach (var it in item.Value)
                                            {

                                                Console.Write("{0} ", it);
                                            }
                                        }

                                        Console.WriteLine();

                                    }
                                }
                                else
                                {
                                    Dictionary<string, List<string>> bfs_path = gr1.BFS(v);
                                    Console.WriteLine("Кратчайшие пути до вершины {0} ", v);
                                    foreach (var item in bfs_path)
                                    {
                                        Console.Write("Путь до  {0}: ", item.Key);
                                        if (item.Value == null)
                                        {
                                            Console.Write("Нет пути");
                                        }
                                        else
                                        {
                                            foreach (var it in item.Value)
                                            {

                                                Console.Write("{0} ", it);
                                            }
                                        }

                                        Console.WriteLine();

                                    }
                                }
                                
                            }
                            catch(Exception eQ)
                            {
                                Console.WriteLine(eQ.Message);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                    case ConsoleKey.K:
                        Console.WriteLine("\tПостроение каркаса графа по алгоритму Прима");
                        Console.WriteLine("\tВы точно хотите найти каркас? [y/n]");
                        string aK = Console.ReadLine().ToLower();
                        if (aK == "y")
                        {
                            if (gr1.is_graph_weighted && !gr1.direction_of_graph)
                            {
                                try
                                {
                                    List<int> intPath = new List<int>();
                                    Console.WriteLine("Введите название начальной вершины");
                                    string primS = Console.ReadLine();
                                    string[] karkas = gr1.AlgPrim(primS, ref intPath);
                                    Console.Write("Каркас: ");
                                    if(!karkas.Contains(null))
                                    {
                                        foreach (var it in karkas)
                                        {
                                            Console.Write("{0} ", it);
                                        }
                                        Console.WriteLine();
                                        int cK = 0;
                                        Console.Write("Каркас: ");
                                        foreach (var it in intPath)
                                        {
                                            Console.Write("{0} ", it);
                                            cK += it;
                                        }
                                        Console.WriteLine();
                                        Console.WriteLine(cK);
                                        Graph kr = new Graph("C:/Users/User/source/repos/Graphs/Graphs/karkas.txt");
                                        kr.PrintToConsole();
                                    }
                                    
                                }
                                catch (Exception eK)
                                {
                                    Console.WriteLine(eK.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("\tВаш граф не соответствует условиям построения каркаса\n");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                        break;
                    case ConsoleKey.B:
                        Console.WriteLine("\n\tПоиск длин кратчайших путей");
                        Console.WriteLine("\tВы точно хотите найти путь? [y/n]");
                        string aB = Console.ReadLine().ToLower();
                        if (aB == "y")
                        {
                            Console.WriteLine("\tВведите название начальной вершины\n");
                            string startVertex = Console.ReadLine();
                            Console.WriteLine("\tВведите название конечной вершины\n");
                            string v1vertex = Console.ReadLine();
                            
                            int v1Path = 0;
                            List<string> pathB = new List<string>();
                            try
                            {
                                
                                gr1.WrapFordBellman(startVertex, v1vertex, ref v1Path, ref pathB);
                                if (v1Path != int.MaxValue)
                                {
                                    Console.WriteLine("Путь от {0} до {1} равен {2}", startVertex, v1vertex, v1Path);
                                    pathB.Reverse();
                                    Console.WriteLine("Выглядит он следующим образом: ");
                                    foreach(var item in pathB)
                                    {
                                        Console.Write("{0} ", item);
                                    }
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine("Пути между вершинами {0} и {1} нет", startVertex, v1vertex);
                                }
                                
                            }
                            catch(Exception eB)
                            {
                                Console.WriteLine(eB.Message);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                    case ConsoleKey.D:
                        Console.WriteLine("\n\tПоиск вершины, сумма длин кратчайших путей от которой до остальных вершин минимальна");
                        Console.WriteLine("\tВы точно хотите найти путь? [y/n]");
                        string aD = Console.ReadLine().ToLower();
                        if (aD == "y")
                        {
                            if(gr1.is_graph_weighted)
                            {
                                string vD = "";
                                int pD = int.MaxValue;
                                gr1.WrapD(ref vD, ref pD);
                                if(pD != int.MaxValue)
                                {
                                    Console.WriteLine("Минимальный путь от себя до всех остальных вершин имеет " +
                                        "вершина {0}, сумма растояний равна {1}", vD, pD);
                                }
                                else
                                {
                                    Console.WriteLine("Все вершины графа являются изолированными");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Граф не является взвешенным");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                        break;
                    case ConsoleKey.F:
                        Console.WriteLine("\n\tПоиск кратчайших путей от вершины V до вершин U1 и U2");
                        Console.WriteLine("\tВы точно хотите найти пути? [y/n]");
                        string aF = Console.ReadLine().ToLower();
                        if (aF == "y")
                        {
                            if (gr1.is_graph_weighted)
                            {
                               
                                long pathu1 = 0;
                                long pathu2 = 0;
                                Console.WriteLine("Введите название стартовой вершины");
                                string stF = Console.ReadLine();
                                Console.WriteLine("Введите название первой конечной вершины");
                                string u1F = Console.ReadLine();
                                Console.WriteLine("Введите название второй конечной вершины");
                                string u2F = Console.ReadLine();
                                
                                try
                                {
                                    gr1.WrapF(stF, u1F, u2F, ref pathu1, ref pathu2);
                                    if (pathu1 < -1000000)
                                    {
                                        Console.WriteLine("Между вершинами {0} и {1} путь бесконечно малой длины", stF, u1F);
                                    }
                                    else if (pathu1 != int.MaxValue)
                                    {
                                        Console.WriteLine("Кратчайший путь путь от {0} до {1} равен {2}", stF, u1F, pathu1);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Между вершинами {0} и {1} нет пути", stF, u1F);
                                    }
                                    if (pathu2 < -1000000)
                                    {
                                        Console.WriteLine("Между вершинами {0} и {1} путь бесконечно малой длины", stF, u2F);
                                    }
                                    else if (pathu2 != int.MaxValue)
                                    {
                                        Console.WriteLine("Кратчайший путь путь от {0} до {1} равен {2}", stF, u2F, pathu2);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Между вершинами {0} и {1} нет пути", stF, u2F);
                                    }
                                }
                                catch(Exception eF)
                                {
                                    Console.WriteLine(eF.Message);
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Граф не является взвешенным");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                    case ConsoleKey.C:
                        Console.WriteLine("\n\tПоиск максимального потока");
                        Console.WriteLine("\tВы точно хотите найти максимальный поток? [y/n]");
                        string aC = Console.ReadLine().ToLower();
                        if (aC == "y")
                        {
                            try
                            {

                                int f = gr1.WrapFlow("0", "4");
                                Console.WriteLine(f);
                            }
                            catch (Exception eC)
                            {
                                Console.WriteLine(eC.Message);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tВозврат на панель действий\n");
                            break;
                        }
                    case ConsoleKey.Escape:
                        Console.WriteLine("\tПрограмма завершила работу. До свидания!\n");
                        break;
                    default:
                        Console.WriteLine("\tТакой команды нет\n");
                        break;
                }
            }

        }
    }
}
