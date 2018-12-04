using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MarioLikeGame.model;

namespace MarioLikeGame.DAL
{
    public class GamerDAL
    {
        //Declarar o objeto de conexão com o Bando de Dados
        private SqlConnection conexao;

        //Exibir mensagem de erro
        public string MensagemErro { get; set; }

        public GamerDAL()
        {
            //Criar o objeto para ler a configuração
            LeitorConfiguracao leitor = new LeitorConfiguracao();

            //Instanciar a conexão
            conexao = new SqlConnection();
            conexao.ConnectionString = leitor.LerConexao();
        
        }

        public bool Inserir(Placar placar)
        {
            bool resultado = false;
            //Limpa mensagem de erro
            MensagemErro = "";

            //Declarar comando SQL
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "INSERT INTO ScorePlayer (Nome_Jogador,Score_Jogador,DataScore_Jogador,Tempo_Jogador)" +
                "VALUES (@Nome,@Score,@DataScore,@Tempo);";

            //Criar os parâmetros
            comando.Parameters.AddWithValue("@Nome",placar.NomeJogador);
            comando.Parameters.AddWithValue("@Score", placar.ScoreJogador);
            comando.Parameters.AddWithValue("@DataScore", placar.DataScoreJogador);
            comando.Parameters.AddWithValue("@Tempo", placar.TempoJogador);

            //Executar o comando 
            try
            {
                //Abrir a conexão
                conexao.Open();

                //Executar o comando
                comando.ExecuteNonQuery();
                //Se chegou até aqui, então funcionou! :)

                resultado = true;

            }
            catch (Exception ex)
            {
                //Se entrou aqui, então deu pau! :(
                MensagemErro = ex.Message;
                
            }
            finally
            {
                //Finalizar, fechando a conexão
                conexao.Close();

            }
            return resultado;

        }

        public List<Placar> Listar()
        {
            //Instanciar a lista
            List<Placar> resultado = new List<Placar>();

            //Declarar o comando
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT TOP 10 Id_Jogador, Nome_Jogador, Score_Jogador, DataScore_Jogador, Tempo_Jogador " +
                " FROM ScorePlayer ORDER BY Score_Jogador DESC";

            //Executar o comando
            try
            {
                //Abrir a conexão
                conexao.Open();

                //Executar o comando e receber o resultado
                SqlDataReader leitor = comando.ExecuteReader();

                //Verificarse encontrou algo
                while (leitor.Read() == true)
                {
                    //instancio o objeto
                    Placar placar = new Placar();
                    placar.IdJogador = Convert.ToInt32(leitor["Id_Jogador"]);
                    placar.NomeJogador = leitor["Nome_Jogador"].ToString();
                    placar.ScoreJogador = Convert.ToInt32(leitor["Score_Jogador"]);
                    placar.DataScoreJogador = Convert.ToDateTime(leitor["DataScore_Jogador"]);
                    placar.TempoJogador = leitor["Tempo_Jogador"].ToString();

                    //Adicionar na lista
                    resultado.Add(placar);
                }

                //Fechar o leitor
                leitor.Close();
            }
            catch (Exception ex)
            {

                //Se entrou aqui entao deu pau :(
                string mensagem = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
            return resultado;

        }

    }
}
