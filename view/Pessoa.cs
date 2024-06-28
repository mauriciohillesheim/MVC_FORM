using controller;
using model;

namespace view;

public class ViewPessoa : Form
{
    private readonly Label LblNome;
    private readonly Label LblCpf;

    private readonly TextBox InpNome;

    private readonly TextBox InpCpf;

    private readonly Button BtnCadastrar;

    private readonly Button BtnAlterar;

    private readonly Button BtnDeletar;

    private readonly DataGridView DgvListagem;

    public ViewPessoa()
    {
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(450, 400);
        LblNome = new Label
        {
            Text = "Nome: ",
            Location = new Point(50, 50)
        };

        LblCpf = new Label
        {
            Text = "Cpf: ",
            Location = new Point(50, 100)
        };

        InpNome = new TextBox
        {
            Size = new Size(200, 20),
            Location = new Point(150, 50)
            
        };

        InpCpf = new TextBox
        {
            Size = new Size(200, 20),
            Location = new Point(150, 100)
        };

        BtnCadastrar = new Button
        {
            Text = "Cadastrar: ",
            Location = new Point(50, 150)
        };
        BtnCadastrar.Click += ClickCadastrar;
       
        
        BtnAlterar = new Button
        {
            Text = "Alterar: ",
            Location = new Point(150, 150)
        };
        BtnAlterar.Click += ClickAlterar;

        BtnDeletar = new Button
        {
            Text = "Deletar: ",
            Location = new Point(250, 150)
        };
        BtnDeletar.Click += ClickDeletar;

        DgvListagem = new DataGridView
        {
            Location = new Point(0, 200),
            Size = new Size(450, 150)
        };

        Controls.Add(LblNome);
        Controls.Add(LblCpf);
        Controls.Add(InpNome);
        Controls.Add(InpCpf);
        Controls.Add(BtnCadastrar);
        Controls.Add(BtnAlterar);
        Controls.Add(BtnDeletar);
        Controls.Add(DgvListagem);
        Listar();
    }

    private void Listar() {
        List<Pessoa> pessoas = ControllerPessoa.ListarPessoa();
        DgvListagem.Columns.Clear();
        DgvListagem.AutoGenerateColumns = false;
        DgvListagem.DataSource = pessoas;

DgvListagem.Columns.Add(new DataGridViewTextBoxColumn{
            HeaderText = "Id",
            DataPropertyName = "Id"
        });
        DgvListagem.Columns.Add(new DataGridViewTextBoxColumn{
            HeaderText = "Nome",
            DataPropertyName = "Nome"
        });
        DgvListagem.Columns.Add(new DataGridViewTextBoxColumn{
            HeaderText = "Idade",
            DataPropertyName = "Idade"
        });
        DgvListagem.Columns.Add(new DataGridViewTextBoxColumn{
            HeaderText = "CPF",
            DataPropertyName = "CPF",
            
        });
    }

    private void ClickCadastrar(Object? sender, EventArgs e)
    {
        if(InpNome.Text.Length < 1 || InpCpf.Text.Length < 1) {
            MessageBox.Show("Favor preencher todos os campos");
            return;
        }
        MessageBox.Show("Cadastrando");
        ControllerPessoa.CriarPessoa(InpNome.Text, 0, InpCpf.Text);
        InpNome.Clear();
        InpCpf.Clear();
        Listar();
    }

    private void ClickAlterar(Object? sender, EventArgs e)
    {
        MessageBox.Show("Alterado");
    }

    private void ClickDeletar(Object? sender, EventArgs e)
    {   int index = DgvListagem.SelectedRows[0].Index;
        MessageBox.Show("Deletado");
        ControllerPessoa.DeletarPessoa(index);
        Listar();

    }
}

