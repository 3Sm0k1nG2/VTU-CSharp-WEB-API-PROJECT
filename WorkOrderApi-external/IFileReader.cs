namespace Project
{
    interface IFileReader
    {
        string FilePath { get; set; }

        string Read();
    }
}