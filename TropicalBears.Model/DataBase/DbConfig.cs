using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TropicalBears.Model.DataBase.Model;
using TropicalBears.Model.DataBase.Repository;

namespace TropicalBears.Model.DataBase
{
    public class DbConfig
    {
        private static DbConfig _instance = null;

        private ISessionFactory _sessionFactory;

        //public PessoaRepository PessoaRepository { get; set; }
        public ProdutoRepository ProdutoRepository { get; set; }
        public UserRepository UserRepository { get; set; }

        private DbConfig()
        {
            Conectar();
            this.ProdutoRepository = new ProdutoRepository(Session);
            this.UserRepository = new UserRepository(Session);
          //  this.PessoaRepository = new PessoaRepository(Session);
          //  this.TelefoneRepository = new TelefoneRepository(Session);
        }

        public static DbConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbConfig();
                }

                return _instance;
            }
        }

        private void Conectar()
        {
            try
            {
                var stringConexao = "Persist Security Info=False;"
                    + "server=localhost;"
                    + "port=3306;"
                    + "database=TropicalBears;"
                    + "uid=root;"
                    + "pwd=aluno"; 

                var mysql = new MySqlConnection(stringConexao);
                try
                {
                    mysql.Open();
                }
                catch
                {
                    CriarSchemaBanco("localhost", "3306", "TropicalBears", "aluno", "root");
                }
                finally
                {
                    mysql.Close();
                }

                ConectarNHibernate(stringConexao);

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível conectar no banco de dados",ex);
            }
        }

        private void ConectarNHibernate(String stringConexao)
        {
            try
            {
                var config = new Configuration();

                //configura a conexão com o banco
                config.DataBaseIntegration(db =>
                {
                    //dialeto de sql
                    db.Dialect<NHibernate.Dialect.MySQLDialect>();
                    //string de conexaão
                    db.ConnectionString = stringConexao;
                    //driver de conexão
                    db.Driver<NHibernate.Driver.MySqlDataDriver>();
                    //provedor de conexão
                    db.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                    //jeito de criação do banco de dados
                    db.SchemaAction = SchemaAutoAction.Update;
                });

                var maps = this.Mapeamento();
                config.AddMapping(maps);

                if (HttpContext.Current == null)
                {
                    config.CurrentSessionContext<ThreadStaticSessionContext>();
                }
                else
                {
                    config.CurrentSessionContext<WebSessionContext>();
                }

                this._sessionFactory = config.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível criar o NHibernate.", ex);
            }
        }

        private HbmMapping Mapeamento()
        {
            try
            {
                var modelMapper = new ModelMapper();

                modelMapper.AddMappings(
                    Assembly.GetAssembly(typeof(User)).GetTypes()
                    //Assembly.GetAssembly(typeof(PessoaMap)).GetTypes()
                    );
                modelMapper.AddMappings(
                    Assembly.GetAssembly(typeof(Produto)).GetTypes()
                    );

                return modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível mapear as classes.", ex);
            }
        }

        private void CriarSchemaBanco(string server, string port, string dbName,
            string psw, string user)
        {
            try
            {
                var stringConexao = "server=" + server + ";user=" + user +
                    ";port=" + port + ";password=" + psw + ";";
                var mySql = new MySqlConnection(stringConexao);
                var cmd = mySql.CreateCommand();

                mySql.Open();
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS `" + dbName + "`;";
                cmd.ExecuteNonQuery();
                mySql.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível criar o schema de banco de dados.", ex);
            }
        }

        private ISession Session
        {
            get
            {
                try
                {
                    if (CurrentSessionContext.HasBind(_sessionFactory))
                        return _sessionFactory.GetCurrentSession();

                    var session = _sessionFactory.OpenSession();

                    CurrentSessionContext.Bind(session);
                    return session;
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível criar a sessão", ex);
                }
            }
        }
        
    }
}
