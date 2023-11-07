using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Турникет
{
    public partial class Удаление_меток : Form
    {
        private SerialPort arduinoSerialPort;
        public Удаление_меток()
        {
            InitializeComponent();
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            this.buttondelete.Click += new System.EventHandler(this.buttondelete_Click);
            // Инициализация SerialPort
            arduinoSerialPort = new SerialPort();
            arduinoSerialPort.PortName = "COM3"; // Укажите COM-порт, который использует Arduino
            arduinoSerialPort.BaudRate = 9600; // Скорость соединения, соответствующая Arduino
            arduinoSerialPort.DataReceived += new SerialDataReceivedEventHandler(arduinoSerialPort_DataReceived);
            try
            {
                arduinoSerialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть COM-порт: " + ex.Message);
            }

            // Заполнение ComboBox
            Status.Items.Add("Разрешён");
            Status.Items.Add("Запрещён");
            Status.SelectedIndex = 0; // Установите начальное значение (например, "Разрешён")

            // Обработчик события FormClosing
            this.FormClosing += new FormClosingEventHandler(Удаление_меток_FormClosing);
        }

        private void arduinoSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = arduinoSerialPort.ReadLine();
            // Обработка данных от Arduino
            

            // Обновление UI на основе полученных данных
            this.Invoke(new Action(() =>
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    textBox2.Visible = true;
                    textBoxUserID.Visible = true;
                    LastName.Visible = true;
                    textBoxIdentifier.Visible = true;
                    Comment.Visible = true;
                    text_the_note.Visible = true;
                    textBox5.Visible = true;
                    textBoxCreatedDateTime.Visible = true;
                    textBox6.Visible = true;
                    textBox10.Visible = true;
                    Address.Visible = true;
                    textBoxc.Visible = true;
                    textBoxdo.Visible = true;
                    FirstName.Visible = true;
                    textBoxExpiryDate.Visible = true;
                    SurName.Visible = true;
                    Status.Visible = true;
                    buttonfilter.Visible = true;



                    textBoxIdentifier.Text = data;

                }
            }));
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрывает текущую форму
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            // Получаем значение RFIDIdentifier для удаления
            string rfidIdentifier = textBoxIdentifier.Text;

            // Проверка наличия значения
            if (string.IsNullOrEmpty(rfidIdentifier))
            {
                MessageBox.Show("Введите RFIDIdentifier для удаления.");
                return;
            }

            // Создает объект подключения к базе данных
            DB db = new DB();
            MySqlConnection connection = db.getConnection();

            try
            {
                // Открываем соединение
                db.openConnection();

                // Формирует SQL-запрос для удаления записи
                string deleteQuery = $"DELETE FROM rfidtags WHERE RFIDIdentifier = '{rfidIdentifier}'";

                // Создает команду для выполнения SQL-запроса
                MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                // Выполняет SQL-запрос
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Выводит сообщение об успешном удалении
                    MessageBox.Show("Запись успешно удалена.");
                }
                else
                {
                    MessageBox.Show("Запись с указанным RFIDIdentifier не найдена.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка при удалении записи: " + ex.Message);
            }
            finally
            {
                // Закрываем соединение
                db.closeConnection();
            }
        }

        private void buttonfilter_Click(object sender, EventArgs e)
        {
            // Формирует SQL-запрос с учетом фильтров
            string sqlQuery = "SELECT * FROM rfidtags WHERE 1=1";

            if (!string.IsNullOrEmpty(textBoxIdentifier.Text))
            {
                sqlQuery += $" AND RFIDIdentifier LIKE '%{textBoxIdentifier.Text}%'";
            }

            if (!string.IsNullOrEmpty(text_the_note.Text))
            {
                sqlQuery += $" AND TagDescription LIKE '%{text_the_note.Text}%'";
            }

            if (!string.IsNullOrEmpty(textBoxCreatedDateTime.Text))
            {
                sqlQuery += $" AND CreatedDateTime LIKE '%{textBoxCreatedDateTime.Text}%'";
            }

            if (!string.IsNullOrEmpty(textBoxc.Text))
            {
                sqlQuery += $" AND AccessTimeStart LIKE '%{textBoxc.Text}%'";
            }

            if (!string.IsNullOrEmpty(textBoxdo.Text))
            {
                sqlQuery += $" AND AccessTimeEnd LIKE '%{textBoxdo.Text}%'";
            }

            if (!string.IsNullOrEmpty(textBoxExpiryDate.Text))
            {
                sqlQuery += $" AND ExpiryDate LIKE '%{textBoxExpiryDate.Text}%'";
            }

            if (Status.SelectedIndex == 0)
            {
                sqlQuery += " AND Status = 'Разрешён'";
            }
            else
            {
                sqlQuery += " AND Status = 'Запрещён'";
            }

            // Создаем объект подключения к базе данных
            DB db = new DB();
            MySqlConnection connection = db.getConnection();

            try
            {
                // Открываем соединение
                db.openConnection();

                // Создаем команду для выполнения SQL-запроса
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);

                // Создаем адаптер данных
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                // Создаем DataSet для хранения результатов
                DataSet dataset = new DataSet();

                // Заполняем DataSet данными из базы
                adapter.Fill(dataset, "rfidtags");

                // Выводим результат в DataGridView
                dataGridView1.DataSource = dataset.Tables["rfidtags"];
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
            }
            finally
            {
                // Закрываем соединение
                db.closeConnection();
            }
        }

        private void Удаление_меток_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Закрыть COM-порт при закрытии формы
            if (arduinoSerialPort.IsOpen)
            {
                arduinoSerialPort.Close();
            }
        }

    }
}