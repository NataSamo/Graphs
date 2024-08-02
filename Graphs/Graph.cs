using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Graphs
{
    struct Edge
    {
        public string start;
        public string current;
        public int way;
        public Edge(string s, string c, int w)
        {
            start = s;
            current = c;
            way = w;
        }
    }
    struct Ed
    {
        public int from;
        public int to;
        public int c;
        public int f;
        public Ed(int fr, int t, int c, int f)
        {
            from = fr;
            to = t;
            this.c = c;
            this.f = f;
        }
        public int rem()
        {
            return this.c - this.f;
        }
    }
    
    class Graph
    {
        Dictionary<string, Dictionary<string, int>> G_w;
        Dictionary<string, Dictionary<string, string>> G_n_w;
        public bool direction_of_graph = false;
        public bool is_graph_weighted = false;
        


        // ПУСТОЙ КОНСТРУКТОР
        public Graph(bool type, bool weight)
        {
            direction_of_graph = type;
            is_graph_weighted = weight;
            if (is_graph_weighted)
            {
                G_w = new Dictionary<string, Dictionary<string, int>>();
            }
            else
            {
                G_n_w = new Dictionary<string, Dictionary<string, string>>();
            }
        }


        
        // ОБЫЧНЫЙ КОНСТРУКТОР
        public Graph(string path)
        {
            //string ex_Usual_constructor = "";
            using (StreamReader fileIn = new StreamReader(path))
            {

                string type = fileIn.ReadLine();
                if (type == "directed")
                {
                    direction_of_graph = true;
                }
                string weight = fileIn.ReadLine();
                if (weight == "weighted")
                {
                    is_graph_weighted = true;
                }
                char[] seps = { ' ', '\t', '\n', '\r', '\v' };
                string line = "";
                //string[] l;
                if (is_graph_weighted)
                {
                    G_w = new Dictionary<string, Dictionary<string, int>>();
                    while (!fileIn.EndOfStream)
                    {

                        string[] l;
                        line = fileIn.ReadLine();
                        l = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                        Dictionary<string, int> dict = new Dictionary<string, int>();
                        for (int i = 1; i < l.Length; i += 2)
                        {
                            dict.Add(l[i], int.Parse(l[i + 1]));
                        }
                        G_w.Add(l[0], dict);
                        line = "";

                    }
                    
                }
                else
                {
                    G_n_w = new Dictionary<string, Dictionary<string, string>>();

                    while (!fileIn.EndOfStream)
                    {

                        string[] l;
                        line = fileIn.ReadLine();
                        l = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                            
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        for (int i = 1; i < l.Length; i++)
                        {
                                dict.Add(l[i], null);

                        }
                        G_n_w.Add(l[0], dict);
                        line = "";

                    }
                    
                    
                }

            }
        }



        // КОНСТРУКТОР КОПИРОВАНИЯ
        public Graph(Graph gr)
        {
            is_graph_weighted = gr.is_graph_weighted;
            direction_of_graph = gr.direction_of_graph;
            if (is_graph_weighted)
            {
                G_w = gr.G_w;
            }
            else
            {
                G_n_w = gr.G_n_w;
            }
        }
            

        // КОНСТРУКТОР СПЕЦ ГРАФА
        public Graph(int count)
        {
            G_n_w = new Dictionary<string, Dictionary<string, string>>();
            is_graph_weighted = false;
            direction_of_graph = false;
            Dictionary<string, string> ways = new Dictionary<string, string>();
            int cur_count = 0;
            List<string> l = new List<string>();
            using (StreamReader fileIn = new StreamReader("C:/Users/User/source/repos/Graphs/Graphs/Input5.txt"))
            {
                string vertex = "";
                while(cur_count < count)
                {
                    vertex = fileIn.ReadLine();
                    G_n_w.Add(vertex, ways);
                    l.Add(vertex);
                    cur_count++;
                }
                foreach(var it in G_n_w)
                {
                    foreach(var item in l)
                    {
                        if(it.Key != item && !it.Value.ContainsKey(item))
                        {
                            it.Value.Add(item, null);
                        }
                    }
                }
                fileIn.Close();
            }
        }


        // ДОБАВЛЕНИЕ ВЕРШИНЫ ВЗВЕШЕННЫЙ ГРАФ
        public void AddVertex(string vertex, Dictionary<string, int> a)
        {
            if (this.G_w.ContainsKey(vertex))
            {
                throw new Exception("This vertex already exists");
            }
            else
            {
                // ДОБАВЛЕНИЕ НОВЫЙ ВЕРШИНЫ СПИСКИ СМЕЖНЫХ С НЕЙ
                int check = 0;
                foreach(var it in a)
                {
                    if(!this.G_w.ContainsKey(it.Key))
                    {
                        check++;
                    }
                }
                if(check == 0)
                {
                    if (this.direction_of_graph)
                    {
                        this.G_w.Add(vertex, a);
                    }
                    else
                    {
                        this.G_w.Add(vertex, a);
                        foreach (var it in a)
                        {
                            this.G_w[it.Key].Add(vertex, it.Value);
                        }
                    }
                }
                else
                {
                    throw new Exception("Some of the vertexes do not exist");
                }
            }
        }


        // ДОБАВЛЕНИЕ ВЕРШИНЫ НЕВЗВЕШЕННЫЙ ГРАФ
        public void AddVertex(string vertex, Dictionary<string, string> a)
        {
            if (this.G_n_w.ContainsKey(vertex))
            {
                throw new Exception("This vertex already exists");
            }
            else
            {
                // ДОБАВЛЕНИЕ НОВЫЙ ВЕРШИНЫ СПИСКИ СМЕЖНЫХ С НЕЙ
                int check = 0;
                foreach (var it in a)
                {
                    if (!this.G_w.ContainsKey(it.Key))
                    {
                        check++;
                    }
                }
                if(check == 0)
                {
                    if (this.direction_of_graph)
                    {
                        this.G_n_w.Add(vertex, a);
                    }
                    else
                    {
                        foreach (var it in a)
                        {
                            this.G_n_w[it.Key].Add(vertex, it.Value);
                        }
                    }
                }
                else
                {
                    throw new Exception("Some of the vertexes do not exist");
                }
                
            }
        }


        //ДОБАВЛЕНИЕ ВЕРШИНЫ С УКАЗАНИЕМ СПИСКА ТЕХ ВЕРШИН, ЧТО СВЯЗАНЫ С НОВОЙ ДЛЯ ВЗЕВЕШЕННОГО ОРИЕНТИРОВАННОГО ГРАФА
        public void AddVertex(string vertex, Dictionary<string, int> d, List<string> l)
        {
            if (this.G_w.ContainsKey(vertex))
            {
                throw new Exception("This vertex already exists");
            }
            else
            {
                // ДОБАВЛЕНИЕ НОВЫЙ ВЕРШИНЫ СПИСКИ СМЕЖНЫХ С НЕЙ
                int check = 0;
                foreach (var it in d)
                {
                    if (!this.G_w.ContainsKey(it.Key))
                    {
                        check++;
                    }
                }
                foreach (var it in l)
                {
                    if (!this.G_w.ContainsKey(it))
                    {
                        check++;
                    }
                }
                if(check == 0)
                {
                    this.G_w.Add(vertex, d);
                    foreach (var it in l)
                    {
                        this.G_w[it].Add(vertex, d[it]);
                    }
                }
                else
                {
                    throw new Exception("Some of the vertexes do not exist");
                }

            }
        }


        //ДОБАВЛЕНИЕ ВЕРШИНЫ С УКАЗАНИЕМ СПИСКА ТЕХ ВЕРШИН, ЧТО СВЯЗАНЫ С НОВОЙ ДЛЯ НЕВЗЕВЕШШНОГО ОРИЕНТИРОВАННОГО ГРАФА
        public void AddVertex(string vertex, Dictionary<string, string> d, List<string> l)
        {
            if (this.G_w.ContainsKey(vertex))
            {
                throw new Exception("This vertex already exists");
            }
            else
            {
                // ДОБАВЛЕНИЕ НОВЫЙ ВЕРШИНЫ СПИСКИ СМЕЖНЫХ С НЕЙ
                int check = 0;
                foreach (var it in d)
                {
                    if (!this.G_w.ContainsKey(it.Key))
                    {
                        check++;
                    }
                }
                foreach (var it in l)
                {
                    if (!this.G_w.ContainsKey(it))
                    {
                        check++;
                    }
                }
                if(check == 0)
                {
                    this.G_n_w.Add(vertex, d);
                    foreach (var it in l)
                    {
                        this.G_n_w[it].Add(vertex, d[it]);
                    }
                }
                else
                {
                    throw new Exception("Some of the vertexes do not exist");
                }

            }
        }


        // ДОБАВЛЕНИЕ ВЕРШИНЫ БЕЗ СПИСКА СМЕЖНОСТИ, ТО ЕСТЬ ИЗОЛИРОВАННОЙ
        public void AddVertex(string vertex)
        {
            if(!this.is_graph_weighted)
            {
                if(this.G_n_w.ContainsKey(vertex))
                {
                    throw new Exception("Эта вершина уже существует");
                }
                else
                {
                    Dictionary<string, string> d = new Dictionary<string, string>();
                    this.G_n_w.Add(vertex, d);
                }
                    
            }
            else
            {
                if (this.G_w.ContainsKey(vertex))
                {
                    throw new Exception("Эта вершина уже существует");
                }
                else
                {
                    Dictionary<string, int> d = new Dictionary<string, int>();
                    this.G_w.Add(vertex, d);
                }
            }
            
        }


        //УДАЛЕНИЕ ВЕРШИНЫ
        public void DeleteVertex(string vertex)
        {
            if (this.is_graph_weighted)
            {
                if (!this.G_w.ContainsKey(vertex))
                {
                    throw new Exception("Такой вершины в графе нет");
                }
                else
                {
                    this.G_w.Remove(vertex);
                       foreach (var it in this.G_w)
                    {
                        if(this.G_w[it.Key].ContainsKey(vertex))
                        {
                            this.G_w[it.Key].Remove(vertex);
                        }
                            //this.G_w[it.Key].Remove(vertex);
                    }
                }
                    
            }
            
            else
            {
                if (!this.G_n_w.ContainsKey(vertex))
                {
                    throw new Exception("такой вершины в графе нет");
                }
                else
                {
                    this.G_n_w.Remove(vertex);
                    foreach (var it in this.G_n_w)
                    {
                        if(this.G_n_w[it.Key].ContainsKey(vertex))
                            this.G_n_w[it.Key].Remove(vertex);
                    }
                }
                   
                
            }
        }


        // ДОБАВЛЕНИЕ РЕБРА В НЕВЗВЕШЕННЫЙ ГРАФ
        public void AddEdge(string vertex1, string vertex2)
        {
            if((this.G_n_w.ContainsKey(vertex1) && this.G_n_w.ContainsKey(vertex2)))
            {
                if(this.direction_of_graph)
                {

                    if (!this.G_n_w[vertex1].ContainsKey(vertex2))
                    {
                        this.G_n_w[vertex1].Add(vertex2, null);
                    }
                    else
                    {
                        throw new Exception("Такое ребро уже есть в графе");
                    }
                    
                }
                else
                {
                    if(!(this.G_n_w[vertex1].ContainsKey(vertex2) && this.G_n_w[vertex2].ContainsKey(vertex1)))
                    {
                        this.G_n_w[vertex1].Add(vertex2, null);
                        this.G_n_w[vertex2].Add(vertex1, null);
                    }
                    else
                    {
                        throw new Exception("Это ребро уже есть в графе");
                    }
                }
            }
            else
            {
                throw new Exception("Таких вершин в графе нет");
            }
        }


        //ДОБАВЛЕНИЕ РЕБРА ВО ВЗВЕШЕННЫЙ ГРАФ
        public void AddEdge(string vertex1, string vertex2, int edge)
        {
            if (this.G_w.ContainsKey(vertex1) && this.G_w.ContainsKey(vertex2))
            {
                if (this.direction_of_graph)
                {
                    if(this.G_w[vertex1].ContainsKey(vertex2))
                    {
                        throw new Exception("Это ребро уже есть в графе");
                    }
                    else
                    {
                        this.G_w[vertex1].Add(vertex2, edge);
                    }
                    
                }
                else
                {
                    if(this.G_w[vertex1].ContainsKey(vertex2))
                    {
                        throw new Exception("Это ребро уже есть в графе");
                    }
                    else
                    {
                        this.G_w[vertex1].Add(vertex2, edge);
                        this.G_w[vertex2].Add(vertex1, edge);
                    }
                    
                }
            }
            else
            {
                throw new Exception("Таких вершин в графе нет");
            }
        }


        //УДАЛЕНИЕ РЕБРА 
        public void DeleteEdge(string vertex1, string vertex2)
        {
            if (this.is_graph_weighted)
            {
                if(this.G_w.ContainsKey(vertex1) && this.G_w.ContainsKey(vertex2))
                {
                    if (this.direction_of_graph)
                    {
                        if (this.G_w[vertex1].ContainsKey(vertex2))
                            this.G_w[vertex1].Remove(vertex2);
                        else
                        {
                            throw new Exception("Такого ребра нет в графе");
                        }
                    }
                    else
                    {

                        if (this.G_w[vertex1].ContainsKey(vertex2) && this.G_w[vertex2].ContainsKey(vertex1))
                        {
                            this.G_w[vertex1].Remove(vertex2);
                            this.G_w[vertex2].Remove(vertex1);
                        }
                        else
                        {
                            throw new Exception("Такого ребра нет в графе");
                        }
                    }
                }
                else
                {
                    throw new Exception("Таких вершин нет в графе");
                }
                
            }
            else
            {
                if(this.G_n_w.ContainsKey(vertex1) && this.G_n_w.ContainsKey(vertex2))
                {
                    if (this.direction_of_graph)
                    {
                        if (this.G_n_w[vertex1].ContainsKey(vertex2))
                            this.G_n_w[vertex1].Remove(vertex2);
                        else
                        {
                            throw new Exception("Такого ребра нет в графе");
                        }
                    }
                    else
                    {

                        if (this.G_n_w[vertex1].ContainsKey(vertex2) && this.G_n_w[vertex2].ContainsKey(vertex1))
                        {
                            this.G_n_w[vertex1].Remove(vertex2);
                            this.G_n_w[vertex2].Remove(vertex1);
                        }
                        else
                        {
                            throw new Exception("Такого ребра нет в графе");
                        }
                    }
                }
                else
                {
                    throw new Exception("Таких вершин нет");
                }
                
            }
        }
            
    


        //ВЫВОД СПИСКА СМЕЖНОСТИ В ФАЙЛ
        public void PrintToFile(string path)
        {
            using (StreamWriter fileOut = new StreamWriter(path))
            {
                string direction = "";
                string weight = "";
                if (this.direction_of_graph)
                {
                    direction = "directed";
                }
                else
                {
                    direction = "undirected";
                }
                if (this.is_graph_weighted)
                {
                    weight = "weighted";
                }
                else
                {
                    weight = "unweighted";
                }
                fileOut.WriteLine($"{direction}");
                fileOut.WriteLine($"{weight}");
                if (this.is_graph_weighted)
                {
                    Dictionary<string, int> d = new Dictionary<string, int>();
                    foreach (var it in this.G_w)
                    {
                        d = it.Value;
                        fileOut.Write($"{it.Key} ");
                        foreach (var item in d)
                        {
                            fileOut.Write($"{item.Key} {item.Value} ");
                        }
                        fileOut.WriteLine();
                    }
                }
                else
                {
                    Dictionary<string, string> d = new Dictionary<string, string>();
                    foreach (var it in this.G_n_w)
                    {
                        d = it.Value;
                        fileOut.Write(it.Key);
                        foreach (var item in it.Value)
                        {
                            fileOut.Write(" {0}", item.Key);
                        }
                        fileOut.WriteLine();
                    }
                }
                fileOut.Close();
            }
            
        }


        //PАБОТА С ПОЛУСТЕПЕНЯМИ ГРАФА
        public List<string>HalfDegree(string vertex)
        {
            

            Dictionary<string, int> HD = new Dictionary<string, int>();
            List<string> l = new List<string>();
            int count = 0;
            if (this.is_graph_weighted)
            {

                if(this.G_w.ContainsKey(vertex))
                {
                    foreach (var item in this.G_w)
                    {

                        HD.Add(item.Key, item.Value.Count());
                    }
                    count = HD[vertex];
                    foreach (var item in HD)
                    {
                        if (item.Value > count)
                        {
                            l.Add(item.Key);
                        }
                    }
                    return l;
                }
                else
                {
                    throw new Exception("Такой вершины в графе нет");
                }
            }
            else
            {
                if(this.G_n_w.ContainsKey(vertex))
                {
                    foreach (var item in this.G_n_w)
                    {

                        HD.Add(item.Key, item.Value.Count());
                    }
                    count = HD[vertex];
                    foreach (var item in HD)
                    {
                        if (item.Value >= count)
                        {
                            l.Add(item.Key);
                        }
                    }
                    return l;

                }
                else
                {
                    throw new Exception("Такой вершины в графе нет");
                }
            }
            
        }


        // ВЫВОД ГРАФА НА ЭКРАН
        public void PrintToConsole()
        {
            if(this.direction_of_graph)
            {
                Console.WriteLine("directed");
            }
            else
            {
                Console.WriteLine("undirected");
            }
            if(this.is_graph_weighted)
            {
                Console.WriteLine("weighted");
                foreach(var it in this.G_w)
                {
                    Console.Write("{0} ", it.Key);
                    foreach(var item in it.Value)
                    {
                        Console.Write("{0} {1} ", item.Key, item.Value);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("unweighted");
                foreach(var it in this.G_n_w)
                {
                    Console.Write("{0} ", it.Key);
                    foreach(var item in it.Value)
                    {
                        Console.Write("{0} ", item.Key);
                    }
                    Console.WriteLine();
                }
            }

        }


        //ПОИСК ВЕРШИНЫ, ДОСТИЖИМОЙ ИЗ ВЕРШИН U И V
        public List<string> FindVertex(string u, string v)
        {
            if(this.is_graph_weighted)
            {
                if(this.G_w.ContainsKey(u) && this.G_w.ContainsKey(v))
                {
                    List<string> foundVertexes = new List<string>();
                    foreach(var it in this.G_w[u])
                    {
                        if(this.G_w[v].ContainsKey(it.Key))
                        {
                            foundVertexes.Add(it.Key);
                        }
                    }
                    return foundVertexes;
                }
                else
                {
                    throw new Exception("Таких вершин в графе нет");
                }
            }
            else
            {
                if (this.G_n_w.ContainsKey(u) && this.G_n_w.ContainsKey(v))
                {
                    List<string> foundVertexes = new List<string>();
                    foreach (var it in this.G_n_w[u])
                    {
                        if (this.G_n_w[v].ContainsKey(it.Key))
                        {
                            foundVertexes.Add(it.Key);
                        }
                    }
                    return foundVertexes;
                }
                else
                {
                    throw new Exception("Таких вершин в графе нет");
                }
            }
        }


        //ПОСТРОЕНИЕ ДОПОЛНЕНИЯ К НЕВЗВЕШЕННОМУ ГРАФУ
        public void AdditionToGraph()
        {
            Dictionary<string, Dictionary<string, string>> Addition = new Dictionary<string, Dictionary<string, string>>();
            List<string> vertexes = new List<string>();
            
            foreach(var ver in this.G_n_w.Keys)
            {
                vertexes.Add(ver);
            }
            foreach(var item in this.G_n_w)
            {
                Dictionary<string, string> d = new Dictionary<string, string>();
                Addition.Add(item.Key, d);
                foreach(var ver in vertexes)
                {
                    if((item.Key != ver) && (!item.Value.ContainsKey(ver)))
                    {
                        Addition[item.Key].Add(ver, null);
                    }
                }
            }
            using (StreamWriter fileOut = new StreamWriter("C:/Users/User/source/repos/Graphs/Graphs/Addition.txt"))
            {

                if (this.direction_of_graph)
                {
                    fileOut.WriteLine("directed");
                }
                else
                {
                    fileOut.WriteLine("undirected");
                }
                if (this.is_graph_weighted)
                {
                    fileOut.WriteLine("weighted");
                }
                else
                {
                    fileOut.WriteLine("unweigted");
                }

                Dictionary<string, string> d = new Dictionary<string, string>();
                foreach (var it in Addition)
                {
                    d = it.Value;
                    fileOut.Write($"{it.Key} ");
                    foreach (var item in d)
                    {
                        fileOut.Write($"{item.Key} ");
                    }
                    fileOut.WriteLine();
                }
            }
        }


        //ДОПОЛНЕНИЕ КО ВЗВЕШЕННОМУ ГРАФУ
        public void AdditionToGraphWeighted()
        {
            Dictionary<string, Dictionary<string, int>> Addition = new Dictionary<string, Dictionary<string, int>>();
            List<string> vertexes = new List<string>();
            foreach(var item in this.G_w.Keys)
            {
                vertexes.Add(item);
            }
            foreach(var item in this.G_w)
            {
                Dictionary<string, int> d = new Dictionary<string, int>();
                Addition.Add(item.Key, d);
                foreach(var ver in vertexes)
                {
                    if((ver != item.Key) && (!item.Value.ContainsKey(ver)))
                    {
                        if(this.G_w[ver].ContainsKey(item.Key))
                        {
                            Dictionary<string, int> d1 = new Dictionary<string, int>();
                            d1 = this.G_w[ver];
                            Addition[item.Key].Add(ver, d1[item.Key]);
                        }
                        else
                        {
                            Addition[item.Key].Add(ver, 0);
                        }
                        
                    }
                }
            }
            using (StreamWriter fileOut = new StreamWriter("C:/Users/User/source/repos/Graphs/Graphs/Addition.txt"))
            {

                if (this.direction_of_graph)
                {
                    fileOut.WriteLine("directed");
                }
                else
                {
                    fileOut.WriteLine("undirected");
                }
                if (this.is_graph_weighted)
                {
                    fileOut.WriteLine("weighted");
                }
                else
                {
                    fileOut.WriteLine("unweigted");
                }

                Dictionary<string, int> d = new Dictionary<string, int>();
                foreach (var it in Addition)
                {
                    d = it.Value;
                    fileOut.Write($"{it.Key} ");
                    foreach (var item in d)
                    {
                        fileOut.Write($"{item.Key} {item.Value} ");
                    }
                    fileOut.WriteLine();
                }
            }
        }


        //ОБХОД В ГЛУБИНУ
        //public List<string> DFSWeighted(string vertex, List<string> passedVertexes)
        //{
        //    Stack<string> stack = new Stack<string>();
        //    List<string> path = new List<string>();
        //    stack.Push(vertex);
        //    while(stack.Count() != 0)
        //    {
        //        vertex = stack.Peek();
        //        if(!passedVertexes.Contains(vertex))
        //        {
        //            path.Add(vertex);
        //        }
        //        bool hasChildren = false;
        //        foreach(var edge in G_w[vertex])
        //        {
        //            if(!passedVertexes.Contains(edge.Key))
        //            {
        //                stack.Push(edge.Key);
        //                hasChildren = true;
        //                break;
        //            }
        //        }
        //        if(hasChildren)
        //        {
        //            stack.Pop();
        //        }
        //    }
        //    return path;
        //}


        //ОБХОД В ГЛУБИНУ ДЛЯ НЕВЗВЕШЕННОГО ГРАФА
        //public List<string> DFSNoteighted(string vertex, List<string> passedVertexes)
        //{
        //    Stack<string> stack = new Stack<string>();
        //    List<string> path = new List<string>();
        //    stack.Push(vertex);
        //    while (stack.Count() != 0)
        //    {
        //        vertex = stack.Peek();
        //        if (!passedVertexes.Contains(vertex))
        //        {
        //            path.Add(vertex);
        //        }
        //        bool hasChildren = false;
        //        foreach (var edge in G_n_w[vertex])
        //        {
        //            if (!passedVertexes.Contains(edge.Key))
        //            {
        //                stack.Push(edge.Key);
        //                hasChildren = true;
        //                break;
        //            }
        //        }
        //        if (hasChildren)
        //        {
        //            stack.Pop();
        //        }
        //    }
        //    return path;
        //}


        //ОБОЛОЧКА ДЛЯ ОБХОДА В ГЛУБИНУ
        //public void DSFWrap(List<string> passedVertexes)
        //{
        //    List<string> path = new List<string>();
        //    if(this.is_graph_weighted)
        //    {
        //        foreach(var ver in G_w.Keys)
        //        {
        //            if(!passedVertexes.Contains(ver))
        //            {
        //                path = 
        //            }
        //        }
        //    }
        //}



        //public List<int> FindPathWithAvoidance(int u, int v, HashSet<int> avoid)
        //{
        //    bool[] visited = new bool[];
        //    int[] parent = new int[];

        //    Queue<int> queue = new Queue<int>();
        //    visited[u] = true;
        //    queue.Enqueue(u);

        //    while (queue.Count != 0)
        //    {
        //        int current = queue.Dequeue();

        //        if (current == v)
        //        {
        //            break;
        //        }

        //        foreach (int neighbor in adj[current])
        //        {
        //            if (!visited[neighbor] && !avoid.Contains(neighbor))
        //            {
        //                visited[neighbor] = true;
        //                parent[neighbor] = current;
        //                queue.Enqueue(neighbor);
        //            }
        //        }
        //    }

        //    if (!visited[v])
        //    {
        //        return new List<int>();
        //    }

        //    List<int> path = new List<int>();
        //    int vertex = v;
        //    while (vertex != u)
        //    {
        //        path.Add(vertex);
        //        vertex = parent[vertex];
        //    }
        //    path.Add(u);
        //    path.Reverse();

        //    return path;
        //}


        public Dictionary<string, List<string>> PathesToAllVertexes(string vertex)
        {
            
            Dictionary<string, List<string>> pathes = new Dictionary<string, List<string>>();
            
            List<string> passed = new List<string>();
            Queue<string> q = new Queue<string>();
            if(this.is_graph_weighted)
            {

                if (this.G_w.ContainsKey(vertex))
                {
                    foreach(var item in this.G_w.Keys)
                    {
                        List<string> l1 = new List<string>();
                        pathes.Add(item, l1);
                        Dictionary<int, List<string>> layers = this.Layers(item);
                        int layer = 0;
                        foreach(var lay in layers)
                        {
                            if(lay.Value.Contains(vertex))
                            {
                                layer = lay.Key;
                                break;
                            }
                        }
                        if(layer == 0)
                        {
                            pathes[item] = null;
                        }
                        else
                        {
                            pathes[item].Add(vertex);
                            string vertex1 = vertex;
                            for(int i = layer - 1; i > 0; i--)
                            {
                                foreach(var k in layers[i])
                                {
                                    if(this.G_w[k].ContainsKey(vertex1))
                                    {
                                        pathes[item].Insert(0, vertex1);
                                        vertex1 = k;
                                    }
                                }
                            }
                        }
                    }
                    
                }
                else
                {
                    throw new Exception("Такой вершины в графе нет");
                }
            }
            else
            {
                if (this.G_n_w.ContainsKey(vertex))
                {
                    foreach (var item in this.G_n_w.Keys)
                    {
                        List<string> l1 = new List<string>();
                        pathes.Add(item, l1);
                        Dictionary<int, List<string>> layers = this.Layers(item);
                        int layer = 0;
                        foreach (var lay in layers)
                        {
                            if (lay.Value.Contains(vertex))
                            {
                                layer = lay.Key;
                                break;
                            }
                        }
                        if (layer == 0)
                        {
                            pathes[item] = null;
                        }
                        else
                        {
                            pathes[item].Add(vertex);
                            string vertex1 = vertex;
                            for (int i = layer - 1; i > 0; i--)
                            {
                                foreach (var k in layers[i])
                                {
                                    if (this.G_n_w[k].ContainsKey(vertex1))
                                    {
                                        vertex1 = k;
                                        pathes[item].Insert(0, vertex1);
                                        break;
                                        //vertex1 = k;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Такой вершины в графе нет");
                }
            }

            return pathes;

        }


        //СЛОИ ГРАФА
        public Dictionary<int, List<string>> Layers(string vertex)
        {
            Dictionary<int, List<string>> d = new Dictionary<int, List<string>>();
            Queue<string> q = new Queue<string>();
            HashSet<string> passed = new HashSet<string>();
            if(!this.is_graph_weighted)
            {
                List<string> l = new List<string>();
                d.Add(1, l);
                passed.Add(vertex);
                foreach(var item in this.G_n_w[vertex].Keys)
                {
                    d[1].Add(item);
                    q.Enqueue(item);
                    passed.Add(item);
                }
                int i = 2;
                while(q.Count() != 0)
                {
                    List<string> l1 = new List<string>();
                    d.Add(i, l1);
                    foreach (var it in d[i - 1])
                    {
                        foreach(var item in this.G_n_w[it].Keys)
                        {
                            if(!passed.Contains(item))
                            {
                                d[i].Add(item);
                                q.Enqueue(item);
                                passed.Add(item);
                            }
                            
                        }
                        q.Dequeue();
                        
                    }
                    i++;
                }
            }
            else
            {
                List<string> l = new List<string>();
                d.Add(1, l);
                passed.Add(vertex);
                foreach (var item in this.G_w[vertex].Keys)
                {
                    d[1].Add(item);
                    q.Enqueue(item);
                    passed.Add(item);
                }
                int i = 2;
                while (q.Count() != 0)
                {
                    List<string> l1 = new List<string>();
                    d.Add(i, l1);
                    foreach (var it in d[i - 1])
                    {
                        foreach (var item in this.G_w[it].Keys)
                        {
                            if (!passed.Contains(item))
                            {
                                d[i].Add(item);
                                q.Enqueue(item);
                                passed.Add(item);
                            }

                        }
                        q.Dequeue();

                    }
                    i++;
                }
            }
            return d;
        }


        //ПУТИ С ПРОПУСКОМ ВЕРШИН
        public List<string> PathWithAvoidence(string startVertex, string endVertex, HashSet<string> avoid)
        {
            
            List<string> path = new List<string>();
            if (this.is_graph_weighted)
            {
                if (this.G_w.ContainsKey(startVertex) && this.G_w.ContainsKey(endVertex))
                {
                    Stack<string> stack = new Stack<string>();
                    List<string> l = new List<string>();
                    stack.Push(startVertex);
                    int count = 0;
                    while (stack.Count() != 0)
                    {
                        string current = stack.Pop();
                        avoid.Add(current);
                        path.Add(current);
                        if (current == endVertex)
                        {
                            break;
                        }
                        else
                        {
                            count = 0;
                            foreach (var n in this.G_w[current])
                            {

                                if (!avoid.Contains(n.Key))
                                {
                                    count++;
                                    stack.Push(n.Key);
                                }
                            }
                            if (count == 0)
                            {
                                path.Remove(current);
                            }
                        }
                    }
                    if (!path.Contains(endVertex))
                    {
                        path.Clear();
                    }
                }

                else
                {
                    throw new Exception("Таких вершин в графе нет");
                }

            }
            else
            {
                if (this.G_n_w.ContainsKey(startVertex) && this.G_n_w.ContainsKey(endVertex))
                {
                    
                    Stack<string> stack = new Stack<string>();
                    List<string> l = new List<string>();
                    stack.Push(startVertex);
                    int count = 0;
                    while (stack.Count() != 0)
                    {
                        string current = stack.Pop();
                        avoid.Add(current);
                        path.Add(current);
                        if (current == endVertex)
                        {
                            break;
                        }
                        else
                        {
                            count = 0;
                            foreach (var n in this.G_n_w[current])
                            {
                                
                                if (!avoid.Contains(n.Key))
                                {
                                    count++;
                                    stack.Push(n.Key);
                                }
                            }
                            if(count == 0)
                            {
                                path.Remove(current);
                            }
                        }
                        
                        
                    }
                    if (!path.Contains(endVertex))
                    {
                        path.Clear();
                    }
                }

                else
                {
                    throw new Exception("Таких вершин в графе нет");
                } 
            }
            return path;
        }

        //АЛГОРИТМ ПРИМА
        public string[] AlgPrim(string start, ref List<int> intPath)
        {
            if(this.G_w.ContainsKey(start))
            {
                string[] karkas = new string[this.G_w.Keys.Count()];
                int m = this.CountEdges();
                Edge[] listofEdges = new Edge[m / 2];
                int minimum = int.MaxValue;
                string s = "";
                string c = "";

                int k = 0;
                foreach (var item in this.G_w)
                {
                    s = item.Key;

                    foreach (var it in item.Value)
                    {

                        c = it.Key;
                        Edge ed = new Edge(s, c, it.Value);
                        Edge ed1 = new Edge(it.Key, item.Key, it.Value);
                        if (!listofEdges.Contains(ed1))
                        {
                            listofEdges[k] = ed;
                            k++;
                        }
                    }
                    if (item.Value.Count() == 0)
                    {
                        throw new Exception("В графе есть изолированные вершины");
                    }
                }
                Dictionary<string, Dictionary<string, int>> prim_MST = new Dictionary<string, Dictionary<string, int>>();
                int i = 0;
                Dictionary<string, int> d = new Dictionary<string, int>();
                karkas[i] = start;
                i++;
                prim_MST.Add(start, d);

                while (karkas.Count() == this.G_w.Keys.Count() && karkas.Contains(null))
                {
                    minimum = this.FindMin(karkas, listofEdges, ref c, ref s);
                    if (minimum != int.MaxValue)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            Edge e = listofEdges[j];
                            if (e.start == s && e.current == c)
                            {
                                karkas[i] = c;
                                Dictionary<string, int> u = new Dictionary<string, int>();
                                prim_MST[s].Add(c, minimum);
                                if (!prim_MST.ContainsKey(c))
                                {
                                    prim_MST.Add(c, u);
                                    prim_MST[c].Add(s, minimum);
                                }
                                else
                                    prim_MST[c].Add(s, minimum);
                                i++;
                                intPath.Add(e.way);
                                e.way = int.MaxValue;
                                listofEdges[j] = e;
                                for (int h = 0; h < listofEdges.Length; h++)
                                {
                                    e = listofEdges[h];
                                    for (int g = 0; g < karkas.Length; g++)
                                    {
                                        if (e.start == karkas[g] && e.current == c)
                                        {
                                            e.way = int.MaxValue;
                                            listofEdges[h] = e;
                                        }
                                        else if (e.start == c && e.current == karkas[g])
                                        {
                                            e.way = int.MaxValue;
                                            listofEdges[h] = e;
                                        }
                                        else if (e.start == s && e.current == karkas[g])
                                        {
                                            e.way = int.MaxValue;
                                            listofEdges[h] = e;
                                        }
                                        else if (e.start == karkas[g] && e.current == s)
                                        {
                                            e.way = int.MaxValue;
                                            listofEdges[h] = e;
                                        }
                                    }

                                }
                                break;
                            }
                            else if (e.start == c && e.current == s)
                            {
                                string f = c;
                                c = s;
                                s = f;
                                karkas[i] = s;
                                Dictionary<string, int> u = new Dictionary<string, int>();
                                if (prim_MST.ContainsKey(s))
                                    prim_MST[s].Add(c, minimum);
                                else
                                {
                                    prim_MST.Add(s, u);
                                    prim_MST[s].Add(c, minimum);
                                }
                                if (!prim_MST.ContainsKey(c))
                                {
                                    prim_MST.Add(c, u);
                                    prim_MST[c].Add(s, minimum);
                                }
                                else
                                    prim_MST[c].Add(s, minimum);
                                i++;
                                intPath.Add(e.way);
                                e.way = int.MaxValue;
                                listofEdges[j] = e;
                                for (int h = 0; h < listofEdges.Length; h++)
                                {
                                    e = listofEdges[h];

                                    for (int g = 0; g < karkas.Length; g++)
                                    {

                                        if (e.start == karkas[g] && e.current == c)
                                        {
                                            e.way = int.MaxValue;
                                            listofEdges[h] = e;
                                        }
                                        else if (e.start == c && e.current == karkas[g])
                                        {
                                            e.way = int.MaxValue;
                                            listofEdges[h] = e;
                                        }
                                        else if (e.start == s && e.current == karkas[g])
                                        {
                                            e.way = int.MaxValue;
                                            listofEdges[h] = e;
                                        }
                                        else if (e.start == karkas[g] && e.current == s)
                                        {
                                            e.way = int.MaxValue;
                                            listofEdges[h] = e;
                                        }
                                    }

                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Я НЕ ЗАСЛУЖИВАЮ ТАКИХ УСЛОВИЙ РАБОТЫ. Я УВОЛЬНЯЮСЬ");
                    }

                }
                using (StreamWriter fileOut = new StreamWriter("C:/Users/User/source/repos/Graphs/Graphs/karkas.txt"))
                {
                    string direction = "undirected";
                    string weight = "weighted";
                    fileOut.WriteLine($"{direction}");
                    fileOut.WriteLine($"{weight}");

                    foreach (var it in prim_MST)
                    {

                        fileOut.Write($"{it.Key} ");
                        foreach (var item in it.Value)
                        {
                            fileOut.Write($"{item.Key} {item.Value} ");
                        }
                        fileOut.WriteLine();
                    }

                    fileOut.Close();
                }
                return karkas;
            }
            else
            {
                throw new Exception("Такой вершины в графе нет");
            }
            //return karkas;
        }


        //ПОИСК МИНИМУМА
        public int FindMin(string[] karkas, Edge[] listOfEdges, ref string current, ref string start)
        {
            int minimum = int.MaxValue;
            foreach (var it in karkas)
            {
                if(it != null)
                {
                    foreach (var item in listOfEdges)
                    {

                        if (item.way < minimum)
                        {
                            //l = item.Key;
                            if (item.start == it)
                            {
                                minimum = item.way;

                                current = item.current;
                                start = it;
                            }
                            else if (item.current == it)
                            {
                                minimum = item.way;

                                current = item.start;
                                start = it;
                            }
                        }
                    }
                }
                
            }
            return minimum;
        }


        //ОБОЛОЧКА ДЛЯ АЛГОРИТМА ФОРДА-БЕЛЛМАНА
        public void WrapFordBellman(string startVertex, string v1vertex, ref int v1Path, ref List<string> pathB)
        {
            if(this.G_w.ContainsKey(startVertex) && this.G_w.ContainsKey(v1vertex))
            {
                //int haveCycle = 0;
                Dictionary<string, string> parent = new Dictionary<string, string>();
                foreach (var item in this.G_w.Keys)
                {
                    parent.Add(item, null);
                }
                Dictionary<string, int> chckedListValue = FordBellman(startVertex, ref parent);
                v1Path = chckedListValue[v1vertex];
                if(v1Path != int.MaxValue)
                {
                    string cur = v1vertex;
                    pathB.Add(cur);
                    while(cur != startVertex)
                    {
                        cur = parent[cur];
                        pathB.Add(cur);
                    }
                }
            }
            else
            {
                throw new Exception("С вершинами возникла некоторая проблема, попробуйте еще раз");
            }
        }


        // РЕАЛИЗАЦИЯ АЛГОРИТМА ФОРДА-БЕЛЛМАНА
        public Dictionary<string, int> FordBellman(string startVertex, ref Dictionary<string, string> parent)
        {
            Dictionary<string, int> checkedListValue = this.ClearCheckedListValForFordBellman();
            //Dictionary<string, string> parent = new Dictionary<string, string>();
            //foreach(var item in this.G_w.Keys)
            //{
            //    parent.Add(item, null);
            //}
            checkedListValue[startVertex] = 0;
            bool flag = true;
            while(flag)
            {
                flag = false;
                foreach (var item in this.G_w)
                {
                    foreach (var it in item.Value)
                    {
                        if (checkedListValue[it.Key] > it.Value + checkedListValue[item.Key] && checkedListValue[item.Key] != int.MaxValue)
                        {
                            checkedListValue[it.Key] = it.Value + checkedListValue[item.Key];
                            parent[it.Key] = item.Key;
                            flag = true;
                        }
                    }
                }
            }
            
            
            return checkedListValue;
        }


        //ЗАПОЛНЕНИЕ СПИСКА ПОСЕЩЕННОСТИ
        public Dictionary<string, int> ClearCheckedListValForFordBellman()
        {
            Dictionary<string, int> checkedListValue = new Dictionary<string, int>();
            foreach (var vertex in this.G_w.Keys)
            {
                checkedListValue.Add(vertex, int.MaxValue);
            }
            return checkedListValue;
        }


        //ЗАДАЧА 6
        public Dictionary<string, List<string>> BFS(string vertex)
        {
            Queue<string> q = new Queue<string>();
            //List<string> path = new List<string>();
            //string[] path = new string[this.G_w.Keys.Count()];
            Dictionary<string, string> parent = new Dictionary<string, string>();
            Dictionary<string, List<string>> pathes = new Dictionary<string, List<string>>();
            if (this.is_graph_weighted)
            {
                
                string[] path = new string[this.G_w.Keys.Count()];
                if (this.G_w.ContainsKey(vertex))
                {
                    foreach(var item in this.G_w.Keys)
                    {
                        parent.Add(item, null);
                    }
                    q.Enqueue(vertex);
                    string current = "";
                    HashSet<string> passed = new HashSet<string>();
                    int i = 0;
                    while(q.Count() != 0)
                    {
                        current = q.Dequeue();
                        passed.Add(current);
                        path[i] = current;
                        i++;
                        foreach(var item in this.G_w[current].Keys)
                        {
                            if(!passed.Contains(item))
                            {
                                q.Enqueue(item);
                                parent[item] = current;
                            }
                        }
                    }
                    //Dictionary<string, List<string>> pathes = new Dictionary<string, List<string>>();
                    for(int j = path.Length - 1; j >= 0; j--)
                    {
                        current = path[j];
                        List<string> l = new List<string>();
                        pathes.Add(current, l);
                        while (current != vertex)
                        {
                            pathes[path[j]].Add(parent[current]);
                            current = parent[current];

                        }
                    }
                    foreach (var ver in this.G_n_w)
                    {
                        if (!pathes.ContainsKey(ver.Key))
                        {
                            pathes.Add(ver.Key, null);
                        }
                    }

                }
                else
                {
                    throw new Exception("Такой вершины в графе нет");
                }
            }
            else
            {
                string[] path = new string[this.G_n_w.Keys.Count()];
                if (this.G_n_w.ContainsKey(vertex))
                {
                    foreach (var item in this.G_n_w.Keys)
                    {
                        parent.Add(item, null);
                    }
                    q.Enqueue(vertex);
                    string current = "";
                    HashSet<string> passed = new HashSet<string>();
                    int i = 0;
                    while (q.Count() != 0)
                    {
                        current = q.Dequeue();
                        if(!passed.Contains(current))
                        {
                            passed.Add(current);
                            path[i] = current;
                            i++;
                            foreach (var item in this.G_n_w[current].Keys)
                            {
                                if (!passed.Contains(item))
                                {
                                    q.Enqueue(item);
                                    parent[item] = current;
                                }
                            }
                        }
                        
                        
                        
                    }

                    //Dictionary<string, List<string>> pathes = new Dictionary<string, List<string>>();
                    for (int j = path.Length - 1; j >= 0; j--)
                    {
                        current = path[j];
                        List<string> l = new List<string>();
                        if(current != null)
                        {
                            pathes.Add(current, l);
                            while (current != vertex)
                            {
                                pathes[path[j]].Add(parent[current]);
                                current = parent[current];

                            }
                        }
                        
                    }
                    foreach(var ver in this.G_n_w)
                    {
                        if(!pathes.ContainsKey(ver.Key))
                        {
                            pathes.Add(ver.Key, null);
                        }
                    }

                }
                else
                {
                    throw new Exception("Такой вершины в графе нет");
                }
            }
            return pathes;
        }


        //РАЗВОРОТ ГРАФА
        public void ReverseGr(string v)
        {
            
            if(this.is_graph_weighted)
            {
                bool isolated = true;
                foreach(var item in this.G_w)
                {
                    if(item.Value.ContainsKey(v))
                    {
                        isolated = false;
                    }
                }
                if(isolated)
                {
                    throw new Exception("Путей от нее до стальных вершин нет");
                }
                else
                {
                    Dictionary<string, Dictionary<string, string>> rev = new Dictionary<string, Dictionary<string, string>>();
                    foreach(var item in this.G_w)
                    {
                        Dictionary<string, string> d = new Dictionary<string, string>();
                        rev.Add(item.Key, d);
                    }
                    foreach(var item in this.G_w)
                    {
                        foreach(var it in item.Value)
                        {
                            rev[it.Key].Add(item.Key, null);
                        }
                    }
                    using (StreamWriter fileOut = new StreamWriter("C:/Users/User/source/repos/Graphs/Graphs/reverse.txt"))
                    {
                        string direction = "directed";
                        string weight = "notweighted";
                        
                        fileOut.WriteLine($"{direction}");
                        fileOut.WriteLine($"{weight}");
                        foreach (var it in rev)
                        {

                            fileOut.Write($"{it.Key} ");
                            foreach (var item in it.Value)
                            {
                                fileOut.Write($"{item.Key} ");
                            }
                            fileOut.WriteLine();
                        }

                        fileOut.Close();
                    }
                    
                }
            }
            else
            {
                bool isolated = true;
                foreach (var item in this.G_n_w)
                {
                    if (item.Value.ContainsKey(v))
                    {
                        isolated = false;
                    }
                }
                if (isolated)
                {
                    throw new Exception("Путей от нее до стальных вершин нет");
                }
                else
                {
                    Dictionary<string, Dictionary<string, string>> rev = new Dictionary<string, Dictionary<string, string>>();
                    foreach (var item in this.G_n_w)
                    {
                        Dictionary<string, string> d = new Dictionary<string, string>();
                        rev.Add(item.Key, d);
                    }
                    foreach (var item in this.G_n_w)
                    {
                        foreach (var it in item.Value)
                        {
                            rev[it.Key].Add(item.Key, null);
                        }
                    }
                    using (StreamWriter fileOut = new StreamWriter("C:/Users/User/source/repos/Graphs/Graphs/reverse.txt"))
                    {
                        string direction = "directed";
                        string weight = "notweighted";
                        
                        fileOut.WriteLine($"{direction}");
                        fileOut.WriteLine($"{weight}");
                        foreach (var it in rev)
                        {

                            fileOut.Write($"{it.Key} ");
                            foreach (var item in it.Value)
                            {
                                fileOut.Write($"{item.Key} ");
                            }
                            fileOut.WriteLine();
                        }

                        fileOut.Close();
                    }

                    
                }
            }
        }


        public void Dijkstra(string v, ref Dictionary<string, int> pathes)
        {
            
            
            HashSet<string> passed = new HashSet<string>();
            
            string opv = MinD(pathes, passed);
            int check = 0;
            while(opv != "")
            {
                check = pathes[opv];
                foreach (var item in this.G_w[opv])
                {
                    if(check + item.Value < pathes[item.Key])
                    {
                        pathes[item.Key] = check + item.Value;
                    }
                }
                passed.Add(opv);
                opv = MinD(pathes, passed);
                
            }
            
        }



        public string MinD(Dictionary<string, int> p, HashSet<string> pd)
        {
            int m = int.MaxValue;
            string opv = "";
            foreach(var item in p)
            {
                if(item.Value < m && !pd.Contains(item.Key))
                {
                    m = item.Value;
                    opv = item.Key;
                }
            }
            return opv;
        }



        public void WrapD(ref string vD, ref int pD)
        {
            //Dictionary<string, int> pathes = new Dictionary<string, int>();
            Dictionary<string, int> count = new Dictionary<string, int>();
            foreach (var item in this.G_w.Keys)
            {
                //pathes.Add(item, int.MaxValue);
                count.Add(item, int.MaxValue);
                
            }
            int c = 0;
            foreach(var item in this.G_w.Keys)
            {
                Dictionary<string, int> pathes = new Dictionary<string, int>();
                foreach (var it in this.G_w.Keys)
                {
                    pathes.Add(it, int.MaxValue);
                    

                }
                pathes[item] = 0;
                this.Dijkstra(item, ref pathes);
                foreach(var it in pathes)
                {
                    if(it.Value == int.MaxValue)
                    {
                        c = int.MaxValue;
                        break;
                    }
                    else
                    {
                        c += it.Value;
                    }
                }
                count[item] = c;
                c = 0;
                
            }
            
            foreach(var item in count)
            {
                if(item.Value < pD)
                {
                    pD = item.Value;
                    vD = item.Key;
                }
            }
        }



        public int CountEdges()
        {
            int m = 0;
            foreach(var item in this.G_w)
            {
                foreach(var it in item.Value)
                {
                    m++;
                }
            }
            return m;
        }

        public void Floyd(ref long[][] dist, int m, Dictionary<string, int> d)
        {
            
            foreach(var item in this.G_w)
            {
                foreach(var it in item.Value)
                {
                    if(item.Key == it.Key)
                    {
                        if(it.Value >= 0)
                            dist[d[item.Key]][d[it.Key]] = 0;
                        else
                            dist[d[item.Key]][d[it.Key]] = it.Value;
                    }
                    else
                        dist[d[item.Key]][d[it.Key]] = it.Value; 
                }
            }
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    for(int c = 0; c < m; c++)
                    {
                        if(dist[c][c] < 0)
                        {
                            for(int k = 0; k < m; k++)
                            {
                                if(dist[c][k] != int.MaxValue)
                                {
                                    dist[c][k] = int.MinValue;
                                }
                            }
                        }
                        if(dist[j][c] > dist[j][i] + dist[i][c] && dist[i][c] != int.MaxValue)
                        {
                            dist[j][c] = dist[j][i] + dist[i][c];
                        }
                        
                        //if (dist[i][c] < int.MaxValue && dist[c][c] < 0 && dist[c][j] < int.MaxValue)
                        //    dist[j][c] = int.MinValue;
                    }
                }
            }
            //for (int i = 0; i < m; i++)
            //{
            //    for (int j = 0; j < m; j++)
            //    {
            //        for (int c = 0; c < m; c++)
            //        {

            //            if (dist[i][c] < int.MaxValue && dist[c][c] < 0 && dist[c][j] < int.MaxValue)
            //                dist[i][c] = int.MinValue;
            //        }
            //    }
            //}
        }


        public void WrapF(string start, string u1, string u2, ref long pathu1, ref long pathu2)
        {
            if(this.G_w.ContainsKey(start) && this.G_w.ContainsKey(u1) && this.G_w.ContainsKey(u2))
            {
                int m = this.G_w.Keys.Count();
                
                long[][] dist = new long[this.G_w.Keys.Count()][];
                for (int i = 0; i < m; i++)
                {
                    dist[i] = new long[m];
                    for (int j = 0; j < m; j++)
                    {
                        if (i != j)
                            dist[i][j] = int.MaxValue;
                        else
                            dist[i][j] = 0;
                    }
                }
                Dictionary<string, int> d = new Dictionary<string, int>();
                int k = 0;
                foreach (var item in this.G_w.Keys)
                {
                    d.Add(item, k);
                    k++;
                }
                this.Floyd(ref dist, m, d);
                pathu1 = dist[d[start]][d[u1]];
                pathu2 = dist[d[start]][d[u2]];
            }
            else
            {
                throw new Exception("Таких вершин в графе нет");
            }
        }



        public void MST_Prim(string startVertex, ref HashSet<int> path)
        {
            if(this.G_w.ContainsKey(startVertex))
            {
                int m = this.CountEdges();
                Edge[] listofEdges = new Edge[m / 2];
                string s = "";
                string c = "";
                int k = 0;
                foreach (var item in this.G_w)
                {
                    s = item.Key;

                    foreach (var it in item.Value)
                    {

                        c = it.Key;
                        Edge ed = new Edge(s, c, it.Value);
                        Edge ed1 = new Edge(it.Key, item.Key, it.Value);
                        if (!listofEdges.Contains(ed1))
                        {
                            listofEdges[k] = ed;
                            k++;
                        }
                    }
                    if (item.Value.Count() == 0)
                    {
                        throw new Exception("В графе есть изолированные вершины");
                    }
                }
                string[] karkas = new string[this.G_w.Count() - 1];
                string start = listofEdges[0].start;
                Dictionary<string, Dictionary<string, int>> MST = new Dictionary<string, Dictionary<string, int>>();
                int i = 0;
                karkas[i] = startVertex;
                i++;
                Dictionary<string, int> d = new Dictionary<string, int>();
                MST.Add(startVertex, d);
                int minimum = int.MaxValue;
                foreach(var item in this.G_w[startVertex])
                {
                    if(item.Value < minimum)
                    {
                        minimum = item.Value;
                        c = item.Key;
                    }
                }
                karkas[i] = c;
                path.Add(minimum);

            }
            else
            {
                throw new Exception("В графе нет такой вершины");
            }

        }


        public void ciferki(ref Dictionary<string, int> ciferki)
        {
            int i = 0;
            foreach(var item in this.G_w.Keys)
            {
                ciferki.Add(item, i);
                i++;
            }
        }

        public int WrapFlow(string S, string T)
        {
            //List<Ed> e = new List<Ed>();
            //Ed[] e = new Ed[2 * this.G_w.Keys.Count()];
            //List<List<int>> g = new List<List<int>>();
            //string S, T;
            List<bool> used = new List<bool>();
            Dictionary<string, int> ciferki = new Dictionary<string, int>();
            this.ciferki(ref ciferki);
            int source = ciferki[S];
            int sink = ciferki[T];
            int i = 0;
            int[,] rg = new int[this.G_w.Keys.Count(), this.G_w.Keys.Count()];

            foreach(var item in this.G_w)
            {
                foreach(var it in item.Value)
                {
                    //Ed edg = new Ed(ciferki[item.Key], ciferki[it.Key], it.Value, 0);
                    //Ed edgr = new Ed(ciferki[it.Key], ciferki[item.Key], 0, 0);
                    //e[i] = edg;
                    //i++;
                    //e[i] = edgr;
                    //i++;
                    rg[ciferki[item.Key], ciferki[it.Key]] = it.Value;
                }
            }
            int[] parent = new int[this.G_w.Keys.Count()];
            int maxFlow = 0;
            int u, v;
            while (BFS(rg, source, sink, parent))
            {
                int pathFlow = int.MaxValue;
                for (v = sink; v != source; v = parent[v])
                {
                    u = parent[v];
                    pathFlow = Math.Min(pathFlow, rg[u, v]);
                }
                Console.Write("{0} по {1} -> ", sink, pathFlow);
                for (v = sink; v != source; v = parent[v])
                {
                    
                    u = parent[v];
                    Console.Write("{0} по {1} -> ", u, pathFlow);
                    rg[u, v] -= pathFlow;
                    rg[v, u] += pathFlow;
                }
                Console.Write("{0} по {1}", source, pathFlow);
                maxFlow += pathFlow;
                //int t = parent[4];
                //while (t != source)
                //{
                //    Console.Write("{0} ", t);
                //    t = parent[t];
                //}
                Console.WriteLine();
            }
            return maxFlow;
            
        }

        private bool BFS(int[,] rg, int source, int sink, int[] parent)
        {
            int V = this.G_w.Keys.Count();
            bool[] visited = new bool[V];
            for (int i = 0; i < V; ++i)
            {
                visited[i] = false;
            }

            Queue<int> q = new Queue<int>();
            q.Enqueue(source);
            visited[source] = true;
            parent[source] = -1;

            while (q.Count != 0)
            {
                int u = q.Dequeue();

                for (int v = 0; v < V; v++)
                {
                    if (visited[v] == false && rg[u, v] > 0)
                    {
                        if (v == sink)
                        {
                            parent[v] = u;
                            return true;
                        }
                        q.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            return false;
        }
    }
}

