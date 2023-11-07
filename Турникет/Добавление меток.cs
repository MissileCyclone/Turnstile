using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace Турникет
{
    public partial class Добавление_меток : Form
    {
        private SerialPort arduinoSerialPort;

        public Добавление_меток()
        {
            // Конструктор формы
            InitializeComponent();

            // Привязка обработчиков событий к кнопкам
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            this.buttonadd.Click += new System.EventHandler(this.buttonadd_Click);

            // Инициализация SerialPort для связи с Arduino
            arduinoSerialPort = new SerialPort();
            arduinoSerialPort.PortName = "COM3"; // Указать COM-порт, который использует Arduino
            arduinoSerialPort.BaudRate = 9600; // Скорость соединения, соответствующая Arduino
            arduinoSerialPort.DataReceived += new SerialDataReceivedEventHandler(arduinoSerialPort_DataReceived);

            try
            {
                arduinoSerialPort.Open(); // Открываем COM-порт для связи с Arduino
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть COM-порт: " + ex.Message);
            }

            // Заполнение ComboBox с возможными статусами
            Status.Items.Add("Разрешён");
            Status.Items.Add("Запрещён");
            Status.SelectedIndex = 0; // Установлено начальное значение (например, "Разрешён")

            //Обработчик события закрытия формы
            this.FormClosing += new FormClosingEventHandler(Добавление_меток_FormClosing);
        }

        private void Добавление_меток_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Обработчик события закрытия формы.
            // Закрыть COM-порт при закрытии формы, чтобы освободить порт.
            if (arduinoSerialPort.IsOpen)
            {
                arduinoSerialPort.Close();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Назад".
            this.Close(); // Закрывает текущую форму.
        }

        private void arduinoSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Обработчик события при получении данных от Arduino.
            string data = arduinoSerialPort.ReadLine();

            // Обработка данных от Arduino
            

            // Обновление пользовательского интерфейса на основе полученных данных
            this.Invoke(new Action(() =>
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    // Отображение полученного UID в текстовом поле
                    RFIDIdentifier.Visible = true;
                    RFIDIdentifier.Text = data;

                    // Включение видимости других элементов
                    Status.Visible = true;
                    AccessTimeStart.Visible = true;
                    CreatedDateTime.Visible = true;
                    textBox6.Visible = true;
                    textBox5.Visible = true;
                    ExpiryDate.Visible = true;
                    AccessTimeEnd.Visible = true;
                    FirstName.Visible = true;
                    TagDescription.Visible = true;
                    Comment.Visible = true;
                    Address.Visible = true;
                    SurName.Visible = true;
                    LastName.Visible = true;
                    textBox10.Visible = true;
                }
            }));
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Добавить метку".

            // Получение значений из элементов управления
            string rfidIdentifier = RFIDIdentifier.Text;
            string tagDescription = TagDescription.Text;
            string accessTimeStart = AccessTimeStart.Text;
            string accessTimeEnd = AccessTimeEnd.Text;
            string status = Status.Text;
            string createdDateTime = CreatedDateTime.Text;
            string expiryDate = ExpiryDate.Text;

            // Создаёт объект подключения к базе данных
            DB db = new DB();
            MySqlConnection connection = db.getConnection();

            try
            {
                // Открытие соединения с базой данных
                db.openConnection();

                // Создание SQL-запроса для вставки данных
                string insertQuery = "INSERT INTO rfidtags (RFIDIdentifier, TagDescription, AccessTimeStart, AccessTimeEnd, Status, CreatedDateTime, ExpiryDate) " +
                                     "VALUES (@RFIDIdentifier, @TagDescription, @AccessTimeStart, @AccessTimeEnd, @Status, @CreatedDateTime, @ExpiryDate)";

                MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@RFIDIdentifier", rfidIdentifier);
                cmd.Parameters.AddWithValue("@TagDescription", tagDescription);
                cmd.Parameters.AddWithValue("@AccessTimeStart", accessTimeStart);
                cmd.Parameters.AddWithValue("@AccessTimeEnd", accessTimeEnd);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@CreatedDateTime", createdDateTime);
                cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);

                // Выполнение SQL-запрос для вставки данных
                cmd.ExecuteNonQuery();

                // Выведит сообщение об успешной вставке данных
                MessageBox.Show("Данные успешно внесены в базу данных.");
            }
            catch (MySqlException ex)
            {
                // Обработчик ошибки, если что-то пошло не так
                MessageBox.Show("Ошибка при вставке данных: " + ex.Message);
            }
            finally
            {
                // Закрытие соединения, чтобы освободить порт
                db.closeConnection();
            }
        }
    }
}