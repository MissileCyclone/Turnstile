using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Турникет
{
    internal class DB
    {
        // Создаем объект подключения к базе данных MySQL с указанием параметров подключения.
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=turnstile");

        // Метод для открытия соединения с базой данных.
        public void openConnection()
        {
            // Проверяем, закрыто ли соединение, и, если да, открываем его.
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        // Метод для закрытия соединения с базой данных.
        public void closeConnection()
        {
            // Проверяем, открыто ли соединение, и, если да, закрываем его.
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        // Метод для получения объекта соединения с базой данных. 
        public MySqlConnection getConnection()
        {
            // Возвращаем объект соединения для использования в других частях программы.
            return connection;
        }
    }
}