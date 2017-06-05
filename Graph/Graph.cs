using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphNode<T>
    {
        public int Id { get; set; }
        public object Data { get; set; }

        public int?[,] AdjacencyMatrix()
        {
            int?[,] adj; 
            // The AdjecencyMatrix has size n^2 where n is the number of nodes/vertices in the graph 
            return null;
        }

    }

    public class Edge
    {
    }

    public class Matrix
    {

    }

    public class AdjacencyList
    {
        private readonly LinkedList<Tuple<int, int>>[] _adjacencyList;

        // Constructor - creates an empty Adjacency List
        public AdjacencyList(int vertices)
        {
            _adjacencyList = new LinkedList<Tuple<int, int>>[vertices];

            for (var i = 0; i < _adjacencyList.Length; i++)
            {
                _adjacencyList[i] = new LinkedList<Tuple<int, int>>();
            }
        }

        public void AddEdgeAtEnd(int startVertex, int endVertex, int weight)
        {
            _adjacencyList[startVertex].AddLast(new Tuple<int, int>(endVertex, weight));
        }

        public void AddEdgeAtBeginning(int startVertex, int endVertex, int weight)
        {
            _adjacencyList[startVertex].AddFirst(new Tuple<int, int>(endVertex, weight));
        }

        public int GetNumberOfVertices()
        {
            return _adjacencyList.Length;
        }

        public LinkedList<Tuple<int, int>> this[int index]
        {
            get
            {
                var edgeList
                        = new LinkedList<Tuple<int, int>>(_adjacencyList[index]);

                return edgeList;
            }
        }

        public bool RemoveEdge(int startVertex, int endVertex, int weight)
        {
            var edge = new Tuple<int, int>(endVertex, weight);

            return _adjacencyList[startVertex].Remove(edge);
        }
    }
}
