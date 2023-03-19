using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestCarAPI.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _dbName;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(@"ProjectContext");
            _dbName = _configuration.GetSection(@"DatabaseName").Value;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task Init()
        {
            await InitDatabase();
            await InitTables();
        }

        private async Task InitDatabase()
        {
            using var connection = CreateConnection();
            var query = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{_dbName}') CREATE DATABASE [{_dbName}];";
            await connection.ExecuteAsync(query);
        }

        private async Task InitTables()
        {
            await InitCarTable();
            await InitUserTable();
        }

        private async Task InitCarTable()
        {
            using var connection = CreateConnection();
            var query = @$"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Cars' and xtype='U')
                            CREATE TABLE [dbo].[Cars](
	                            [Id] [int] NOT NULL IDENTITY(1,1),
	                            [ClientName] [nvarchar](50) NOT NULL,
	                            [ProductionYear] [int] NOT NULL,
	                            [Model] [nvarchar](50) NOT NULL,
	                            [Manufacturer] [nvarchar](50) NOT NULL,
	                            [Price] [decimal](18, 0) NOT NULL,
                             CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
                            (
	                            [Id] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                            ) ON [PRIMARY]";
            await connection.ExecuteAsync(query);
        }

        private async Task InitUserTable()
        {
            using var connection = CreateConnection();
            var query = @$"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Cars' and xtype='U')
                            CREATE TABLE [dbo].[Cars](
	                            [Id] [int] NOT NULL IDENTITY(1,1),
	                            [ClientName] [nvarchar](50) NOT NULL,
	                            [ProductionYear] [int] NOT NULL,
	                            [Model] [nvarchar](50) NOT NULL,
	                            [Manufacturer] [nvarchar](50) NOT NULL,
	                            [Price] [decimal](18, 0) NOT NULL,
                             CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
                            (
	                            [Id] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                            ) ON [PRIMARY]";
            await connection.ExecuteAsync(query);
        }
    }
}
