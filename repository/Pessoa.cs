using model;
using MySqlConnector;

namespace repository
{
    public class ListPessoa
    {
        private static MySqlConnection conexao;
        static public List<Pessoa> pessoas = new List<Pessoa>();

        public static void InitConexao()
        {
            string info = "server=localhost;database=projeto_integrador_seb;user id=root;password=''";
            conexao = new MySqlConnection(info);
            try
            {
                conexao.Open();
            }
            catch
            {
                MessageBox.Show("Não foi possivel conectar com o Banco.");
            }
        }
         public static void CloseConexao() {
            conexao.Close();
        }

        public static List<Pessoa> Sincronizar() {
            // inicializa a conexão com o banco
            InitConexao();
            string query = "SELECT * FROM pessoas";
            MySqlCommand command = new MySqlCommand(query, conexao);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = Convert.ToInt32(reader["id"].ToString());
                pessoa.Nome = reader["nome"].ToString();
                pessoa.Idade = Convert.ToInt32(reader["idade"].ToString());
                pessoa.Cpf = reader["cpf"].ToString();
                pessoas.Add(pessoa);
            }
            // fcha a conexão com o banco
            CloseConexao();
            return pessoas;
        }

        public static void Criar(Pessoa pessoa) {
            InitConexao();
            string insert = "INSERT INTO pessoas (nome, idade, cpf) VALUES (@Nome, @Idade, @Cpf)";
            MySqlCommand command = new MySqlCommand(insert, conexao);
            try {
                if(pessoa.Nome == null || pessoa.Idade < 0 || pessoa.Cpf == null) {
                    MessageBox.Show("Deu ruim, favor preencher a pessoa");
                } else {
                    command.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    command.Parameters.AddWithValue("@Idade", pessoa.Idade);
                    command.Parameters.AddWithValue("@Cpf", pessoa.Cpf);

                    int rowsAffected = command.ExecuteNonQuery();
                    pessoa.Id = Convert.ToInt32(command.LastInsertedId);

                    if(rowsAffected > 0){
                        MessageBox.Show("Pessoa cadastrada com sucesso");
                        pessoas.Add(pessoa);
                    } else {
                        MessageBox.Show("Deu ruim, não deu pra adicionar");
                    }
                }
            } catch (Exception e) {
                MessageBox.Show("Deu ruim: " + e.Message);
            }

            CloseConexao();
        }

        public static void UpdatePessoa(
            int indice,
            string nome,
            int idade,
            string cpf
        ){
            InitConexao();
            MessageBox.Show("iniciando");
            string query = "UPDATE pessoas SET nome = @Nome, idade = @Idade, cpf = @Cpf WHERE id = @Id";
            MySqlCommand command = new MySqlCommand(query, conexao);
            Pessoa pessoa = pessoas[indice];
            try {
                if(nome != null || idade > 0 || cpf != null) {
                    command.Parameters.AddWithValue("@Id", pessoa.Id);
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@Cpf", cpf);
                    command.Parameters.AddWithValue("@Idade", idade);
                    int rowsAffected = command.ExecuteNonQuery();
                
                    if (rowsAffected > 0) {
                        pessoa.Nome = nome;
                        pessoa.Idade = idade;
                        pessoa.Cpf = cpf;
                    }
                    else {
                        MessageBox.Show(rowsAffected.ToString());
                    }
                }else {
                    MessageBox.Show("Usuário não encontrado");
                }
            }catch (Exception ex){
                MessageBox.Show("Erro durante a execução do comando: " + ex.Message);
            }
            CloseConexao();
        }

        public static void Delete(int index) {
            InitConexao();
            string delete = "DELETE FROM pessoas WHERE id = @Id";
            MySqlCommand command = new MySqlCommand(delete, conexao);
            command.Parameters.AddWithValue("@Id", pessoas[index].Id);
            // executar
            int rowsAffected = command.ExecuteNonQuery();
            if(rowsAffected > 0) {
                pessoas.RemoveAt(index);
                MessageBox.Show("Pessoa deletada com sucesso.");
            } else {
                MessageBox.Show("Usuário não encontrado.");
            }
            CloseConexao();
        }

    }

}