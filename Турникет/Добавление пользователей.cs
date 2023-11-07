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
    public partial class Добавление_пользователей : Form
    {
        private PictureBox userPictureBox; // Создание переменной для хранения изображения пользователя.
        private OpenFileDialog openFileDialog1; // Создание переменной для открытия файла изображения.

        public Добавление_пользователей()
        {
            InitializeComponent();
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрывает текущую форму.
        }

        private void Добавление_пользователей_Load(object sender, EventArgs e)
        {
            // Создание PictureBox для отображения изображения пользователя.
            userPictureBox = new PictureBox();
            userPictureBox.Location = new System.Drawing.Point(10, 10); // Установка позиции PictureBox на форме.
            userPictureBox.Size = new System.Drawing.Size(100, 100); // Установка размеров PictureBox.
            userPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Установка режима масштабирования изображения.
            this.Controls.Add(userPictureBox); // Добавление PictureBox на форму.

            // Создание OpenFileDialog для выбора изображения.
            openFileDialog1 = new OpenFileDialog();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Изображения|*.jpg;*.png;*.jpeg;*.gif;*.bmp"; // Установка фильтра для выбора изображений.

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog1.FileName; // Получение пути к выбранному изображению.
                userPictureBox.Image = Image.FromFile(imagePath); // Отображение выбранного изображения в PictureBox.
            }
        }
    }
}