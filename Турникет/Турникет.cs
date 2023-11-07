using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Турникет
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            // Привязка обработчиков событий к элементам меню.
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            this.оРазработкеToolStripMenuItem.Click += new System.EventHandler(this.оРазработкеToolStripMenuItem_Click);
            this.управлениеПользователямиToolStripMenuItem.Click += new System.EventHandler(this.управлениеПользователямиToolStripMenuItem_Click);
            this.упрМеткамиToolStripMenuItem.Click += new System.EventHandler(this.упрМеткамиToolStripMenuItem_Click);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Завершает выполнение приложения.
        }

        private void оРазработкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия на пункт меню "О разработке".
            // Создает и отображает окно "Информация_о_разработке".
            Информация_о_разработке form = new Информация_о_разработке();
            form.Show();
        }

        private void управлениеПользователямиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия на пункт меню "Управление пользователями".
            // Создает и отображает окно "Выбор_над_пользователями".
            Выбор_над_пользователями form = new Выбор_над_пользователями();
            form.Show();
        }

        private void упрМеткамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Обработчик события нажатия на пункт меню "Управление метками".
            // Создает и отображает окно "Выбор_над_метками".
            Выбор_над_метками form = new Выбор_над_метками();
            form.Show();
        }
    }
}