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
    public partial class Выбор_над_метками : Form
    {
        public Выбор_над_метками()
        {
            // Конструктор формы. Вызывает метод InitializeComponent для инициализации компонентов.
            InitializeComponent();

            // Привязка обработчиков событий к кнопкам.
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button6.Click += new System.EventHandler(this.button6_Click);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Назад".
            this.Close(); // Закрывает текущую форму.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Добавить метки".
            // Создает и отображает новую форму "Добавление меток".
            Добавление_меток form = new Добавление_меток();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Удалить метки".
            // Создает и отображает новую форму "Удаление меток".
            Удаление_меток form = new Удаление_меток();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Присвоить метки".
            // Создает и отображает новую форму "Присвоение меток".
            Присвоение_меток form = new Присвоение_меток();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия кнопки "Изъять метку".
            // Создает и отображает новую форму "Изъятие метки".
            Изымание_метки form = new Изымание_метки();
            form.Show();
        }
    }
}