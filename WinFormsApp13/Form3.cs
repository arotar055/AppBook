using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp13
{
    public partial class Form3 : Form
    {
        private Book book;
        private List<Author> authors;
        private bool isNew;

        public Form3(Book book, List<Author> authors, bool isNew)
        {
            InitializeComponent();
            this.book = book ?? throw new ArgumentNullException(nameof(book));
            this.authors = authors ?? throw new ArgumentNullException(nameof(authors));
            this.isNew = isNew;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            Text = isNew ? "Добавить книгу" : "Редактировать книгу";
            label1.Text = isNew ? "Добавление новой книги" : "Редактирование книги";

            if (!isNew && book.Title != null)
            {
                textBox1.Text = book.Title;
            }

            button1.Click += Button1_Click;
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Название книги не может быть пустым.");
                return;
            }

            book.Title = textBox1.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
