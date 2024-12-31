using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace WinFormsApp13
{
    public class Model : IModel
    {
        private List<Author> authors = new List<Author>();
        private List<Book> books = new List<Book>();

        public Model()
        {
            Author author1 = new Author { Name = "Лев Толстой" };
            author1.Books.Add(new Book { Title = "Война и мир", Author = author1 });
            author1.Books.Add(new Book { Title = "Анна Каренина", Author = author1 });
            authors.Add(author1);

            Author author2 = new Author { Name = "Фёдор Достоевский" };
            author2.Books.Add(new Book { Title = "Преступление и наказание", Author = author2 });
            author2.Books.Add(new Book { Title = "Братья Карамазовы", Author = author2 });
            authors.Add(author2);

            Author author3 = new Author { Name = "Александр Пушкин" };
            author3.Books.Add(new Book { Title = "Евгений Онегин", Author = author3 });
            author3.Books.Add(new Book { Title = "Руслан и Людмила", Author = author3 });
            authors.Add(author3);

            Author author4 = new Author { Name = "Антон Чехов" };
            author4.Books.Add(new Book { Title = "Вишнёвый сад", Author = author4 });
            author4.Books.Add(new Book { Title = "Человек в футляре", Author = author4 });
            authors.Add(author4);

            // Собираем все книги
            foreach (var a in authors)
            {
                books.AddRange(a.Books);
            }
        }

        public List<Author> GetAuthors()
        {
            return authors;
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public void AddAuthor(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
                throw new ArgumentException("Имя автора не может быть пустым.");

            var newAuthor = new Author(authorName);
            authors.Add(newAuthor);
        }

        public void EditAuthor(Author author, string newName)
        {
            if (author == null)
                throw new ArgumentNullException("Автор не выбран.");
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Имя автора не может быть пустым.");

            author.Name = newName;
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
                throw new ArgumentNullException("Автор не выбран.");

            books.RemoveAll(b => b.Author == author);
            authors.Remove(author);
        }

        public void AddBook(Author author, string bookTitle)
        {
            if (author == null)
                throw new ArgumentNullException("Автор не выбран.");
            if (string.IsNullOrWhiteSpace(bookTitle))
                throw new ArgumentException("Название книги не может быть пустым.");

            var newBook = new Book(bookTitle, author);
            author.Books.Add(newBook);
            books.Add(newBook);
        }

        public void EditBook(Book book, string newTitle)
        {
            if (book == null)
                throw new ArgumentNullException("Книга не выбрана.");
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Название книги не может быть пустым.");

            book.Title = newTitle;
        }

        public void DeleteBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("Книга не выбрана.");

            book.Author.Books.Remove(book);
            books.Remove(book);
        }

        public void LoadData(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(List<Author>));
                    authors = (List<Author>)serializer.ReadObject(fs);
                    books.Clear();
                    foreach (var author in authors)
                        books.AddRange(author.Books);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при загрузке данных: " + ex.Message);
            }
        }

        public void SaveData(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(List<Author>));
                    serializer.WriteObject(fs, authors);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при сохранении данных: " + ex.Message);
            }
        }
    }
}
