using Blog.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.ViewModels
{
    public class ResultViewModel<T>
    {
        private List<Category> categorias;

        public ResultViewModel(T data, List<string> erros) {
            Data = data;
            Erros = erros;
        }

        public ResultViewModel(T data)
        {
            Data = data;
        }

        public ResultViewModel(string error)
        {
            Erros.Add(error);
        }

         public ResultViewModel(List<string> erros)
        {
            Erros = erros;
        }

        public T Data  { get; private set; }

        public List<string> Erros { get; private set; } = new();
    }
}
