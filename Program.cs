using Dapper;
using MySql.Data.MySqlClient;

public class Program
{
    public static void Main()
    {
        string stringConexao = "Server=localhost;Port=3306;Database=livrosdb;Uid=root;Pwd=NrbnnesC^4Y#qMaE*$73Y$sTw;";
        using (MySqlConnection conexao = new MySqlConnection(stringConexao))
        {
            Console.Clear();
            Console.WriteLine("Bem vindo ao sistema de cadastro de livros!");
            Interface(conexao);
        }
    }
    public static void Interface(MySqlConnection conexao)
    {
        Console.WriteLine("");
        Console.WriteLine("Digite o número da operação que deseja realizar:");
        Console.WriteLine("1 | Cadastrar livro.");
        Console.WriteLine("2 | Listar livros.");
        Console.WriteLine("3 | Atualizar livro.");
        Console.WriteLine("4 | Deletar livro.");
        Console.WriteLine("0 | Sair");

        string opcao = Console.ReadLine();

        Console.WriteLine("");
        switch (opcao)
        {
            case "1":
                Adicionar(conexao);
                break;
            case "2":
                Listar(conexao);
                break;
            case "3":
                Atualizar(conexao);
                break;
            case "4":
                Deletar(conexao);
                break;
            case "0":
                System.Environment.Exit(0);
                break;
        }
    }

    public static void Adicionar(MySqlConnection conexao)
    {
        Console.WriteLine("Digite o título do livro que deseja cadastrar:");
        string titulo = Console.ReadLine();
        Console.WriteLine("Digite o nome do autor do livro:");
        string autor = Console.ReadLine();
        Console.WriteLine("Digite a descrição do livro:");
        string descricao = Console.ReadLine();

        Livro novoLivro = new Livro
        {
            Titulo = titulo,
            Autor = autor,
            Descricao = descricao
        };

        string insertSql = "INSERT INTO Livros (Titulo, Autor, Descricao) VALUES (@Titulo, @Autor, @Descricao)";
        conexao.Execute(insertSql, novoLivro);

        Console.WriteLine("");
        Console.WriteLine("Livro cadastrado!");
        Interface(conexao);
    }

    public static void Listar(MySqlConnection conexao)
    {
        var outputList = conexao.Query<Livro>("SELECT * FROM Livros").ToList();
        foreach (var item in outputList)
        {
            Console.WriteLine($"Id: {item.Id} | Nome: {item.Titulo} | Autor: {item.Autor} | Descricao: {item.Descricao}");
        }
        Console.WriteLine("");
        Interface(conexao);
    }

    public static void Atualizar(MySqlConnection conexao)
    {
        Console.WriteLine("Digite o título do livro que deseja atualizar:");
        string titulo = Console.ReadLine();

        Console.WriteLine("Digite o novo título do livro:");
        string novoTitulo = Console.ReadLine();

        Console.WriteLine("Digite o novo autor do livro:");
        string novoAutor = Console.ReadLine();

        Console.WriteLine("Digite a nova descrição do livro:");
        string novaDesc = Console.ReadLine();

        string updateSql = "UPDATE Livros SET Titulo = @NovoTitulo, Autor = @NovoAutor, Descricao = @NovaDesc WHERE Titulo = @Titulo";
        conexao.Execute(updateSql, new { NovoTitulo = novoTitulo, NovoAutor = novoAutor, NovaDesc = novaDesc, Titulo = titulo });

        Console.WriteLine("");
        Console.WriteLine("Livro atualizado!");
        Interface(conexao);
    }

    public static void Deletar(MySqlConnection conexao)
    {
        Console.WriteLine("Digite o título do livro que deseja deletar:");
        string titulo = Console.ReadLine();


        string deleteSQL = "DELETE FROM Livros where Titulo = @Titulo;";
        conexao.Execute(deleteSQL, new { Titulo = titulo });

        Console.WriteLine("");
        Console.WriteLine("Livro deletado!");
        Interface(conexao);
    }
}
