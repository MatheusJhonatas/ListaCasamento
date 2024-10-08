namespace ListaCasamento.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T data, List<string> errors)
        {
            this.Data = data;
            this.Errors = errors;
        }
        public ResultViewModel(T data)
        {
            this.Data = data;
        }
        public ResultViewModel(List<string> errors)
        {
            this.Errors = errors;
        }
        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }

        public T Data { get; private set; }
        public List<String> Errors { get; private set; } = new();
    }
}