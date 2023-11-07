using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Турникет
{
    public partial class Выбор_над_пользователями : Form
    {
        public Выбор_над_пользователями()
        {
            // Конструктор формы. Вызывает метод InitializeComponent для инициализации компонентов.
            InitializeComponent();

            // Привязка обработчиков событий к кнопкам.
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button3.Click += new System.EventHandler(this.button3_Click);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Назад".
            this.Close(); // Закрывает текущую форму.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Добавление".
            // Создает и отображает новую форму "Добавление_пользователей".
            Добавление_пользователей form = new Добавление_пользователей();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Изменение".
            // Создает и отображает новую форму "Изменение_пользователей".
            Изменение_пользователей form = new Изменение_пользователей();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Удаление".
            // Создает и отображает новую форму "Удаление_пользователей".
            Удаление_пользователей form = new Удаление_пользователей();
            form.Show();
        }
    }
}