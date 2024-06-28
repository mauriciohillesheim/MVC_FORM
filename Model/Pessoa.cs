using repository;

namespace model
{
    public class Pessoa
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cpf { get; set; }

        public Pessoa() { }
        public Pessoa(string nome, int idade, string cpf)
        {
            this.Nome = nome;
            Idade = idade;
            Cpf = cpf;

            ListPessoa.Criar(this);
        }

        public static void Sincronizar()
        {
            ListPessoa.Sincronizar();

        }
        public static List<Pessoa> ListarPessoa()
        {
            return ListPessoa.pessoas;
        }

        public static void AlterarPessoa(
            int indice,
            string nome,
            int idade,
            string cpf
        )
        {
            Pessoa person = ListPessoa.pessoas[indice];
            person.Nome = nome;
            person.Idade = idade;
            person.Cpf = cpf;

            ListPessoa.pessoas[indice] = person;
        }

        public static void DeletarPessoa(int indice)
        {
            ListPessoa.Delete(indice);
        }

        public void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome}, Idade: {Idade}");
        }
    }
}